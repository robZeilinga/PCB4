using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCB4
{
    public partial class Form1 
    {

        public event EventHandler<StreamEventArgs> RaiseStreamEvent;
        protected virtual void OnRaiseStreamEvent(StreamEventArgs e)
        {
            //addToLog("OnRaiseStreamEvent " + e.Status.ToString());
            //RaisePosEvent?.Invoke(this, e);
            EventHandler<StreamEventArgs> handler = RaiseStreamEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        // check free buffer before sending 
        // 1. requestSend(data) to add cleaned data to stack (sendLines) for sending / extract code for 2nd COM port
        // 2. processSend() check if grbl-buffer is free to take commands
        // 3. sendLine(data) if buffer can take commands
        // 4. updateStreaming(rxdata) check if command was sent
        private int grblBufferSize = 127;  //rx bufer size of grbl on arduino 127
        private int grblBufferFree = 127;    //actual suposed free bytes on grbl buffer
        private List<string> sendLines = new List<string>();
        private int sendLinesCount = 0;             // actual buffer size
        private int sendLinesSent = 0;              // actual sent line
        private int sendLinesConfirmed = 0;         // already received line


        /*  requestSend fill up send buffer, called by main-prog for single commands
         *  or called by preProcessStreaming to stream GCode data
         *  requestSend -> processSend -> sendLine
         * */
        public bool requestSend(string data)
        {
            if (isStreamingRequestPause)
            { addToLog("!!! Command blocked - wait for IDLE " + data); }
            else
            {
                var tmp = cleanUpCodeLine(data);
                if ((!string.IsNullOrEmpty(tmp)) && (tmp[0] != ';'))    // trim lines and remove all empty lines and comment lines
                {
                    if (tmp == "$#") preventEvent = 5;                  // no response echo for parser state
                    sendLines.Add(tmp);
                    sendLinesCount++;
                    processSend();
                    feedBackSettings(tmp);
                }
            }
            return serialPort.IsOpen;
        }



        private void feedBackSettings(string tmp)
        {
            if (!isStreaming || isStreamingPause)
            {
                tmp = tmp.Replace(" ", String.Empty);
                if (tmp.Contains("$32"))
                {
                    if (tmp.Contains("$32=1")) isLasermode = true;
                    if (tmp.Contains("$32=0")) isLasermode = false;
                    OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.lasermode));
                }
                if (tmp.IndexOf("$") >= 0)
                { btnCheckGRBLResult.Enabled = false; btnCheckGRBLResult.BackColor = SystemColors.Control; }
            }
        }

        private string insertVariable(string line)
        {
            int pos = 0, posold = 0;
            string variable, mykey = "";
            double myvalue = 0;
            if (line.Length > 5)        // min length needed to be replaceable: x#TOLX
            {
                do
                {
                    pos = line.IndexOf('#', posold);
                    if (pos > 0)
                    {
                        myvalue = 0;
                        variable = line.Substring(pos, 5);
                        mykey = variable.Substring(1);
                        if (gcodeVariable.ContainsKey(mykey))
                        { myvalue = gcodeVariable[mykey]; }
                        else { line += " (" + mykey + " not found)"; }
                        line = line.Replace(variable, string.Format("{0:0.000}", myvalue));
                        //                  addToLog("replace "+ mykey+" by "+ myvalue.ToString());
                    }
                    posold = pos + 5;
                } while (pos > 0);
            }
            return line.Replace(',', '.');
        }

        public void realtimeCommand(int cmd)
        {
            realtimeCommand((byte)cmd);
        }


        public void realtimeCommand(byte cmd)
        {
            var dataArray = new byte[] { Convert.ToByte(cmd) };
            serialPort.Write(dataArray, 0, 1);
            addToLog("> '0x" + cmd.ToString("X") + "' " + grbl.getRealtime(cmd));
            if ((cmd == 0x85) && !(isStreaming && !isStreamingPause))                   //  Jog Cancel
            {
                sendLinesSent = 0;
                sendLinesCount = 0;
                sendLinesConfirmed = 0;
                sendLines.Clear();
                grblBufferFree = grblBufferSize;
            }
        }


        /*  processSend - send data if GRBL-buffer is ready to take new data
 *  called by timer and rx-interrupt
 *  take care of keywords
 * */
        private bool waitForIdle = false;
        private bool externalProbe = false;
        private string[] eeprom1 = { "G54", "G55", "G56", "G57", "G58", "G59" };
        private string[] eeprom2 = { "G10", "G28", "G30", "G28" };
        public void processSend()
        {
            while ((sendLinesSent < sendLinesCount) && (grblBufferFree >= sendLines[sendLinesSent].Length + 1))
            {
                var line = sendLines[sendLinesSent];
                bool replaced = false;

                if (!isStreaming)       // check tool change coordinates
                {
                    int cmdTNr = gcode.getIntGCode('T', line);
                    if (cmdTNr >= 0)
                    {
                        //toolTable.init();       // fill structure
                        setToolChangeCoordinates(cmdTNr, line);
                        // save actual tool info as last tool info
                        gcodeVariable["TOLN"] = gcodeVariable["TOAN"];
                        gcodeVariable["TOLX"] = gcodeVariable["TOAX"];
                        gcodeVariable["TOLY"] = gcodeVariable["TOAY"];
                        gcodeVariable["TOLZ"] = gcodeVariable["TOAZ"];
                    }
                }
                if (line.IndexOf('#') > 0)                      // check if variable neededs to be replaced
                {
                    line = insertVariable(line);
                    replaced = true;
                    if (grblBufferFree < grblBufferSize)
                        waitForIdle = true;
                }
                if (line.IndexOf("(^2") >= 0)                   // forward cmd to 2nd GRBL
                    if (grblBufferFree < grblBufferSize)
                        waitForIdle = true;

                for (int i = 0; i < eeprom1.Length; i++)           // wait for IDLE beacuse of EEPROM access
                {
                    if (line.IndexOf(eeprom1[i]) >= 0)
                    {
                        if (grblBufferFree < grblBufferSize)
                            waitForIdle = true;
                        break;
                    }
                }
                for (int i = 0; i < eeprom2.Length; i++)        // wait for IDLE beacuse of EEPROM access
                {
                    if (line.IndexOf(eeprom2[i]) >= 0)
                    {
                        if (grblBufferFree < grblBufferSize)
                            waitForIdle = true;
                        break;
                    }
                }

                if ((!waitForIdle) || (grblStateNow == grblState.alarm))
                {
                    if (replaced)
                        sendLines[sendLinesSent] = line;    // needed to get correct length when receiving 'ok'
                                                            //  rtbLog.AppendText(string.Format("!!!> {0} {1}\r\n", line, sendLinesSent));
                    if (serialPort.IsOpen)
                    {
                        sendLine(line);                         // now really send data to Arduino
                        if (lastSentToCOM.Count > 10)
                            lastSentToCOM.Dequeue();            // store last sent commands via COM for error analysis
                        grblBufferFree -= (line.Length + 1);
                        sendLinesSent++;
                    }
                    else
                    {
                        addToLog("!!! Port is closed !!!");
                        resetStreaming();
                    }


                    if (line.IndexOf("$TOOL") >= 0) { grblStatus = grblStreaming.toolchange; }
                    if (line == "($TOOL-IN)") { toolInSpindle = true; }
                    if (line == "($TOOL-OUT)") { toolInSpindle = false; }
                    if (line == "($END)") { grblStatus = grblStreaming.ok; }
                    if (line == "($PROBE)")
                    { waitForIdle = true; externalProbe = true; }
                }
                else
                    return;
            }
        }

        /// <summary>
        /// Clear all streaming counters
        /// </summary>
        private void resetStreaming()
        {
            externalProbe = false;
            isStreaming = false;
            isStreamingRequestPause = false;
            isStreamingPause = false;
            gCodeLinesSent = 0;
            gCodeLinesCount = 0;
            gCodeLinesConfirmed = 0;
            gCodeLinesTotal = 0;
            gCodeLines.Clear();
            gCodeLineNr.Clear();
            sendLinesSent = 0;
            sendLinesCount = 0;
            sendLinesConfirmed = 0;
            sendLines.Clear();
            grblBufferFree = grblBufferSize;
        }


        /// <summary>
        /// sendLine - now really send data to Arduino
        /// </summary>
        private void sendLine(string data)
        {
            try
            {
                serialPort.Write(data + "\r");
                lastSentToCOM.Enqueue(data);        // store last sent commands via COM for error analysis
#if (debuginfo)
                rtbLog.AppendText(string.Format("< {0} {1} {2} {3} \r\n", data, sendLinesSent, sendLinesConfirmed, grblBufferFree));//if not in transfer log the txLine
#endif
                if (!isHeightProbing && (!(isStreaming && !isStreamingPause)) || (cbStatus.Checked))
                {
                    rtbLog.AppendText(string.Format("> {0} \r\n", data));//if not in transfer log the txLine
                    rtbLog.ScrollToCaret();
                }
            }
            catch (Exception err)
            {
                logError("Sending line", err);
                updateControls();
            }
        }


        //Send reset sentence
        public void grblReset(bool savePos = true)//Stop/reset button
        {
            if (savePos)
            { saveLastPos(); }
            resetVariables();
            mParserState.reset();
            isStreaming = false;
            isStreamingPause = false;
            isHeightProbing = false;
            toolInSpindle = false;
            waitForIdle = false;
            externalProbe = false;
            var dataArray = new byte[] { 24 };//Ctrl-X
            if (serialPort.IsOpen)
                serialPort.Write(dataArray, 0, 1);
            rtbLog.AppendText("[CTRL-X]\r\n");
            preventOutput = 0; preventEvent = 0;
            grbl.axisA = false; grbl.axisB = false; grbl.axisC = false; grbl.axisUpdate = false;
        }

        #region serial receive handling
        /*  RX Interupt
         * */
        string mens;
        Exception err;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while ((serialPort.IsOpen) && (serialPort.BytesToRead > 0))
            {
                rxString = string.Empty;
                try
                {
                    rxString = serialPort.ReadTo("\r\n");              //read line from grbl, discard CR LF
                    isDataProcessing = true;
                    this.Invoke(new EventHandler(handleRxData));        //tigger rx process 
                    while ((serialPort.IsOpen) && (isDataProcessing))   //wait previous data line processed done
                    { }
                }
                catch (Exception errort)
                {
                    //MessageBox.Show(errort.ToString());
                    //serialPort.Close();
                    mens = "Error reading line from serial port";
                    err = errort;
                    this.Invoke(new EventHandler(logErrorThr));
                }
            }
        }

        /*  Filter received message before further use
         * */
        public string lastError = "";
        private static int preventOutput = 0;
        private static int preventEvent = 0;
        private void handleRxData(object sender, EventArgs e)
        {
            try
            {
                char[] charsToTrim = { '<', '>', '[', ']', ' ' };
                int tmp;
                //addToLog(string.Format("raw '{0}'", rxString));

                // reset message
                if (rxString.IndexOf("['$' for help]") >= 0)
                {
                    handleRX_Reset(rxString);
                    timerSerial.Enabled = true;
                    isDataProcessing = false;
                    lastError = "";
                    if (true)   // read grbl settings
                    {
                        addToLog("> Read grbl settings, hide response");
                        grbl.axisA = false; grbl.axisB = false; grbl.axisC = false; grbl.axisUpdate = false;
                        preventOutput = 10; preventEvent = 10;
                        requestSend("$$");  // get setup
                        requestSend("$#");  // get parameter
                    }
                    return;
                }

                else if (rxString.IndexOf("ok") >= 0)
                {
                    if (!isStreaming || isStreamingPause)
                    {
                        if (!isHeightProbing || cbStatus.Checked)
                            addToLog(string.Format("< {0}", rxString));          // < ok
                    }
#if (debuginfo)
          //  rtbLog.AppendText(string.Format("> ok {0} {1} {2}\r\n", sendLinesSent, sendLinesConfirmed, sendLinesCount));//if not in transfer log the txLine
                rtbLog.AppendText(string.Format("< {0} {1} {2}  \r\n", sendLinesSent, sendLinesConfirmed, grblBufferFree));//if not in transfer log the txLine
#endif
                    updateStreaming(rxString);                              // process all other messages
                    isDataProcessing = false;
                    return;
                }

                // Process status message with coordinates
                else if (((tmp = rxString.IndexOf('<')) >= 0) && (rxString.IndexOf('>') > tmp))
                {
                    if (cbStatus.Checked)
                        addToLog(rxString);
                    handleRX_Status(rxString.Trim(charsToTrim));// Process status message with coordinates
                    isDataProcessing = false;
                    return;
                }

                // Process feedback message with coordinates
                else if (((tmp = rxString.IndexOf('[')) >= 0) && (rxString.IndexOf(']') > tmp))
                {
                    handleRX_Feedback(rxString.Trim(charsToTrim).Split(':'));
                    if (!isHeightProbing || cbStatus.Checked)
                    {
                        if (preventOutput == 0)
                            addToLog(rxString);
                    }
                    isDataProcessing = false;
                    return;
                }

                else if (rxString.IndexOf("ALARM") >= 0)
                {
                    lastError = "";
                    addToLog("<\r\n< !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    addToLog(string.Format("< {0} \t{1}", rxString, grbl.getAlarm(rxString)));
                    resetStreaming();
                    isDataProcessing = false;
                    isHeightProbing = false;
                    grblStateNow = grblState.alarm;
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
                    this.WindowState = FormWindowState.Minimized;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    return;
                }
                else if (rxString.IndexOf("error") >= 0)
                {
                    string tmpMsg = "";
                    if (rxString != lastError)
                    {
                        addToLog("<\r\n< !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        addToLog(string.Format("< {0} \t{1}", rxString, grbl.getError(rxString)));
                        lastError = rxString + " " + grbl.getError(rxString) + "\r\n";
                        this.WindowState = FormWindowState.Minimized;
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        addToLog(">>> Last sent commmands to grbl, oldest first:");
                        lastError += ">>> Last sent commmands to grbl, oldest first:";
                        foreach (string lastLine in lastSentToCOM)
                        {
                            tmpMsg = ">>> " + lastLine;
                            addToLog(tmpMsg);
                            lastError += tmpMsg + "\r\n";
                        }
                    }
                    grblStatus = grblStreaming.error;
                    if (isStreaming)
                    {
                        tmpMsg = string.Format("< Error before code line {0} \r\n", gCodeLineNr[gCodeLinesSent]);
                        addToLog(tmpMsg);
                        lastError += tmpMsg;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                        stopStreaming();
                    }
                    resetStreaming();
                    isHeightProbing = false;
                    isDataProcessing = false;
                    return;
                }

                // Show GRBL Settings Info if Version is >= 1.0
                else if ((rxString.IndexOf("$") >= 0) && (rxString.IndexOf("=") >= 0))
                {
                    handleRX_Setup(rxString);
                    isDataProcessing = false;
                    return;
                }
            }
            catch (Exception cc)
            {

            }
            isDataProcessing = false;
            return;
        }

        // process further RX messages (> ok)
        public void updateStreaming(string rxString)
        {
            int tmpIndex = gCodeLinesSent;
            //addToLog(string.Format("### {0} {1} {2}\r\n", sendLinesConfirmed, sendLinesSent, sendLinesCount));
            // 'ok' received, increment confirmend
            if (sendLinesConfirmed < sendLinesCount)
            {
                grbl.updateParserState(sendLines[sendLinesConfirmed], ref mParserState);
                grblBufferFree += (sendLines[sendLinesConfirmed].Length + 1);   //update bytes supose to be free on grbl rx bufer
                sendLinesConfirmed++;                   // line processed
                                                        // Remove already sent lines to release memory
                if ((sendLines.Count > 1) && (sendLinesConfirmed == sendLinesSent == sendLinesCount > 1))
                {
                    sendLines.RemoveAt(0);
                    sendLinesConfirmed--;
                    sendLinesSent--;
                    sendLinesCount--;
                }
            }
            // check if buffer is empty and system = IDLE 
            if ((sendLinesConfirmed == sendLinesCount) && (grblStateNow == grblState.idle))   // addToLog(">> Buffer empty\r");
            {
                if (isStreamingRequestPause)
                {
                    isStreamingPause = true;
                    isStreamingRequestPause = false;
                    grblStatus = grblStreaming.pause;
                    gcodeVariable["MLAX"] = posMachine.X; gcodeVariable["MLAY"] = posMachine.Y; gcodeVariable["MLAZ"] = posMachine.Z;
                    gcodeVariable["WLAX"] = posWork.X; gcodeVariable["WLAY"] = posWork.Y; gcodeVariable["WLAZ"] = posWork.Z;

                    if (getParserState)
                    { requestSend("$G"); }
                }
            }
            if (isStreaming)
            {
                if (!isStreamingPause)
                {
                    gCodeLinesConfirmed++;  //line processed
                                            // Remove already handled GCode lines to release memory
                    if ((gCodeLines.Count > 1) && (gCodeLinesSent > 1))
                    {
                        gCodeLines.RemoveAt(0);
                        gCodeLineNr.RemoveAt(0);
                        gCodeLinesConfirmed--;
                        gCodeLinesSent--;
                        gCodeLinesCount--;
                        tmpIndex = gCodeLinesSent;
                    }
                }
                else
                    grblStatus = grblStreaming.pause;   // update status
                                                        //Transfer finished and processed? Update status and controls
                if ((gCodeLinesConfirmed >= gCodeLinesCount) && (sendLinesConfirmed == sendLinesCount))
                {
                    isStreaming = false;
                    addToLog("\r\n[Streaming finish]");
                    grblStatus = grblStreaming.finish;
                    requestSend("$G");
                    if (isStreamingCheck)
                    { requestSend("$C"); isStreamingCheck = false; }
                    updateControls();
                    allowStreamingEvent = true;
                }
                else//not finished
                {
                    if (!(isStreamingPause || isStreamingRequestPause))
                        preProcessStreaming();//If more lines on file, send it  
                }
                if ((oldStatus != grblStatus) || allowStreamingEvent)
                {
                    sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    oldStatus = grblStatus;     //grblStatus = oldStatus;
                    allowStreamingEvent = false;
                }
            }
            processSend();

        }

        /*  sendStreamEvent update main prog 
         * */
        private void sendStreamEvent(int lineNr, grblStreaming status)
        {
            float codeFinish = (float)lineNr * 100 / (float)gCodeLinesTotal;
            float buffFinish = (float)(grblBufferSize - grblBufferFree) * 100 / (float)grblBufferSize;
            if (codeFinish > 100) { codeFinish = 100; }
            if (buffFinish > 100) { buffFinish = 100; }
            OnRaiseStreamEvent(new StreamEventArgs((int)lineNr, codeFinish, buffFinish, status));
        }

        private void handleRX_Reset(string rxString)
        {
            grblBufferSize = 127;  //rx bufer size of grbl on arduino 127
            resetStreaming();
            addToLog("> RESET\r\n" + rxString);
            if (rxString.ToLower().IndexOf("grbl 0") >= 0)
            { isGrblVers0 = true; isLasermode = false; }
            if (rxString.ToLower().IndexOf("grbl 1") >= 0)
            { isGrblVers0 = false; addToLog("> Version 1.x\r\n"); }
            grblVers = rxString.Substring(0, rxString.IndexOf('['));
            if (lastError.Length > 2)
            {
                addToLog("> last error: " + lastError);
                OnRaiseStreamEvent(new StreamEventArgs(0, -1, 0, grblStreaming.reset));
            }
            else
                OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.reset));

            lblSrBf.Text = "";
            lblSrFS.Text = "";
            lblSrPn.Text = "";
            lblSrLn.Text = "";
            lblSrOv.Text = "";
            lblSrA.Text = "";
            return;
        }

        private void handleRX_Feedback(string[] dataField)  // dataField = rxString.Trim(charsToTrim).Split(':')
        {
            if (dataField[0].IndexOf("GC") >= 0)            // handle G-Code parser state [GC:G0 G54 G17 G21 G90 G94 M5 M9 T0 F0.0 S0]
            {
                parserStateGC = dataField[1];
                grbl.updateParserState(dataField[1], ref mParserState);
                if (isGrblVers0)
                    parserStateGC = parserStateGC.Replace("M0 ", "");
                posPause = posWork;
                getParserState = false;
            }
            else if (dataField[0].IndexOf("PRB") >= 0)                // Probe message with coordinates // [PRB:-155.000,-160.000,-28.208:1]
            {
                grblStateNow = grblState.probe;
                posProbeOld = posProbe;
                grbl.getPosition("PRB:" + dataField[1], ref posProbe);  // get numbers from string
                gcodeVariable["PRBX"] = posProbe.X; gcodeVariable["PRBY"] = posProbe.Y; gcodeVariable["PRBZ"] = posProbe.Z;
                gcodeVariable["PRDX"] = posProbe.X - posProbeOld.X; gcodeVariable["PRDY"] = posProbe.Y - posProbeOld.Y; gcodeVariable["PRDZ"] = posProbe.Z - posProbeOld.Z;
                if (preventEvent == 0)
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
            }
            else if (dataField[0].IndexOf("MSG") >= 0) //[MSG:Pgm End]
            {
                if (dataField[1].IndexOf("Pgm End") >= 0)
                {
                    if ((isStreaming) || (isHeightProbing))
                    {
                        isStreaming = false;
                        isHeightProbing = false;
                        preventEvent = 0; preventOutput = 0;
                        addToLog("\r[Streaming finish]");
                        grblStatus = grblStreaming.finish;
                        if (isStreamingCheck)
                        { requestSend("$C"); isStreamingCheck = false; }
                        updateControls();
                        allowStreamingEvent = true;
                        OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.finish));
                    }
                }
            }
            if (iamSerial == 1)
                grbl.setCoordinates(dataField[0], dataField[1]);
        }

        private void handleRX_Setup(string rxString)
        {
            string[] splt = rxString.Split('=');
            int id;
            if (int.TryParse(splt[0].Substring(1), out id))
            {
                if (!isGrblVers0)
                {
                    string msgNr = splt[0].Substring(1).Trim();
                    if (preventOutput == 0)
                        addToLog(string.Format("< {0} ({1})", rxString.PadRight(14, ' '), grbl.getSetting(msgNr)));   // output $$ response
                    if (id == 32)
                    {
                        if (splt[1].IndexOf("1") >= 0)
                            isLasermode = true;
                        else
                            isLasermode = false;
                        OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.lasermode));
                    }
                }
                else
                    addToLog(string.Format("< {0}", rxString));
                GRBLSettings.Add(rxString);
                if (iamSerial == 1)
                    grbl.setSettings(id, splt[1]);
            }
            else
                addToLog(string.Format("< {0}", rxString));
        }

        private grblState grblStateNow = grblState.unknown;
        private grblState grblStateLast = grblState.unknown;

        // should occur with same frequent as timer interrupt -> each 200ms
        // old:         <Idle,MPos:0.000,0.000,0.000,WPos:0.000,0.000,0.000>
        // new in 1.1   < Idle | MPos:0.000,0.000,0.000 | FS:0,0 | WCO:0.000,0.000,0.000 >
        private bool allowStreamingEvent = true;
        private void handleRX_Status(string text)    // '<' and '>' already removed
        {
            char splitAt = '|';
            if (isGrblVers0)
                splitAt = ',';
            string[] dataField = text.Split(splitAt);
            string status = dataField[0].Trim(' ');
            if (isGrblVers0)
            {
                grbl.getPosition(dataField[1] + "," + dataField[2] + "," + dataField[3] + " ", ref posMachine);
                grbl.getPosition(dataField[4] + "," + dataField[5] + "," + dataField[6] + " ", ref posWork);
                posWCO = posMachine - posWork;
            }
            else
            {
                machineState.Clear(); //lblSrPn.Text = ""; //lblSrA.Text = "";
                if (dataField.Length > 2)
                {
                    for (int i = 2; i < dataField.Length; i++)
                    {
                        if (dataField[i].IndexOf("WCO") >= 0)           // Work Coordinate Offset
                        {
                            grbl.getPosition(dataField[i], ref posWCO);
                            continue;
                        }
                        string[] data = dataField[i].Split(':');
                        if (dataField[i].IndexOf("Bf:") >= 0)            // Buffer state
                        { machineState.Bf = lblSrBf.Text = data[1]; continue; }
                        if (dataField[i].IndexOf("Ln:") >= 0)            // Line number
                        { machineState.Ln = lblSrLn.Text = data[1]; continue; }
                        if (dataField[i].IndexOf("FS:") >= 0)            // Current Feed and Speed
                        { machineState.FS = lblSrFS.Text = data[1]; continue; }
                        if (dataField[i].IndexOf("F:") >= 0)             // Current Feed 
                        { machineState.FS = lblSrFS.Text = data[1]; continue; }
                        if (dataField[i].IndexOf("Pn:") >= 0)            // Input Pin State
                        { machineState.Pn = lblSrPn.Text = data[1]; continue; }
                        if (dataField[i].IndexOf("Ov:") >= 0)            // Override Values
                        { machineState.Ov = lblSrOv.Text = data[1]; lblSrPn.Text = ""; lblSrA.Text = ""; continue; }
                        if (dataField[i].IndexOf("A:") >= 0)             // Accessory State
                        { machineState.A = lblSrA.Text = data[1]; continue; }
                    }
                }
                if (dataField[1].IndexOf("MPos") >= 0)
                {
                    grbl.getPosition(dataField[1], ref posMachine);
                    posWork = posMachine - posWCO;
                }
                else
                {
                    grbl.getPosition(dataField[1], ref posWork);
                    posMachine = posWork + posWCO;
                }
            }

            if (iamSerial == 1)
            {
                if (!grbl.posChanged)
                    grbl.posChanged = !(xyzPoint.AlmostEqual(grbl.posWCO, posWCO) && xyzPoint.AlmostEqual(grbl.posMachine, posMachine));
                if (!grbl.wcoChanged)
                    grbl.wcoChanged = !(xyzPoint.AlmostEqual(grbl.posWCO, posWCO));
                grbl.posWCO = posWCO; grbl.posWork = posWork; grbl.posMachine = posMachine;
            } // make it global

            gcodeVariable["MACX"] = posMachine.X; gcodeVariable["MACY"] = posMachine.Y; gcodeVariable["MACZ"] = posMachine.Z;
            gcodeVariable["WACX"] = posWork.X; gcodeVariable["WACY"] = posWork.Y; gcodeVariable["WACZ"] = posWork.Z;
            grblStateNow = grbl.parseStatus(status);
            lblSrState.BackColor = grbl.grblStateColor(grblStateNow);
            lblSrState.Text = status;

            lblSrPos.Text = posWork.Print(false, grbl.axisB || grbl.axisC); // show actual work position
            if (grblStateNow != grblStateLast) { grblStateChanged(); }
            OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));

            if ((grblStateNow == grblState.idle) || (grblStateNow == grblState.check))
            {
                waitForIdle = false;
                if (externalProbe)
                {
                    posProbe = posMachine;
                    externalProbe = false;
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblState.probe, machineState, mParserState, "($PROBE)"));
                }
                processSend();
            }
            grblStateLast = grblStateNow;
            //            OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
            allowStreamingEvent = true;
        }
        public event EventHandler<PosEventArgs> RaisePosEvent;
        protected virtual void OnRaisePosEvent(PosEventArgs e)
        {
            //addToLog("OnRaisePosEvent " + e.Status.ToString());
            RaisePosEvent?.Invoke(this, e);
            /*EventHandler<PosEventArgs> handler = RaisePosEvent;
            if (handler != null)
            {
                handler(this, e);
            }*/
        }
        private void saveLastPos()
        {
            rtbLog.AppendText("\rSave last pos.: \r" + posWork.Print(true, true) + "\n");    // print in single lines
            PCB4.Properties.Settings.Default.lastOffsetX = Math.Round(posWork.X, 3);
            PCB4.Properties.Settings.Default.lastOffsetY = Math.Round(posWork.Y, 3);
            PCB4.Properties.Settings.Default.lastOffsetZ = Math.Round(posWork.Z, 3);
            PCB4.Properties.Settings.Default.lastOffsetA = Math.Round(posWork.A, 3);
            PCB4.Properties.Settings.Default.lastOffsetB = Math.Round(posWork.B, 3);
            PCB4.Properties.Settings.Default.lastOffsetC = Math.Round(posWork.C, 3);
                int gNr = mParserState.coord_select;
                gNr = ((gNr >= 54) && (gNr <= 59)) ? gNr : 54;
            PCB4.Properties.Settings.Default.lastOffsetCoord = gNr;    //global.grblParserState.coord_select;
            PCB4.Properties.Settings.Default.Save();
        }

        #endregion

        /*  cleanUpCodeLine remove unneccessary char but keep keywords
*/
        private string cleanUpCodeLine(string data)
        {
            var line = data.Replace("\r", "");  //remove CR
            line = line.Replace("\n", "");      //remove LF
            var orig = line;
            int start = orig.IndexOf('(');
            int end = orig.LastIndexOf(')');
            if (start >= 0) line = orig.Substring(0, start);
            if (end >= 0) line += orig.Substring(end + 1);

            // extract GCode for 2nd COM Port
            if ((start >= 0) && (end > start))  // send data to 2nd COM-Port
            {
                var cmt = orig.Substring(start, end - start + 1);
                if ((cmt.IndexOf("(^2") >= 0) || (cmt.IndexOf("($") == 0))
                {
                    line += cmt;                // keep 2nd COM port data for further use
                }
            }

            line = line.ToUpper();              //all uppercase
            line = line.Trim();
            return line;
        }

        // Streaming
        // 1. startStreaming() copy and filter gcode to list
        // 2. proceedStreaming() to copy data to stack for sending
        private List<string> gCodeLines = new List<string>();      // buffer with gcode commands
        private List<int> gCodeLineNr = new List<int>();         // corresponding line-nr from main-form
        private int gCodeLinesCount = 0;             // amount of lines to sent
        private int gCodeLinesSent = 0;              // actual sent line
        private int gCodeLinesConfirmed = 0;         // received line
        private int gCodeLinesTotal = 0;
        private bool isStreaming = false;        // true when steaming is in progress
        private bool isStreamingRequestPause = false; // true when request pause (wait for idle to switch to pause)
        private bool isStreamingPause = false;    // true when steaming-pause 
        private bool isStreamingCheck = false;    // true when steaming is in progress (check)
        private bool getParserState = false;      // true to send $G after status switched to idle
        private bool isDataProcessing = false;      // false when no data processing pending
        private grblStreaming grblStatus = grblStreaming.ok;
        private grblStreaming oldStatus = grblStreaming.ok;
        public void stopStreaming()
        {
            int line = 0;
            if ((gCodeLineNr != null) && (gCodeLinesSent < gCodeLineNr.Count))
            {
                line = gCodeLineNr[gCodeLinesSent];
                sendStreamEvent(line, grblStreaming.stop);
            }
            isHeightProbing = false;
            addToLog("[STOP Streaming (" + line.ToString() + ")]");
            resetStreaming();
            if (isStreamingCheck)
            {
                sendLine("$C");
                isStreamingCheck = false;
            }
            updateControls();
        }
        public void pauseStreaming()
        {
            if (!isStreamingPause)
            {
                isStreamingRequestPause = true;     // wait until buffer is empty before switch to pause
                addToLog("[Pause streaming]");
                addToLog("[Save Settings]");
                grblStatus = grblStreaming.waitidle;
                getParserState = true;
            }
            else
            {   //if ((posPause.X != posWork.X) || (posPause.Y != posWork.Y) || (posPause.Z != posWork.Z))
                addToLog("++++++++++++++++++++++++++++++++++++");
                if (!xyzPoint.AlmostEqual(posPause, posWork))
                {
                    addToLog("[Restore Position]");
                    requestSend(string.Format("G90 G0 X{0:0.000} Y{1:0.000}", posPause.X, posPause.Y).Replace(',', '.'));  // restore last position
                    string noG = parserStateGC.Substring(parserStateGC.IndexOf("M") - 1);
                    addToLog("[Restore Settings: " + noG + " ]");
                    requestSend(noG);           // restore actual GCode settings one by one
                    requestSend("G4 P2");       // wait 2 seconds
                    requestSend(string.Format("G1 Z{0:0.000}", posPause.Z).Replace(',', '.'));                      // restore last position
                }
                addToLog("[Start streaming - no echo]");
                addToLog("[Restore Settings: " + parserStateGC + " ]");
                isStreamingPause = false;
                isStreamingRequestPause = false;
                grblStatus = grblStreaming.ok;
                requestSend(parserStateGC);         // restore actual GCode settings one by one
                gCodeLinesConfirmed--;              // each restored setting will cause 'ok' and gCodeLinesConfirmed++

                preProcessStreaming();
            }
            updateControls();
        }

        private void btnScanPort_Click(object sender, EventArgs e)
        { refreshPorts(); }

        private void refreshPorts()
        {
            List<String> tList = new List<String>();
            cbPort.Items.Clear();
            cbPort.Text = "";
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames()) tList.Add(s);
            if (tList.Count < 1) logError("No serial ports found", null);
            else
            {
                tList.Sort();
                cbPort.Items.AddRange(tList.ToArray());
            }
        }


        private bool replaceFeedRate = false;
        private bool replaceSpindleSpeed = false;
        private string replaceFeedRateCmd = "";
        private string replaceSpindleSpeedCmd = "";
        private string replaceFeedRateCmdOld = "";
        private string replaceSpindleSpeedCmdOld = "";
        // called by MainForm -> get override events from form "StreamingForm" for GRBL 0.9
        public void injectCode(string cmd, int value, bool enable)
        {
            if (cmd == "F")
            {
                replaceFeedRate = enable;
                replaceFeedRateCmd = cmd + value.ToString();
                if (isStreaming)
                {
                    if (enable)
                        injectCodeLine(replaceFeedRateCmd);
                    else
                    {
                        if (replaceFeedRateCmdOld != "")
                            injectCodeLine(replaceFeedRateCmdOld);
                    }
                }
            }
            if (cmd == "S")
            {
                replaceSpindleSpeed = enable;
                replaceSpindleSpeedCmd = cmd + value.ToString();
                if (isStreaming)
                {
                    if (enable)
                        injectCodeLine(replaceSpindleSpeedCmd);
                    else
                    {
                        if (replaceSpindleSpeedCmdOld != "")
                            injectCodeLine(replaceSpindleSpeedCmdOld);
                    }
                }
            }
        }
        private void injectCodeLine(string data)
        {
            int index = gCodeLinesSent + 1;
            int linenr = gCodeLineNr[gCodeLinesSent];
            addToLog("!!! Override: " + data + " in line " + linenr);
            gCodeLineNr.Insert(index, linenr);
            gCodeLines.Insert(index, data);
            index++;
            gCodeLinesCount++;
        }


        /*  startStreaming called by main-Prog
         *  get complete GCode list and copy to own list
         *  initialize streaming
         *  if startAtLine > 0 start with pause
         * */
        public void startStreaming(IList<string> gCodeList, int startAtLine, bool check = false)
        {
            lastError = "";
            lastSentToCOM.Clear();
            //toolTable.init();       // fill structure
            rtbLog.Clear();
            if (!check)
                addToLog("[Start streaming - no echo]");
            else
                addToLog("[Start code check]");
            saveLastPos();
            if (replaceFeedRate)
                addToLog("!!! Override Feed Rate");
            if (replaceSpindleSpeed)
                addToLog("!!! Override Spindle Speed");
            isStreamingPause = false;
            isStreamingRequestPause = false;
            isStreamingCheck = check;
            grblStatus = grblStreaming.ok;
            string[] gCode = gCodeList.ToArray<string>();
            gCodeLines = new List<string>();
            gCodeLineNr = new List<int>();
            resetStreaming();
            if (isStreamingCheck)
            {
                sendLine("$C");
                grblBufferSize = 100;  //reduce size to avoid fake errors
            }

            string tmp;
            double pWord, lWord, oWord;
            string subline;
            for (int i = startAtLine; i < gCode.Length; i++)
            {
                tmp = cleanUpCodeLine(gCode[i]);
                if ((!string.IsNullOrEmpty(tmp)) && (tmp[0] != ';'))//trim lines and remove all empty lines and comment lines
                {
                    if (tmp.IndexOf("M98") >= 0)    // any subroutines?
                    {
                        pWord = findDouble("P", -1, tmp);
                        lWord = findDouble("L", 1, tmp);
                        int subStart = 0, subEnd = 0;
                        if (pWord > 0)
                        {
                            oWord = -1;
                            for (int si = i; si < gCode.Length; si++)   // find subroutine
                            {
                                subline = gCode[si];
                                if (subline.IndexOf("O") >= 0)          // find O-Word
                                {
                                    oWord = findDouble("O", -1, subline);
                                    if (oWord == pWord)
                                        subStart = si + 1;              // note start of sub
                                }
                                else                                    // find end of sub
                                {
                                    if (subStart > 0)                   // is match?
                                    {
                                        if (subline.IndexOf("M99") >= 0)
                                        { subEnd = si; break; }     // note end of sub
                                    }
                                }
                            }
                            //MessageBox.Show("start " + subStart.ToString()+" end "+ subEnd.ToString());
                            if (subStart < subEnd)
                            {
                                for (int repeat = 0; repeat < lWord; repeat++)
                                {
                                    for (int si = subStart; si < subEnd; si++)   // copy subroutine
                                    {
                                        gCodeLines.Add(gCode[si]);          // add gcode line to list to send
                                        gCodeLineNr.Add(si);                // add line nr
                                        gCodeLinesCount++;                  // Count total lines
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        gCodeLines.Add(tmp);        // add gcode line to list to send
                        gCodeLineNr.Add(i);         // add line nr
                        gCodeLinesCount++;          // Count total lines
                        if (tmp.IndexOf("M30") >= 0)
                            break;
                    }
                }
            }
            gCodeLines.Add("()");        // add gcode line to list to send
            gCodeLineNr.Add(gCode.Length - 1);         // add line nr
            gCodeLinesTotal = gCode.Length - 1;  // gCodeLinesCount will reduced after each 'confirmed' line
            isStreaming = true;
            updateControls();
            if (startAtLine > 0)
            {  // pauseStreaming();
                isStreamingPause = true;
            }
            else
                preProcessStreaming();
        }

        private static double findDouble(string start, double notfound, string txt)
        {
            int istart = txt.IndexOf(start);
            if (istart < 0)
                return notfound;
            string line = txt.Substring(istart + start.Length);
            string num = "";
            foreach (char c in line)
            {
                if (Char.IsLetter(c))
                    break;
                else if (Char.IsNumber(c) || c == '.' || c == '-')
                    num += c;
            }
            if (num.Length < 1)
                return notfound;
            return double.Parse(num, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        /*  preProcessStreaming copy line by line (requestSend(line)) to sendBuffer 
         *  if buffer free, to be able to track line-nr for feedback
         * */
        //       int currentTool = -1;
        private void preProcessStreaming()
        {
            while ((gCodeLinesSent < gCodeLinesCount) && (grblBufferFree >= gCodeLines[gCodeLinesSent].Length + 1) && !waitForIdle)
            {
                string line = gCodeLines[gCodeLinesSent];
                int cmdMNr = gcode.getIntGCode('M', line);
                int cmdGNr = gcode.getIntGCode('G', line);
                int cmdTNr = gcode.getIntGCode('T', line);
                if (grbl.unknownG.Contains(cmdGNr))
                {
                    gCodeLines[gCodeLinesSent] = "(" + line + " - unknown)";  // don't pass unkown GCode to GRBL because is unknown
                    line = gCodeLines[gCodeLinesSent];
                    gCodeLinesConfirmed++;      // GCode is count as sent (but wasn't send) also count as received
                    addToLog(line);
                }
                if ((replaceFeedRate) && (gcode.getStringValue('F', line) != ""))
                {
                    string old_value = gcode.getStringValue('F', line);
                    replaceFeedRateCmdOld = old_value;
                    line = line.Replace(old_value, replaceFeedRateCmd);
                    gCodeLines[gCodeLinesSent] = line;
                    //                    addToLog("Replace feed in [" + line + "] old : " + old_value);
                }
                if ((replaceSpindleSpeed) && (gcode.getStringValue('S', line) != ""))
                {
                    string old_value = gcode.getStringValue('S', line);
                    line = line.Replace(old_value, replaceSpindleSpeedCmd);
                    replaceSpindleSpeedCmdOld = old_value;
                    gCodeLines[gCodeLinesSent] = line;
                    //                    addToLog("Replace spindle speed in [" + line + "] old : " + old_value);
                }
                // regular GCode expression 'T'
                if (cmdTNr >= 0) //&& (line.IndexOf("T") == 0) && (line.IndexOf("#T") < 0) && (line.IndexOf("$T") < 0))
                {   // T-word is allowed by grbl - no need to filter
                    setToolChangeCoordinates(cmdTNr, line);
                }
                if (cmdMNr == 6)
                {
                    /*
                    if (Properties.Settings.Default.ctrlToolChange)
                    {   // insert script code into GCODE
                        int index = gCodeLinesSent + 1;
                        int linenr = gCodeLineNr[gCodeLinesSent];
                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                        index = insertComment(index, linenr, "($TOOL-START)");
                        addToLog("\r[TOOL change: T" + gcodeVariable["TOAN"].ToString() + " at " + gcodeVariable["TOAX"].ToString() + " , " + gcodeVariable["TOAY"].ToString() + " , " + gcodeVariable["TOAZ"].ToString() + "]");
                        if (toolInSpindle)
                        {   addToLog("[TOOL run script 1) " + Properties.Settings.Default.ctrlToolScriptPut + "  T" + gcodeVariable["TOLN"].ToString() + " at " + gcodeVariable["TOLX"].ToString() + " , " + gcodeVariable["TOLY"].ToString() + " , " + gcodeVariable["TOLZ"].ToString() + "]");
                            index = insertCode(Properties.Settings.Default.ctrlToolScriptPut, index, linenr, true);
                            index = insertComment(index, linenr, "($TOOL-OUT)");
                        }
                        addToLog("[TOOL run script 2) " + Properties.Settings.Default.ctrlToolScriptSelect + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptSelect,index, linenr,true);
                        addToLog("[TOOL run script 3) " + Properties.Settings.Default.ctrlToolScriptGet + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptGet,   index, linenr, true);
                        index = insertComment(index, linenr, "($TOOL-IN)");
                        addToLog("[TOOL run script 4) " + Properties.Settings.Default.ctrlToolScriptProbe + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptProbe, index, linenr, true);
                        index = insertComment(index, linenr, "($END)");

                        // save actual tool info as last tool info
                        gcodeVariable["TOLN"] = gcodeVariable["TOAN"];
                        gcodeVariable["TOLX"] = gcodeVariable["TOAX"];
                        gcodeVariable["TOLY"] = gcodeVariable["TOAY"];
                        gcodeVariable["TOLZ"] = gcodeVariable["TOAZ"];

                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    }
                    gCodeLines[gCodeLinesSent] = "($" + line + ")";  // don't pass M6 to GRBL because is unknown
                    line = gCodeLines[gCodeLinesSent];
                    gCodeLinesConfirmed++;      // M6 is count as sent (but wasn't send) also count as received
                    */
                }
                if (cmdMNr == 30)
                {
                    /*
                    if (Properties.Settings.Default.ctrlToolChange)
                    {   // insert script code into GCODE
                        int index = gCodeLinesSent + 1;
                        int linenr = gCodeLineNr[gCodeLinesSent];
                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);

                        if (toolInSpindle)
                        {
                            addToLog("[TOOL run script 1) " + Properties.Settings.Default.ctrlToolScriptPut + "  T" + gcodeVariable["TOLN"].ToString() + " at " + gcodeVariable["TOLX"].ToString() + " , " + gcodeVariable["TOLY"].ToString() + " , " + gcodeVariable["TOLZ"].ToString() + "]");
                            index = insertCode(Properties.Settings.Default.ctrlToolScriptPut, index, linenr, true);
                            index = insertComment(index, linenr, "($TOOL-OUT)");
                        }
                    }
                    */
                }
                if ((cmdMNr == 0) && !isStreamingCheck)
                {
                    isStreamingRequestPause = true;
                    addToLog("[Pause streaming]");
                    addToLog("[Save Settings]");
                    grblStatus = grblStreaming.waitidle;
                    getParserState = true;
                    sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    gCodeLinesSent++;
                    return;                 // abort while - don't fill up buffer
                }
                requestSend(line);
                gCodeLinesSent++;
            }
        }

        private void grblStateChanged()
        {
            if ((sendLinesConfirmed == sendLinesCount) && (grblStateNow == grblState.idle))   // addToLog(">> Buffer empty\r");
            {
                if (isStreamingRequestPause)
                {
                    isStreamingPause = true;
                    isStreamingRequestPause = false;
                    grblStatus = grblStreaming.pause;
                    if (getParserState)
                    { requestSend("$G"); }
                    updateControls();
                }
            }
        }

        private void setToolChangeCoordinates(int cmdTNr, string line = "")
        {
            /*
            toolProp toolInfo = toolTable.getToolProperties(cmdTNr);
            if (toolInfo.toolnr != cmdTNr)
            {
                addToLog("\r[TOOL change: " + cmdTNr.ToString() + " no Information found! (" + line + ")]");
            }
            else
            {   // get new values
//                addToLog("\r[set tool coordinates "+ cmdTNr.ToString() + "]");
                gcodeVariable["TOAN"] = cmdTNr;
                gcodeVariable["TOAX"] = (double)toolInfo.X + (double)Properties.Settings.Default.toolOffX;
                gcodeVariable["TOAY"] = (double)toolInfo.Y + (double)Properties.Settings.Default.toolOffY;
                gcodeVariable["TOAZ"] = (double)toolInfo.Z + (double)Properties.Settings.Default.toolOffZ;
            }
            */
        }

    }
}

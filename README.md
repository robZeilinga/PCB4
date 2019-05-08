# PCB4
PCB Drilling Robot Controler - GCODE generator

# DANGER WILL ROBINSON! Work in Progress

# The Code is terrible! (yes I know, I hope to refactor the entire project once running)

## Motivation
Producing PCB boards at home has become easier and easier with the introduction of things like 
Dry-film Photoresist and fantastic PCB design tools (Eagle, KiCad etc.) 

But the one nightmare for me has been acurately drilling sub millimeter holes and getting the holes 
exactley in the center of the pads. 

After a passionate discussion one evening at [www.house4hack.co.za] the idea of a
PCB drilling robot was born! 

## Goal
The project has a number of goals: 
- Easy to use - duh
- Simple tool chain - no complicated conversions and steps to get going.
- Be able to handle Etched PCBs that are not 100% aligned to an axis. 
- Integrated alignment and zeroing capability via camera.
- The ability to flip displays to allow the PCB to be mounted bottom side up!  

## Limitations
At the moment the MVP for the project is limited to a single drill size. 
The User can easily drill larger holes using the drilled holes for reference.

## Hardware
- My home-built 3d printer (based on the i3-Berlin) provided the majority of the physical hardware.
- The Arduino Mega & Ramps board were replaced with an Arduino UNO and GRBL CNC board. 
- The Hot End was removed and a mounting for a flexible dremel (well chinesium copy) extension was designed and printed.
- A chinesium endoscope was obtained to act as the tool mounted camera.
- A piece of 20mm MDF was mounted on the bed as a sacraficial PCB mount.

## Software 
in spite of venement objection by the majority of the house members, I decided to write the controller in
C# on a windows OS! 

## Intended User Flow.
1. Create PCB in your favorite PCB design tool (as long as it can generate EXCELLON Drill files)
2. Generate EXCELLON Drill files in metric.
3. Etch PCB.
4. Mount Etched PCB on Bed.
5. Power up Robot & Home.
6. Start up Controller software.
7. Load Drill file (*.drd), a representation of the holes will be displayed
8. Choose the Zero hole ( the hole around which the others will be rotated )
9. Choose a second hole. (preferanbly furthest diagonally away from the zero hole) 
10. Using the mounted camera feed and cross hairs, jog the robot head to locate the Zero hole on the PCB press the Zero Button
11. Repeat for the 2nd hole.
12. Ensure camera offset has been set.
13. Generate gcode and stream to robot.
14. Sit back and enjoy a cup of coffee while the robot drills your PCB accurately.

## Attributions
The Video Cross-Hair overlay and a number of other functions were obtained from the excellent project by SvenHB [https://github.com/svenhb/GRBL-Plotter]

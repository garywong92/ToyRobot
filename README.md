INTRODUCTION
------------

The ToyRobot App allows for a simulation of a toy robot moving on a tabletop. The robot is free to 
roam around the surface of the table, but will be prevented from falling off the table. The robot 
can be placed on a specific position on the table, turn left, turn right, and move forward 1 unit 
depending on the toy robot's orientation.



INSTALLATION
------------

Please locate and find the following executable file where "~" is the root of the solution folder: 
"~\ToyRobot\bin\Debug\ToyRobot.exe"

Run the executable file to launch the ToyRobot command line interface.



CONFIGURATION
-------------

There are a number of customizable parameters which can be configured from the config file at 
"~\ToyRobot\bin\Debug\ToyRobot.exe.config".

Configuration of table size, welcome message and error message can be done by modifying the values 
in the config file.



COMMANDS
--------

PLACE X,Y,ORIENTATION

Use the above command to place the toy robot at a specific position on the table, where X and Y are 
numbers, while orientation should either be North, South, East or West.

MOVE

Use the above command to move the toy robot one unit forward depending on the toy robot's orientation.
However, the command will denied if the move will cause the robot to fall off the table.

LEFT

Use the above command to turn 90 degrees to the left from the toy robot's current orientation.

RIGHT

Use the above command to turn 90 degrees to the right from the toy robot's current orientation.

REPORT

Use the above command to report to the user of the toy robot's current position and orientation.
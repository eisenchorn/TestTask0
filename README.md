**Assumptions**
1) Input file fits in RAM. So as list of robots etc.(input data of unlimited size isn't expected)
2) Input data is expeted to be valid. There are some validation checks for input data but I doubt that the cover all cases(example: you can initialize robot with coordinates, that are invalid for current wold and if it happens right near on of the borders robot can start its way as usual when he shouldn't).
3) Proper exception handling should be added.
3) Robots appear on the map at the same moment and run their scripts one by one. If second robot appears right after moment when first finished his script there are possible situation, when more than one robot are lost at the same position
4) Collisions between robots are impossible. Robots can appear at same spot and go through each other freely. If collisions are possible and should be solved World class should be extended with list of robots and their positions.
5) UnitTests arn't clean. Some of them are using more than one method of tested entity.
6) All logic should be separated from Program.Main so we can make this method as runner for many automated tests on different sets of input data
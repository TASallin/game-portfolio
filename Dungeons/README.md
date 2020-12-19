Read this to see what each of the fields in the editor do to the generation. Click on the "Dungeon Master" GameObject and expand the "Room Creator" script.
X Size/Z Size = the number of tiles in each dimension
Room Size = the relative size of rooms compared to the total floor size
Room Density = the relative percentage of the floor area that will be taken up by rooms
Hallway Order = how "straight" the hallways are
Rectangularity = The relative chance of a room having a rectangular shape instead of an asymmetric shape.
Interior Features = The relative chance of a room having an additional algorithm to spruce up the interior. There are 3 different patterns that can be selected.
Treasure Density = The relative number of treasure chests for the floor size.
Pallette = A number from 0 to 15 that changes the color and textures of the dungeon.
Random Parameters = If checked, randomize everything all fields above.
Random Seed = If checked, ignore the public seed for a random one
Seed = The public seed for generation.
In Steps = If checked, generation will render in 4 phases: Rooms-Hallways-Room Modification-Treasure.

Gameplay Controls
wasd = camera controls
q = zoom out
e = zoom in
space = Generate a new floor, or a new phase of the current floor if "in steps" is checked
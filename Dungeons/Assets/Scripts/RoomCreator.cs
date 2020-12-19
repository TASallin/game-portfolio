using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    public int xSize = 25; //Length of the x dimension
    public int zSize = 25; //Length of the z dimension
    public float roomSize = 0.5f; //The size of rooms relative to the total size of the map
    public float roomDensity = 0.5f; //The relative proportion of the map that will be filled with rooms
    public float hallwayOrder = 0.5f; //How straight hallways will be
    public float rectangularity = 0.5f; //How rectangular the rooms will be
    public float interiorFeatures = 0.5f; //The chance of rooms having a different layout.
    public float treasureDensity = 0.5f; //Amount of treasure relative to the floor size.
    public int pallette = 0; //The pallette 0-15 of the dungeon
    public bool randomParameters = false; //If true, randomize all parameters except seed and ignore human input
    public bool randomSeed = false; //If true, the seed will randomize with each play, ignoring the manual input
    public int seed = 2020; //the rng seed
    public bool inSteps = false; //If true, each stage of the algorithm will be rendered separately, use SPACE to go through.
    public CameraMotion globalCamera;
    public GameObject wallTile;
    public GameObject treasure;
    public GameObject floorTile;
    public GameObject waterTile;
    public GameObject voidTile;
    public GameObject bridgeTile;
    public GameObject spikeTile;
    public GameObject cwallTile;
    public GameObject cfloorTile;
    public GameObject treeTile;
    public GameObject grassTile;
    public GameObject sandTile;
    public GameObject bubbleTile;
    
    private System.Random rng;
    private int floorTiles;

    private GameObject tileF;
    private GameObject tileE;
    private GameObject tileM;
    private GameObject tileH;
    private GameObject tileB;
    private GameObject tileV;
    private GameObject tileW;
    private GameObject tileS;
    private GameObject tile0;
    private GameObject tileP;
    private GameObject tileO;
    private float height0;
    private float heightO;
    private float heightP;
    private float heightV;
    private int stepState = 0;
    private char[,] asciiGrid;
    private List<int[]> cannonRooms;

    // Start is called before the first frame update
    void Start()
    {   
        if (randomSeed) {
            rng = new System.Random();
        } else {
            rng = new System.Random(seed);
        }
        if (randomParameters) {
            xSize = rng.Next(20, 101);
            zSize = rng.Next(20, 101);
            roomSize = (float)rng.NextDouble()*0.6f + 0.4f;
            roomDensity = (float)rng.NextDouble()*0.8f + 0.2f;
            hallwayOrder = (float)rng.NextDouble();
            rectangularity = (float)rng.NextDouble();
            interiorFeatures = (float)rng.NextDouble();
            treasureDensity = (float)rng.NextDouble()*0.6f + 0.2f;
            pallette = rng.Next(16);
        } else {
            pallette = pallette % 16;
        }
        CreateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
                Clear();
            if (inSteps) {
                switch (stepState) {
                    case 0:
                        CreateHallways(asciiGrid, cannonRooms);
                        break;
                    case 1:
                        ModifyRooms(asciiGrid, cannonRooms);
                        break;
                    case 2:
                        PlaceTreasure(asciiGrid, cannonRooms.Count);
                        break;
                    default:
                        if (randomParameters) {
                           xSize = rng.Next(20, 101);
                            zSize = rng.Next(20, 101);
                            roomSize = (float)rng.NextDouble()*0.6f + 0.4f;
                            roomDensity = (float)rng.NextDouble()*0.8f + 0.2f;
                            hallwayOrder = (float)rng.NextDouble();
                            rectangularity = (float)rng.NextDouble();
                            interiorFeatures = (float)rng.NextDouble();
                            treasureDensity = (float)rng.NextDouble()*0.6f + 0.2f;
                            pallette = rng.Next(16);
                        }
                        CreateRooms();
                        stepState = -1;
                        break;
                } 
                stepState++;
                Render(asciiGrid);
            } else {
                if (randomParameters) {
                    xSize = rng.Next(20, 101);
                    zSize = rng.Next(20, 101);
                    roomSize = (float)rng.NextDouble()*0.6f + 0.4f;
                    roomDensity = (float)rng.NextDouble()*0.8f + 0.2f;
                    hallwayOrder = (float)rng.NextDouble();
                    rectangularity = (float)rng.NextDouble();
                    interiorFeatures = (float)rng.NextDouble();
                    treasureDensity = (float)rng.NextDouble()*0.6f + 0.2f;
                    pallette = rng.Next(16);
                }
                CreateRooms();
            }
        }
    }

    void CreateRooms() {
        char[,] grid = new char[zSize, xSize]; //Array is filled with Zeros (not '0')
        floorTiles = 0;
        //Place the Entrance and Exit Rooms
        List<int[]> rooms = new List<int[]>(); //A list of 4 integers {startX, endX, startZ, endZ}
        int[] exit = PlaceRoom(grid, rooms);
        rooms.Add(exit);
        int[] entrance = PlaceRoom(grid, rooms);
        rooms.Add(entrance);
        while (true) {
            if (rng.NextDouble() / 4.0 - 0.125 + (double)floorTiles / (grid.GetLength(0)*grid.GetLength(1)) >= roomDensity) {
                break;
            }
            int[] room = PlaceRoom(grid, rooms);
            if (room == null) {
                break;
            } else {
                rooms.Add(room);
            }
        }
        if (inSteps) {
            asciiGrid = grid;
            cannonRooms = rooms;
            globalCamera.Set(xSize/2, System.Math.Max(xSize/2, zSize/2), zSize/2); 
            SetTiles(pallette);
            Render(grid);
        } else {
            CreateHallways(grid, rooms);
            ModifyRooms(grid, rooms);
            PlaceTreasure(grid, rooms.Count);
            SetTiles(pallette);
            globalCamera.Set(xSize/2, System.Math.Max(xSize/2, zSize/2), zSize/2);   
            Render(grid);
        }
         
    }

    //Chooses a random legal spot to place a room
    int[] PlaceRoom(char[,] grid, List<int[]> rooms) {
        //Calculate the dimensions of the room first
        int zLength = CalculateRoomDimension(grid.GetLength(0));
        int xLength = CalculateRoomDimension(grid.GetLength(1)); 

        //Get every legal room placement
        List<int[]> free = new List<int[]>();
        for (int z = 1; z < grid.GetLength(0) - zLength; z++) {
            for (int x = 1; x < grid.GetLength(1) - xLength; x++) {
                if (RayTraceRect(new int[] {x, x + xLength - 1, z, z + zLength - 1}, rooms)) {
                    free.Add(new int[] {x, x + xLength - 1, z, z + zLength - 1});
                }
            }
        }
        
        if (free.Count == 0) {
            return null;
        }
        int[] room = free[rng.Next(free.Count)];

        //Dig out the room in a rectangle for now and mark tiles around it as well
        for (int x = room[0] - 1; x <= room[1] + 1; x++) {
            for (int z = room[2] - 1; z <= room[3] + 1; z++) {
                if (Diggable(grid[z, x])) {
                    if (room[0] - x == 1 || x - room[1] == 1 || room[2] - z == 1 || z - room[3] == 1) {
                        grid[z, x] = 'P';
                    } else if (room[0] == x || room[1] == x || room[2] == z || room[3] == z) {
                        grid[z, x] = 'E';
                    } else {
                        grid[z, x] = 'F';
                    }
                    floorTiles += 1;
                }
            }
        }

        //Make the room extend to 1 tile around it - no merging rooms together (for now)
        //room[0] -= 1;
        //room[1] += 1;
        //room[2] -= 1;
        //room[3] += 1;
        return room;
    }

    //Returns true if the proposed room does not intersect with the other rooms. Rooms are of the form {startX, endX, startZ, endZ} and these values are 1 beyond the edges of the room itself
    bool RayTraceRect(int[] target, List<int[]> rooms) {
        foreach (int[] room in rooms) {
            if (!(target[0] > room[1] + 1 || room[0] - 1 > target[1] || target[2] > room[3] + 1 || room[2] - 1 > target[3])) {
                return false;
            }
        }
        return true;
    }

    //Determine the length of a room dimension
    int CalculateRoomDimension(int floorSize) {
        int average = (int)(floorSize/3 * roomSize);
        int result = rng.Next((int)average/2, (int)average*3/2);
        return System.Math.Max(result, 3);
    }

    //Is this tile free to be changed without breaking something
    bool Diggable(char code) {
        if (code == 0 || code == 'N') {
            return true;
        } else {
            return false;
        }
    }

    void PrintGrid(char[,] grid) {
        for (int z = 0; z < grid.GetLength(0); z++) {
            string row = "";
            for (int x = 0; x < grid.GetLength(1); x++) {
                if (grid[z, x] == 0) {
                    row += '0';
                } else {
                    row += grid[z, x];
                }
            }
            Debug.Log(row);
        }
    } 

    //Draw the dungeon in Unity
    void Render(char[,] grid) {
        GameObject current;
        for (int z = 0; z < grid.GetLength(0); z++) {
            for (int x = 0; x < grid.GetLength(1); x++) {
                switch (grid[z, x]) {
                    case (char)0:
                        current = Instantiate(tile0, new Vector3(x + 0.5f, height0, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, (char)0);
                        if (height0 > -1.5) {
                            current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        }
                        if (height0 > -0.5) {
                            current = Instantiate(tileF, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'F');
                        }
                        break;
                    case 'P':
                        current = Instantiate(tileP, new Vector3(x + 0.5f, heightP, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'P');
                        if (heightP > -1.5) {
                            current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        }
                        if (heightP > -0.5) {
                            current = Instantiate(tileF, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'F');
                        }
                        break;
                    case 'O':
                        current = Instantiate(tileO, new Vector3(x + 0.5f, heightO, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'O');
                        if (heightO > -1.5) {
                            current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        }
                        if (heightO > -0.5) {
                            current = Instantiate(tileF, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'F');
                        }
                        break;
                    case 'V':
                        current = Instantiate(tileV, new Vector3(x + 0.5f, heightV, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        if (heightV > -1.5) {
                            current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        }
                        if (heightV > -0.5) {
                            current = Instantiate(tileF, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                            current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'F');
                        }
                        break;
                    case 'W':
                        current = Instantiate(tileW, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'W');
                        break;
                    case 'S':
                        current = Instantiate(tileS, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'S');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'B':
                        current = Instantiate(tileB, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'B');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'H':
                        current = Instantiate(tileH, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'H');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'M':
                        current = Instantiate(tileM, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'M');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'E':
                        current = Instantiate(tileE, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'E');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'F':
                        current = Instantiate(tileF, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'F');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                    case 'T':
                        current = Instantiate(tileM, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'M');
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -1.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        Instantiate(treasure, new Vector3(x + 0.5f, 0.15f, z + 0.5f), Quaternion.identity);
                        break;
                    default:
                        current = Instantiate(tileV, new Vector3(x + 0.5f, -0.5f, z + 0.5f), Quaternion.identity);
                        current.GetComponent<Renderer>().material.color = GetPallette(pallette, 'V');
                        break;
                }
            }
        }
    }

    //Get rid of everything rendered
    void Clear () {
        GameObject[] toDelete = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject g in toDelete) {
            Destroy(g);
        }
    }

    //Connect all the rooms into one traversable area
    void CreateHallways(char[,] grid, List<int[]> rooms) {
        //Put all rooms in a graph.
        List<int> roots = new List<int>();
        for (int i = 0; i < rooms.Count; i++) {
            roots.Add(i);
        }
        //Make hallways until everything is connected
        int breaker = 0;
        while (!Connected(roots)) {
            int startIndex = rng.Next(rooms.Count);
            int endIndex = FindSeparate(startIndex, roots);
            MakeHallway(grid, startIndex, endIndex, rooms, roots);
            breaker++;
            if (breaker > 1000) {
                Debug.Log("Fail");
                break;
            }
        }
    }

    //Make a path between 2 rooms
    void MakeHallway(char[,] grid, int startIndex, int endIndex, List<int[]> rooms, List<int> roots) {
        //Find all legal directions to start the hallway from
        int[] start = rooms[startIndex];
        int[] end = rooms[endIndex];
        List<int> legal = new List<int>();
        if (start[0] > end[1]) {
            legal.Add(0);
        }
        if (start[1] < end[0]) {
            legal.Add(1);
        }
        if (start[2] > end[3]) {
            legal.Add(2);
        }
        if (start[3] < end[2]) {
            legal.Add(3);
        }
        int direction = legal[rng.Next(legal.Count)];

        //Choose the tiles for the start and end of the path. Prioritize tiles already in a hallway
        int startX, startZ, endX, endZ;
        if (direction < 2) {
            startX = start[direction];
            endX = end[(direction + 1) % 2];
            startZ = rng.Next(start[2], start[3] + 1);
            endZ = rng.Next(end[2], end[3] + 1);
            for (int i = start[2]; i <= start[3]; i++) {
                if (grid[i, startX] == 'M') {
                    startZ = i;
                    break;
                }
            }
            for (int i = end[2]; i <= end[3]; i++) {
                if (grid[i, endX] == 'M') {
                    endZ = i;
                    break;
                }
            }
        } else {
            startZ = start[direction];
            endZ = end[(direction + 1) % 2 + 2];
            startX = rng.Next(start[0], start[1] + 1);
            endX = rng.Next(end[0], end[1] + 1);
            for (int i = start[0]; i <= start[1]; i++) {
                if (grid[startZ, i] == 'M') {
                    startX = i;
                    break;
                }
            }
            for (int i = end[0]; i <= end[1]; i++) {
                if (grid[endZ, i] == 'M') {
                    endX = i;
                    break;
                }
            }
        }

        //Excavate the path
        int currX = startX;
        int currZ = startZ;
        grid[currZ, currX] = 'M';
        bool incomplete = true;
        bool success = false;
        bool horizontal = false;
        int checkpoint = 0;
        int breaker = 0;
        while (incomplete) {
            breaker++;
            if (breaker > 1000) {
                Debug.Log("NoDig");
                Debug.Log(currX);
                Debug.Log(currZ);
                Debug.Log(endX);
                Debug.Log(endZ);
                Debug.Log(grid[currZ, currX]);
                success = true;
                break;
            }
            //Pick where to move. Rule priority:
            //If one direction needs no more movement choose the other one
            //Dig away from the starting room first
            //If you dig towards another hallway continue down it
            //Dig in the same direction with probability hallwayOrder
            //Dig randomly
            if (currX == endX) {
                currZ += (endZ-currZ)/System.Math.Abs(currZ-endZ);    
                horizontal = false;
            } else if (currZ == endZ) {
                currX += (endX-currX)/System.Math.Abs(endX-currX);
                horizontal = true;
            } else if (direction < 2 && System.Math.Abs(currX - startX) < 2) {
                currX += direction*2 - 1;
                horizontal = true;
            } else if (direction >= 2 && System.Math.Abs(currZ - startZ) < 2) {
                currZ += direction*2 - 5;
                horizontal = false;
            } else if (horizontal && grid[currZ, currX + (endX-currX)/System.Math.Abs(endX-currX)] == 'H') {
                currX += (endX-currX)/System.Math.Abs(endX-currX);
            } else if (!horizontal && grid[currZ + (endZ-currZ)/System.Math.Abs(currZ-endZ), currX] == 'H') {
                currZ += (endZ-currZ)/System.Math.Abs(currZ-endZ);
            } else if (horizontal && rng.NextDouble() <= hallwayOrder) {
                currX += (endX-currX)/System.Math.Abs(endX-currX);
            } else if (!horizontal && rng.NextDouble() <= hallwayOrder) {
                currZ += (endZ-currZ)/System.Math.Abs(currZ-endZ);
            } else if (rng.NextDouble() >= 0.5) {
                currX += (endX-currX)/System.Math.Abs(endX-currX);
                horizontal = true;
            } else {
                currZ += (endZ-currZ)/System.Math.Abs(currZ-endZ);
                horizontal = false;
            }

            //Determine what to do with the new position based on its ascii code.
            char code = grid[currZ, currX];
            if (code == 0) {
                grid[currZ, currX] = 'H';
            } else if (code == 'P' || code == 'H') { //Check if you digged into a room. If you dig to the "wrong room", dig from that room to the destination.
                grid[currZ, currX] = 'H';
                int west = CheckAdjacent(currX - 1, currZ, start, end, rooms, grid);
                if (west == -1) {
                    incomplete = false;
                    success = true;
                    grid[currZ, currX - 1] = 'M';
                    roots[endIndex] = roots[startIndex];
                } else if (west >= 0) {
                    incomplete = false;
                    grid[currZ, currX - 1] = 'M';
                    roots[west] = roots[startIndex];
                    checkpoint = west;
                }
                int east = CheckAdjacent(currX + 1, currZ, start, end, rooms, grid);
                if (east == -1) {
                    incomplete = false;
                    success = true;
                    grid[currZ, currX + 1] = 'M';
                    roots[endIndex] = roots[startIndex];
                } else if (east >= 0) {
                    incomplete = false;
                    grid[currZ, currX + 1] = 'M';
                    roots[east] = roots[startIndex];
                    checkpoint = east;
                }
                int north = CheckAdjacent(currX, currZ + 1, start, end, rooms, grid);
                if (north == -1) {
                    incomplete = false;
                    success = true;
                    grid[currZ + 1, currX] = 'M';
                    roots[endIndex] = roots[startIndex];
                } else if (north >= 0) {
                    incomplete = false;
                    grid[currZ + 1, currX] = 'M';
                    roots[north] = roots[startIndex];
                    checkpoint = north;
                }
                int south = CheckAdjacent(currX, currZ - 1, start, end, rooms, grid);
                if (south == -1) {
                    incomplete = false;
                    success = true;
                    grid[currZ - 1, currX] = 'M';
                    roots[endIndex] = roots[startIndex];
                } else if (south >= 0) {
                    incomplete = false;
                    grid[currZ - 1, currX] = 'M';
                    roots[south] = roots[startIndex];
                    checkpoint = south;
                }
            }
        }
        if (success) {
            return;
        } else {
            MakeHallway(grid, checkpoint, endIndex, rooms, roots);
        }
    }

    //Check which room a hallway has led to
    int CheckAdjacent(int x, int z, int[] start, int[] end, List<int[]> rooms, char[,] grid) {
        if (grid[z, x] != 'E' && grid[z, x] != 'F' && grid[z, x] != 'M') {
            return -2;
        } 
        if (x >= start[0] &&  x <= start[1] && z >= start[2] && z <= start[3]) {
            return -2;
        } else if (x >= end[0] &&  x <= end[1] && z >= end[2] && z <= end[3]) {
            return -1;
        }
        for (int i = 0; i < rooms.Count; i++) {
            int[] room = rooms[i];
            if (x >= room[0] &&  x <= room[1] && z >= room[2] && z <= room[3]) {
                return i;
            }
        }
        return -2;
    }   

    //returns true if every node has the same root
    bool Connected(List<int> roots) {
        int choice = ParseTree(0, roots);
        for (int i = 1; i < roots.Count; i++) {
            if (ParseTree(i, roots) != choice) {
                return false;
            }
        }
        return true;
    }

    //Gets the bottom root of the given node and updates the roots to be more efficient.
    int ParseTree(int index, List<int> roots) {
        if (roots[index] == index) {
            return index;
        } else {
            int root = ParseTree(roots[index], roots);
            roots[index] = root;
            return root;
        }
    }

    int FindSeparate(int index, List<int> roots) {
        int choice = ParseTree(index, roots);
        List<int> legal = new List<int>();
        for (int i = 0; i < roots.Count; i++) {
            if (choice != ParseTree(i, roots)) {
                legal.Add(i);
            }
        }
        if (legal.Count == 0) {
            Debug.Log("No separate");
            return 0;
        }
        return legal[rng.Next(legal.Count)];
    }

    //Place treasure chests on the map
    void PlaceTreasure(char[,] grid, int numRooms) {
        int amount = rng.Next((int)(treasureDensity*numRooms*0.5), (int)(treasureDensity*numRooms)) + 1;
        for (int i = 0; i < amount; i++) {    
            List<int[]> legal = new List<int[]>(); //{z, x} coordinates
            for (int z = 0; z < grid.GetLength(0); z++) {
                for (int x = 0; x < grid.GetLength(0); x++) {
                    if (LegalChestLocation(grid, z, x)) {
                        legal.Add(new int[] {z, x});
                    }
                }
            } 
            if (legal.Count == 0) {
                break; //nowhere to put one
            } else {
                int choiceIndex = rng.Next(legal.Count);
                int[] choice = legal[choiceIndex];
                legal.RemoveAt(choiceIndex);
                grid[choice[0], choice[1]] = 'T'; //Maybe add orientation
            }
        }
    }

    //Determine if a tile can have a treasure chest placed on it without breaking things
    public bool LegalChestLocation(char[,] grid, int z, int x) {
        if (z <= 0 || x <= 0 || z >= grid.GetLength(0) - 1 || x >= grid.GetLength(1) - 1) {
            return false;//out of bounds
        }
        if (!(grid[z, x] == 'F' || grid[z, x] == 'E')) {
            return false;//Not the right type of tile
        }
        bool north = IsFloor(grid[z+1, x]);
        bool south = IsFloor(grid[z-1, x]);
        bool east = IsFloor(grid[z, x+1]);
        bool west = IsFloor(grid[z, x-1]);
        bool ne = IsFloor(grid[z+1, x+1]);
        bool se = IsFloor(grid[z-1, x+1]);
        bool nw = IsFloor(grid[z+1, x-1]);
        bool sw = IsFloor(grid[z-1, x-1]);
        //if ((ne && se && nw) || (sw && se && nw) || (ne && sw && nw) || (ne && se && sw)) {
            //return true; //enough corners to be safe
        //}
        if (((north && east) && !ne) || ((south && east) && !se) || ((north && west) && !nw) || ((south && west) && !sw)) {
            return false; //cutoff a path
        }
        if ((south && north && !((east && ne && se ) || (west && nw && sw))) || (east && west && !((north && ne && nw) || (south && se && sw)))) {
            return false;
        }        
        return true;
    }

    //Returns true if the code is of a floor tile
    public bool IsFloor(char code) {
        if (code == 'F' || code == 'H' || code == 'M' || code == 'E' || code == 'C' || code == 'B') {
            return true;
        }
        return false;
    }

    //Modify the rooms to be less boring
    void ModifyRooms(char[,] grid, List<int[]> rooms) {
        int oscillation = 0;
        foreach (int[] room in rooms) {
            /*
            if (oscillation == 0) {
                if ((float)rng.NextDouble() >= rectangularity) {
                    ModifyPerimeter(grid, room);
                } else if ((float)rng.NextDouble() < interiorFeatures) {
                    ModifyInterior(grid, room);
                }
                oscillation = 1;
            } else {
            */
                if ((float)rng.NextDouble() < interiorFeatures) {
                    ModifyInterior(grid, room);
                } else if ((float)rng.NextDouble() >= rectangularity) {
                    ModifyPerimeter(grid, room);
                }
                //oscillation = 0;
            //}
        }
    }
    
    //Make the room shaped less like a rectangle
    void ModifyPerimeter(char[,] grid, int[] room) {
        List<int[]> legal = new List<int[]>();
        for (int z = room[2]; z <= room[3]; z++) {
            for (int x = room[0]; x <= room[1]; x++) {
                if (grid[z, x] == 'E' && LegalChestLocation(grid, z, x)) {
                    legal.Add(new int[] {z, x});
                }
            }
        }
        if (System.Math.Min(rng.Next(2*(room[3]-room[2] + 1) + 2*(room[1]-room[0]+1)),rng.Next(2*(room[3]-room[2] + 1) + 2*(room[1]-room[0]+1))) < legal.Count) {
            int[] choice = legal[rng.Next(legal.Count)];
            grid[choice[0], choice[1]] = 'P';
            if (grid[choice[0] + 1, choice[1]] == 'F') {
                grid[choice[0] + 1, choice[1]] = 'E';
            }
            if (grid[choice[0] - 1, choice[1]] == 'F') {
                grid[choice[0] - 1, choice[1]] = 'E';
            }
            if (grid[choice[0], choice[1] + 1] == 'F') {
                grid[choice[0], choice[1] + 1] = 'E';
            }
            if (grid[choice[0], choice[1] - 1] == 'F') {
                grid[choice[0], choice[1] - 1] = 'E';
            }
            ModifyPerimeter(grid, room);
        }
    }

    //Change the interior of the room to be more interesting
    void ModifyInterior(char[,] grid, int[] room) {
        if (room[1] - room[0] <= 2 || room[3] - room[2] <= 2) {
            return; //no interior
        }
        int type = rng.Next(3);
        if (type == 0) {
            ObstacleBlob(grid, new int[] {room[0] + 1, room[1] - 1, room[2] + 1, room[3] - 1});
        } else if (type == 1) {
            IdenticalPillars(grid, new int[] {room[0] + 1, room[1] - 1, room[2] + 1, room[3] - 1});
        } else {
            CentralLake(grid, new int[] {room[0] + 1, room[1] - 1, room[2] + 1, room[3] - 1});
        }
        
    }

    //An algorithm that forms an irregular obstacle in a room interior
    void ObstacleBlob(char[,] grid, int[] interior) {
        int x = rng.Next(interior[0], interior[1] + 1);
        int z = rng.Next(interior[2], interior[3] + 1);
        int totalTiles = (interior[1]+1-interior[0])*(interior[3]+1-interior[2]);
        int blockedTiles = 0;
        int tileSeed = rng.Next(3);
        char tileType = 'V';
        if (tileSeed == 1) {
            tileType = 'W';
        } else if (tileSeed == 2) {
            tileType = 'O';
        }
        while (true) {
            grid[z, x] = tileType;
            blockedTiles++;
            List<int[]> legal = new List<int[]>();
            for (int i = interior[2]; i <= interior[3]; i++) {
                for (int j = interior[0]; j <= interior[1]; j++) {
                    if (IsFloor(grid[i, j]) && !(IsFloor(grid[i+1, j]) && IsFloor(grid[i-1, j]) && IsFloor(grid[i, j+1]) && IsFloor(grid[i, j-1]))) {
                        legal.Add(new int[] {i, j});
                    }
                }
            }
            if (rng.Next(totalTiles) >= blockedTiles && legal.Count > 0) {
                int[] choice = legal[rng.Next(legal.Count)];
                z = choice[0];
                x = choice[1];
            } else {
                break;
            }
        }
        for (int i = interior[2] + 1; i < interior[3]; i++) {
            for (int j = interior[0] + 1; j < interior[1]; j++) {
                if (grid[i, j] == 'F' && grid[i+1, j] == tileType && grid[i-1, j] == tileType && grid[i, j+1] == tileType && grid[i, j-1] == tileType) {
                    grid[i, j] = tileType;
                }
            }   
        }
    }

    //An algorithm that creates a large gap in the center with 0 or more bridges
    void CentralLake(char[,] grid, int[] interior) {
        int tileSeed = rng.Next(2);
        char tileType = 'V';
        if (tileSeed == 1) {
            tileType = 'W';
        }
        for (int x = interior[0]; x <= interior[1]; x++) {
            for (int z = interior[2]; z <= interior[3]; z++) {
                grid[z, x] = tileType;
            }
        }
        while (true) {
            List<int[]> legal = new List<int[]>();
            for (int x = interior[0] - 1; x <= interior[1] + 1; x++) {
                for (int z = interior[2] - 1; z <= interior[3] + 1; z++) {
                    if (IsFloor(grid[z, x])) {
                        legal.Add(new int[] {z, x});
                    }
                }
            }
            if (rng.NextDouble() < 0.75 && legal.Count > 0) {
                int[] choice = legal[rng.Next(legal.Count)];
                List<char> directions = new List<char>();
                if (!IsFloor(grid[choice[0] + 1, choice[1]]) && choice[0] < interior[3]) {
                    directions.Add('N');
                }
                if (!IsFloor(grid[choice[0] - 1, choice[1]]) && choice[0] > interior[2]) {
                    directions.Add('S');
                }
                if (!IsFloor(grid[choice[0], choice[1] + 1]) && choice[1] < interior[1]) {
                    directions.Add('E');
                }
                if (!IsFloor(grid[choice[0], choice[1] - 1]) && choice[1] > interior[0]) {
                    directions.Add('W');
                }
                if (directions.Count == 0) {
                    break;
                }
                char direction = directions[rng.Next(directions.Count)];
                int dz = 0;
                int dx = 0;
                if (direction == 'N') {
                    dz = 1;
                } else if (direction == 'S') {
                    dz = -1;
                } else if (direction == 'E') {
                    dx = 1;
                } else if (direction == 'W') {
                    dx = -1;
                }
                int z = choice[0];
                int x = choice[1];
                while (true) {
                    z = z + dz;
                    x = x + dx;
                    if (IsFloor(grid[z, x])) {
                        break;
                    }
                    grid[z, x] = 'B';
                    if (rng.NextDouble() < 0.25) {
                        break;
                    }
                }
            } else {
                break;
            }
        }
    }

    //An Algorithm that places obstacles at regular intervals
    void IdenticalPillars(char[,] grid, int[] interior) {
        int xLength = interior[1] + 1 - interior[0];
        int zLength = interior[3] + 1 - interior[2];
        int pillarX = rng.Next(xLength/2) + 1;
        int pillarZ = rng.Next(zLength/2) + 1;
        int tileSeed = rng.Next(4);
        char tileType = 'V';
        if (tileSeed == 1) {
            tileType = 'W';
        } else if (tileSeed == 2) {
            tileType = 'S';
        } else if (tileSeed == 3) {
            tileType = 'O';
        }
        //pillar creation
        char[,] pillar = new char[pillarZ, pillarX];
        for (int z = 0; z < pillarZ; z++) {
            for (int x = 0; x < pillarX; x++) {
                if (rng.NextDouble() > 0.5) {
                    pillar[z, x] = tileType;
                } else {
                    pillar[z, x] = 'F';
                }
            }   
        }
        for (int z = 1; z < pillarZ - 1; z++) {
            for (int x = 1; x < pillarX - 1; x++) {
                if (pillar[z, x] == 'F' && pillar[z+1, x] == tileType && pillar[z-1, x] == tileType && pillar[z, x+1] == tileType && pillar[z, x-1] == tileType) {
                    pillar[z, x] = tileType;
                }
            }   
        }
        //Placing them
        int xLeeway = xLength - pillarX;
        int zLeeway = zLength - pillarZ;
        int xChoice = rng.Next(System.Math.Min(xLeeway, pillarX + 1));
        int zChoice = rng.Next(System.Math.Min(zLeeway, pillarZ + 1));
        int currX = xChoice;
        int currZ = zChoice;
        //int zDirection;
        //int xDirection = 1;
        while (currX <= xLeeway) {
            //zDirection = 1;
            currZ = zChoice;
            while (currZ <= zLeeway) {
                for (int z = 0; z < pillarZ; z++) {
                    for (int x = 0; x < pillarX; x++) {
                        grid[z + currZ + interior[2], x + currX + interior[0]] = pillar[z, x];
                    }   
                }
                currZ += pillarZ + 1;
                //if (currZ > zLeeway) {
                    //zDirection = -1;
                   // currZ = zChoice - (pillarZ + 1);
                //}
            }
            currX += pillarX + 1;
            //if (currX > xLeeway) {
                //xDirection = -1;
              //  currX = xChoice - (pillarX + 1);
            //}
        }
        
    }

    //Get the color for the given pallette and tile
    /*
    0 = The pit
    1 = Forest
    2 = Underwater
    3 = Volcano
    4 = Islands
    5 = Swamp
    6 = Tunnels
    7 = Ruins
    8 = Facility
    9 = Clouds
    10 = Desert
    11 = Cult Base
    12 = Crystalline
    13 = Castle
    14 = Void
    15 = Snow
    */
    public Color GetPallette(int pallette, char code) {
        switch (pallette) {
            case 0:
                switch (code) {
                    case 'F':
                    case 'E':
                    case 'M':
                    case 'H':
                        return new Color(0.1f, 0.1f, 0.2f, 1);
                        break;
                    case 'V':
                        return new Color(0, 0, 0, 1);
                        break;
                    case 'W':
                        return new Color(0.1f, 0.1f, 0.4f, 1);
                        break;
                    case 'S':
                        return new Color(0.5f, 0.1f, 0.2f, 1);
                        break;
                    case 'B':
                        return new Color(0.8f, 0.1f, 0.1f, 1);
                        break;
                    case (char)0:
                    case 'P':
                    case 'O':
                        return new Color(0.1f, 0.1f, 0.1f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 1:
                switch (code) {
                    case 'F':
                    case 'H':
                        return new Color(0, 1f, 0, 1);
                        break;
                    case 'E':
                    case 'M':
                        return new Color(0, 0.8f, 0, 1);
                        break;
                    case 'V':
                        return new Color(0.2f, 0.2f, 0, 1);
                        break;
                    case 'W':
                        return new Color(0, 0, 1, 1);
                        break;
                    case 'S':
                        return new Color(1f, 0.4f, 0, 1);
                        break;
                    case 'B':
                        return new Color(1, 0.9f, 0.5f, 1);
                        break;
                    case (char)0:
                    case 'P':
                    case 'O':
                        return new Color(0.2f, 0.5f, 0.1f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 2:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                        return new Color(0, 0, 1, 1);
                        break;
                    case 'H':
                        return new Color(0, 0.2f, 1, 1);
                        break;
                    case 'V':
                        return new Color(0, 0, 0.2f, 1);
                        break;
                    case 'W':
                        return new Color(0, 0, 0.6f, 1);
                        break;
                    case 'S':
                        return new Color(0, 0.5f, 0.7f, 1);
                        break;
                    case 'B':
                        return new Color(1, 0.4f, 0, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0, 0, 0.4f, 1);
                        break;
                    case 'O':
                        return new Color(0.8f, 0.6f, 0.2f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 3:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                        return new Color(0.5f, 0.1f, 0.1f, 1);
                        break;
                    case 'H':
                        return new Color(0.3f, 0.2f, 0.1f, 1);
                        break;
                    case 'V':
                        return new Color(0.8f, 0, 0, 1);
                        break;
                    case 'W':
                        return new Color(0.3f, 0, 0.2f, 1);
                        break;
                    case 'S':
                        return new Color(1, 0, 0, 1);
                        break;
                    case 'B':
                        return new Color(0.2f, 0.2f, 0.2f, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.2f, 0.1f, 0.1f, 1);
                        break;
                    case 'O':
                        return new Color(1, 0, 0.2f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 4:
                switch (code) {
                    case 'F':
                        return new Color(1, 0.8f, 0.7f, 1);
                        break;
                    case 'M':
                    case 'E':
                        return new Color(0.9f, 0.7f, 0.7f, 1);
                        break;
                    case 'H':
                        return new Color(0.8f, 0.4f, 0.6f, 1);
                        break;
                    case 'V':
                        return new Color(0, 0, 0.5f, 1);
                        break;
                    case 'W':
                        return new Color(0, 0, 1, 1);
                        break;
                    case 'S':
                        return new Color(0, 0.7f, 0, 1);
                        break;
                    case 'B':
                        return new Color(0.8f, 0.4f, 0.6f, 1);
                        break;
                    case (char)0:
                        return new Color(0, 0, 0.8f, 1);
                        break;
                    case 'P':
                        return new Color(0, 0, 1, 1);
                        break;
                    case 'O':
                        return new Color(0, 1, 0.2f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 5:
                switch (code) {
                    case 'F':
                        return new Color(0, 0.5f, 0.2f, 1);
                        break;
                    case 'M':
                    case 'E':
                        return new Color(0.2f, 0.5f, 0.3f, 1);
                        break;
                    case 'H':
                        return new Color(0.2f, 0.4f, 0.2f, 1);
                        break;
                    case 'V':
                        return new Color(0, 0.4f, 0.3f, 1);
                        break;
                    case 'W':
                        return new Color(0.2f, 0.1f, 0.6f, 1);
                        break;
                    case 'S':
                        return new Color(0, 0.5f, 0.5f, 1);
                        break;
                    case 'B':
                        return new Color(0.3f, 0.8f, 0.3f, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.1f, 0.4f, 0.4f, 1);
                        break;
                    case 'O':
                        return new Color(0.3f, 0.7f, 0, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 6:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                    case 'H':
                        return new Color(0.6f, 0.2f, 0.2f, 1);
                        break;
                    case 'V':
                        return new Color(0.7f, 0.3f, 0.3f, 1);
                        break;
                    case 'W':
                        return new Color(0, 0, 1, 1);
                        break;
                    case 'S':
                        return new Color(0, 0.6f, 0, 1);
                        break;
                    case 'B':
                        return new Color(0.4f, 0.2f, 0.2f, 1);
                        break;
                    case (char)0:
                    case 'P':
                    case 'O':
                        return new Color(0.3f, 0.2f, 0.2f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 7:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                    case 'H':
                        return new Color(0.8f, 1, 0.8f, 1);
                        break;
                    case 'V':
                        return new Color(0.7f, 0, 0, 1);
                        break;
                    case 'W':
                        return new Color(0.8f, 0.8f, 0, 1);
                        break;
                    case 'S':
                        return new Color(0.5f, 0.5f, 0.5f, 1);
                        break;
                    case 'B':
                        return new Color(0.8f, 1, 0.8f, 1);
                        break;
                    case (char)0:
                    case 'P':
                    case 'O':
                        return new Color(0.8f, 0.8f, 0.8f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 8:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                        return new Color(1, 1, 1, 1);
                        break;
                    case 'H':
                        return new Color(0.6f, 1, 1, 1);
                        break;
                    case 'V':
                        return new Color(1, 1, 0, 1);
                        break;
                    case 'W':
                        return new Color(0, 1, 0.2f, 1);
                        break;
                    case 'S':
                        return new Color(1, 0.2f, 0.8f, 1);
                        break;
                    case 'B':
                        return new Color(0, 0.2f, 1, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(1, 1, 1, 1);
                        break;
                    case 'O':
                        return new Color(0.8f, 1, 1, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 9:
                switch (code) {
                    case 'F':
                    case 'E':
                        return new Color(1, 1, 1, 1);
                        break;
                    case 'M':
                    case 'H':
                        return new Color(1, 0.6f, 0.6f, 1);
                        break;
                    case 'V':
                        return new Color(0.5f, 0.9f, 1, 1);
                        break;
                    case 'W':
                        return new Color(0.5f, 0.5f, 0.5f, 1);
                        break;
                    case 'S':
                        return new Color(1, 1, 0, 1);
                        break;
                    case 'B':
                        return new Color(1, 0.6f, 0.6f, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.5f, 0.9f, 1, 1);
                        break;
                    case 'O':
                        return new Color(0.8f, 0.8f, 0.8f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 10:
                switch (code) {
                    case 'F':
                    case 'M':
                    case 'E':
                        return new Color(1, 0.8f, 0.4f, 1);
                        break;
                    case 'H':
                        return new Color(0.5f, 0.5f, 0.5f, 1);
                        break;
                    case 'V':
                        return new Color(1, 0.8f, 0.4f, 1);
                        break;
                    case 'W':
                        return new Color(1, 0.8f, 0.4f, 1);
                        break;
                    case 'S':
                        return new Color(1, 0.4f, 0.4f, 1);
                        break;
                    case 'B':
                        return new Color(0.7f, 0.6f, 0.6f, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(1, 0.6f, 0.6f, 1);
                        break;
                    case 'O':
                        return new Color(0.7f, 0.7f, 0.7f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 11:
                switch (code) {
                    case 'F':
                    case 'E':
                        return new Color(0.4f, 0.3f, 0.3f, 1);
                        break;
                    case 'M':
                        return new Color(0.7f, 0.4f, 0.7f, 1);
                        break;
                    case 'H':
                        return new Color(0, 0, 0, 1);
                        break;
                    case 'V':
                        return new Color(1, 0, 0, 1);
                        break;
                    case 'W':
                        return new Color(0.7f, 0.2f, 0.2f, 1);
                        break;
                    case 'S':
                        return new Color(0.1f, 0.1f, 0.1f, 1);
                        break;
                    case 'B':
                        return new Color(0.5f, 0.2f, 0.5f, 1);
                        break;
                    case (char)0:
                        return new Color(0.1f, 0.1f, 0.1f, 1);
                        break;
                    case 'P':
                    case 'O':
                        return new Color(0.7f, 0.2f, 0.2f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 12:
                switch (code) {
                    case 'F':
                        return new Color(1, 1, 1, 1);
                        break;
                    case 'E':
                    case 'M':
                        return new Color(0.4f, 1, 0.4f, 1);
                        break;
                    case 'H':
                        return new Color(0.4f, 0.4f, 1, 1);
                        break;
                    case 'V':
                        return new Color(1, 1, 1, 1);
                        break;
                    case 'W':
                        return new Color(1, 0.5f, 1, 1);
                        break;
                    case 'S':
                        return new Color(1, 0.4f, 0.4f, 1);
                        break;
                    case 'B':
                        return new Color(0.7f, 0.7f, 0.7f, 1);
                        break;
                    case (char)0:
                        return new Color(1, 1, 0.4f, 1);
                        break;
                    case 'P':
                        return new Color(1, 0.4f, 1, 1);
                        break;
                    case 'O':
                        return new Color(0.4f, 1, 1, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 13:
                switch (code) {
                    case 'F':
                        return new Color(0.9f, 0, 0, 1);
                        break;
                    case 'E':
                    case 'M':
                    case 'H':
                        return new Color(0.9f, 0.9f, 0.9f, 1);
                        break;
                    case 'V':
                        return new Color(0.4f, 0.4f, 0.4f, 1);
                        break;
                    case 'W':
                        return new Color(1, 0.3f, 0.3f, 1);
                        break;
                    case 'S':
                        return new Color(0.9f, 0.9f, 0.9f, 1);
                        break;
                    case 'B':
                        return new Color(1, 1, 1, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.9f, 0.9f, 0.9f, 1);
                        break;
                    case 'O':
                        return new Color(0.9f, 0.5f, 0.9f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 14:
                switch (code) {
                    case 'F':
                    case 'E':
                    case 'M':
                    case 'H':
                        return new Color(0.5f, 0, 1, 1);
                        break;
                    case 'V':
                        return new Color(0, 0, 0, 1);
                        break;
                    case 'W':
                        return new Color(0.5f, 0.2f, 0.7f, 1);
                        break;
                    case 'S':
                        return new Color(1, 0, 0, 1);
                        break;
                    case 'B':
                        return new Color(0.5f, 0, 1, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.1f, 0, 0.1f, 1);
                        break;
                    case 'O':
                        return new Color(0, 0.1f, 0, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            case 15:
                switch (code) {
                    case 'F':
                    case 'E':
                    case 'M':
                    case 'H':
                        return new Color(0.8f, 0.9f, 1, 1);
                        break;
                    case 'V':
                        return new Color(0.5f, 0.7f, 1, 1);
                        break;
                    case 'W':
                        return new Color(0.8f, 0.9f, 1, 1);
                        break;
                    case 'S':
                        return new Color(0.5f, 0.7f, 1, 1);
                        break;
                    case 'B':
                        return new Color(0.6f, 0.3f, 0.3f, 1);
                        break;
                    case (char)0:
                    case 'P':
                        return new Color(0.8f, 0.9f, 1, 1);
                        break;
                    case 'O':
                        return new Color(0, 0.8f, 0.5f, 1);
                        break;
                    default:
                        return new Color(1, 1, 1, 1);
                        break;
                }
                break;
            default:
                return new Color(1, 1, 1, 1);
                break;
        }
    }

    //Changes the tile textures for the current pallette
    void SetTiles(int pallette) {
        heightV = -1.5f;
        height0 = 0.5f;
        heightP = 0.5f;
        heightO = 0.5f;
        switch (pallette) {
            case 0:
                tileF = floorTile;
                tileE = floorTile;
                tileM = floorTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = wallTile;
                break;
            case 1:
                tileF = grassTile;
                tileE = grassTile;
                tileM = grassTile;
                tileH = grassTile;
                tileB = bridgeTile;
                tileV = bubbleTile;
                tileW = waterTile;
                tileS = grassTile;
                tile0 = treeTile;
                tileP = treeTile;
                tileO = treeTile;
                break;
            case 2:
                tileF = waterTile;
                tileE = waterTile;
                tileM = waterTile;
                tileH = waterTile;
                tileB = floorTile;
                tileV = bubbleTile;
                //heightV = -0.5f;
                tileW = waterTile;
                tileS = bubbleTile;
                tile0 = bubbleTile;
                height0 = -0.5f;
                tileP = bubbleTile;
                heightP = -0.5f;
                tileO = wallTile;
                break;
            case 3:
                tileF = floorTile;
                tileE = floorTile;
                tileM = floorTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = bubbleTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = wallTile;
                break;
            case 4:
                tileF = sandTile;
                tileE = sandTile;
                tileM = sandTile;
                tileH = bridgeTile;
                tileB = bridgeTile;
                tileV = bubbleTile;
                tileW = waterTile;
                tileS = grassTile;
                tile0 = waterTile;
                height0 = -1.5f;
                tileP = waterTile;
                heightP = -1.5f;
                tileO = treeTile;
                break;
            case 5:
                tileF = waterTile;
                tileE = grassTile;
                tileM = grassTile;
                tileH = grassTile;
                tileB = bridgeTile;
                tileV = bubbleTile;
                tileW = waterTile;
                tileS = bubbleTile;
                tile0 = bubbleTile;
                height0 = -1.5f;
                tileP = bubbleTile;
                heightP = -1.5f;
                tileO = treeTile;
                break;
            case 6:
                tileF = floorTile;
                tileE = floorTile;
                tileM = floorTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = grassTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = wallTile;
                break;
            case 7:
                tileF = floorTile;
                tileE = cfloorTile;
                tileM = cfloorTile;
                tileH = cfloorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = cwallTile;
                tileP = cwallTile;
                tileO = cwallTile;
                break;
            case 8:
                tileF = cfloorTile;
                tileE = cfloorTile;
                tileM = cfloorTile;
                tileH = bridgeTile;
                tileB = bridgeTile;
                tileV = grassTile;
                tileW = bubbleTile;
                tileS = bubbleTile;
                tile0 = cwallTile;
                tileP = cwallTile;
                tileO = cwallTile;
                break;
            case 9:
                tileF = sandTile;
                tileE = sandTile;
                tileM = bridgeTile;
                tileH = bridgeTile;
                tileB = bridgeTile;
                tileV = bubbleTile;
                tileW = sandTile;
                tileS = grassTile;
                tile0 = bubbleTile;
                height0 = -1.5f;
                tileP = bubbleTile;
                heightP = -1.5f;
                tileO = cwallTile;
                break;
            case 10:
                tileF = sandTile;
                tileE = sandTile;
                tileM = sandTile;
                tileH = cfloorTile;
                tileB = cfloorTile;
                tileV = voidTile;
                tileW = sandTile;
                tileS = spikeTile;
                tile0 = cwallTile;
                tileP = cwallTile;
                tileO = wallTile;
                break;
            case 11:
                tileF = cfloorTile;
                tileE = cfloorTile;
                tileM = treeTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = cwallTile;
                tileP = cwallTile;
                tileO = cwallTile;
                break;
            case 12:
                tileF = floorTile;
                tileE = floorTile;
                tileM = floorTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = wallTile;
                break;
            case 13:
                tileF = sandTile;
                tileE = cfloorTile;
                tileM = cfloorTile;
                tileH = cfloorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = wallTile;
                tileP = cwallTile;
                tileO = cwallTile;
                break;
            case 14:
                tileF = bridgeTile;
                tileE = bridgeTile;
                tileM = bridgeTile;
                tileH = bridgeTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = bubbleTile;
                tileS = bubbleTile;
                tile0 = voidTile;
                height0 = -1.5f;
                tileP = voidTile;
                heightP = -1.5f;
                tileO = voidTile;
                heightO = -1.5f;
                break;
            case 15:
                tileF = sandTile;
                tileE = sandTile;
                tileM = sandTile;
                tileH = grassTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = treeTile;
                break;
            default:
                tileF = floorTile;
                tileE = floorTile;
                tileM = floorTile;
                tileH = floorTile;
                tileB = bridgeTile;
                tileV = voidTile;
                tileW = waterTile;
                tileS = spikeTile;
                tile0 = wallTile;
                tileP = wallTile;
                tileO = wallTile;
                break;
        }
    }

    /*
    The ASCII Codex
    True 0 = Wall
    C = Room Center
    F = Floor
    E = edge of room
    M = mouth of hallway
    T = Treasure Chest
    P = perimeter wall tile, adjacent to floor
    N = wall tile next to the perimeter
    B = Bridge Floor Tile
    V = void
    W = water
    S = Spikes
    O = Obstacle
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{   
    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {   
        GameState.GetGame().rng = new System.Random();
        System.Random rng =  GameState.GetGame().rng;
        char[,] grid = GenerateLayout(rng);
        GetComponent<WallCreator>().Render(grid, rng);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public char[,] GenerateLayout(System.Random rng) {
        char[,] grid = new char[4, 4];
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                grid[i, j] = 'W';
            }
        }
        int start = rng.Next(4);
        int end = rng.Next(4);
        grid[3, start] = 'S';
        grid[0, end] = 'E';
        int row = 3;
        int col = start;
        int direction;
        while (row != 0 || col != end) {
            if (col == end) {
                direction = rng.Next(2)*2 - 1;
            } else {
                direction = (end-col)/System.Math.Abs(end-col);
            }
            if (row == 0) {
                col += direction;
            } else if (rng.Next(2) == 0) {
                row -= 1;
            } else if (col + direction >= 0 && col + direction < grid.GetLength(1)) {
                col += direction;
            } else {
                row -= 1;
            }
            if (grid[row, col] == 'W') {
                grid[row, col] = 'R';
            }
        }
        for (int i = 0; i < rng.Next(7) + 2; i++) {
            row = rng.Next(4);
            col = rng.Next(4);
            if (grid[row, col] == 'W' && AdjacentRoom(row, col, grid)) {
                grid[row, col] = 'R';
            }
        }
        int[,] neighbors = Neighbors(grid);
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (grid[i, j] == 'R' && neighbors[i, j] == 1) {
                    grid[i, j] = 'B';
                }
            }
        }
        return Remap(grid, rng);
    }

    private bool AdjacentRoom(int row, int col, char[,] grid) {
        bool n, s, e, w;
        if (row == 0) {
            n = false;
        } else {
            n = (grid[row - 1, col] != 'W');
        }
        if (row == grid.GetLength(0) - 1) {
            s = false;
        } else {
            s = (grid[row + 1, col] != 'W');
        }
        if (col == 0) {
            w = false;
        } else {
            w = (grid[row, col - 1] != 'W');
        }
        if (col == grid.GetLength(1) - 1) {
            e = false;
        } else {
            e = (grid[row, col + 1] != 'W');
        }
        return n || s || e || w;
    }

    public void Clean() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        foreach (Transform t in minimap.transform) {
            Destroy(t.gameObject);
        }
        System.Random rng = new System.Random();
        char[,] grid = GenerateLayout(rng);
        GetComponent<WallCreator>().Render(grid, rng);
    }

    private char[,] Remap(char[,] grid, System.Random rng) {
        char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1)];
        switch (rng.Next(6)) {
            case 0:
                for (int i = 0; i < grid.GetLength(0); i++) {
                    for (int j = 0; j < grid.GetLength(1); j++) {
                        newGrid[i, j] = grid[grid.GetLength(0)-i-1, grid.GetLength(1)-j-1];
                    }
                }
                break;
            case 1:
                for (int i = 0; i < grid.GetLength(0); i++) {
                    for (int j = 0; j < grid.GetLength(1); j++) {
                        newGrid[i, j] = grid[j, i];
                    }
                }
                break;
            case 2:
                for (int i = 0; i < grid.GetLength(0); i++) {
                    for (int j = 0; j < grid.GetLength(1); j++) {
                        newGrid[i, j] = grid[i, grid.GetLength(1)-j-1];
                    }
                }
                break;
            case 3:
                for (int i = 0; i < grid.GetLength(0); i++) {
                    for (int j = 0; j < grid.GetLength(1); j++) {
                        newGrid[i, j] = grid[grid.GetLength(0)-i-1, j];
                    }
                }
                break;
            case 4:
                for (int i = 0; i < grid.GetLength(0); i++) {
                    for (int j = 0; j < grid.GetLength(1); j++) {
                        newGrid[i, j] = grid[grid.GetLength(0) - j - 1, grid.GetLength(1) - i - 1];
                    }
                }
                break;
            default:
                return grid;
                break;
        }
        return newGrid;
    }

    public int[,] Neighbors(char[,] grid) {
        int[,] neighbors = new int[grid.GetLength(0),grid.GetLength(1)];
        for (int row = 0; row < grid.GetLength(0); row++) {
            for (int col = 0; col < grid.GetLength(1); col++) {
                if (grid[row, col] == 'W') {
                    neighbors[row, col] = 0;
                } else {
                    int n = 0;
                    if (row > 0 && grid[row-1, col] != 'W') {
                        n++;
                    }
                    if (row < grid.GetLength(0)-1 && grid[row+1, col] != 'W') {
                        n++;
                    }
                    if (col > 0 && grid[row, col-1] != 'W') {
                        n++;
                    }
                    if (col < grid.GetLength(1)-1 && grid[row, col+1] != 'W') {
                        n++;
                    }
                    neighbors[row, col] = n;
                }
            }
        }
        return neighbors;
    }
}

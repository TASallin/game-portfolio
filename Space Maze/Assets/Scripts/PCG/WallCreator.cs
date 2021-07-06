using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{

  public GameObject roomHolder;
  public GameObject wall;
  public GameObject doorWall;
  public GameObject floor;
  public GameObject cameraMover;
  public GameObject player;
  public GameObject loadingScreen;
  public GameObject minimap;
  public GameObject mapTile;
  public GameObject roomObject;
  public Music jukebox;
  public List<Color> wallColor;
  public List<Color> floorColor;
  public Material tutoritexture;

  public int roomLength = 50;
  public int startX = 0;
  public int startZ = 150;
  static Quaternion rotationNorth = Quaternion.Euler(0, 90, -90);
  static Quaternion rotationSouth = Quaternion.Euler(0, 90, -90);
  static Quaternion rotationEast = Quaternion.Euler(0, 0, 90);
  static Quaternion rotationWest = Quaternion.Euler(0, 0, -90);

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Render(char[,] grid, System.Random rng) {
    bool nd, sd, ed, wd;
    GameObject room;
    GetComponent<RoomPicker>().Shuffle(rng);
    for (int i = 0; i < grid.GetLength(0); i++) {
      for (int j = 0; j < grid.GetLength(1); j++) {
        if (grid[i, j] != 'W') {
          if (i == 0) {
            nd = false;
          } else {
            nd = grid[i - 1, j] != 'W';
          }
          if (i == grid.GetLength(0) - 1) {
            sd = false;
          } else {
            sd = grid[i + 1, j] != 'W';
          }
          if (j == 0) {
            wd = false;
          } else {
            wd = grid[i, j - 1] != 'W';
          }
          if (j == grid.GetLength(1) - 1) {
            ed = false;
          } else {
            ed = grid[i, j + 1] != 'W';
          }
          room = Instantiate(roomObject, new Vector3(0, 0, 0), Quaternion.identity, transform);
          room.GetComponent<Room>().x = j;
          room.GetComponent<Room>().z = i;
          if (grid[i, j] == 'E') {
            room.GetComponent<Room>().jukebox = jukebox;
          }
          GameObject wallObject;
          if (nd) {
            wallObject = Instantiate(doorWall, new Vector3(startX + j*roomLength, 5, startZ - i*roomLength + roomLength/2), rotationNorth, room.transform);
          } else {
            wallObject = Instantiate(wall, new Vector3(startX + j*roomLength, 5, startZ - i*roomLength + roomLength/2), rotationNorth, room.transform);
          }
          foreach (Transform t in wallObject.transform) {
            t.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());
            t.GetComponent<MeshRenderer>().material.color = wallColor[GameState.GetGame().level-1];
          }
          if (sd) {
            wallObject = Instantiate(doorWall, new Vector3(startX + j*roomLength, 5, startZ - i*roomLength - roomLength/2), rotationSouth, room.transform);
          } else {
            wallObject = Instantiate(wall, new Vector3(startX + j*roomLength, 5, startZ - i*roomLength - roomLength/2), rotationSouth, room.transform);
          }
          foreach (Transform t in wallObject.transform) {
            t.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());
            t.GetComponent<MeshRenderer>().material.color = wallColor[GameState.GetGame().level-1];
          }
          if (ed) {
            wallObject = Instantiate(doorWall, new Vector3(startX + j*roomLength + roomLength/2, 5, startZ - i*roomLength), rotationEast, room.transform);
          } else { 
            wallObject = Instantiate(wall, new Vector3(startX + j*roomLength + roomLength/2, 5, startZ - i*roomLength), rotationEast, room.transform);
          }
          foreach (Transform t in wallObject.transform) {
            t.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());
            t.GetComponent<MeshRenderer>().material.color = wallColor[GameState.GetGame().level-1];
          }
          if (wd) {
            wallObject = Instantiate(doorWall, new Vector3(startX + j*roomLength - roomLength/2, 5, startZ - i*roomLength), rotationWest, room.transform);
          } else {
            wallObject = Instantiate(wall, new Vector3(startX + j*roomLength - roomLength/2, 5, startZ - i*roomLength), rotationWest, room.transform);
          }
          foreach (Transform t in wallObject.transform) {
            t.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());
            t.GetComponent<MeshRenderer>().material.color = wallColor[GameState.GetGame().level-1];
          }
          wallObject = Instantiate(floor, new Vector3(startX + j*roomLength, 0, startZ - i*roomLength), Quaternion.identity, room.transform);
          if (GameState.GetGame().level == 1 && grid[i, j] == 'S') {
            wallObject.GetComponent<MeshRenderer>().material = tutoritexture;
          } else {
            wallObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());
          }
          wallObject.GetComponent<MeshRenderer>().material.color = floorColor[GameState.GetGame().level-1];
          Instantiate(cameraMover, new Vector3(startX + j*roomLength, 0, startZ - i*roomLength), Quaternion.identity, room.transform);
          GetComponent<RoomPicker>().ChooseRoom(startX + j*roomLength, startZ - i*roomLength, grid[i, j], room.transform, rng);

          GameObject m = Instantiate(mapTile, minimap.transform, false);
          m.transform.localPosition = new Vector3(-75 + j*20, -15 - i*20);
          room.GetComponent<Room>().mapSquare = m;
          m.SetActive(false);

          if (grid[i, j] == 'S')
          {
            player.transform.position = new Vector3(startX + j * roomLength, 1, startZ - i * roomLength);
            Camera.main.transform.position = new Vector3(startX + j * roomLength, 30, startZ - i * roomLength - 28);
            Camera.main.GetComponent<CameraController>().SetTarget(startX + j * roomLength, startZ - i * roomLength - 25);
            room.GetComponentInChildren<RoomCollider>().SpawnHere();
          }
        }
      }
    }
    loadingScreen.SetActive(false);
  }
}

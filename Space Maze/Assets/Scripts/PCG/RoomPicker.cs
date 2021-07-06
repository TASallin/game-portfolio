using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPicker : MonoBehaviour
{

    int[] order;
    int index;
    static int COUNT = 25;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shuffle(System.Random rng) {
        order = new int[COUNT];
        for (int i = 0; i < COUNT; i++) {
            order[i] = i;
        }
        for (int i = 0; i < COUNT; i ++) {
            int ind = rng.Next(COUNT-i);
            int temp = order[ind];
            order[ind] = order[COUNT - i - 1];
            order[COUNT - i - 1] = temp;
        }
        index = 0;

        GetComponent<RoomsT>().SetRNG();
        GetComponent<RoomsJ>().SetRNG();
        GetComponent<RoomsL>().SetRNG();
        GetComponent<RoomsP>().SetRNG();
    }

    public void ChooseRoom(int x, int z, char type, Transform parent, System.Random rng) {
        if (type == 'S') {
            gameObject.GetComponent<RoomsT>().ButtonTutorial(x, z, parent);
        } else if (type == 'E') {
            if (rng.Next(2) == 0) {
                gameObject.GetComponent<RoomsT>().Exit(x, z, parent);
            } else {
                gameObject.GetComponent<RoomsT>().Exit2(x, z, parent);
            }
        } else if (type == 'B') {
            switch (rng.Next(5)) {
                case 0:
                    gameObject.GetComponent<RoomsT>().Item(x, z, parent);
                    break;
                case 1:
                    gameObject.GetComponent<RoomsT>().Trollzer(x, z, parent);
                    break;
                case 2:
                    gameObject.GetComponent<RoomsT>().MagicMissile(x, z, parent);
                    break;
                case 3:
                    gameObject.GetComponent<RoomsT>().SpikedShield(x, z, parent);
                    break;
                default:
                    gameObject.GetComponent<RoomsT>().GuardedHealth(x, z, parent);
                    break;
            }
        } else {
            switch (order[index]) {
                case 0:
                    gameObject.GetComponent<RoomsT>().DefaultRoom(x, z, parent);
                    break;
                case 1:
                    gameObject.GetComponent<RoomsT>().PlatformRoom(x, z, parent);
                    break;
                case 2:
                    gameObject.GetComponent<RoomsT>().RobotRoom(x, z, parent);
                    break;
                case 3:
                    gameObject.GetComponent<RoomsT>().PathRoom(x, z, parent);
                    break;
                case 4:
                    gameObject.GetComponent<RoomsT>().TurretRoom(x, z, parent);
                    break;
                case 5:
                    gameObject.GetComponent<RoomsT>().Samurai(x, z, parent);
                    break;
                case 6:
                    gameObject.GetComponent<RoomsT>().Spreadshot(x, z, parent);
                    break;
                case 7:
                    gameObject.GetComponent<RoomsT>().ZigZag(x, z, parent);
                    break;
                case 8:
                    gameObject.GetComponent<RoomsT>().Pedestal(x, z, parent);
                    break;
                case 9:
                    gameObject.GetComponent<RoomsT>().Castle(x, z, parent);
                    break;
                case 10:
                    gameObject.GetComponent<RoomsT>().Traffic(x, z, parent);
                    break;
                case 11:
                    gameObject.GetComponent<RoomsT>().BigOne(x, z, parent);
                    break;
                case 12:
                    gameObject.GetComponent<RoomsT>().BoxDrop(x, z, parent);
                    break;
                case 13:
                    gameObject.GetComponent<RoomsT>().TrenchWar(x, z, parent);
                    break;
                case 14:
                    gameObject.GetComponent<RoomsT>().WaveAttack(x, z, parent);
                    break;
                case 15:
                    gameObject.GetComponent<RoomsJ>().PortalRoom(x, z, parent);
                    break;
                case 16:
                    gameObject.GetComponent<RoomsJ>().MovingPlatformsRoom(x, z, parent);
                    break;
                case 17:
                    gameObject.GetComponent<RoomsJ>().ChoiceRoom(x, z, parent);
                    break;
                case 18:
                    gameObject.GetComponent<RoomsL>().NewPortal(x, z, parent);
                    break;
                case 19:
                    gameObject.GetComponent<RoomsL>().PortalsAndLasers(x, z, parent);
                    break;
                case 20:
                    gameObject.GetComponent<RoomsL>().PlatformsWithPortals(x, z, parent);
                    break;
                case 21:
                    gameObject.GetComponent<RoomsP>().SpikePlatformRoom(x, z, parent);
                    break;
                case 22:
                    gameObject.GetComponent<RoomsP>().HidenSeek(x, z, parent);
                    break;
                case 23:
                    gameObject.GetComponent<RoomsP>().HighHorse(x, z, parent);
                    break;
                default:
                    gameObject.GetComponent<RoomsT>().Lasers(x, z, parent);
                    break;
            }
            index ++;
        }
    }
}
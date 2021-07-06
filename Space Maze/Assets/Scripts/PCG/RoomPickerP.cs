using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPickerP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseRoom(int x, int z, char type, Transform parent, System.Random rng)
    {
        gameObject.GetComponent<RoomsP>().HighHorse(x, z, parent);
/**
        if (type == 'S')
        {
            gameObject.GetComponent<RoomsP>().ButtonTutorial(x, z, parent);
        }
        else if (type == 'E')
        {
            if (rng.Next(2) == 0)
            {
                gameObject.GetComponent<RoomsP>().Exit(x, z, parent);
            }
            else
            {
                gameObject.GetComponent<RoomsP>().Exit2(x, z, parent);
            }
        }
        else if (type == 'B')
        {
            switch (rng.Next(5))
            {
                case 0:
                    gameObject.GetComponent<RoomsP>().Item(x, z, parent);
                    break;
                case 1:
                    gameObject.GetComponent<RoomsP>().Trollzer(x, z, parent);
                    break;
                case 2:
                    gameObject.GetComponent<RoomsP>().MagicMissile(x, z, parent);
                    break;
                case 3:
                    gameObject.GetComponent<RoomsP>().SpikedShield(x, z, parent);
                    break;
                default:
                    gameObject.GetComponent<RoomsP>().GuardedHealth(x, z, parent);
                    break;
            }
        }
        else
        {
            switch (rng.Next(16))
            {
                case 0:
                    gameObject.GetComponent<RoomsP>().DefaultRoom(x, z, parent);
                    break;
                case 1:
                    gameObject.GetComponent<RoomsP>().SpikePlatformRoom(x, z, parent);
                    break;
            }
        }**/
    }
    
}
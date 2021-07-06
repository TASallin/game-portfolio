using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsP : MonoBehaviour
{
    public GameObject healthCollectible;
    public GameObject shieldCollectible;
    public GameObject speedCollectible;
    public GameObject bigstairs;
    public GameObject r4;
    public GameObject highplatform;
    public GameObject higherplatform;
    public GameObject longspikes;
    public GameObject longerspikes;
    public GameObject mine;
    public GameObject spikes;
    public GameObject button;
    public GameObject fakebutton;
    public GameObject terminal;
    public GameObject robot;
    public GameObject pathNetwork;
    public GameObject pathNode;
    public GameObject pathMine;
    public GameObject laser;
    public GameObject laserButton;
    public GameObject stairs;
    public GameObject pillar;
    public GameObject box;
    public GameObject walkway;
    public GameObject block;
    public GameObject ladder;
    public GameObject dataDisc;
    public GameObject laserBlaster;
    public GameObject pathBoss;
    public GameObject extraguard;
    public GameObject extraguard2;
    public GameObject smallguard;
    public GameObject smallguard2;
    public GameObject regularguard;
    public GameObject xxlguard;
    public FloorGenerator roomHolder;
    public GameObject loadingScreen;
    private System.Random rng;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        level = GameState.GetGame().level;
        if (rng == null) {
            rng = new System.Random();
        }
    }

    void Update()
    {

    }

    public void SetRNG() {
        rng = new System.Random();
    }

    public void SpikePlatformRoom(int x, int z, Transform parent)
    {
        Instantiate(healthCollectible, new Vector3(x + 17, 1, z + 12), Quaternion.identity, parent);
        Instantiate(bigstairs, new Vector3(x + 17, 7.39f, z - 20f), Quaternion.identity, parent);
        Instantiate(highplatform, new Vector3(x + 9.45f, -6.47f, z - 14), Quaternion.Euler(0,90,0), parent);
        Instantiate(highplatform, new Vector3(x - 19.2f, -6.47f, z - 4.66f), Quaternion.identity, parent);
        Instantiate(highplatform, new Vector3(x - 19.2f, -6.47f, z + 15.34f), Quaternion.identity, parent);
        Instantiate(longerspikes, new Vector3(x - 2.1f, -11.2f, z - 13.85f), Quaternion.identity, parent);
        Instantiate(longerspikes, new Vector3(x - 2.1f, -11.2f, z + 10), Quaternion.identity, parent);
        //Instantiate(longerspikes, new Vector3(x , -11.2f, z - 31.5f), Quaternion.Euler(0,90,0), parent);
        Instantiate(longerspikes, new Vector3(x + 3.89f, -11.2f, z - 5), Quaternion.identity, parent);
        Instantiate(longspikes, new Vector3(x + 2.3f, -11.58f, z + 3.2f), Quaternion.identity, parent);
        Instantiate(longspikes, new Vector3(x + 2.3f, -11.58f, z + 11.1f), Quaternion.identity, parent);
        Instantiate(longspikes, new Vector3(x - 21.8f, -11.58f, z + 10f), Quaternion.Euler(0,90,0), parent);
        Instantiate(button, new Vector3(x + 3, 0, z - 3.5f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 13.28f, 6.1f, z + 13), Quaternion.identity, parent);//the fake
        Instantiate(mine, new Vector3(x - 15, 6.1f, z + 12), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 15, 6.1f, z + 6), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 15, 6.1f, z), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 15, 6.1f, z - 6), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 15, 6.1f, z - 12), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 15, 6.1f, z - 18), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 10, 6.1f, z + 9), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 11, 6.1f, z + 3), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 10, 6.1f, z - 3), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 11, 6.1f, z - 9), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 10, 6.1f, z - 15), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 8, 6.1f, z - 23), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 4, 6.1f, z - 22), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x, 6.1f, z - 23), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x + 4, 6.1f, z - 22), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x + 8, 6.1f, z - 23), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 6, 6.1f, z - 18), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x - 2, 6.1f, z - 18), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x + 2, 6.1f, z - 18), Quaternion.identity, parent);
        //Instantiate(mine, new Vector3(x + 6, 6.1f, z - 18), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 1, z), Quaternion.identity, parent);
        }

        if (level >= 2)
        {
            Instantiate(mine, new Vector3(x + 9, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 6, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 3, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 0, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 3, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 6, 0, z - 22), Quaternion.identity, parent);
        }
        if (level >= 5)
        {
            Instantiate(shieldCollectible, new Vector3(0,0,0), Quaternion.identity , parent);
        }
    }

    public void HidenSeek(int x, int z, Transform parent)
    {
        Instantiate(r4, new Vector3(x + 10, 14.1f, z - 19.8f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 0, 0, z), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x + 8.19f, 0, z - 19.69f), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x - 15.48f, 7.19f, z + 7.57f), Quaternion.Euler(90, 0, 0), parent);
        Instantiate(button, new Vector3(x - 4.88f, 9.04f, z - 14.2f), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x + 10, 5.45f, z - 8.93f), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x + 11, 0, z), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x + 4, 0, z + 17), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 18, 0, z - 10), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x - 15, 0, z - 13), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x - 19, 0, z + 20), Quaternion.identity, parent);
        //Instantiate(button, new Vector3(x - 15.41f, 15.92f, z + 8.92f), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x - 5, 10, z - 14), Quaternion.identity, parent);
        }
        if (level >= 4)
        {
            Instantiate(mine, new Vector3(x - 9, 0, z - 6), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 20, 0, z - 3), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 1, 0, z - 22), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 9, 0, z + 21.5f), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 8.5f, 0, z - 9), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 12, 0, 0), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 14, 0, z - 13), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 19, 0, z + 18), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(0, 0, 0), Quaternion.identity, parent);
        }
        //if (level >= 5)
        //{
            //Instantiate(speedCollectible, new Vector3(0, 0, 0), Quaternion.identity, parent);
        //}
    }

    public void HighHorse(int x, int z, Transform parent) {

        Instantiate(highplatform, new Vector3(x - 19, -6.5f, z + 3), Quaternion.identity, parent);
        Instantiate(highplatform, new Vector3(x + 9.95f, -6.5f, z - 6.36f), Quaternion.Euler(0, 90, 0), parent);
        Instantiate(highplatform, new Vector3(x + 7.5f, -6.5f, z - 3.68f), Quaternion.identity, parent);
        //Instantiate(higherplatform, new Vector3(x + 9, -11.25f, z - 22), Quaternion.Euler(0, -180, 0), parent);
        //Instantiate(higherplatform, new Vector3(x - 13, -11.25f, z + 5), Quaternion.Euler(0, -180, 0), parent);
        //Instantiate(higherplatform, new Vector3(x + 9, -11.25f, z - 4), Quaternion.Euler(0, -180, 0), parent);
        //Instantiate(higherplatform, new Vector3(x + 19, -11.25f, z + 19.5f), Quaternion.Euler(0, 90, 0), parent);
        //Instantiate(higherplatform, new Vector3(x + 1, -11.25f, z + 19.5f), Quaternion.Euler(0, 90, 0), parent);
        Instantiate(button, new Vector3(x + 13.8f, 6, z - 5.6f), Quaternion.identity, parent);
        //Instantiate(button, new Vector3(x + 4.8f, 10.5f, z - 20), Quaternion.identity, parent);
        Instantiate(fakebutton, new Vector3(x - 12.8f, 6, z - 12.5f), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x - 17.5f, 10.5f, z + 6.5f), Quaternion.identity, parent);
        //Instantiate(fakebutton, new Vector3(x + 17, 10.5f, z + 15), Quaternion.identity, parent);
        //GameObject r = Instantiate(robot, new Vector3(x - 17.8f, 10.65f, z + 20.7f), Quaternion.identity, parent);
        //GameObject t = Instantiate(terminal, new Vector3(x + 24, 1.5f, z - 10), Quaternion.identity, parent);
        //t.GetComponent<TerminalManager>().bot = r.GetComponent<HackedRobotController>();
        GameObject r2 = Instantiate(robot, new Vector3(x + 0, 6, z - 13), Quaternion.identity, parent);
        GameObject t2 = Instantiate(terminal, new Vector3(x + 24, 1.5f, z + 10), Quaternion.identity, parent);
        t2.GetComponent<TerminalManager>().bot = r2.GetComponent<HackedRobotController>();
        //Instantiate(extraguard, new Vector3(x + 2.0515f, 10.65f, z + 17.4f), Quaternion.Euler(90, 0, 0), parent);
        Instantiate(extraguard2, new Vector3(x - 2.544f, 6.2f, z - 16.13f), Quaternion.Euler(90, 0, 0), parent);
        //Instantiate(smallguard, new Vector3(x - 17.23f, 10.71f, z + 4.26f), Quaternion.Euler(0, 90, 90), parent);
        //Instantiate(smallguard, new Vector3(x + 19.75f, 10.71f, z + 16.36f), Quaternion.Euler(0, 0, 90), parent);
        //Instantiate(smallguard, new Vector3(x + 4.75f, 10.71f, z - 22.73f), Quaternion.Euler(0, 90, 90), parent);
        Instantiate(smallguard2, new Vector3(x - 12.5f, 6.2f, z + 3.85f), Quaternion.Euler(0, 90, 90), parent);
        Instantiate(regularguard, new Vector3(x - 15.8f, 6.2f, z - 6.1f), Quaternion.Euler(90, 90, 0), parent);
        GameObject r = Instantiate(regularguard, new Vector3(x - 9.2f, 6.2f, z - 2.85f), Quaternion.Euler(90, 90, 0), parent);
        r.transform.localScale = new Vector3(13.5f, 0.05f, 0.5f);
        Instantiate(regularguard, new Vector3(x + .7f , 6.2f, z - 9.585f), Quaternion.Euler(90, 0, 0), parent);
        Instantiate(regularguard, new Vector3(x + 17.275f , 6.2f, z - 12.79f), Quaternion.Euler(90, 90, 0), parent);
        Instantiate(smallguard2, new Vector3(x + 14f, 6.2f, z - 2.95f), Quaternion.Euler(0, 90, 90), parent);
        Instantiate(smallguard2, new Vector3(x + 14f, 6.2f, z - 22.65f), Quaternion.Euler(0, 90, 90), parent);
        Instantiate(smallguard2, new Vector3(x + 10.75f, 6.2f, z - 19.45f), Quaternion.Euler(0, 0, 90), parent);
        Instantiate(smallguard2, new Vector3(x + 10.75f, 6.2f, z - 6.25f), Quaternion.Euler(0, 0, 90), parent);
        //Instantiate(xxlguard, new Vector3(x + 6.894f, 10.65f, z - 4.81f), Quaternion.Euler(90, 90, 0), parent);
        //Instantiate(xxlguard, new Vector3(x + 2.6625f, 10.65f, z - 4.81f), Quaternion.Euler(90, 90, 0), parent);

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x - 18, 0, z + 21), Quaternion.identity, parent);
        }

        if (level >= 2)
        {
            Instantiate(mine, new Vector3(x - 8.5f, 0, z - 9), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 12, 0, 0), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 14, 0, z - 13), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x + 19, 0, z + 18), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(0, 0, 0), Quaternion.identity, parent);
        }
        if (level >= 5)
        {
            Instantiate(healthCollectible, new Vector3(x - 6.2f, -1.4f, z + 23.3f), Quaternion.identity, parent);
        }
    }
}



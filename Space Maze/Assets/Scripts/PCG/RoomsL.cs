using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsL : MonoBehaviour
{
    public GameObject startPortal;
    public GameObject endPortal;
    public GameObject buttonPlatform;
    public GameObject pathNetwork;
    public GameObject pathNode;
    public GameObject platformAgent;
    public GameObject turret;
    public GameObject spikes;
    public GameObject stairs;
    public GameObject laser;
    public GameObject pillar;
    public GameObject block;
    public GameObject platform;
    public GameObject button;
    public GameObject healthCollectible;
    public GameObject shieldCollectible;
    public GameObject mine;
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

    // Update is called once per frame
    void Update()
    {


    }

    public void SetRNG() {
        rng = new System.Random();
    }

    public void NewPortal(int x, int z, Transform parent)
    {
        level = GameState.GetGame().level;
        GameObject sPortal = Instantiate(startPortal, new Vector3(x + 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject ePortal = Instantiate(endPortal, new Vector3(x + 17, 12f, z - 10), Quaternion.identity, parent);

        sPortal.GetComponent<PortalHandler>().portalDestination = ePortal.transform.position;
        Debug.Log("portal destination " + sPortal.GetComponent<PortalHandler>().portalDestination);

        GameObject sPortal2 = Instantiate(startPortal, new Vector3(x - 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject ePortal2 = Instantiate(endPortal, new Vector3(x - 3, 12f, z - 5), Quaternion.identity, parent);

        sPortal2.GetComponent<PortalHandler>().portalDestination = ePortal2.transform.position;
        Debug.Log("portal destination " + sPortal.GetComponent<PortalHandler>().portalDestination);

        GameObject sPortal3 = Instantiate(startPortal, new Vector3(x + 3, 2f, z - 10), Quaternion.identity, parent);
        GameObject ePortal3 = Instantiate(endPortal, new Vector3(x - 15, 12f, z - 15), Quaternion.identity, parent);

        sPortal3.GetComponent<PortalHandler>().portalDestination = ePortal3.transform.position;
        Debug.Log("portal destination " + sPortal.GetComponent<PortalHandler>().portalDestination);

        GameObject pillar1 = Instantiate(pillar, new Vector3(x + 17, 2f, z - 10), Quaternion.identity, parent);
        GameObject button1 = Instantiate(button, new Vector3(x + 17, 7f, z - 10), Quaternion.identity, parent);

        GameObject pillar2 = Instantiate(pillar, new Vector3(x - 17, 2f, z - 10), Quaternion.identity, parent);
        GameObject button2 = Instantiate(button, new Vector3(x - 17, 7f, z - 10), Quaternion.identity, parent);

        Instantiate(spikes, new Vector3(x - 17, 0.5f, z - 17), Quaternion.identity, parent);

        GameObject mine1 = Instantiate(mine, new Vector3(x - 1, 0.5f, z - 1), Quaternion.identity, parent);
        GameObject mine2 = Instantiate(mine, new Vector3(x - 3, 0.5f, z), Quaternion.identity, parent);
        GameObject mine3 = Instantiate(mine, new Vector3(x, 0.5f, z - 5), Quaternion.identity, parent);

        level = GameState.GetGame().level;

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x - 15, 8, z - 15), Quaternion.identity, parent);
        }

        if (level >= 2) {
            GameObject turr = Instantiate(turret, new Vector3(x - 20, 0, z - 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z), Quaternion.identity, parent);
                        Instantiate(spikes, new Vector3(x, 0.5f, z + 10), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 5, 0.5f, z - 10), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 10, 0.5f, z - 3), Quaternion.identity, parent);
        }

        if (level >= 3) {
            GameObject turr2 = Instantiate(turret, new Vector3(x + 20, 0, z - 20), Quaternion.identity, parent);
            turr2.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            GameObject mine4 = Instantiate(mine, new Vector3(x + 3, 0.5f, z - 1), Quaternion.identity, parent);
            GameObject mine5 = Instantiate(mine, new Vector3(x - 3, 0.5f, z + 3), Quaternion.identity, parent);
            GameObject mine6 = Instantiate(mine, new Vector3(x - 7, 0.5f, z - 5), Quaternion.identity, parent);

        }

        if (level >= 4) {
            GameObject turr3 = Instantiate(turret, new Vector3(x, 0, z + 20), Quaternion.identity, parent);
            turr3.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            GameObject mine7 = Instantiate(mine, new Vector3(x, 0.5f, z + 5), Quaternion.identity, parent);
            GameObject mine8 = Instantiate(mine, new Vector3(x + 10, 0.5f, z + 3), Quaternion.identity, parent);
            GameObject mine9 = Instantiate(mine, new Vector3(x - 10, 0.5f, z - 7), Quaternion.identity, parent);

        }

    }

    public void PortalsAndLasers(int x, int z, Transform parent)
    {
        level = GameState.GetGame().level;
        GameObject shieldC = Instantiate(shieldCollectible, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject topLeftS = Instantiate(startPortal, new Vector3(x - 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject finalPortal = Instantiate(endPortal, new Vector3(x + 20, 9.5f, z - 15), Quaternion.identity, parent);
        topLeftS.GetComponent<PortalHandler>().portalDestination = finalPortal.transform.position;

        GameObject topRightS = Instantiate(startPortal, new Vector3(x + 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject topLeftP = Instantiate(endPortal, new Vector3(x - 20, 6f, z + 5), Quaternion.identity, parent);
        topRightS.GetComponent<PortalHandler>().portalDestination = topLeftP.transform.position;

        GameObject sPortal3 = Instantiate(startPortal, new Vector3(x + 3, 2f, z - 10), Quaternion.identity, parent);
        GameObject topRightP = Instantiate(endPortal, new Vector3(x + 20, 6f, z + 12), Quaternion.identity, parent);
        sPortal3.GetComponent<PortalHandler>().portalDestination = topRightP.transform.position;

        GameObject sPortal4 = Instantiate(startPortal, new Vector3(x - 3, 2f, z - 11), Quaternion.identity, parent);
        GameObject ePortal4 = Instantiate(endPortal, new Vector3(x + 3, 12f, z + 3), Quaternion.identity, parent);
        sPortal4.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

        GameObject topLeftS2 = Instantiate(startPortal, new Vector3(x - 19, 2f, z - 17), Quaternion.identity, parent);
        topLeftS2.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

        GameObject ePortalbotLeft = Instantiate(endPortal, new Vector3(x - 20, 6f, z - 5), Quaternion.identity, parent);

        GameObject topRightS2 = Instantiate(startPortal, new Vector3(x + 19, 2f, z - 5), Quaternion.identity, parent);
        topRightS2.GetComponent<PortalHandler>().portalDestination = ePortalbotLeft.transform.position;

        GameObject sPortal7 = Instantiate(startPortal, new Vector3(x - 8, 2f, z - 10), Quaternion.identity, parent);
        sPortal7.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

        GameObject sPortal8 = Instantiate(startPortal, new Vector3(x + 5, 2f, z + 3), Quaternion.identity, parent);
        sPortal8.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;


        GameObject pillar1 = Instantiate(pillar, new Vector3(x + 20, 2f, z - 15), Quaternion.identity, parent);
        GameObject button1 = Instantiate(button, new Vector3(x + 20, 7f, z - 15), Quaternion.identity, parent);
        GameObject button2 = Instantiate(button, new Vector3(x - 21, 0.5f, z - 15), Quaternion.identity, parent);

        GameObject l = Instantiate(laser, new Vector3(x - 15.65f, 4, z), Quaternion.Euler(0, 90, 0), parent);
        l.transform.localScale = new Vector3(49f, 7, 0.3f);
        l = Instantiate(laser, new Vector3(x - 20, 4, z), Quaternion.identity, parent);
        l.transform.localScale = new Vector3(9f, 7, 0.3f);


        l = Instantiate(laser, new Vector3(x + 15.65f, 4, z), Quaternion.Euler(0, 90, 0), parent);
        l.transform.localScale = new Vector3(49f, 7, 0.3f);
        l = Instantiate(laser, new Vector3(x + 20, 4, z), Quaternion.identity, parent);
        l.transform.localScale = new Vector3(9f, 7, 0.3f);

        level = GameState.GetGame().level;

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x, 1f, z), Quaternion.identity, parent);
        }

        if (level >= 2) {
            sPortal3.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;
            sPortal8.GetComponent<PortalHandler>().portalDestination = topRightP.transform.position;
            GameObject sPortal9 = Instantiate(startPortal, new Vector3(x + 8, 2f, z - 12), Quaternion.identity, parent);
            sPortal9.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

            GameObject sPortal10 = Instantiate(startPortal, new Vector3(x - 10, 2f, z - 15), Quaternion.identity, parent);
            sPortal10.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

        }

        if (level >= 3) {
            topLeftS.GetComponent<PortalHandler>().portalDestination = topRightP.transform.position;
            topRightS.GetComponent<PortalHandler>().portalDestination = finalPortal.transform.position;
            sPortal8.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;
            sPortal7.GetComponent<PortalHandler>().portalDestination = topLeftP.transform.position;
            GameObject sPortal11 = Instantiate(startPortal, new Vector3(x + 15, 2f, z - 20), Quaternion.identity, parent);
            sPortal11.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

            GameObject sPortal12 = Instantiate(startPortal, new Vector3(x, 2f, z - 20), Quaternion.identity, parent);
            sPortal12.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

        }

        if (level >= 4) {
            topLeftS.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;
            topRightS.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

            sPortal7.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;
            GameObject sPortal13 = Instantiate(startPortal, new Vector3(x - 15, 2f, z - 20), Quaternion.identity, parent);
            sPortal13.GetComponent<PortalHandler>().portalDestination = finalPortal.transform.position;

            GameObject sPortal14 = Instantiate(startPortal, new Vector3(x - 15, 2f, z), Quaternion.identity, parent);
            sPortal14.GetComponent<PortalHandler>().portalDestination = ePortal4.transform.position;

            sPortal14.GetComponent<PortalHandler>().portalDestination = topRightP.transform.position;
            //topRightS2.GetComponent<PortalHandler>().portalDestination = topLeftP.transform.position;

        }
    }

    public void PlatformsWithPortals(int x, int z, Transform parent)
    {

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x + 14, 7f, z - 10), Quaternion.identity, parent);
        }

        level = GameState.GetGame().level;
        GameObject sPortal = Instantiate(startPortal, new Vector3(x + 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject ePortal = Instantiate(endPortal, new Vector3(x + 14, 11f, z - 10), Quaternion.identity, parent);
        sPortal.GetComponent<PortalHandler>().portalDestination = ePortal.transform.position;

        GameObject sPortal2 = Instantiate(startPortal, new Vector3(x - 20, 2f, z + 20), Quaternion.identity, parent);
        GameObject ePortal2 = Instantiate(endPortal, new Vector3(x - 3, 12f, z - 5), Quaternion.identity, parent);
        sPortal2.GetComponent<PortalHandler>().portalDestination = ePortal2.transform.position;

        GameObject pillar1 = Instantiate(pillar, new Vector3(x + 10, 2f, z - 10), Quaternion.identity, parent);
        GameObject button1 = Instantiate(button, new Vector3(x + 10, 7f, z - 10), Quaternion.identity, parent);

        GameObject pillar2 = Instantiate(pillar, new Vector3(x - 17, 2f, z + 10), Quaternion.identity, parent);
        GameObject button2 = Instantiate(button, new Vector3(x - 17, 7f, z + 10), Quaternion.identity, parent);

        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject p2 = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);

        GameObject n1 = Instantiate(pathNode, new Vector3(x + 5, 6f, z + 10), Quaternion.identity, p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x + 5, 6f, z - 10), Quaternion.Euler(0, 90, 0), p.transform);
        GameObject n3 = Instantiate(pathNode, new Vector3(x - 12, 6f, z + 10), Quaternion.identity, p.transform);
        GameObject n4 = Instantiate(pathNode, new Vector3(x - 12, 6f, z - 10), Quaternion.Euler(0, 90, 0), p.transform);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors();
        net.CalculateVertices();

        PathNetwork net2 = p2.GetComponent<PathNetwork>();
        net2.AddEdge(n3.GetComponent<PathNode>(), n4.GetComponent<PathNode>());
        net2.SetNeighbors();
        net2.CalculateVertices();

        n1.GetComponent<PathNode>().DisableRender();
        n2.GetComponent<PathNode>().DisableRender();
        n3.GetComponent<PathNode>().DisableRender();
        n4.GetComponent<PathNode>().DisableRender();

        GameObject platformObject = Instantiate(platform, n1.transform.position, Quaternion.identity, parent);
        PathAgent agent = platformObject.GetComponent<PathAgent>();
        agent.speed = 6;
        agent.network = net;
        agent.location = n1.GetComponent<PathNode>();

        GameObject platformObject2 = Instantiate(platform, n3.transform.position, Quaternion.identity, parent);
        PathAgent agent2 = platformObject2.GetComponent<PathAgent>();
        agent2.speed = 6;
        agent2.network = net2;
        agent2.location = n3.GetComponent<PathNode>();

        level = GameState.GetGame().level;

        if (level >= 4) {
            GameObject turr3 = Instantiate(turret, new Vector3(x, 0, z + 20), Quaternion.identity, parent);
            turr3.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            GameObject pillar3 = Instantiate(pillar, new Vector3(x + 15, 2f, z), Quaternion.identity, parent);
            GameObject button3 = Instantiate(button, new Vector3(x + 15, 7f, z), Quaternion.identity, parent);
        }

        if (level >= 3) {
            GameObject turr2 = Instantiate(turret, new Vector3(x + 20, 0, z - 20), Quaternion.identity, parent);
            turr2.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            GameObject pillar4 = Instantiate(pillar, new Vector3(x + 10, 2f, z + 10), Quaternion.identity, parent);
            GameObject button4 = Instantiate(button, new Vector3(x + 10, 7f, z + 10), Quaternion.identity, parent);
        }

        if (level >= 2) {
            GameObject turr = Instantiate(turret, new Vector3(x - 20, 0, z - 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            GameObject pillar5 = Instantiate(pillar, new Vector3(x - 17, 2f, z - 10), Quaternion.identity, parent);
            GameObject button5 = Instantiate(button, new Vector3(x - 17, 7f, z - 10), Quaternion.identity, parent);
        }

    }

}

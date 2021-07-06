using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsJ : MonoBehaviour
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
    public GameObject pillar;
    public GameObject block;
    public GameObject platform;
    public GameObject button;
    public GameObject healthCollectible;
    public GameObject shieldCollectible;
    public GameObject mine;
    private int level;
    private System.Random rng;

    // Start is called before the first frame update
    void Start()
    {
        level = GameState.GetGame().level;
        if (rng == null) {
            rng = new System.Random();
        }
    }

    public void SetRNG() {
        rng = new System.Random();
    }

    public void PortalRoom(int x, int z, Transform parent) {
    
        GameObject sPortal = Instantiate(startPortal, new Vector3(x - 10, 2f, z + 20), Quaternion.identity, parent);
        GameObject ePortal = Instantiate(endPortal, new Vector3(x, 12f, z - 17), Quaternion.identity, parent);

        sPortal.GetComponent<PortalHandler>().portalDestination = ePortal.transform.position;

        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject n1 = Instantiate(pathNode, new Vector3(x - 10, 8f, z - 17), Quaternion.identity, p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x + 17, 8f, z - 17), Quaternion.Euler(0, 90, 0), p.transform);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors();
        net.CalculateVertices();

        n1.GetComponent<PathNode>().DisableRender();
        n2.GetComponent<PathNode>().DisableRender();

        GameObject bPlatform = Instantiate(buttonPlatform, n1.transform.position, Quaternion.identity, parent);

        PathAgent agent = bPlatform.GetComponent<PathAgent>();
        agent.speed = 6;
        agent.network = net; 
        agent.location = n1.GetComponent<PathNode>();

        GameObject turr = Instantiate(turret, new Vector3(x + 20, 0, z + 20), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x - 20, 0, z - 20), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;

        level = GameState.GetGame().level;

        if (level >= 2) {
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z - 17), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 5, 0.5f, z - 17), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x, 0.5f, z - 17), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 5, 0.5f, z - 17), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 10, 0.5f, z - 17), Quaternion.identity, parent);
        }
    }

    public void MovingPlatformsRoom(int x, int z, Transform parent)
    {
        // Instantiate(pillar, new Vector3(x + 6, 5, z + 13), Quaternion.identity, parent);
        GameObject stairsObject = Instantiate(stairs, new Vector3(x + 12, 0, z - 9), Quaternion.Euler(0, 90, 0), parent);
        stairsObject.transform.localScale = new Vector3(0.5f, 1, 0.5f);

        GameObject blockObject = Instantiate(block, new Vector3(x + 12, 0, z - 8), Quaternion.identity, parent);
        blockObject.transform.localScale = new Vector3(5, 12, 5);

        GameObject blockObject2 = Instantiate(block, new Vector3(x + 12, 0, z - 3), Quaternion.identity, parent);
        blockObject2.transform.localScale = new Vector3(5, 12, 5);
 
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        p.transform.localScale = new Vector3(5, 0.5f, 5);

        GameObject n1 = Instantiate(pathNode, new Vector3(x + 7, 6f, z - 3), Quaternion.identity, p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x - 12, 6f, z - 3), Quaternion.Euler(0, 90, 0), p.transform);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors();
        net.CalculateVertices();

        n1.GetComponent<PathNode>().DisableRender();
        n2.GetComponent<PathNode>().DisableRender();

        // moving platform
        GameObject platformObject = Instantiate(platform, n1.transform.position, Quaternion.identity, parent);

        PathAgent agent = platformObject.GetComponent<PathAgent>();
        agent.speed = 4;
        agent.network = net; 
        agent.location = n1.GetComponent<PathNode>();

        GameObject buttonBlock = Instantiate(block, new Vector3(x - 17, 0, z - 3), Quaternion.identity, parent);
        buttonBlock.transform.localScale = new Vector3(5, 12, 5);
        GameObject buttonObject = Instantiate(button, new Vector3(x - 17, 6f, z - 3), Quaternion.identity, parent);

        // turrets and their blocks
        GameObject turretBlock1 = Instantiate(block, new Vector3(x - 4, 0, z + 7), Quaternion.identity, parent);
        turretBlock1.transform.localScale = new Vector3(5, 12, 5);
        GameObject turretBlock2 = Instantiate(block, new Vector3(x - 8, 0, z - 12), Quaternion.identity, parent);
        turretBlock2.transform.localScale = new Vector3(5, 12, 5);


        GameObject turr1 = Instantiate(turret, new Vector3(x - 4, 6f, z + 7), Quaternion.identity, parent);
        turr1.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        GameObject turr2 = Instantiate(turret, new Vector3(x - 8, 6f, z - 12), Quaternion.identity, parent);
        turr2.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;

        level = GameState.GetGame().level;

        if (level >= 3)
        {
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z - 3), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 5, 0.5f, z - 3), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x, 0.5f, z - 3), Quaternion.identity, parent);
        }
    }

    public void ChoiceRoom(int x, int z, Transform parent)
    {
        GameObject sPortal1 = Instantiate(startPortal, new Vector3(x - 10, 2f, z - 18), Quaternion.identity, parent);
        GameObject sPortal2 = Instantiate(startPortal, new Vector3(x, 2f, z - 18), Quaternion.identity, parent);
        GameObject sPortal3 = Instantiate(startPortal, new Vector3(x + 10, 2f, z - 18), Quaternion.identity, parent);

        GameObject[] roomPortals =  new GameObject[] {
            Instantiate(endPortal, new Vector3(x - 10, 2f, z - 10), Quaternion.identity, parent),
            Instantiate(endPortal, new Vector3(x + 3, 2f, z - 10), Quaternion.identity, parent),
            Instantiate(endPortal, new Vector3(x + 9f, 10f, z - 8), Quaternion.identity, parent),
        };

        // set exit portals to exit choice corridors
        GameObject exitPortal1 = Instantiate(startPortal, new Vector3(x - 10, 2f, z + 10), Quaternion.identity, parent);
        GameObject exitPortal2 = Instantiate(startPortal, new Vector3(x + 3, 2f, z + 10), Quaternion.identity, parent);
        GameObject exitPortal3 = Instantiate(startPortal, new Vector3(x + 12, 2f, z + 10), Quaternion.identity, parent);

        GameObject exitPortalDestination = Instantiate(
            endPortal,
            new Vector3(x - 20, 6, z - 23),
            Quaternion.identity,
            parent
        );
        
        exitPortal1.GetComponent<PortalHandler>().portalDestination = exitPortalDestination.transform.position;
        exitPortal2.GetComponent<PortalHandler>().portalDestination = exitPortalDestination.transform.position;
        exitPortal3.GetComponent<PortalHandler>().portalDestination = exitPortalDestination.transform.position;

        // shuffle end portals 3 times for randomization
        for (int i = 0; i < roomPortals.Length; i++) {
            // generate rand index from [0, portals.length)
            int newIndex = rng.Next(roomPortals.Length);

            GameObject tempPortal = roomPortals[newIndex];

            roomPortals[newIndex] = roomPortals[i];
            roomPortals[i] = tempPortal;
        }

        // set portal destinations
        sPortal1.GetComponent<PortalHandler>().portalDestination = roomPortals[0].transform.position;
        sPortal2.GetComponent<PortalHandler>().portalDestination = roomPortals[1].transform.position;
        sPortal3.GetComponent<PortalHandler>().portalDestination = roomPortals[2].transform.position;

        Vector3 innerWallScale = new Vector3(30, 12, 0.5f);

        GameObject innerNorthWall = Instantiate(pillar, new Vector3(x, 0, z + 14), Quaternion.identity, parent);
        innerNorthWall.transform.localScale = innerWallScale;
        GameObject innerSouthWall = Instantiate(pillar, new Vector3(x, 0, z - 14), Quaternion.identity, parent);
        innerSouthWall.transform.localScale = innerWallScale;

        Vector3 innerSideWallScale = new Vector3(28, 12, 0.5f);

        GameObject innerEastWall = Instantiate(pillar, new Vector3(x + 15, 0, z), Quaternion.identity, parent);
        innerEastWall.transform.localScale = innerSideWallScale;
        innerEastWall.transform.Rotate(0, 90f, 0, Space.Self);
        GameObject innerWestWall = Instantiate(pillar, new Vector3(x - 15, 0, z), Quaternion.identity, parent);
        innerWestWall.transform.localScale = innerSideWallScale;
        innerWestWall.transform.Rotate(0, 90f, 0, Space.Self);

        GameObject innerDivider1 = Instantiate(pillar, new Vector3(x + 5, 0, z), Quaternion.identity, parent);
        innerDivider1.transform.localScale = innerSideWallScale;
        innerDivider1.transform.Rotate(0, 90f, 0, Space.Self);

        GameObject innerDivider2 = Instantiate(pillar, new Vector3(x - 5, 0, z), Quaternion.identity, parent);
        innerDivider2.transform.localScale = innerSideWallScale;
        innerDivider2.transform.Rotate(0, 90f, 0, Space.Self);

        // populate choice corridors

        // leftmost corridor
        GameObject healthUp = Instantiate(healthCollectible, new Vector3(x - 12, 1f, z + 5), Quaternion.identity, parent);
        GameObject shieldUp = Instantiate(shieldCollectible, new Vector3(x - 8, 1f, z + 1), Quaternion.identity, parent);

        // middle corridor
        GameObject turr1 = Instantiate(turret, new Vector3(x - 3, 0, z + 10f), Quaternion.identity, parent);
        turr1.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        GameObject turr2 = Instantiate(turret, new Vector3(x + 3, 0, z - 3.2f), Quaternion.identity, parent);
        turr2.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;

        GameObject coverWall1 = Instantiate(pillar, new Vector3(x - 1f, 0, z + 4.5f), Quaternion.identity, parent);
        coverWall1.transform.localScale = new Vector3(4, 6f, 1f);
        GameObject coverWall2 = Instantiate(pillar, new Vector3(x + 2f, 0, z - 0.5f), Quaternion.identity, parent);
        coverWall2.transform.localScale = new Vector3(5.5f, 6f, 1f);
        GameObject coverWall3 = Instantiate(pillar, new Vector3(x + 3f, 0, z - 6.0f), Quaternion.identity, parent);
        coverWall3.transform.localScale = new Vector3(4, 6f, 1f);

        // rightmost corridor
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);

        GameObject n1 = Instantiate(pathNode, new Vector3(x + 8, 6f, z + 10), Quaternion.identity, p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x + 8, 6f, z - 10), Quaternion.Euler(0, 90, 0), p.transform);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors();
        net.CalculateVertices();

        n1.GetComponent<PathNode>().DisableRender();
        n2.GetComponent<PathNode>().DisableRender();
    
        GameObject platformObject = Instantiate(platform, n1.transform.position, Quaternion.identity, parent);
        PathAgent agent = platformObject.GetComponent<PathAgent>();
        agent.speed = 6;
        agent.network = net; 
        agent.location = n1.GetComponent<PathNode>();

        GameObject buttonObject = Instantiate(button, new Vector3(x + 12, 6f, z + 2), Quaternion.identity, parent);
        GameObject buttonPillar = Instantiate(pillar, new Vector3(x + 12, 3f, z + 2), Quaternion.identity, parent);
        buttonPillar.transform.localScale = new Vector3(2f, 6f, 2f);

        level = GameState.GetGame().level;
        
        if (level >= 2) {
            Instantiate(spikes, new Vector3(x + 10, 0.5f, z), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 8, 0.5f, z - 10), Quaternion.identity, parent);

            if (level >= 3) {
                Instantiate(mine, new Vector3(x + 8, 0.125f, z - 8), Quaternion.identity, parent);
                Instantiate(mine, new Vector3(x + 12, 0.125f, z - 6), Quaternion.identity, parent);
                Instantiate(mine, new Vector3(x + 12, 0.125f, z), Quaternion.identity, parent);
                Instantiate(mine, new Vector3(x + 11, 0.125f, z + 4), Quaternion.identity, parent);

            }
        }
        // for exit portal
        GameObject pillarForPortal = Instantiate(pillar, new Vector3(x - 20, 2f, z- 23), Quaternion.identity, parent);
        pillarForPortal.transform.localScale = new Vector3(2f, 4.5f, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsT : MonoBehaviour
{
    public GameObject healthCollectible;
    public GameObject shieldCollectible;
    public GameObject speedCollectible;
    public GameObject mine;
    public GameObject spikes;
    public GameObject button;
    public GameObject terminal;
    public GameObject robot;
    public GameObject missile;
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
    public GameObject crawler;
    public GameObject swordCrawler;
    public GameObject gunCrawler;
    public GameObject bossTurret;
    public GameObject dataDisc;
    public GameObject missileLauncher;
    public GameObject laserBlaster;
    public GameObject pathBoss;
    public GameObject turret;
    public GameObject pathTurret;
    public FloorGenerator roomHolder;
    public GameObject loadingScreen;

    private System.Random rng;
    
    // Start is called before the first frame update
    void Start()
    {
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

    public void DefaultRoom(int x, int z, Transform parent) {
        //Instantiate(speedCollectible, new Vector3(x - 10, 1f, z - 20), Quaternion.identity, parent);

        GameObject mineObj = Instantiate(mine, new Vector3(x + 5, 0.125f, z), Quaternion.identity, parent);
        // GameObject mineExplosion = mineObj.transform.Find("Big Explosion").gameObject;
        // mineExplosion.GetComponent<ExplosionManager>().body = mineObj.transform.Find("Mine Body").gameObject;

        Instantiate(spikes, new Vector3(x, 0.5f, z + 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 5, 0, z - 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 4.5f, 0, z + 4.5f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 15, 0, z - 15), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x - 8, 2, z + 5), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x + 18, 2, z - 5), Quaternion.identity, parent);
        GameObject t = Instantiate(terminal, new Vector3(x - 24.4f, 1.5f, z - 15), Quaternion.identity, parent);
        GameObject r = Instantiate(robot, new Vector3(x + 15, 0.5f, z - 8), Quaternion.identity, parent);
        t.GetComponent<TerminalManager>().bot = r.GetComponent<HackedRobotController>();

        GameObject c = Instantiate(crawler, new Vector3(x, 0, z), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;

        GameObject missileObj = Instantiate(missile, new Vector3(x - 20, 1.5f, z - 20), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        // GameObject missileExplosion = missileObj.transform.Find("Big Explosion").gameObject;
        // missileExplosion.GetComponent<ExplosionManager>().body = missileObj.transform.Find("Missile Body").gameObject;

        
        if (GameState.GetGame().rng.Next(2) == 0) {
            Instantiate(healthCollectible, new Vector3(x + 10, 1f, z + 10), Quaternion.identity, parent);
        } else {
            Instantiate(shieldCollectible, new Vector3(x - 10, 1f, z + 20), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;

        if (level >= 2) {
            c = Instantiate(swordCrawler, new Vector3(x - 13, 0, z - 13), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(gunCrawler, new Vector3(x + 8, 0, z - 7), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
            GameObject n1 = Instantiate(pathNode, new Vector3(x+7, 0, z + 2), Quaternion.identity, p.transform);
            GameObject n2 = Instantiate(pathNode, new Vector3(x+7, 0, z + 7), Quaternion.Euler(0, 270, 0), p.transform);
            GameObject n3 = Instantiate(pathNode, new Vector3(x+2, 0, z + 7), Quaternion.Euler(0, 180, 0), p.transform);
            GameObject n4 = Instantiate(pathNode, new Vector3(x+2, 0, z + 2), Quaternion.Euler(0, 90, 0), p.transform);
            PathNetwork net = p.GetComponent<PathNetwork>();
            net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>()); net.AddEdge(n2.GetComponent<PathNode>(), n3.GetComponent<PathNode>());
            net.AddEdge(n3.GetComponent<PathNode>(), n4.GetComponent<PathNode>()); net.AddEdge(n4.GetComponent<PathNode>(), n1.GetComponent<PathNode>()); 
            net.SetNeighbors(); net.CalculateVertices();
            GameObject pMine = Instantiate(pathMine, new Vector3(x+7, 0.125f, z + 2), Quaternion.identity, parent);
            PathAgent mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net; 
            mineAgent.location = n1.GetComponent<PathNode>();
        }
        if (level >= 4) {
            GameObject ml = Instantiate(missileLauncher, new Vector3(x + 20, 1.5f, z - 24.4f), Quaternion.identity, parent);
            ml.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            Instantiate(mine, new Vector3(x - 15, 0.125f, z - 15), Quaternion.identity, parent);
        }
    }

    public void ButtonTutorial(int x, int z, Transform parent) {
        // GameObject mineObj = Instantiate(mine, new Vector3(x + 5, 0.125f, z), Quaternion.identity, parent);

        Instantiate(button, new Vector3(x, 0.2f, z - 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 5, 0.2f, z - 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 10, 0.2f, z - 5), Quaternion.identity, parent);

        int level = GameState.GetGame().level;
        if (level == 2) {
            Instantiate(healthCollectible, new Vector3(x - 10, 1f, z + 5), Quaternion.identity, parent);
        }
        if (level >= 2) {
            Instantiate(healthCollectible, new Vector3(x + 10, 1f, z + 5), Quaternion.identity, parent);
        }
    }

    public void Exit(int x, int z, Transform parent) {
        GameObject l = Instantiate(ladder, new Vector3(x, 5, z), Quaternion.identity, parent);
        LadderManager m = l.GetComponent<LadderManager>();
        m.roomHolder = roomHolder;
        m.loadingScreen = loadingScreen;
        //Instantiate(healthCollectible, new Vector3(x + 10, 1f, z + 10), Quaternion.identity, parent);
        //Instantiate(healthCollectible, new Vector3(x - 10, 1f, z + 10), Quaternion.identity, parent);
        GameObject b = Instantiate(bossTurret, new Vector3(x, 0, z), Quaternion.identity, parent);
        b.GetComponent<BossTurretAI>().player = gameObject.GetComponent<WallCreator>().player;
        b.GetComponent<BossTurretAI>().hatch = l;
        l.SetActive(false);
        List<GameObject> targets = new List<GameObject>();
        GameObject d = Instantiate(dataDisc, new Vector3(x, 0, z - 10), Quaternion.identity, parent);
        targets.Add(d);

        int level = GameState.GetGame().level;

        if (level >= 2) {
            d = Instantiate(dataDisc, new Vector3(x - 8, 0, z + 4), Quaternion.identity, parent);
            targets.Add(d);
            d = Instantiate(dataDisc, new Vector3(x + 8, 0, z + 4), Quaternion.identity, parent);
            targets.Add(d);
        }
        if (level == 2) {
            GameObject c = Instantiate(swordCrawler, new Vector3(x - 15, 0, z + 15), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(swordCrawler, new Vector3(x + 15, 0, z + 15), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level == 3) {
            Instantiate(box, new Vector3(x + 4, 2, z + 4), Quaternion.identity, parent);
            Instantiate(box, new Vector3(x - 4, 2, z + 4), Quaternion.identity, parent);
            Instantiate(box, new Vector3(x, 2, z - 5), Quaternion.identity, parent);
        }
        if (level == 4) {
            Instantiate(spikes, new Vector3(x - 2, 0.5f, z + 5), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 2, 0.5f, z + 5), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 4, 0.5f, z + 2), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 4, 0.5f, z - 2), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 4, 0.5f, z + 2), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 4, 0.5f, z - 2), Quaternion.identity, parent);
        }
        if (level == 5) {
            GameObject la = Instantiate(laser, new Vector3(x, 4, z), Quaternion.Euler(0, 45, 0), parent);
            GameObject lb = Instantiate(laserButton, new Vector3(x, 0.2f, z + 20), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = la;
            lb = Instantiate(laserButton, new Vector3(x, 0.2f, z - 20), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = la;
            la = Instantiate(laser, new Vector3(x, 4, z), Quaternion.Euler(0, -45, 0), parent);
            lb = Instantiate(laserButton, new Vector3(x - 20, 0.2f, z), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = la;
            lb = Instantiate(laserButton, new Vector3(x + 20, 0.2f, z), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = la;
        }

        b.GetComponent<BossTurretAI>().targets = targets;

    }

    public void Exit2(int x, int z, Transform parent) {
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject n1 = Instantiate(pathNode, new Vector3(x + 20, 0, z + 8), Quaternion.Euler(0, 270, 0), p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x - 20, 0, z + 8), Quaternion.Euler(0, 90, 0), p.transform);
        n1.transform.localScale = new Vector3(1, 1, 4);
        n2.transform.localScale = new Vector3(1, 1, 4);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors(); net.CalculateVertices();
        GameObject pBoss = Instantiate(pathBoss, new Vector3(x+20, 0, z + 8), Quaternion.identity, parent);
        PathAgent pathAgent = pBoss.GetComponent<PathAgent>();
        pathAgent.network = net; 
        pathAgent.location = n1.GetComponent<PathNode>();

        GameObject l = Instantiate(ladder, new Vector3(x, 5, z), Quaternion.identity, parent);
        LadderManager m = l.GetComponent<LadderManager>();
        m.roomHolder = roomHolder;
        m.loadingScreen = loadingScreen;
        pBoss.GetComponent<BossTurretAI>().player = gameObject.GetComponent<WallCreator>().player;
        pBoss.GetComponent<BossTurretAI>().hatch = l;
        l.SetActive(false);
        List<GameObject> targets = new List<GameObject>();
        GameObject d = Instantiate(dataDisc, new Vector3(x - 15, 0, z - 15), Quaternion.identity, parent);
        targets.Add(d);
        d = Instantiate(dataDisc, new Vector3(x, 0, z - 15), Quaternion.identity, parent);
        targets.Add(d);
        d = Instantiate(dataDisc, new Vector3(x + 15, 0, z - 15), Quaternion.identity, parent);
        targets.Add(d);
        pBoss.GetComponent<BossTurretAI>().targets = targets;

        int level = GameState.GetGame().level;
        pathAgent.speed = 3 + 2*level;

        if (level <= 3) {
            Instantiate(pillar, new Vector3(x + 7.5f, 5, z - 12), Quaternion.identity, parent);
            Instantiate(pillar, new Vector3(x - 7.5f, 5, z - 12), Quaternion.identity, parent);
        }
        if (level == 1) {
            GameObject ml = Instantiate(missileLauncher, new Vector3(x + 24.4f, 1.5f, z - 18), Quaternion.Euler(0, 270, 0), parent);
            ml.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level == 2) {
            Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z - 18), Quaternion.Euler(0, 90, 0), parent);
        }
        if (level == 3) {
            Instantiate(box, new Vector3(x - 15, 2, z - 10), Quaternion.identity, parent);
            Instantiate(box, new Vector3(x, 2, z - 10), Quaternion.identity, parent);
            Instantiate(box, new Vector3(x + 15, 2, z - 10), Quaternion.identity, parent);
        }
        if (level == 5) {
            GameObject c = Instantiate(crawler, new Vector3(x - 15, 0, z - 20), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(crawler, new Vector3(x + 15, 0, z - 20), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void Item(int x, int z, Transform parent) {
        if (GameState.GetGame().rng.Next(2) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 1, z), Quaternion.identity, parent);
        } else {
            Instantiate(shieldCollectible, new Vector3(x, 1, z), Quaternion.identity, parent);
        }
    }

    public void Lasers(int x, int z, Transform parent) {
        GameObject t = Instantiate(terminal, new Vector3(x - 24.4f, 1.5f, z - 15), Quaternion.identity, parent);
        GameObject r = Instantiate(robot, new Vector3(x + 15, 0.5f, z + 8), Quaternion.identity, parent);
        t.GetComponent<TerminalManager>().bot = r.GetComponent<HackedRobotController>();
        GameObject l = Instantiate(laser, new Vector3(x, 4, z), Quaternion.identity, parent);
        GameObject lb = Instantiate(laserButton, new Vector3(x - 6, 0.2f, z + 10), Quaternion.identity, parent);
        lb.GetComponent<LaserManager>().laser = l;
        Instantiate(button, new Vector3(x + 10, 0.2f, z + 10), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 10, 0.2f, z - 10), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 1, z), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;
        if (level >= 2) {
            GameObject c = Instantiate(swordCrawler, new Vector3(x, 0, z + 10), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            Instantiate(laserBlaster, new Vector3(x - 10, 2, z - 24.4f), Quaternion.identity, parent);
        }
        if (level >= 5) {
            GameObject m = Instantiate(missileLauncher, new Vector3(x + 10, 2, z - 24.4f), Quaternion.identity, parent);
            m.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void PlatformRoom(int x, int z, Transform parent) {
        Instantiate(button, new Vector3(x + 23.5f, 0.2f, z - 23.5f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 0.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 9, 2.2f, z + 6), Quaternion.identity, parent);
        Instantiate(stairs, new Vector3(x + 5, 0, z + 6), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 0.5f, 6, z + 6), Quaternion.identity, parent);
        GameObject b = Instantiate(box, new Vector3(x - 5, 7, z + 6), Quaternion.identity, parent);
        b.GetComponent<HealthAgent>().health = 150;
        Instantiate(box, new Vector3(x - 12, 1, z - 13), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 9, 1, z + 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 10, 5, z - 10), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 12, z - 2), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;
        if (level >= 2) {
            GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
            GameObject n1 = Instantiate(pathNode, new Vector3(x+2.5f, 0, z - 2.5f), Quaternion.identity, p.transform);
            GameObject n2 = Instantiate(pathNode, new Vector3(x+2.5f, 0, z + 2.5f), Quaternion.Euler(0, 270, 0), p.transform);
            GameObject n3 = Instantiate(pathNode, new Vector3(x-2.5f, 0, z + 2.5f), Quaternion.Euler(0, 180, 0), p.transform);
            GameObject n4 = Instantiate(pathNode, new Vector3(x-2.5f, 0, z - 2.5f), Quaternion.Euler(0, 90, 0), p.transform);
            PathNetwork net = p.GetComponent<PathNetwork>();
            net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>()); net.AddEdge(n2.GetComponent<PathNode>(), n3.GetComponent<PathNode>());
            net.AddEdge(n3.GetComponent<PathNode>(), n4.GetComponent<PathNode>()); net.AddEdge(n4.GetComponent<PathNode>(), n1.GetComponent<PathNode>()); 
            net.SetNeighbors(); net.CalculateVertices();
            GameObject pMine = Instantiate(pathMine, new Vector3(x+2.5f, 0.125f, z - 2.5f), Quaternion.identity, parent);
            PathAgent mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net; 
            mineAgent.location = n1.GetComponent<PathNode>();
            Instantiate(spikes, new Vector3(x + 19.5f, 0.5f, z - 19.5f), Quaternion.identity, parent);
        }
        if (level >= 3) {
            Instantiate(spikes, new Vector3(x + 19.5f, 0.5f, z - 23.5f), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 23.5f, 0.5f, z - 19.5f), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x-9, 2.25f, z + 6), Quaternion.identity, parent);
        }
        if (level >= 4) {
            Instantiate(spikes, new Vector3(x + 19.5f, 0.5f, z - 21.5f), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 21.5f, 0.5f, z - 19.5f), Quaternion.identity, parent);
        }
        if (level >= 5) {
            Instantiate(laserBlaster, new Vector3(x - 24.4f, 7, z + 6), Quaternion.Euler(0, 90, 0), parent);
        }
    }

    public void RobotRoom(int x, int z, Transform parent) {
        GameObject t = Instantiate(terminal, new Vector3(x-24.4f, 1.5f, z - 15), Quaternion.identity, parent);
        GameObject r = Instantiate(robot, new Vector3(x - 16.5f, 0.5f, z - 16.5f), Quaternion.identity, parent);
        t.GetComponent<TerminalManager>().bot = r.GetComponent<HackedRobotController>();
        GameObject l = Instantiate(laser, new Vector3(x - 18, 4, z), Quaternion.Euler(0, 90, 0), parent);
        l.transform.localScale = new Vector3(50f*18f/25f, 8, 0.3f);
        l = Instantiate(laser, new Vector3(x + 18, 4, z), Quaternion.Euler(0, 90, 0), parent);
        l.transform.localScale = new Vector3(50f*18f/25f, 8, 0.3f);
        l = Instantiate(laser, new Vector3(x, 4, z + 18), Quaternion.identity, parent);
        l.transform.localScale = new Vector3(50f*18f/25f, 8, 0.3f);
        l = Instantiate(laser, new Vector3(x, 4, z - 18), Quaternion.identity, parent);
        l.transform.localScale = new Vector3(50f*18f/25f, 8, 0.3f);
        Instantiate(button, new Vector3(x - 16.5f, 0.2f, z + 16.5f), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z - 14), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z - 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 14, 1, z + 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 10, 1, z + 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 6, 1, z + 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 2, 1, z + 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z - 10), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z - 6), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z + 6), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z + 10), Quaternion.identity, parent);

        Instantiate(block, new Vector3(x, 1, z + 6), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z + 6), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z - 6), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z - 6), Quaternion.identity, parent);
        //Instantiate(block, new Vector3(x + 8, 1, z), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x - 4, 1, z), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x + 10, 1, z + 4), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x + 10, 1, z - 7), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x + 12, 1, z + 12), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x + 12, 1, z), Quaternion.identity, parent);
        //Instantiate(box, new Vector3(x + 13, 1, z + 6), Quaternion.identity, parent);
        
        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x + 22, 1f, z + 22), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;

        if (level >= 2) {
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z - 19), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z - 21), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x - 10, 0.5f, z - 23), Quaternion.identity, parent);
        }
        if (level >= 3) {
            Instantiate(mine, new Vector3(x + 24, 0.25f, z - 15), Quaternion.identity, parent);
        }
        if (level >= 4) {
            GameObject m = Instantiate(missileLauncher, new Vector3(x + 24.4f, 2, z + 22), Quaternion.Euler(0, 270, 0), parent);
            m.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            Instantiate(laserBlaster, new Vector3(x - 22, 2, z - 24.4f), Quaternion.identity, parent);
        }
    }

    public void PathRoom(int x, int z, Transform parent) {
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject n1 = Instantiate(pathNode, new Vector3(x, 0, z), Quaternion.identity, p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x, 0, z + 15), Quaternion.Euler(0, 180, 0), p.transform);
        GameObject n3 = Instantiate(pathNode, new Vector3(x + 10, 0, z + 10), Quaternion.Euler(0, -135, 0), p.transform);
        GameObject n4 = Instantiate(pathNode, new Vector3(x + 15, 0, z), Quaternion.Euler(0, -90, 0), p.transform);
        GameObject n5 = Instantiate(pathNode, new Vector3(x + 10, 0, z - 10), Quaternion.Euler(0, -45, 0), p.transform);
        GameObject n6 = Instantiate(pathNode, new Vector3(x, 0, z - 15), Quaternion.identity, p.transform);
        GameObject n7 = Instantiate(pathNode, new Vector3(x - 10, 0, z - 10), Quaternion.Euler(0, 45, 0), p.transform);
        GameObject n8 = Instantiate(pathNode, new Vector3(x - 15, 0, z), Quaternion.Euler(0, 90, 0), p.transform);
        GameObject n9 = Instantiate(pathNode, new Vector3(x - 10, 0, z + 10), Quaternion.Euler(0, 135, 0), p.transform);
        n2.transform.localScale = new Vector3(1, 1, 3); n3.transform.localScale = new Vector3(1, 1, 3); n4.transform.localScale = new Vector3(1, 1, 3);
        n5.transform.localScale = new Vector3(1, 1, 3); n6.transform.localScale = new Vector3(1, 1, 3); n7.transform.localScale = new Vector3(1, 1, 3);
        n8.transform.localScale = new Vector3(1, 1, 3); n9.transform.localScale = new Vector3(1, 1, 3);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>()); net.AddEdge(n1.GetComponent<PathNode>(), n3.GetComponent<PathNode>());
        net.AddEdge(n1.GetComponent<PathNode>(), n4.GetComponent<PathNode>()); net.AddEdge(n1.GetComponent<PathNode>(), n5.GetComponent<PathNode>()); 
        net.AddEdge(n1.GetComponent<PathNode>(), n6.GetComponent<PathNode>()); net.AddEdge(n1.GetComponent<PathNode>(), n7.GetComponent<PathNode>());
        net.AddEdge(n1.GetComponent<PathNode>(), n8.GetComponent<PathNode>()); net.AddEdge(n1.GetComponent<PathNode>(), n9.GetComponent<PathNode>()); 
        net.SetNeighbors(); net.CalculateVertices();
        GameObject pTurr = Instantiate(pathTurret, new Vector3(x, 0, z), Quaternion.identity, parent);
        PathAgent turrAgent = pTurr.GetComponent<PathAgent>();
        pTurr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turrAgent.network = net;
        turrAgent.location = n1.GetComponent<PathNode>();
        Instantiate(button, new Vector3(x - 23.5f, 0.2f, z + 23.5f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 23.5f, 0.2f, z - 23.5f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 0.2f, z), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x + 8, 1f, z - 8), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;
        GameObject pMine;
        PathAgent mineAgent;
        if (level >= 2) {
            pMine = Instantiate(pathMine, new Vector3(x + 10, 0.125f, z + 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n3.GetComponent<PathNode>();
        }
        if (level >= 3) {
            pMine = Instantiate(pathMine, new Vector3(x - 10, 0.125f, z - 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n7.GetComponent<PathNode>();
            GameObject turr = Instantiate(turret, new Vector3(x + 20, 0, z + 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x - 20, 0, z - 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            pMine = Instantiate(pathMine, new Vector3(x + 10, 0.125f, z - 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n5.GetComponent<PathNode>();
        }
        if (level >= 5) {
            pMine = Instantiate(pathMine, new Vector3(x - 10, 0.125f, z + 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n9.GetComponent<PathNode>();
        }

    }

    public void TurretRoom(int x, int z, Transform parent) {
        Instantiate(pillar, new Vector3(x, 5, z), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 12, 5, z + 12), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 12, 5, z + 12), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 12, 5, z - 12), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 12, 5, z - 12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 12, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 12, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 12, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 12, 1, z+4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 12, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 12, 1, z-4), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 12, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 12, 1, z+8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 12, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 12, 1, z-8), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z+12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z+12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z+12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z+12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z+12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x, 1, z-12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 4, 1, z-12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 4, 1, z-12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 8, 1, z-12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 8, 1, z-12), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 22.4f, 1, z-22.4f), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 22.4f, 1, z+22.4f), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 22.4f, 1, z+22.4f), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 22.4f, 1, z-22.4f), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 10, 2.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 10, 2.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 2.2f, z - 10), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x + 6, 1f, z + 20), Quaternion.identity, parent);
        }

        GameObject turr = Instantiate(turret, new Vector3(x + 23, 2, z + 23), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x - 23, 2, z + 23), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x - 23, 2, z - 23), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x + 23, 2, z - 23), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;

        int level = GameState.GetGame().level;

        if (level >= 2) {
            Instantiate(block, new Vector3(x - 12, 1, z+22.4f), Quaternion.identity, parent);
            Instantiate(block, new Vector3(x + 12, 1, z+22.4f), Quaternion.identity, parent);
            turr = Instantiate(turret, new Vector3(x - 12, 2, z + 23), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x + 12, 2, z + 23), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            Instantiate(block, new Vector3(x - 12, 1, z-22.4f), Quaternion.identity, parent);
            Instantiate(block, new Vector3(x + 12, 1, z-22.4f), Quaternion.identity, parent);
            turr = Instantiate(turret, new Vector3(x - 12, 2, z - 23), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x + 12, 2, z - 23), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            Instantiate(block, new Vector3(x + 22.4f, 1, z + 12), Quaternion.identity, parent);
            Instantiate(block, new Vector3(x + 22.4f, 1, z - 12), Quaternion.identity, parent);
            turr = Instantiate(turret, new Vector3(x + 23, 2, z + 12), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x + 23, 2, z - 12), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            Instantiate(block, new Vector3(x - 22.4f, 1, z + 12), Quaternion.identity, parent);
            Instantiate(block, new Vector3(x - 22.4f, 1, z - 12), Quaternion.identity, parent);
            turr = Instantiate(turret, new Vector3(x - 23, 2, z + 12), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x - 23, 2, z - 12), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
    }

    public void Samurai(int x, int z, Transform parent) {
        Instantiate(pillar, new Vector3(x + 6, 5, z - 13), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 6, 5, z + 13), Quaternion.identity, parent);
        Instantiate(stairs, new Vector3(x - 9, 0, z), Quaternion.Euler(0, 180, 0), parent);
        Instantiate(walkway, new Vector3(x - 4.5f, 6, z), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 10.5f, 6, z), Quaternion.identity, parent);
        Instantiate(stairs, new Vector3(x - 9, 0, z + 10), Quaternion.Euler(0, 180, 0), parent);
        Instantiate(walkway, new Vector3(x - 4.5f, 6, z + 10), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 10.5f, 6, z + 10), Quaternion.identity, parent);
        Instantiate(stairs, new Vector3(x - 9, 0, z - 10), Quaternion.Euler(0, 180, 0), parent);
        Instantiate(walkway, new Vector3(x - 4.5f, 6, z - 10), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 10.5f, 6, z - 10), Quaternion.identity, parent);
        GameObject c = Instantiate(swordCrawler, new Vector3(x - 3, 6, z), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        Instantiate(block, new Vector3(x + 15, 7, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 15, 8.2f, z), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x - 3, 1f, z - 14), Quaternion.identity, parent);
        }

        int level = GameState.GetGame().level;

        if (level >= 2) {
            c = Instantiate(swordCrawler, new Vector3(x + 7, 6, z + 6), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            c = Instantiate(swordCrawler, new Vector3(x + 7, 6, z - 6), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            c = Instantiate(swordCrawler, new Vector3(x + 12, 6, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            c = Instantiate(swordCrawler, new Vector3(x - 6, 6, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void Spreadshot(int x, int z, Transform parent) {
        GameObject c = Instantiate(gunCrawler, new Vector3(x + 12, 0, z - 20), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(gunCrawler, new Vector3(x - 12, 0, z - 20), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        Instantiate(box, new Vector3(x - 16, 2, z + 5), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x - 8, 2, z + 3), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x, 2, z + 1), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x + 8, 2, z + 3), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x + 16, 2, z + 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 18, 0.2f, z + 22), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 18, 0.2f, z + 22), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 0.2f, z + 18), Quaternion.identity, parent);
        
        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x + 12, 1f, z - 23), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            c = Instantiate(gunCrawler, new Vector3(x + 20, 0, z - 22), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(gunCrawler, new Vector3(x - 20, 0, z - 22), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            GameObject missileObj = Instantiate(missile, new Vector3(x - 16, 1.5f, z - 18), Quaternion.identity, parent);
            missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
            missileObj = Instantiate(missile, new Vector3(x + 16, 1.5f, z - 18), Quaternion.identity, parent);
            missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            c = Instantiate(gunCrawler, new Vector3(x + 6, 0, z - 16), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(gunCrawler, new Vector3(x - 6, 0, z - 16), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            c = Instantiate(crawler, new Vector3(x, 0, z - 12), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void ZigZag(int x, int z, Transform parent) {
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject n1 = Instantiate(pathNode, new Vector3(x - 20, 0, z - 20), Quaternion.Euler(0, 90, 0), p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x + 20, 0, z - 20), Quaternion.Euler(0, -75.5f, 0), p.transform);
        GameObject n3 = Instantiate(pathNode, new Vector3(x - 20, 0, z - 10), Quaternion.Euler(0, 75.5f, 0), p.transform);
        GameObject n4 = Instantiate(pathNode, new Vector3(x + 20, 0, z - 10), Quaternion.Euler(0, -90, 0), p.transform);
        GameObject n5 = Instantiate(pathNode, new Vector3(x - 20, 0, z), Quaternion.Euler(0, 90, 0), p.transform);
        GameObject n6 = Instantiate(pathNode, new Vector3(x + 20, 0, z), Quaternion.Euler(0, -75.5f, 0), p.transform);
        GameObject n7 = Instantiate(pathNode, new Vector3(x - 20, 0, z + 10), Quaternion.Euler(0, 75.5f, 0), p.transform);
        GameObject n8 = Instantiate(pathNode, new Vector3(x + 20, 0, z + 10), Quaternion.Euler(0, -90, 0), p.transform);
        GameObject n9 = Instantiate(pathNode, new Vector3(x - 20, 0, z + 20), Quaternion.Euler(0, 90, 0), p.transform);
        GameObject n10 = Instantiate(pathNode, new Vector3(x + 20, 0, z + 20), Quaternion.Euler(0, 180, 0), p.transform);
        GameObject n11 = Instantiate(pathNode, new Vector3(x - 20, 0, z + 20), Quaternion.Euler(0, 180, 0), p.transform);
        n2.transform.localScale = new Vector3(1, 1, 8.2f); n3.transform.localScale = new Vector3(1, 1, 8.2f); n4.transform.localScale = new Vector3(1, 1, 8);
        n5.transform.localScale = new Vector3(1, 1, 8); n6.transform.localScale = new Vector3(1, 1, 8.2f); n7.transform.localScale = new Vector3(1, 1, 8.2f);
        n8.transform.localScale = new Vector3(1, 1, 8); n9.transform.localScale = new Vector3(1, 1, 8);
        n10.transform.localScale = new Vector3(1, 1, 8); n11.transform.localScale = new Vector3(1, 1, 8); n1.transform.localScale = new Vector3(1, 1, 8);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>()); net.AddEdge(n2.GetComponent<PathNode>(), n3.GetComponent<PathNode>());
        net.AddEdge(n3.GetComponent<PathNode>(), n4.GetComponent<PathNode>()); net.AddEdge(n3.GetComponent<PathNode>(), n6.GetComponent<PathNode>()); 
        net.AddEdge(n5.GetComponent<PathNode>(), n6.GetComponent<PathNode>()); net.AddEdge(n6.GetComponent<PathNode>(), n7.GetComponent<PathNode>());
        net.AddEdge(n7.GetComponent<PathNode>(), n8.GetComponent<PathNode>()); net.AddEdge(n7.GetComponent<PathNode>(), n10.GetComponent<PathNode>());
        net.AddEdge(n9.GetComponent<PathNode>(), n10.GetComponent<PathNode>()); net.AddEdge(n1.GetComponent<PathNode>(), n3.GetComponent<PathNode>()); 
        net.AddEdge(n3.GetComponent<PathNode>(), n5.GetComponent<PathNode>()); net.AddEdge(n5.GetComponent<PathNode>(), n7.GetComponent<PathNode>());
        net.AddEdge(n7.GetComponent<PathNode>(), n9.GetComponent<PathNode>()); net.AddEdge(n2.GetComponent<PathNode>(), n4.GetComponent<PathNode>());
        net.AddEdge(n4.GetComponent<PathNode>(), n6.GetComponent<PathNode>()); net.AddEdge(n6.GetComponent<PathNode>(), n8.GetComponent<PathNode>());
        net.AddEdge(n8.GetComponent<PathNode>(), n10.GetComponent<PathNode>());
        net.SetNeighbors(); net.CalculateVertices();
        GameObject pMine = Instantiate(pathMine, new Vector3(x - 20, 0.125f, z - 20), Quaternion.identity, parent);
        PathAgent mineAgent = pMine.GetComponent<PathAgent>();
        mineAgent.network = net;
        mineAgent.location = n1.GetComponent<PathNode>();
        pMine.transform.localScale = new Vector3(5, 5, 5);
        pMine = Instantiate(pathMine, new Vector3(x + 20, 0.125f, z - 20), Quaternion.identity, parent);
        mineAgent = pMine.GetComponent<PathAgent>();
        mineAgent.network = net;
        mineAgent.location = n2.GetComponent<PathNode>();
        pMine.transform.localScale = new Vector3(5, 5, 5);
        pMine = Instantiate(pathMine, new Vector3(x - 20, 0.125f, z + 20), Quaternion.identity, parent);
        mineAgent = pMine.GetComponent<PathAgent>();
        mineAgent.network = net;
        mineAgent.location = n9.GetComponent<PathNode>();
        pMine.transform.localScale = new Vector3(5, 5, 5);
        pMine = Instantiate(pathMine, new Vector3(x + 20, 0.125f, z + 20), Quaternion.identity, parent);
        mineAgent = pMine.GetComponent<PathAgent>();
        mineAgent.network = net;
        mineAgent.location = n10.GetComponent<PathNode>();
        pMine.transform.localScale = new Vector3(5, 5, 5);
        Instantiate(button, new Vector3(x, 0.2f, z + 12), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 16, 0.2f, z - 6), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 12, 0.2f, z - 16), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x - 8, 1f, z + 18), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            pMine = Instantiate(pathMine, new Vector3(x - 20, 0.125f, z - 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n3.GetComponent<PathNode>();
            pMine.transform.localScale = new Vector3(5, 5, 5);
        }
        if (level >= 3) {
            pMine = Instantiate(pathMine, new Vector3(x + 20, 0.125f, z + 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n8.GetComponent<PathNode>();
            pMine.transform.localScale = new Vector3(5, 5, 5);
        }
        if (level >= 4) {
            pMine = Instantiate(pathMine, new Vector3(x - 20, 0.125f, z + 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n7.GetComponent<PathNode>();
            pMine.transform.localScale = new Vector3(5, 5, 5);
        }
        if (level >= 5) {
            pMine = Instantiate(pathMine, new Vector3(x + 20, 0.125f, z - 10), Quaternion.identity, parent);
            mineAgent = pMine.GetComponent<PathAgent>();
            mineAgent.network = net;
            mineAgent.location = n4.GetComponent<PathNode>();
            pMine.transform.localScale = new Vector3(5, 5, 5);
        }
    }

    public void Pedestal(int x, int z, Transform parent) {
        Instantiate(stairs, new Vector3(x, 0, z), Quaternion.identity, parent);
        Instantiate(stairs, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0), parent);
        Instantiate(stairs, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0), parent);
        Instantiate(stairs, new Vector3(x, 0, z), Quaternion.Euler(0, 270, 0), parent);
        GameObject b = Instantiate(block, new Vector3(x, 3, z), Quaternion.identity, parent);
        b.transform.localScale = new Vector3(6, 6, 6);
        Instantiate(button, new Vector3(x, 6.2f, z), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 13, 2.5f, z + 3), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 9, 4.5f, z - 3), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 5, 6.5f, z + 1), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 5, 6.5f, z - 1), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 3, 2.125f, z - 13), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x, 4.125f, z - 9), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x - 3, 2.125f, z - 5), Quaternion.identity, parent);
        GameObject p = Instantiate(pathNetwork, new Vector3(x, 0, z), Quaternion.identity, parent);
        GameObject n1 = Instantiate(pathNode, new Vector3(x + 4, 6, z - 5), Quaternion.Euler(0, 270, 0), p.transform);
        GameObject n2 = Instantiate(pathNode, new Vector3(x - 4, 6, z - 5), Quaternion.Euler(0, 90, 0), p.transform);
        PathNetwork net = p.GetComponent<PathNetwork>();
        net.AddEdge(n1.GetComponent<PathNode>(), n2.GetComponent<PathNode>());
        net.SetNeighbors(); net.CalculateVertices();
        GameObject pMine = Instantiate(pathMine, new Vector3(x + 4, 6, z - 5), Quaternion.identity, parent);
        PathAgent pathAgent = pMine.GetComponent<PathAgent>();
        pathAgent.network = net; 
        pathAgent.location = n1.GetComponent<PathNode>();
        GameObject turr = Instantiate(turret, new Vector3(x + 9, 4, z - 2), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x + 13, 2, z + 2), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        Instantiate(spikes, new Vector3(x - 2, 2.5f, z + 13), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x, 6.125f, z + 6), Quaternion.identity, parent);
        turr = Instantiate(turret, new Vector3(x + 2, 4, z + 9), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 8, z), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            GameObject c = Instantiate(swordCrawler, new Vector3(x - 10, 0, z-10), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            GameObject m = Instantiate(missileLauncher, new Vector3(x - 10, 2, z + 23.4f), Quaternion.Euler(0, 180, 0), parent);
            m.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            GameObject g = Instantiate(gunCrawler, new Vector3(x + 10, 0, z+10), Quaternion.identity, parent);
            g.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            Instantiate(laserBlaster, new Vector3(x + 24.4f, 7, z - 24.4f), Quaternion.Euler(0, 45, 0), parent);
        }
    }

    public void Castle(int x, int z, Transform parent) {
        Instantiate(pillar, new Vector3(x + 12, 5, z + 8), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 12, 5, z + 8), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 12, 5, z + 20), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 12, 5, z + 20), Quaternion.identity, parent);
        GameObject b = Instantiate(block, new Vector3(x, 1, z + 14), Quaternion.identity, parent);
        b.transform.localScale = new Vector3(28, 2, 16);
        Instantiate(spikes, new Vector3(x - 13, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 11, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 9, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 7, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 5, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 3, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 1, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 13, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 11, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 9, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 7, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 1, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 5), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 7), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 9), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 11), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 13), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 15), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 17), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 19), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 16, 0.5f, z + 21), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 5), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 7), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 9), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 11), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 13), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 15), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 17), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 19), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 16, 0.5f, z + 21), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 2.2f, z + 14), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 15, 0.2f, z - 15), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 15, 0.2f, z - 15), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x - 8, 3, z + 18), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;
        
        if (level >= 2) {
            GameObject turr = Instantiate(turret, new Vector3(x + 5, 2, z + 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x - 5, 2, z + 20), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            GameObject c = Instantiate(swordCrawler, new Vector3(x - 5, 0, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(swordCrawler, new Vector3(x + 5, 0, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            GameObject cee = Instantiate(crawler, new Vector3(x - 17, 0, z - 10), Quaternion.identity, parent);
            cee.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            cee = Instantiate(crawler, new Vector3(x + 17, 0, z - 10), Quaternion.identity, parent);
            cee.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            GameObject g = Instantiate(gunCrawler, new Vector3(x + 8, 2, z - 18), Quaternion.identity, parent);
            g.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void Traffic(int x, int z, Transform parent) {
        Instantiate(pillar, new Vector3(x - 18, 5, z + 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 6, 5, z + 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 6, 5, z + 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 18, 5, z + 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 18, 5, z + 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 6, 5, z + 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 6, 5, z + 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 18, 5, z + 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 18, 5, z - 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 6, 5, z - 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 6, 5, z - 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 18, 5, z - 6), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 18, 5, z - 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x - 6, 5, z - 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 6, 5, z - 18), Quaternion.identity, parent);
        Instantiate(pillar, new Vector3(x + 18, 5, z - 18), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 12, 0.2f, z - 12), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 0.2f, z + 12), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 12, 0.2f, z), Quaternion.identity, parent);
        GameObject c = Instantiate(crawler, new Vector3(x, 0, z), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        
        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x, 1, z - 12), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            GameObject m = Instantiate(missileLauncher, new Vector3(x - 24.4f, 2, z + 12), Quaternion.Euler(0, 90, 0), parent);
            m.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 3) {
            c = Instantiate(crawler, new Vector3(x + 12, 0, z - 12), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 4) {
            GameObject m = Instantiate(missileLauncher, new Vector3(x + 12, 2, z - 24.4f), Quaternion.identity, parent);
            m.GetComponent<MissileLauncherController>().player = gameObject.GetComponent<WallCreator>().player;
        }
        if (level >= 5) {
            c = Instantiate(crawler, new Vector3(x - 12, 0, z + 12), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        }
    }

    public void BigOne(int x, int z, Transform parent) {
        GameObject c = Instantiate(swordCrawler, new Vector3(x, 0, z), Quaternion.Euler(0, 45, 0), parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c.GetComponent<CrawlerAI>().swingRadius += 30;
        c.GetComponent<CrawlerAI>().attackInterval += 1;
        c.GetComponent<CrawlerAI>().maxSpeed = 0;
        c.transform.localScale = new Vector3(5, 5, 5);
        GameObject b1 = Instantiate(block, new Vector3(x, 1, z + 14), Quaternion.identity, parent);
        GameObject b4 = Instantiate(block, new Vector3(x, 1, z - 14), Quaternion.identity, parent);
        GameObject b2 = Instantiate(block, new Vector3(x + 14, 1, z), Quaternion.identity, parent);
        GameObject b3 = Instantiate(block, new Vector3(x - 14, 1, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 14, 2.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 2.2f, z - 14), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 14, 2.2f, z), Quaternion.identity, parent);
        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x, 3, z + 14), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            b1.transform.localScale = new Vector3(8, 2, 8);
        }
        if (level >= 3) {
            b2.transform.localScale = new Vector3(8, 2, 8);
        }
        if (level >= 4) {
            b3.transform.localScale = new Vector3(8, 2, 8);
        }
        if (level >= 5) {
            b4.transform.localScale = new Vector3(8, 2, 8);
        }
    }

    public void BoxDrop(int x, int z, Transform parent) {
        GameObject b = Instantiate(block, new Vector3(x, 1, z - 15), Quaternion.identity, parent);
        b.transform.localScale = new Vector3(4, 2, 8);
        b = Instantiate(block, new Vector3(x - 10, 1, z - 15), Quaternion.identity, parent);
        b.transform.localScale = new Vector3(4, 2, 8);
        b = Instantiate(block, new Vector3(x + 10, 1, z - 15), Quaternion.identity, parent);
        b.transform.localScale = new Vector3(4, 2, 8);
        Instantiate(walkway, new Vector3(x, 6, z + 5), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x, 6, z - 5), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x - 10, 6, z + 5), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 10, 6, z - 5), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x - 10, 6, z - 5), Quaternion.identity, parent);
        Instantiate(walkway, new Vector3(x + 10, 6, z + 5), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x - 10, 2.2f, z - 16), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 2.2f, z - 16), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 10, 2.2f, z - 16), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x, 2.325f, z - 16), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x, 8, z - 8), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x - 6, 8, z), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x + 6, 8, z), Quaternion.identity, parent);
        Instantiate(box, new Vector3(x, 8, z + 8), Quaternion.identity, parent);
        GameObject t = Instantiate(terminal, new Vector3(x - 24.4f, 1.5f, z - 15), Quaternion.identity, parent);
        GameObject r = Instantiate(robot, new Vector3(x, 6.5f, z), Quaternion.identity, parent);
        t.GetComponent<TerminalManager>().bot = r.GetComponent<HackedRobotController>();
        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x, 1, z - 8), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            Instantiate(mine, new Vector3(x + 10, 2.325f, z - 16), Quaternion.identity, parent);
        }
        if (level >= 3) {
            Instantiate(mine, new Vector3(x - 10, 2.325f, z - 16), Quaternion.identity, parent);
        }
        if (level >= 4) {
            Instantiate(spikes, new Vector3(x - 5, 6.5f, z - 8), Quaternion.identity, parent);
            Instantiate(spikes, new Vector3(x + 5, 6.5f, z - 8), Quaternion.identity, parent);
        }
        if (level >= 5) {
            Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z - 15), Quaternion.Euler(0, 270, 0), parent);
        }
    }

    public void TrenchWar(int x, int z, Transform parent) {
        Instantiate(block, new Vector3(x - 14, 1, z + 15), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 14, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x - 14, 1, z - 15), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 14, 1, z + 15), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 14, 1, z), Quaternion.identity, parent);
        Instantiate(block, new Vector3(x + 14, 1, z - 15), Quaternion.identity, parent);
        Instantiate(mine, new Vector3(x + 8, 0.125f, z - 4), Quaternion.identity, parent);
        
        Instantiate(mine, new Vector3(x + 4, 0.125f, z - 8), Quaternion.identity, parent);
        
        Instantiate(mine, new Vector3(x, 0.125f, z - 6), Quaternion.identity, parent);
        
        Instantiate(mine, new Vector3(x - 4, 0.125f, z - 9), Quaternion.identity, parent);
        
        Instantiate(mine, new Vector3(x - 10, 0.125f, z - 4), Quaternion.identity, parent);
        GameObject c = Instantiate(swordCrawler, new Vector3(x - 18, 0, z - 12), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(swordCrawler, new Vector3(x - 18, 0, z + 12), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(gunCrawler, new Vector3(x + 18, 0, z - 7), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(gunCrawler, new Vector3(x + 18, 0, z + 7), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        Instantiate(button, new Vector3(x - 20, 0.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x, 0.2f, z), Quaternion.identity, parent);
        Instantiate(button, new Vector3(x + 20, 0.2f, z), Quaternion.identity, parent);

        if (rng.Next(3) == 0) {
            Instantiate(healthCollectible, new Vector3(x + 3, 1, z - 6), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            GameObject turr = Instantiate(turret, new Vector3(x - 14, 2, z + 15), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turr = Instantiate(turret, new Vector3(x + 14, 2, z - 15), Quaternion.identity, parent);
            turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            Instantiate(mine, new Vector3(x + 6, 0.125f, z + 10), Quaternion.identity, parent);
        }
        if (level >= 3) {
            c = Instantiate(swordCrawler, new Vector3(x - 10, 0, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            c = Instantiate(gunCrawler, new Vector3(x + 18, 0, z), Quaternion.identity, parent);
            c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
            Instantiate(mine, new Vector3(x - 6, 0.125f, z), Quaternion.identity, parent);
            Instantiate(mine, new Vector3(x - 8, 0.125f, z + 8), Quaternion.identity, parent);
        }
        if (level >= 4) {
            GameObject turt = Instantiate(turret, new Vector3(x + 14, 2, z + 15), Quaternion.identity, parent);
            turt.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            turt = Instantiate(turret, new Vector3(x - 14, 2, z - 15), Quaternion.identity, parent);
            turt.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
            Instantiate(mine, new Vector3(x + 2, 0.125f, z + 1), Quaternion.identity, parent);
        }
        if (level >= 5) {
            GameObject l = Instantiate(laser, new Vector3(x, 4, z), Quaternion.Euler(0, 100, 0), parent);
            GameObject lb = Instantiate(laserButton, new Vector3(x - 14, 2.2f, z), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = l;
            lb = Instantiate(laserButton, new Vector3(x + 14, 2.2f, z), Quaternion.identity, parent);
            lb.GetComponent<LaserManager>().laser = l;
            Instantiate(mine, new Vector3(x - 2, 0.125f, z + 3), Quaternion.identity, parent);
        }
    }

    public void WaveAttack(int x, int z, Transform parent) {
        GameObject c = Instantiate(swordCrawler, new Vector3(x, 0, z - 12), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x, 0, z), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x - 4, 0, z - 4), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x + 4, 0, z - 4), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x, 0, z - 8), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x - 8, 0, z - 8), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        c = Instantiate(crawler, new Vector3(x + 8, 0, z - 8), Quaternion.identity, parent);
        c.GetComponent<CrawlerAI>().player = gameObject.GetComponent<WallCreator>().player;
        Instantiate(button, new Vector3(x, 0.2f, z + 20), Quaternion.identity, parent);
        if (rng.Next(3) == 0) {
            Instantiate(shieldCollectible, new Vector3(x, 1, z - 20), Quaternion.identity, parent);
        }
        int level = GameState.GetGame().level;

        if (level >= 2) {
            Instantiate(button, new Vector3(x - 13, 0.2f, z + 13), Quaternion.identity, parent);
        }
        if (level >= 3) {
            Instantiate(button, new Vector3(x + 13, 0.2f, z + 13), Quaternion.identity, parent);
        }
        if (level >= 4) {
            Instantiate(button, new Vector3(x - 20, 0.2f, z), Quaternion.identity, parent);
        }
        if (level >= 5) {
            Instantiate(button, new Vector3(x + 20, 0.2f, z), Quaternion.identity, parent);
        }
    }

    public void GuardedHealth(int x, int z, Transform parent) {
        Instantiate(healthCollectible, new Vector3(x + 2, 1f, z + 2), Quaternion.identity, parent);
        Instantiate(healthCollectible, new Vector3(x + 2, 1f, z - 2), Quaternion.identity, parent);
        Instantiate(healthCollectible, new Vector3(x - 2, 1f, z - 2), Quaternion.identity, parent);
        Instantiate(healthCollectible, new Vector3(x - 2, 1f, z + 2), Quaternion.identity, parent);
        GameObject turr = Instantiate(turret, new Vector3(x, 0, z), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x + 2, 0, z), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x - 2, 0, z), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x, 0, z - 2), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
        turr = Instantiate(turret, new Vector3(x, 0, z + 2), Quaternion.identity, parent);
        turr.GetComponent<TurretAI>().player = GetComponent<WallCreator>().player;
    }

    public void SpikedShield(int x, int z, Transform parent) {
        Instantiate(shieldCollectible, new Vector3(x, 1f, z), Quaternion.identity, parent);
        Instantiate(shieldCollectible, new Vector3(x + 3, 1f, z + 3), Quaternion.identity, parent);
        Instantiate(shieldCollectible, new Vector3(x + 3, 1f, z - 3), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x - 1, 0.5f, z), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 1, 0.5f, z + 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 1, 0.5f, z - 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z + 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z - 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z + 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z - 2), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 5, 0.5f, z - 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z - 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 3, 0.5f, z + 4), Quaternion.identity, parent);
        Instantiate(spikes, new Vector3(x + 7, 0.5f, z), Quaternion.identity, parent);
    }

    public void MagicMissile(int x, int z, Transform parent) {
        Instantiate(shieldCollectible, new Vector3(x, 1f, z), Quaternion.identity, parent);
        Instantiate(shieldCollectible, new Vector3(x + 1, 1f, z), Quaternion.identity, parent);
        GameObject missileObj = Instantiate(missile, new Vector3(x - 2, 1.5f, z - 2), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x - 4, 1.5f, z), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x - 2, 1.5f, z + 2), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x, 1.5f, z + 4), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x + 2, 1.5f, z + 2), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x + 4, 1.5f, z), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x + 2, 1.5f, z - 2), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x, 1.5f, z - 4), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
        missileObj = Instantiate(missile, new Vector3(x, 1.5f, z), Quaternion.identity, parent);
        missileObj.GetComponent<MissileAI>().player = gameObject.GetComponent<WallCreator>().player;
    }

    public void Trollzer(int x, int z, Transform parent) {
        Instantiate(healthCollectible, new Vector3(x + 2, 1f, z), Quaternion.identity, parent);
        Instantiate(healthCollectible, new Vector3(x, 1f, z - 2), Quaternion.identity, parent);
        GameObject l = Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z - 24.4f), Quaternion.Euler(0, 45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 0.33f;
        l = Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z - 22.4f), Quaternion.Euler(0, 45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 0.66f;
        l = Instantiate(laserBlaster, new Vector3(x - 22.4f, 2, z - 24.4f), Quaternion.Euler(0, 45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 1;
        l = Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z - 24.4f), Quaternion.Euler(0, -45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 1.33f;
        l = Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z - 22.4f), Quaternion.Euler(0, -45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 1.66f;
        l = Instantiate(laserBlaster, new Vector3(x + 22.4f, 2, z - 24.4f), Quaternion.Euler(0, -45, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 2f;
        l = Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z + 24.4f), Quaternion.Euler(0, -135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 2.33f;
        l = Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z + 22.4f), Quaternion.Euler(0, -135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 2.66f;
        l = Instantiate(laserBlaster, new Vector3(x + 22.4f, 2, z + 24.4f), Quaternion.Euler(0, -135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 3f;
        l = Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z + 24.4f), Quaternion.Euler(0, 135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 3.33f;
        l = Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z + 22.4f), Quaternion.Euler(0, 135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 3.66f;
        l = Instantiate(laserBlaster, new Vector3(x - 22.4f, 2, z + 24.4f), Quaternion.Euler(0, 135, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 4f;
        l = Instantiate(laserBlaster, new Vector3(x + 6, 2, z - 24.4f), Quaternion.Euler(0, 0, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 0.5f;
        l = Instantiate(laserBlaster, new Vector3(x - 6, 2, z + 24.4f), Quaternion.Euler(0, 180, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 1.5f;
        l = Instantiate(laserBlaster, new Vector3(x + 24.4f, 2, z + 6), Quaternion.Euler(0, -90, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 2.5f;
        l = Instantiate(laserBlaster, new Vector3(x - 24.4f, 2, z - 6), Quaternion.Euler(0, 90, 0), parent);
        l.GetComponent<LaserBlasterController>().offset = 3.5f;
    }
}

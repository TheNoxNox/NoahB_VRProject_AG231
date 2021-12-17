using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Player thePlayer;

    public GameObject zombiePrefab;

    public List<Zombie> Zombies = new List<Zombie>();

    public List<GameObject> SpawnPts = new List<GameObject>();

    public bool awaitingZombieSpawn = true;

    public float zombieSpawnCD = 7.5f;
    public float zombieSpawnTimer;

    public int maxZombieAmount = 7;

    public int ZombieCount => Zombies.Count;

    public float GameTime = 0f;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            zombieSpawnTimer = zombieSpawnCD;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameTime += Time.deltaTime;

        if (awaitingZombieSpawn)
        {
            zombieSpawnTimer -= Time.deltaTime;
            if(zombieSpawnTimer <= 0)
            {
                SpawnZombie();
                awaitingZombieSpawn = false;
            }
        }
        else
        {
            if(Zombies.Count < maxZombieAmount)
            {
                awaitingZombieSpawn = true;
                zombieSpawnTimer = zombieSpawnCD + Random.Range(-2.5f, 2.5f);
            }
        }

        if(GameTime >= 180f)
        {
            zombieSpawnCD = 4.5f;
            maxZombieAmount = 18;
        }
        else if(GameTime >= 120f)
        {
            zombieSpawnCD = 5.5f;
            maxZombieAmount = 13;
        }
        else if(GameTime >= 60f)
        {
            zombieSpawnCD = 6.5f;
            maxZombieAmount = 10;
        }
    }

    public void SpawnZombie()
    {
        int spawnPt = Random.Range(0, SpawnPts.Count);
        Zombie zomb = Instantiate(zombiePrefab, SpawnPts[spawnPt].transform.position, SpawnPts[spawnPt].transform.rotation).GetComponent<Zombie>();
        Zombies.Add(zomb);
    }

    public void RemoveZombie(Zombie zomb)
    {
        Zombies.Remove(zomb);
    }
}

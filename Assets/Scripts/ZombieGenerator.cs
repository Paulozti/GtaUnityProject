using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public System.Collections.Generic.List<GameObject> points;
    public GameObject zombiePrefab;
    private GameObject player;
    private bool shouldSpam = true;
    public int maxNumberOfZombies = 80;
    public int debug;
    public static int numberOfSpawnedZombies = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Timer());
    }

    
    void Update()
    {
        debug = numberOfSpawnedZombies;
    }

    private void SpawnZombie()
    {
        int rand = UnityEngine.Random.Range(0, (points.Count - 1));
        GameObject zombie = Instantiate(zombiePrefab, points[rand].transform.position, Quaternion.identity);
        zombie.transform.LookAt(player.transform);
        numberOfSpawnedZombies++;
    }

    IEnumerator Timer()
    {
        while (shouldSpam)
        {
            if(numberOfSpawnedZombies < maxNumberOfZombies)
            {
                SpawnZombie();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

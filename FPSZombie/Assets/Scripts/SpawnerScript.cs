using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public Transform[] spawnPoint;
    public GameObject[] whatToSpawn;
    public GameObject[] spawnClone;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;

    private void Start()
    {
        StartCoroutine(waitSpawner());
    }
    private void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randEnemy = Random.Range(0,1);
            spawnClone[randEnemy] = Instantiate(whatToSpawn[randEnemy], spawnPoint[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            //yield return new WaitForSeconds(spawnWait);
            spawnClone[randEnemy] = Instantiate(whatToSpawn[randEnemy], spawnPoint[1].transform.position, Quaternion.Euler(9, 0, 13)) as GameObject;
            yield return new WaitForSeconds(spawnWait);
        }
     }

}

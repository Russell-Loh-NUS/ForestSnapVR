using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {

    public GameObject[] dynamicMonsters;
    public GameObject[] staticMonsters;
    public GameObject[] dynamicSpawn;
    public GameObject[] staticSpawn;
    public float spawnInterval = 3.0f;

    float timer;
	// Use this for initialization
	void Start () {
        timer = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer <= 0){
            GameObject monster;
            Vector3 spawnPos;
            if(Random.Range(0,2) == 1){
                monster = staticMonsters[(int)Random.Range(0, staticMonsters.Length)];
                spawnPos = staticSpawn[(int)Random.Range(0, staticSpawn.Length)].transform.position;
            }
            else{
                monster = dynamicMonsters[(int)Random.Range(0, dynamicMonsters.Length)];
                spawnPos = dynamicSpawn[(int)Random.Range(0, dynamicSpawn.Length)].transform.position;
            }

            Quaternion monsterRotation = Quaternion.Euler(monster.transform.rotation.x, Random.Range(0, 360), monster.transform.rotation.z);
            Instantiate(monster, spawnPos, monsterRotation);

            timer = spawnInterval;
        }
        timer -= Time.deltaTime;
	}
}

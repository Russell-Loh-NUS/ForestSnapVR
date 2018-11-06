using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] dynamicMonsters;
    public GameObject[] staticMonsters;
    public GameObject[] dynamicSpawn;
    public GameObject[] staticSpawn;
    public float spawnInterval = 3.0f;

    public int score;
    public Text scoreText;
    public Text timerText;

    public AudioClip soundEffect;
    public AudioClip background;
    public AudioSource soundSource;
    public AudioSource bgSource;

    float timer;

    public int gameTime = 30;

	// Use this for initialization
	void Start () {
        StartCoroutine("LoseTime");
        timer = spawnInterval;
        soundSource.clip = soundEffect;
        bgSource.clip = background;
        bgSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Time: " + gameTime;
        if (gameTime <= 0) {
            StopCoroutine("LoseTime");
        }

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

    public void AddScore(int newScoreValue) {
        soundSource.Play();
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            gameTime--;
        }
    }
}

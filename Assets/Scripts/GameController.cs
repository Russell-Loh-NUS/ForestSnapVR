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
    public int gameState = 0;

    public int score;
    public Text scoreText;
    public Text endScoreText;
    public Text timerText;
    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject gamePanel;

    public AudioClip soundEffect;
    public AudioClip background;
    public AudioSource soundSource;
    public AudioSource bgSource;

    float timer;

    public int gameTime = 30;
    int initialGameTime = 0;

    GameObject[] creatures;

	// Use this for initialization
	void Start () {
        timer = spawnInterval;
        initialGameTime = gameTime;
        soundSource.clip = soundEffect;
        bgSource.clip = background;
        bgSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == 1)
        {
            timerText.text = "Time: " + gameTime;
            if (gameTime <= 0)
            {
                StopCoroutine("LoseTime");
                stopSpawn();
                endScoreText.text = "Your Score: " + score;
                gamePanel.SetActive(false);
            }

            if (timer <= 0)
            {
                GameObject monster;
                Vector3 spawnPos;
                if (Random.Range(0, 2) == 1)
                {
                    monster = staticMonsters[(int)Random.Range(0, staticMonsters.Length)];
                    spawnPos = staticSpawn[(int)Random.Range(0, staticSpawn.Length)].transform.position;
                }
                else
                {
                    monster = dynamicMonsters[(int)Random.Range(0, dynamicMonsters.Length)];
                    spawnPos = dynamicSpawn[(int)Random.Range(0, dynamicSpawn.Length)].transform.position;
                }

                Quaternion monsterRotation = Quaternion.Euler(monster.transform.rotation.x, Random.Range(0, 360), monster.transform.rotation.z);
                Instantiate(monster, spawnPos, monsterRotation);

                timer = spawnInterval;
            }
            timer -= Time.deltaTime;
        }
        else if(gameState == 2){
            //END GAME
            if (Input.GetMouseButtonDown(0))
            {
                gamePanel.SetActive(true);
                startSpawn();
            }
        }
        else{
            //START GAME
            if (Input.GetMouseButtonDown(0))
            {
                gamePanel.SetActive(true);
                startSpawn();
            }
        }
	}

    public void AddScore(int newScoreValue) {
        soundSource.Play();
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    void startSpawn()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        timer = spawnInterval;
        gameTime = initialGameTime;
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        StartCoroutine("LoseTime");
        gameState = 1;
    }

    void stopSpawn()
    {
        endPanel.SetActive(true);
        gameState = 2;
        creatures = GameObject.FindGameObjectsWithTag("Creatures");
        foreach (GameObject creature in creatures) {
            Destroy(creature);
        }
    }

    //countdown timer
    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            gameTime--;
        }
    }
}

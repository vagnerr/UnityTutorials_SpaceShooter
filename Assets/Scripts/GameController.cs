using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text waveText;

    private bool gameOver;
    private bool restart;
    private int score;
    private int wave;


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true) {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
                     
            }
            wave++;
            UpdateWave();
        }
    }

    public int Wave()
    {
        return wave;
    }

	// Use this for initialization
	void Start () {
        score = 0;
        wave = 1;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        UpdateWave();

        StartCoroutine(SpawnWaves());

	}

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    void UpdateWave()
    {
        waveText.text = "Wave: " + wave;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
        
    }
    public void AddScore( int newScoreValue)
    {
        score += ( newScoreValue + ((wave-1)*5)); // Todo: expose wave multiplier
        UpdateScore();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}

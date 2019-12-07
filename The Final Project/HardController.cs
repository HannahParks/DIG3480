using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HardController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text PointText;
    public Text winText;
    public Text restartText;
    public Text GameOverText;
    public Text countdown;
    public Text hardText;
    public Text playText;

    public int points;
    public int timeLeft = 30;

    public bool restart;
    public bool gameOver;

    public AudioSource musicSource;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        GameOverText.text = "";
        winText.text = "";
        hardText.text = "";
        playText.text = "";
        points = 0;
        StartCoroutine("LoseTime");
        Time.timeScale = 1;

        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        countdown.text = ("" + timeLeft);

        if (timeLeft == 0)
        {
            GameObject.FindWithTag("Player").SetActive(false);
            GameOver();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                playText.text = "Press 'N' for Normal Mode";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        points += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        PointText.text = "Points: " + points;
        if (points >= 100)
        {
            winText.text = "You win! Game Created by Hannah Parks";
            gameOver = true;
            restart = true;
            GameObject.FindWithTag("Player").SetActive(false);
            musicSource.clip = winMusic;
            musicSource.Play();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = loseMusic;
        musicSource.Play();

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject enemyTwoPrefab;
    public GameObject powerupPrefab;
    public GameObject gameOverMenu;
    public GameObject audioPlayer;
    public GameObject coinPrefab;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        gameOver = false;
        Instantiate(playerPrefab, transform.position = new Vector3(transform.position.x * 1, -verticalScreenSize + 3.5f), Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1, 3);
        InvokeRepeating("CreateEnemyTwo", Random.Range(3,9), Random.Range(2,5));

        StartCoroutine(SpawnPowerup());
        powerupText.text = "No power ups Yet!";

        StartCoroutine(SpawnCoins());
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        scoreText.text = "Score: " + score.ToString();
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateEnemyTwo()
    {
       Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }
    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, verticalScreenSize * .8f), 0), Quaternion.identity);
    }
    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }
    void CreateCoins()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, verticalScreenSize * .8f), 0), Quaternion.identity);
    }
    IEnumerator SpawnCoins()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreateCoins();
        StartCoroutine(SpawnCoins());
    }
public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }

public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
            audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
            break;

            case 2:
            audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
            break;
        }
    }
//ontriggerenter2d AddScore(1)
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void GameOver()
    {
            //set out game over object menu to be true
        gameOverMenu.SetActive(true);
        //game over to be true
        gameOver = true;
    }
}

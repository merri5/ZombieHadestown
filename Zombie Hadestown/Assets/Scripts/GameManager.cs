using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Threading;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public Text clickToStart;
    public Text scoreText;
    public Text scoreDeathText;
    private int score = 0;
    private static int highScore = 0;
    private float scoreFloat = 0;

    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;
    private bool replayed = false;


    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deathMenu;


    public bool PlayerActive
    {
        get { return playerActive; }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }

    public bool GameStarted
    {
        get { return gameStarted; }
    }

    public bool Replayed
    {
        get { return replayed; }
        set { replayed = value; }
    }
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Assert.IsNotNull(mainMenu);
        Assert.IsNotNull(deathMenu);
        Assert.IsNotNull(scoreText);
    }


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        highScore = PlayerPrefs.GetInt("highScore");
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted && playerActive)
        {
            scoreFloat +=  0.7f * Time.deltaTime;
            if(scoreFloat >= 1)
            {
                score++;
                scoreFloat--;
                scoreText.text = "Score: " + score;
            }
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.8f);
        deathMenu.SetActive(true);

    }

    public void PlayerCollided()
    {
        gameOver = true;
        StartCoroutine(Wait());
        
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore = score;
        }
        scoreDeathText.text = "Score: " + score + "\nHighscore: " + highScore;
        score = 0;
        scoreFloat = 0;
        
    }
    public void PlayerStartedGame()
    {
        playerActive = true;
        clickToStart.enabled = false;
    }

    public void EnterGame()
    {
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        gameStarted = true;
        
    }

    public void EnterReplayedGame()
    {
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        gameStarted = true;
        gameOver = false;
        playerActive = false;
        replayed = true;
        clickToStart.enabled = true;

    }
    public void AddPoint()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
    

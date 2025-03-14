using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Score
     private int score;

    // Reference the Player
    public GameObject Player;

    // UI Objects
    public Text scoreText;
    public GameObject winText;
    public GameObject loseText;
    public GameObject menuPanel;
    public Text timeText;

    [HideInInspector]
    public GameObject[] Collectables;

    [HideInInspector]
    public int CollectablesAtStart;

    [HideInInspector]
    public int CollectablesAtPresent;

    private AudioSource music;

    // Timer Variables
    public float timeRemaining = 120f; // 2 mintues
    public bool timerIsRunning = false;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        // Calculate the number of collectables at start of scene
        Collectables = GameObject.FindGameObjectsWithTag("pickup");
        CollectablesAtStart = Collectables.Length;
        // Debug.Log(CollectablesAtStart);

        // Unpause game from restart menu
        Time.timeScale = 1;

        // start timer
        timerIsRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the number of collectables left at every frame
        Collectables = GameObject.FindGameObjectsWithTag("pickup");
        CollectablesAtPresent = Collectables.Length;

        SetScoreText();
        WinCondition();
        TimerCountdown();
        LoseCondition();

        

        
    }


    void SetScoreText()
    {
        score = (CollectablesAtStart - CollectablesAtPresent) * 10;
        // Display score converted to a string
        scoreText.text = "Score: " + score.ToString();
    }


    void WinCondition()
    {
       

        if (CollectablesAtPresent <= 0)
        {
            menuPanel.SetActive(true);
            winText.SetActive(true);
            // Pause time and stop background music
            Time.timeScale = 0;
            music.Stop();
        }
            

        

    }

    void LoseCondition()
    {
        if (Player.transform.position.y <= -5 || timerIsRunning == false)
        {
            // Debug.Log("Game Lost");
            menuPanel.SetActive(true);
            loseText.SetActive(true);
            // Pause time and stop background music
            Time.timeScale = 0;
            music.Stop();
        }
    }

    void TimerCountdown()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }  
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

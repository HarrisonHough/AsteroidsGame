﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Game Manager Class (Singleton)
*/

/// <summary>
/// GameManager is the Central "always alive" singleton class responsible for global events and functions
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static int score;
    public static int lastScore;
    public static int highScore;

    public GameObject playerPrefab;
    public SpawnController spawner;
    public UIControl uiControl;    
    public int lives;    
    public Player player;

    private SoundController _soundControl;
    private bool _gameOver = false;
    

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Awake() {
        EnforceSingleton();
        Inititialize();
    }

    /// <summary>
    /// 
    /// </summary>
    private void EnforceSingleton() {
        //Enforces singleton so there is only one instance, multiples will self delete.
        //Game Manager kept alive throughout scene changes.
        if (instance == null)
            instance = this;
        else if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Inititialize()
    {
        if (!PlayerPrefs.HasKey("LastScore"))
        {
            PlayerPrefs.SetInt("LastScore", 0);
            PlayerPrefs.Save();
        }
        else
            lastScore = PlayerPrefs.GetInt("LastScore");

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
        }
        else
            highScore = PlayerPrefs.GetInt("HighScore");

        if (player == null)
            player = FindObjectOfType<Player>();
        player.gameObject.SetActive(false);
        _soundControl = GetComponent<SoundController>();
    }

    /// <summary>
    /// Called on scene start
    /// </summary>
    void Start() {
        //check for null references
        //assign if null
        if (spawner == null)
            spawner = FindObjectOfType<SpawnController>();
        if (uiControl == null)
            uiControl = FindObjectOfType<UIControl>();
    }

    /// <summary>
    /// This function controls the asteroid collision event
    /// </summary>
    /// <param name="asteroid"></param>
    public void AsteroidHit(Asteroid asteroid) {
        if (!_gameOver)
        {
            int type = (int)asteroid.m_Type;
            switch (type)
            {
                case 0:
                    AddScore(10);
                    break;
                case 1:
                    AddScore(25);
                    spawner.SpawnSmallAsteroid(asteroid.transform.position);
                    break;
                case 2:
                    AddScore(50);
                    spawner.SpawnMediumAsteroid(asteroid.transform.position);
                    break;

            }
        }
        _soundControl.PlayerExplode();

        Destroy(asteroid.gameObject);
    }

    /// <summary>
    /// Destroys asteroid when it collides with the border
    /// </summary>
    /// <param name="asteroid"></param>
    public void AsteroidHitBorder(Asteroid asteroid) {

        Destroy(asteroid.gameObject);
    }

    /// <summary>
    /// Adds score to global value and updates UI
    /// </summary>
    /// <param name="scoreToAdd"></param>
    private void AddScore(int scoreToAdd) {
        //add to score
        score += scoreToAdd;

        //update score UI
        uiControl.UpdateScore(score);

    }

    /// <summary>
    /// Spawns player at zero position
    /// </summary>
    public void SpawnPlayer() {
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when players dies this function controls lives count
    /// and player spawning
    /// </summary>
    public void PlayerDeath() {
        //check number of lives
        if (lives > 0)
        {
            //decrement lives count
            lives--;
            //update lives UI
            uiControl.UpdateLives(lives);
            //spawn player after a delay
            StartCoroutine(DelaySpawn());
        }
        else
            GameOver();
    }

    /// <summary>
    /// This coroutine is used to spawn the player after a delay (seconds)
    /// </summary>
    /// <returns></returns>
    IEnumerator DelaySpawn() {
        //1 second delay
        yield return new WaitForSeconds(1);
        
        SpawnPlayer();
    }
    /// <summary>
    /// Stops spawning and enables Home screen, this function is 
    /// called when the games over
    /// </summary>
    void GameOver() {
        _gameOver = true;
        spawner.StopSpawning();
        uiControl.ShowHomeScreen();
        CheckForHighScore();

        
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckForHighScore() {
        if (score > highScore)
        {
            SetHighScore();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetHighScore() {
        highScore = score;
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();

        uiControl.UpdateHomeUIScores();
    }

    /// <summary>
    /// Resets global values and restarts the game loop
    /// </summary>
    public void StartGame()
    {
        SetLastScore();
        ResetGameValues();
        ResetUIDisplayText();
        SpawnPlayer();
        spawner.StartSpawning();
        _gameOver = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetLastScore()
    {
        lastScore = score;
        PlayerPrefs.SetInt("LastScore", lastScore);
        PlayerPrefs.Save();
        uiControl.UpdateHomeUIScores();
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void ResetGameValues()
    {
        //reset values
        score = 0;
        lives = 3;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetUIDisplayText() {
        //reset ui display text
        uiControl.UpdateScore(0);
        uiControl.UpdateLives(3);
    }

    /// <summary>
    /// Loads scores from file and assigns it to global variables
    /// </summary>
    public void LoadScores() {
        if (PlayerPrefs.HasKey("LastScore")) {
            lastScore = PlayerPrefs.GetInt("LastScore");
        }
        if (PlayerPrefs.HasKey("HighScore")) {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }
}
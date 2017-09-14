using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: UI Control Class
*/

/// <summary>
/// UI Control class controls the display canvas and
/// all events
/// </summary>
public class UIControl : MonoBehaviour {

    #region Public Variables
    public Text scoreText;
    public Text livesText;

    public Text lastScoreText;
    public Text highScoreText;

    public GameObject gameUIPanel;
    public GameObject homeScreenPanel;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        UpdateHomeUIScores();
	}

    /// <summary>
    /// Updates Score text element
    /// </summary>
    /// <param name="score">Int: Score value to display</param>
    public void UpdateScore(int score) {
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Updates Lives text element
    /// </summary>
    /// <param name="lives">Lives: Lives value to display</param>
    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    /// <summary>
    /// Updates Score on HomeUI (menu) panel
    /// </summary>
    public void UpdateHomeUIScores() {
        lastScoreText.text = "Last Score : " + GameManager.lastScore;
        highScoreText.text = "Best Score : " + GameManager.highScore;
    }

    /// <summary>
    /// Enables Home screen panel
    /// </summary>
    public void ShowHomeScreen() {
        homeScreenPanel.SetActive(true);
    }

    /// <summary>
    /// Triggers game start/ restart
    /// </summary>
    public void StartGame() {
        homeScreenPanel.SetActive(false);
        GameManager.instance.StartGame();
    }

    /// <summary>
    /// Exit/Quits the game
    /// </summary>
    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }
}

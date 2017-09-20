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
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _livesText;

    [SerializeField]
    private Text _lastScoreText;
    [SerializeField]
    private Text _highScoreText;

    [SerializeField]
    private GameObject _gameUIPanel;
    [SerializeField]
    private GameObject _homeScreenPanel;
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
        _scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Updates Lives text element
    /// </summary>
    /// <param name="lives">Lives: Lives value to display</param>
    public void UpdateLives(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }

    /// <summary>
    /// Updates Score on HomeUI (menu) panel
    /// </summary>
    public void UpdateHomeUIScores() {
        _lastScoreText.text = "Last Score : " + GameManager.lastScore;
        _highScoreText.text = "Best Score : " + GameManager.highScore;
    }

    /// <summary>
    /// Enables Home screen panel
    /// </summary>
    public void ShowHomeScreen() {
        _homeScreenPanel.SetActive(true);
    }

    /// <summary>
    /// Triggers game start/ restart
    /// </summary>
    public void StartGame() {
        _homeScreenPanel.SetActive(false);
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

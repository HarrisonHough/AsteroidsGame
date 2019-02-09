﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
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

    [SerializeField]
    private int _dynamicTextArrayLength = 10;
    private int _dynamicTextArrayIndex = 0;
    [SerializeField]
    private GameObject _dynamicTextArrayHolder;
    private DynamicText[] _dynamicTextArray;

    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        UpdateHomeUIScores();
        CreateTextArray();
	}

    private void CreateTextArray()
    {
        _dynamicTextArray = new DynamicText[_dynamicTextArrayLength];
        GameObject dynamicTextObject = _dynamicTextArrayHolder.transform.GetChild(0).gameObject;

        //assign first dynamic text as it already exists
        _dynamicTextArray[0] = dynamicTextObject.GetComponent<DynamicText>();
        for (int i = 1; i < _dynamicTextArrayLength; i++)
        {
            //create new dynamic text by duplicating the existing one and parent to dynamicTextArrayHolder
            _dynamicTextArray[i] = Instantiate(dynamicTextObject, _dynamicTextArrayHolder.transform).GetComponent<DynamicText>();
        }

    }

    public void ShowTextAtPosition(string textToDisplay, Vector3 worldPosition)
    {
        _dynamicTextArray[_dynamicTextArrayIndex].SetTextAndPosition(textToDisplay, worldPosition);
        _dynamicTextArrayIndex++;
        if (_dynamicTextArrayIndex >= _dynamicTextArrayLength)
        {
            _dynamicTextArrayIndex = 0;
        }
    }

    public void ShowTextAtPosition(string textToDisplay, Vector2 screenPosition)
    {

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
        _lastScoreText.text = "Last Score : " + GameManager.LastScore;
        _highScoreText.text = "Best Score : " + GameManager.HighScore;
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
        GameManager.Instance.StartGame();
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
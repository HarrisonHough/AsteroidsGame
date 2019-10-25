using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    #region Local Variables
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI lastScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private GameObject gameUIPanel;
    [SerializeField]
    private GameObject homeScreenPanel;

    [SerializeField]
    private int dynamicTextArrayLength = 10;
    private int dynamicTextArrayIndex = 0;
    [SerializeField]
    private GameObject dynamicTextArrayHolder;
    private DynamicText[] dynamicTextArray;

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
        dynamicTextArray = new DynamicText[dynamicTextArrayLength];
        GameObject dynamicTextObject = dynamicTextArrayHolder.transform.GetChild(0).gameObject;

        //assign first dynamic text as it already exists
        dynamicTextArray[0] = dynamicTextObject.GetComponent<DynamicText>();
        for (int i = 1; i < dynamicTextArrayLength; i++)
        {
            //create new dynamic text by duplicating the existing one and parent to dynamicTextArrayHolder
            dynamicTextArray[i] = Instantiate(dynamicTextObject, dynamicTextArrayHolder.transform).GetComponent<DynamicText>();
        }

    }

    public void ShowTextAtPosition(string textToDisplay, Vector3 worldPosition)
    {
        dynamicTextArray[dynamicTextArrayIndex].SetTextAndPosition(textToDisplay, worldPosition);
        dynamicTextArrayIndex++;
        if (dynamicTextArrayIndex >= dynamicTextArrayLength)
        {
            dynamicTextArrayIndex = 0;
        }
    }

    public void ShowTextAtPosition(string textToDisplay, Vector2 screenPosition)
    {
        dynamicTextArray[dynamicTextArrayIndex].SetTextAndPosition(textToDisplay, screenPosition);

        dynamicTextArrayIndex++;
        if (dynamicTextArrayIndex >= dynamicTextArrayLength)
        {
            dynamicTextArrayIndex = 0;
        }
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
        lastScoreText.text = "Last Score : " + GameManager.LastScore;
        highScoreText.text = "Best Score : " + GameManager.HighScore;
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
        GameManager.Instance.StartGame();
    }

    /// <summary>
    /// Exit/Quits the game
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

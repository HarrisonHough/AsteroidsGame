using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Dynamic Text Class
*/

public class DynamicText : MonoBehaviour {
    
    private Text _text;
    private RectTransform _rectTransform;
    [SerializeField]
    private float _displayTime = 2f;

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
        _rectTransform = GetComponent<RectTransform>();
	}

    public void SetTextAndPosition(string textToDisplay, Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        _text.text = textToDisplay;
        _rectTransform.position = screenPosition;
        //clear after delay
        Invoke("ClearDynamicText", _displayTime);
    }

    private void ClearDynamicText()
    {
        _text.text = "";
    }
}

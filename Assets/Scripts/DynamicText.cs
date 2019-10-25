using UnityEngine;
using TMPro;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Dynamic Text Class
*/

public class DynamicText : MonoBehaviour {
    
    private TextMeshProUGUI text;
    private RectTransform rectTransform;
    [SerializeField]
    private float displayTime = 2f;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
	}

    public void SetTextAndPosition(string textToDisplay, Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        text.text = textToDisplay;
        rectTransform.position = screenPosition;
        //clear after delay
        Invoke("ClearDynamicText", displayTime);
    }

    private void ClearDynamicText()
    {
        text.text = "";
    }
}

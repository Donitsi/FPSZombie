using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
    
    public Button retryButton;
    public Button exitButton;
    public int scoreValue;
    // Use this for initialization

    public Text text;

    public void Start ()
    {
        //retryButton = retryButton.GetComponent<Button>();
        //exitButton = exitButton.GetComponent<Button>();
        //text = text.GetComponent<Text>();


        text.text = "You scored: " + ScoreManager.score;

    }
    
    public void RetryPressed()
    {
        // reset the level
        Application.LoadLevel(1);
    }

    public void ExitPressed()
    {
        // return to main menu
        Application.LoadLevel(0);   
        
    }
	
	// Update is called once per frame
	void Update () {
        //text.text = "You scored: " + ScoreManager.score;
    }
}

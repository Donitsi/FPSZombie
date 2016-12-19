using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas optionsMenu;
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public Button applyButton;
    public Options options;

	// Use this for initialization
	void Start () {

        // get components for the public variables
        quitMenu = quitMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        applyButton = applyButton.GetComponent<Button>();
        quitMenu.enabled = false;
        optionsMenu.enabled = false;

        AudioListener.volume = Options.GetVolume();
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startButton.enabled = false;
        optionsButton.enabled = false;
        exitButton.enabled = false;
    }

    public void OptionsPressed()
    {
        optionsMenu.enabled = true;
        startButton.enabled = false;
        optionsButton.enabled = false;
        exitButton.enabled = false;        
    }
    public void ApplyPressed()
    {
        optionsMenu.enabled = false;
        startButton.enabled = true;
        optionsButton.enabled = true;
        exitButton.enabled = true;
        options.Save();
        AudioListener.volume = Options.GetVolume();
    }

    public void NoPressed()
    {
        quitMenu.enabled = false;
        startButton.enabled = true;
        optionsButton.enabled = true;
        exitButton.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

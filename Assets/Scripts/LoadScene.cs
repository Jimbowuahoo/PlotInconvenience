using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    private int index = 0;
    public GameObject ui;
    private Text OptionsText;
	// Use this for initialization
	void Start () {
        OptionsText = ui.transform.Find("Options").Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadGame()
    {
        SceneManager.LoadScene("Scenes/game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeText()
    {
        if (index == 0)
        {
            OptionsText.fontSize = 40;
            OptionsText.text = "You have no options";
            index++;
        }
        else if (index == 1)
        {
            OptionsText.text = "No Choice";
            index++;
        }
        else if (index == 2)
        {
            OptionsText.text = "Nope";
            index++;
        }
        else if (index == 3)
        {
            OptionsText.text = "Nada";
            index++;
        }
        else if (index == 4)
        {
            OptionsText.text = "You're persistant";
            index++;
        }
        else if (index == 5)
        {
            OptionsText.text = "No Thank You";
            index++;
        }
        else if (index == 6)
        {
            OptionsText.text = "Not Buying";
            index++;
        }
        else if (index == 7)
        {
            OptionsText.text = "I'm not your friend";
            index++;
        }
        else if (index == 8)
        {
            OptionsText.text = "Please Stop...";
            index++;
        }
        else if (index == 9)
        {
            OptionsText.text = "I am genuinely concerned for your health";
            index++;
        }
        else if (index == 10)
        {
            OptionsText.text = "Go out";
            index++;
        }
        else if (index == 11)
        {
            OptionsText.text = "Do something with your life";
            index++;
        }
        else if (index == 12)
        {
            OptionsText.text = "Fine, let's just move on";
            index++;
        }
        else
        {
            OptionsText.text = "Just Play The Game";
        }
    }
}

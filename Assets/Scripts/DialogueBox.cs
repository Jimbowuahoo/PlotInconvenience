using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour {

    public bool isOn = false;
    public Sprite box;
    public Sprite boxRetro;
    public Sprite inactive;
    public Camera main;
    public SpriteRenderer sr;

    bool turnOff = false;

    private AudioSource soundGen;

    DialogueList[] dList;
    public Font currentFont;

    public GameObject worldObject;
    private WorldManager world;

    private Text textUI;
    private string fileName = "Dialogue.json";
    private string filePath;
    

    private string[] dialogue = new string[0];
    private int index = 0;
	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = box;
        world = worldObject.GetComponent<WorldManager>();
        textUI = gameObject.transform.Find("Canvas").Find("Text").GetComponent<Text>();
        currentFont = Resources.Load<Font>("kenvector_future");
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string dia = File.ReadAllText(filePath);
        dList = JsonHelper.getJsonArray<DialogueList>(dia);
        soundGen = worldObject.transform.Find("SFX").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        
        if (isOn)
        {
            if (world.isRetro)
            {
                sr.sprite = boxRetro;
                currentFont = Resources.Load<Font>("kenpixel_mini_square");
            }
            else
            {
                sr.sprite = box;
                currentFont = Resources.Load<Font>("kenvector_future");
            }
        }
        else
        {
            sr.sprite = inactive;
        }
        gameObject.transform.position = new Vector3(main.transform.position.x, main.transform.position.y - 1.4f, 0);
        
        if (Input.GetKeyDown(KeyCode.Space) && isOn)
        {
            print(index);
            if (index < dialogue.Length - 1)
            {
                index++;
                textUI.text = dialogue[index];
                soundGen.clip = Resources.Load<AudioClip>("Assets/Music/Blip");
                soundGen.Play();
                if (textUI.text == "fuck this game.")
                {
                    turnOff = true;
                }
                    
            }
            else
            {
                textUI.text = "";
                isOn = false;
                index = 0;
                if (turnOff)
                {
                    SceneManager.LoadScene("Scenes/menu");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TurnOn(0);
        }
        textUI.font = currentFont;
    }

    public void TurnOn(int index)
    {
        string[] d = dList[index].lines;
        isOn = true;
        index = 0;
        dialogue = d;      
        textUI.text = d[0];
        soundGen.clip = Resources.Load<AudioClip>("Assets/Music/Blip");
        soundGen.Play();
    }
}

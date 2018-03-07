using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public bool isRetro = false;
    public GameObject stageManagerObject;
    public GameObject playerObject;

    private bool isMute= false;

    private StageManager stageManager;
    private Player player;

    private AudioSource audioReg;
    private AudioSource audioRet;

    // Initialize Audio
    void Start () {
        player = playerObject.GetComponent<Player>();
        stageManager = stageManagerObject.GetComponent<StageManager>();
        audioReg = transform.Find("AudioReg").GetComponent<AudioSource>();
        audioRet = transform.Find("AudioRet").GetComponent<AudioSource>();
        audioReg.clip= stageManager.currentStage.reg;
        audioRet.clip = stageManager.currentStage.ret;
        audioRet.Play();
        audioReg.Play();
    }
	
	// Update Audio
	void Update () {
        audioReg.clip = stageManager.currentStage.reg;
        audioRet.clip = stageManager.currentStage.ret;
        if (!audioReg.isPlaying)
        {
            audioReg.Play();
        }
        if (!audioRet.isPlaying)
        {
            audioRet.Play();
        }

        if (isRetro && !isMute)
        {
            audioReg.mute = true;
            audioRet.mute = false;
        }
        else if (!isMute)
        {
            audioReg.mute = false;
            audioRet.mute = true;
        }
        else
        {
            audioReg.mute = true;
            audioRet.mute = true;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRetro = !isRetro;
            foreach (Transform child in stageManagerObject.transform)
            {
                child.GetComponent<Stage>().ChangeSprite();
            }
            
            print(isRetro);
        }*/

        if (Input.GetKeyDown(KeyCode.M))
        {
            isMute = !isMute;
        }
        

    }

    public void Change()
    {
        isRetro = !isRetro;
        foreach (Transform child in stageManagerObject.transform)
        {
            child.GetComponent<Stage>().ChangeSprite();
        }
    }
}

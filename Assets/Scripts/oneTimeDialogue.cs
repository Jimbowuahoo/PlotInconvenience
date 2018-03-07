using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneTimeDialogue : MonoBehaviour {

    public GameObject dialogueObject;
    private DialogueBox dialogue;
    public int requiredState;

    public GameObject worldObject;
    private WorldManager world;

    public int dialogueIndex;
    
    void Start () {
        world = worldObject.GetComponent<WorldManager>();
        dialogue = dialogueObject.GetComponent<DialogueBox>();
    }
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (world.isRetro && requiredState == 0)//display when retro
        {
            dialogue.TurnOn(dialogueIndex);
            Destroy(gameObject);
        }
        else if (!world.isRetro && requiredState == 1)
        {
            dialogue.TurnOn(dialogueIndex);
            Destroy(gameObject);
        }
        else if (requiredState == 2)//play either way
        {
            dialogue.TurnOn(dialogueIndex);
            Destroy(gameObject);
        }
    }
}

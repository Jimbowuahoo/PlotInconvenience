using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public GameObject worldObject;
    private WorldManager world;

    public Sprite reg;
    public Sprite ret;

    public AudioClip soundReg;
    public AudioClip soundRet;

    private SpriteRenderer spriteRenderer;

    public int type;
    public int dialogueIndex = 0;
    public int dialogueIndex2 = 0;

    public int state = 0;
    private Vector2 location;

	// Initialize position and get world
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        location = transform.position;
        world = worldObject.GetComponent<WorldManager>();
    }
	
	// Update the position of the interactable object
	void Update () {

		if (type == 3)
        {
            if (state == 0)
            {
                transform.position = location;
                transform.rotation = new Quaternion(0, 0, 90, 90);
            }
            else
            {
                transform.position = new Vector2 (location.x - 1f, location.y + 0.7f);
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
        }

        //changing the interactable sprite
        if (world.isRetro)
        {
            spriteRenderer.sprite = ret;
        }
        else
        {
            spriteRenderer.sprite = reg;
        }
	}

    public void ChangeState(int change)
    {
        state = change;
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for step-on buttons in the game
public class Button : MonoBehaviour {
    public int type = 0;
    public GameObject doorObject;
    private Interactable door;
    bool isDown = false;
    private SpriteRenderer spriteRenderer;

    public Sprite up;
    public Sprite down;

    private AudioSource soundGen;

    public Sprite upRet;
    public Sprite downRet;

    public GameObject worldObject;
    private WorldManager world;


    //Initialize
    void Start () {
        door = doorObject.GetComponent<Interactable>();
        world = worldObject.GetComponent<WorldManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = up;
        soundGen = worldObject.transform.Find("SFX").GetComponent<AudioSource>();
    }
	
	// Updating the gamestate based on if the button is down
	void Update () {
		if (isDown)
        {
            if (world.isRetro)
            {
                spriteRenderer.sprite = downRet;
            }
            else
            {
                spriteRenderer.sprite = down;
            }
            
        }
        else
        {
            if (world.isRetro)
            {
                spriteRenderer.sprite = upRet;
            }
            else
            {
                spriteRenderer.sprite = up;
            }
        }
	}

    //Perform actions based on the Type of button (Blue, Red, Purple)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (world.isRetro)
            {
                if (type == 0)
                {
                    if (!isDown)
                    {
                        soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                        soundGen.Play();
                    }
                    door.ChangeState(1);
                    isDown = true;
                }
                if (type == 1)
                {
                    soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                    soundGen.Play();
                    if (door.state == 0)
                    {
                        door.ChangeState(1);
                    }
                    else
                    {
                        door.ChangeState(0);
                    }

                }
                if (type == 2)
                {
                    
                    door.ChangeState(1);
                    soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                    soundGen.Play();

                }
            }
        }
        if (collision.tag == "Interactable")
        {
            if (type == 0)
            {
                if (!isDown)
                {
                    soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                    soundGen.Play();
                }
                door.ChangeState(1);
                isDown = true;
                
            }
            if (type == 1)
            {
                if (door.state == 0)
                {
                    door.ChangeState(1);
                    soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                    soundGen.Play();
                }
                else
                {
                    door.ChangeState(0);
                    soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                    soundGen.Play();
                }

            }
            if (type == 2)
            {
                soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                soundGen.Play();
            }
        }
        
    }

    //How is the button affected by staying on it?
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (world.isRetro)
            {
                isDown = true;
                
            }
        }

        if (collision.tag == "Interactable")
        {
            isDown = true;
            if (type == 2)
            {
                door.ChangeState(1);

            }
        }
    }

    //How is the button affected by exiting?
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (type != 0)
        {
            if (isDown)
            {
                isDown = false;
                soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                soundGen.Play();
            }
            
            
            
        }
        if (type == 2)
        {
            door.ChangeState(0);
            if (world.isRetro)
            {
                soundGen.clip = Resources.Load<AudioClip>("Assets/Music/buttonRet");
                soundGen.Play();
            }
            

        }
    }
}

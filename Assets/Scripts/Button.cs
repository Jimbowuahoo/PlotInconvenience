using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Use this for initialization
    void Start () {
        door = doorObject.GetComponent<Interactable>();
        world = worldObject.GetComponent<WorldManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = up;
        soundGen = worldObject.transform.Find("SFX").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
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

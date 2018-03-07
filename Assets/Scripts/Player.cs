using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    protected Animator anim;
    protected Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;
    public GameObject worldObject;
    private WorldManager world;

    public int maxSpeed = 7;
    private bool isRight = false;

    public GameObject dialogueObject;
    private DialogueBox dialogue;

    public GameObject inventoryRenderer;
   
    private int pickAxeNumber = 0;

    private Image pickaxeImage;
    private Text pickaxeText;

    private AudioSource soundGen;

    private Interactable moving;

    private bool isInteracting = false;

    private bool canPickUp = false;

    bool hasKey = false;
    

    private float moveH;
    private float moveV;
    private bool amIRetro;
	
    // Get everything initialized
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        world = worldObject.GetComponent<WorldManager>();
        amIRetro = world.isRetro;
        dialogue = dialogueObject.GetComponent<DialogueBox>();
        pickaxeImage = dialogueObject.transform.Find("Canvas").Find("Pickaxe").GetComponent<Image>();
        pickaxeText = dialogueObject.transform.Find("Canvas").Find("PickaxeNum").GetComponent<Text>();
        pickaxeText.fontSize = 20;
        moving = new Interactable();
        soundGen = transform.Find("Sound").GetComponent<AudioSource>();


        CheckPickaxe();
    }

    //for animation
    void FixedUpdate()
    {
        anim.SetBool("Retro", world.isRetro);
        anim.SetBool("Carrying", isInteracting);
        anim.SetBool("Moving", moveV != 0 || moveH != 0);
    }

    void Update () {
        //lock character if dialogue is happening
        if(dialogue.isOn)
        {
            maxSpeed = 0;
        }
        else
        {
            maxSpeed = 7;
        }

        //Get movement
        moveH = Input.GetAxis("Horizontal") * maxSpeed;
        moveV = Input.GetAxis("Vertical") * maxSpeed;
        rig.velocity = new Vector2(moveH, moveV);

        if (moveH > 0 && !isRight)
        {
            Flip();
        }
        else if (moveH < 0 && isRight)
        {
            Flip();
        }

        if (isInteracting)
        {
            if (!isRight)
            {
                moving.transform.position = new Vector2(gameObject.transform.position.x - 1.1f, gameObject.transform.position.y);
            }
            else
            {
                moving.transform.position = new Vector2(gameObject.transform.position.x + 1.1f, gameObject.transform.position.y);
            }

        }

        //Changing images
        if (world.isRetro)
        {
            pickaxeText.font = Resources.Load<Font>("kenpixel_mini_square");
            pickaxeImage.sprite = Resources.Load<Sprite>("Assets/Items/Pickaxe");
        }
        else
        {
            pickaxeText.font = Resources.Load<Font>("kenvector_future");
            pickaxeImage.sprite = Resources.Load<Sprite>("Assets/Items/Sword");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //interactions with different objects in the game
        if (other.tag == "Interactable")
        {
            if (!isInteracting)
            {
                Interactable intObject =  other.GetComponent<Interactable>();
                print("HI");
                if (intObject.type == 0)//it's a movable
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        print("Moving");
                        moving = intObject;
                        isInteracting = true;
                        if (world.isRetro)
                        {
                            soundGen.clip = intObject.soundRet;
                            soundGen.Play();
                        }
                        else
                        {
                            soundGen.clip = intObject.soundReg;
                            soundGen.Play();
                        }
                               
                    }
                    
                }
                else if (intObject.type == 1 || intObject.type == 3)//talker or door
                {
                    print("Door");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialogue.TurnOn(intObject.dialogueIndex);
                        if (world.isRetro)
                        {
                            soundGen.clip = intObject.soundRet;
                            soundGen.Play();
                        }
                        else
                        {
                            soundGen.clip = intObject.soundReg;
                            soundGen.Play();
                        }
                    }
                }
                else if  (intObject.type == 2)//pickaxe
                {
                    print("GOTEM");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(other.gameObject);
                        pickAxeNumber += 1;
                        CheckPickaxe();

                        if (world.isRetro)
                        {
                            soundGen.clip = intObject.soundRet;
                            soundGen.Play();
                        }
                        else
                        {
                            soundGen.clip = intObject.soundReg;
                            soundGen.Play();
                        }
                    }
                }
                else if (intObject.type == 4)//stone
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (pickAxeNumber > 0)
                        {
                            if (world.isRetro)
                            {
                                pickAxeNumber -= 1;
                                CheckPickaxe();
                                Destroy(other.gameObject);
                                dialogue.TurnOn(intObject.dialogueIndex2);

                                if (world.isRetro)
                                {
                                    soundGen.clip = intObject.soundRet;
                                    soundGen.Play();
                                }
                                else
                                {
                                    soundGen.clip = intObject.soundReg;
                                    soundGen.Play();
                                }
                            }
                            else
                            {
                                dialogue.TurnOn(intObject.dialogueIndex);
                            }
                        }
                        else
                        {
                            dialogue.TurnOn(intObject.dialogueIndex);
                        }
                    }
                    
                }
                else if (intObject.type == 5)//guy
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (pickAxeNumber > 0)
                        {
                            if (!world.isRetro)
                            {
                                pickAxeNumber -= 1;
                                CheckPickaxe();
                                Destroy(other.gameObject);
                                dialogue.TurnOn(intObject.dialogueIndex2);

                                if (world.isRetro)
                                {
                                    soundGen.clip = intObject.soundRet;
                                    soundGen.Play();
                                }
                                else
                                {
                                    soundGen.clip = intObject.soundReg;
                                    soundGen.Play();
                                }
                            }
                            else
                            {
                                dialogue.TurnOn(intObject.dialogueIndex);
                            }
                        }
                        else
                        {
                            dialogue.TurnOn(intObject.dialogueIndex);
                        }
                    }

                        
                }
                else if (intObject.type == 6)//switch
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        world.Change();
                    }
                    
                }
                else if (intObject.type == 7)//Key
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialogue.TurnOn(intObject.dialogueIndex);
                        Destroy(other.gameObject);
                        hasKey = true;

                        if (world.isRetro)
                        {
                            soundGen.clip = intObject.soundRet;
                            soundGen.Play();
                        }
                        else
                        {
                            soundGen.clip = intObject.soundReg;
                            soundGen.Play();
                        }
                    }

                }
                else if (intObject.type == 8)//Boulder
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (pickAxeNumber > 3)
                        {
                            if (world.isRetro)
                            {
                                pickAxeNumber -= 4;
                                CheckPickaxe();
                                Destroy(other.gameObject);
                                dialogue.TurnOn(intObject.dialogueIndex2);

                                if (world.isRetro)
                                {
                                    soundGen.clip = intObject.soundRet;
                                    soundGen.Play();
                                }
                                else
                                {
                                    soundGen.clip = intObject.soundReg;
                                    soundGen.Play();
                                }
                            }
                            else
                            {
                                dialogue.TurnOn(intObject.dialogueIndex);
                            }
                        }
                        else
                        {
                            dialogue.TurnOn(intObject.dialogueIndex);
                        }
                    }

                }
                else if (intObject.type == 9)//gold door
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (hasKey)
                        {
                            Destroy(other.gameObject);
                            dialogue.TurnOn(intObject.dialogueIndex2);
                            if (world.isRetro)
                            {
                                soundGen.clip = intObject.soundRet;
                                soundGen.Play();
                            }
                            else
                            {
                                soundGen.clip = intObject.soundReg;
                                soundGen.Play();
                            }
                        }
                        else
                        {
                            dialogue.TurnOn(intObject.dialogueIndex);
                        }

                        
                    }

                }
            }
            else
            {
                Interactable intObject = other.GetComponent<Interactable>();

                if (intObject.type == 0)//it's a movable
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        print("Stop Moving");
                        moving = new Interactable();
                        isInteracting = false;
                        intObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    }

                }
            }
        }
    }

    void CheckPickaxe()
    {
        if (pickAxeNumber > 0)
        {
            pickaxeText.text = "x" + pickAxeNumber.ToString();
            pickaxeImage.color = new Vector4(255, 255, 255, 255);
        }
        else
        {
            pickaxeText.text = "";
            pickaxeImage.color = new Vector4(255, 255, 255, 0);
        }
    }


    void Flip()
    {
        isRight = !isRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        print("hi");
    }
}

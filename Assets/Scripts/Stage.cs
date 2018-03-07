using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    private Vector2 center;
    private Vector2 playerPos;
    public bool isLit = false;

    public GameObject playerObject;
    private Player player;

    public GameObject stageMangerObject;
    private StageManager stageManager;

    
    public Sprite stageSprite;
    public Sprite regSprite;
    public Sprite retSprite;

    public AudioClip reg;
    public AudioClip ret;

    private float height;
    private float width;


	// Use this for initialization
	void Start ()
    {
        player = playerObject.GetComponent<Player>();
        stageManager = stageMangerObject.GetComponent<StageManager>();
        stageSprite = retSprite;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 20;
        gameObject.GetComponent<SpriteRenderer>().sprite = stageSprite;
        

        center = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        height = 10.8f;
        width = 19f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (IsPlayerInBounds())
        {
            OnPlayerEnter();
        }
    }

    void OnPlayerEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        isLit = true;
        stageManager.currentStage = this;
    }

    bool IsPlayerInBounds()
    {
        if (playerPos.x > center.x - width/2 && playerPos.x < center.x + width/2 && playerPos.y > center.y - height/2 && playerPos.y < center.y + height/2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void ChangeSprite()
    {
        if (stageSprite == regSprite)
        {
            stageSprite = retSprite;
        }
        else
        {
            stageSprite = regSprite;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = stageSprite;
    }
}

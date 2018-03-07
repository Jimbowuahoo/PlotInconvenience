using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Camera current;
    public GameObject marsh;

    private WorldManager world;
    public GameObject worldObject;

    public GameObject stageManagerObject;
    private StageManager stageManager;

    // Use this for initialization
    void Start () {
        world = worldObject.GetComponent<WorldManager>();
        current = gameObject.GetComponent<Camera>();
        stageManager = stageManagerObject.GetComponent<StageManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(!world.isRetro)
        {
            current.transform.position = new Vector3(marsh.transform.position.x, marsh.transform.position.y, -10);
        }
        else
        {
            current.transform.position = new Vector3(stageManager.currentStage.transform.position.x, stageManager.currentStage.transform.position.y, -10);
            print(stageManager.currentStage.transform.position.x);
        }
	}
}

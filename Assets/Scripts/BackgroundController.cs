using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public Transform player;
    public float scrollSpeed;
	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        if (player.gameObject.activeSelf)
            transform.Translate(scrollSpeed * Time.deltaTime * player.GetComponent<PlayerController>().velocity, Camera.main.transform); 
    }

	// Update is called once per frame
	void Update () {
	
	}
}

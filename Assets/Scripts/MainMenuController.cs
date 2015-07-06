using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitClicked()
    {
        Application.Quit();
        
    }

    public void OriginalSceneClicked()
    {
        Application.LoadLevel("Level1");
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class UIEnabler : MonoBehaviour
{
    private float fixedDeltaTime;

    public GameObject BackgroundUI;

    public GameObject PressStartUI;
    public GameObject P1ConnectedUI;
    public GameObject P2ConnectedUI;
    public GameObject PressSelectUI;
    public PlayerInputAssign inputAssign;
    void Start()
    {
        BackgroundUI.SetActive(true);
        PressStartUI.SetActive(true);
        StartCoroutine(PressSelectCoroutine());
    }

    void Update()
    {
        if (inputAssign.p1Assigned)
        {
            P1ConnectedUI.SetActive(true);
            Time.timeScale = 0; // pause the game

            if (inputAssign.p2Assigned)
            {   
                P2ConnectedUI.SetActive(true);
            }
        }



        if (Time.timeScale == 0) // Only check when paused
        {
            // Check if any connected gamepad pressed Select
            foreach (var pad in Gamepad.all)
            {
                if (pad.selectButton.wasPressedThisFrame)
                {
                    ResumeGame();
                    break; // Stop after the first one
                }
            }
        }
    }

    IEnumerator PressSelectCoroutine () {
	while(true){ // This creates a never-ending loop
		if (inputAssign.p2Assigned){
            Debug.Log ("InputAssign");
            yield return new WaitForSecondsRealtime (2.0f);
            PressSelectUI.SetActive (true);
        }
      break;
	}
    }


private void ResumeGame() {
        {
            BackgroundUI.SetActive(false);
            PressStartUI.SetActive(false);
            P1ConnectedUI.SetActive(false);
            P2ConnectedUI.SetActive(false);
            PressSelectUI.SetActive(false);
            Time.timeScale = 1; //unpause the game
            

            this.enabled = false;
        }
    }
}


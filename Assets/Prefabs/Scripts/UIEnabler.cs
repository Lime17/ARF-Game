using UnityEngine;
using UnityEngine.InputSystem;

public class UIEnabler : MonoBehaviour
{
    private float fixedDeltaTime;

    public GameObject PressToPairUI;
    public GameObject P1ConnectedUI;
    public GameObject P2ConnectedUI;
    public GameObject PressToBeginUI;
    public PlayerInputAssign inputAssign;

    void Start()
    {
        PressToPairUI.SetActive(true);
    }

    void Update()
    {
        if (inputAssign.p1Assigned)
        {
            PressToPairUI.SetActive(false);
            P1ConnectedUI.SetActive(true);
            Time.timeScale = 0;

            if (inputAssign.p2Assigned)
            {
                P2ConnectedUI.SetActive(true);
                PressToBeginUI.SetActive(true);
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

private void ResumeGame() {
        {
            P1ConnectedUI.SetActive(false);
            P2ConnectedUI.SetActive(false);
            PressToBeginUI.SetActive(false);
            Time.timeScale = 1;

            this.enabled = false;
        }
    }
}


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
    public CharacterController p1control;
    public CharacterController p2control;

    public AudioClip activateSound;  // plays when object is set active
    public AudioClip gamestartSound; // plays when the game starts after pairing
    public AudioClip loopSound;      // plays constantly while active

    private AudioSource audioSource;

    public bool gameStarted = false; // new flag

    private bool waitingForSelect = false;
    private bool selectUIStarted = false;

    public float selectUIDelay = 0.7f; // delay in seconds

     void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false; // default to non-looping
    }

    void Start()
    {
        BackgroundUI.SetActive(true);
        PressStartUI.SetActive(true);
        p1control.enabled = false;
        p2control.enabled = false;
    }

    void Update()
    {
         if (gameStarted) return;
        foreach (var pad in Gamepad.all)
        {
            if (pad.startButton.wasPressedThisFrame)
            { audioSource.PlayOneShot(activateSound); }
        }

        if (audioSource.clip != loopSound || !audioSource.isPlaying)
        {
            audioSource.clip = loopSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (inputAssign.p1Assigned)
        {
            P1ConnectedUI.SetActive(true);

            if (inputAssign.p2Assigned)
            {   
                P2ConnectedUI.SetActive(true);
                  
                  // Start the delayed display of PressSelectUI only once
                if (!selectUIStarted)
                {
                    StartCoroutine(ShowSelectUIDelayed());
                    selectUIStarted = true;
                }

                // Only check for select if UI is active
                if (waitingForSelect)
                {
                    CheckSelectPress();
                }
        }
    }
    }

    
    private IEnumerator ShowSelectUIDelayed()
    {
        yield return new WaitForSeconds(selectUIDelay);
        PressSelectUI.SetActive(true);
        waitingForSelect = true; // now we can check for Select input
    }

    private void CheckSelectPress()
    {
        foreach (var pad in Gamepad.all)
        {
            if (pad.selectButton.wasPressedThisFrame)
            {
                audioSource.PlayOneShot(gamestartSound);
                ResumeGame();
                return;
            }
        }
    }


private void ResumeGame() {
        
           gameStarted = true; // prevent Update from turning UI back on
           
            BackgroundUI.SetActive(false);
            PressStartUI.SetActive(false);
            P1ConnectedUI.SetActive(false);
            P2ConnectedUI.SetActive(false);
            PressSelectUI.SetActive(false);
            p1control.enabled = true;
            p2control.enabled = true;

            StartCoroutine(EndScript());
        
    }


private IEnumerator EndScript()
    {
        yield return new WaitForSeconds (2.1f);
        
         if (audioSource != null)
        {
            audioSource.Stop();
        }
            

            this.enabled = false;
    }
    }



using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    
    public UIEnabler UIscript;
    public AudioClip fightMusic;
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false; // default to non-looping
    }
   
   void Update(){ 
    
    if (UIscript.gameStarted == true)
    {

         if (audioSource.clip != fightMusic || !audioSource.isPlaying)
        {
            audioSource.clip = fightMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

   }
}

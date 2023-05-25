using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    public AudioSource dialogueSource; // AudioSource for playing dialogue clips

    public AudioClip[] dialogueClips; // Array of dialogue audio clips

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Get the AudioSource component from the AudioManager
        dialogueSource = GetComponent<AudioSource>();
        Debug.Log(dialogueSource);
    }

    // Play a random dialogue clip
    public void PlayRandomDialogueClip()
    {
        if (dialogueClips.Length > 0)
        {
            int randomIndex = Random.Range(0, dialogueClips.Length);
            AudioClip randomClip = dialogueClips[randomIndex];

            Debug.Log("Playing random dialogue clip: " + randomClip.name); // Add this line

            dialogueSource.clip = randomClip;
            dialogueSource.Play();
        }
        else
        {
            Debug.LogWarning("No dialogue clips assigned to AudioManager.");
        }
    }

}

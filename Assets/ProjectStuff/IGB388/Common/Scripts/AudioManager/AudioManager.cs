using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    public AudioSource dialogueSource; // AudioSource for playing dialogue clips

    [Header("Bookshelf Activity Dialogue Scripts")]
    public AudioClip[] bookshelfCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] bookshelfIncompletedialogueClips; // Array of dialogue audio clips

    [Header("WeaponChest Activity Dialogue Scripts")]
    public AudioClip[] weaponChestCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] weaponChestIncompletedialogueClips; // Array of dialogue audio clips

    [Header("FireWood Activity Dialogue Scripts")]
    public AudioClip[] fireWoodCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] fireWoodIncompletedialogueClips; // Array of dialogue audio clips

    [Header("Plant Activity Dialogue Scripts")]
    public AudioClip[] plantCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] plantIncompletedialogueClips; // Array of dialogue audio clips

    [Header("Dishes Activity Dialogue Scripts")]
    public AudioClip[] dishesCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] dishesIncompletedialogueClips; // Array of dialogue audio clips

    [Header("WeaponRack Activity Dialogue Scripts")]
    public AudioClip[] weaponRackCompletedialogueClips; // Array of dialogue audio clips
    public AudioClip[] weaponRackIncompletedialogueClips; // Array of dialogue audio clips

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
    public void PlayRandomDialogueClip(AudioClip[] dialogueClips)
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

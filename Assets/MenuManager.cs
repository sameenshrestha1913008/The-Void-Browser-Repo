using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SoundEffectPlayer))]
[RequireComponent(typeof(AudioSource))]

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    private AudioSource audioSource;
    private SoundEffectPlayer soundEffectPlayer;

    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        if (GetComponent<SoundEffectPlayer>() != null)
        {
            soundEffectPlayer = GetComponent<SoundEffectPlayer>();
        }

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager FOUND!");
        }
    }
    public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound, soundEffectPlayer.soundListGetter, audioSource);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound, soundEffectPlayer.soundListGetter, audioSource);

        Debug.Log("We Quit the Game");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound, soundEffectPlayer.soundListGetter, audioSource);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SoundEffectPlayer))]
[RequireComponent(typeof(AudioSource))]

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

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
            Debug.LogError("No audio manage found in the scene");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound, soundEffectPlayer.soundListGetter, audioSource);

        Debug.Log("Application Quit!");
        Application.Quit();
    }

    // Update is called once per frame
    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound, soundEffectPlayer.soundListGetter, audioSource);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound, soundEffectPlayer.soundListGetter, audioSource);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manage found in the scene");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);

        Debug.Log("Application Quit!");
        Application.Quit();
    }

    // Update is called once per frame
    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}

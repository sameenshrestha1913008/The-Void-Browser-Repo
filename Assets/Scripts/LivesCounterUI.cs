using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesCounterUI : MonoBehaviour
{

    private TMP_Text livesText;

    // Start is called before the first frame update
    void Awake()
    {
        livesText= GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "LIVES: " + GameMaster.RemainingLives.ToString();
    }
}

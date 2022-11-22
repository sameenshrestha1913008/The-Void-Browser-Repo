using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SoundEffectPlayer))]
[RequireComponent(typeof(AudioSource))]
public class GameMaster : MonoBehaviour
{
    private AudioSource audioSource;
    private SoundEffectPlayer soundEffectPlayer;
    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;
    
    private static int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }


    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public string respawnCoundownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
    public string gameOverSoundName = "GameOver";

    public CameraShake camerashake;

    [SerializeField]
    private Transform gameOverUI;
    [SerializeField]
    private Transform gameCompletedScreen;

    // cache
    private AudioManager audioManager;

    private Player playerClass;

    void Start()
    {
        playerClass = playerPrefab.GetComponent<Player>();
        if (GetComponent<SoundEffectPlayer>() != null)
        {
            soundEffectPlayer = GetComponent<SoundEffectPlayer>();
        }

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (camerashake== null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }

        _remainingLives = maxLives;

        // caching
        audioManager = AudioManager.instance;
        if (audioManager ==null)
        {
            Debug.LogError("No audio manage found in the scene");
        }
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName, soundEffectPlayer.soundListGetter, audioSource);
        
        Debug.Log("Game Over");
        gameOverUI.gameObject.SetActive(true);
    }

       
    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound(respawnCoundownSoundName, soundEffectPlayer.soundListGetter, audioSource);
              

        yield return new WaitForSeconds(spawnDelay);

        Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy (clone.gameObject, 3f);


        playerPrefab.gameObject.SetActive (true);



        audioManager.PlaySound(spawnSoundName, soundEffectPlayer.soundListGetter, audioSource);
        playerPrefab.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        playerClass.Respawn();
        // Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
        

    }
    public void KillPlayer()
    {
        playerClass.gameObject.transform.position=new Vector2(playerClass.gameObject.transform.position.x, -100000);
        // Destroy(player.gameObject);
        _remainingLives-=1;
        if(_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
        
    }

    public static void KillEnemy (Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        //Let's play some sound
        audioManager.PlaySound(_enemy.deathSoundName, soundEffectPlayer.soundListGetter, audioSource);

        //Add Particles
        GameObject _clone = Instantiate(_enemy.deathParticles.gameObject, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);

        //Go Camershake
        camerashake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

    public void GameComplete()
    {
        gameCompletedScreen.gameObject.SetActive(true);
    }
}

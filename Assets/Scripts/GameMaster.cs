using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

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

    public CameraShake camerashake;


    void Start()
    {
        if(camerashake== null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }
    }

       
    public IEnumerator _RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        playerPrefab.gameObject.SetActive (true);
        playerPrefab.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        // Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
        // Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        // Destroy (clone, 3f);

    }
    public static void KillPlayer(Player player)
    {
        player.gameObject.SetActive(false);
        //Destroy(player.gameObject);
        gm.StartCoroutine(gm._RespawnPlayer());
    }

    public static void KillEnemy (Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        GameObject _clone = Instantiate(_enemy.deathParticles.gameObject, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);
        camerashake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}

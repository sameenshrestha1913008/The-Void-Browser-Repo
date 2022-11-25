
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(SoundEffectPlayer))]
[RequireComponent(typeof(AudioSource))]
[System.Serializable]



public class PlayerStats
{

    public int maxHealth = 100;

    private int _curHealth;
    public int curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void Init()
    {
        curHealth = maxHealth;
    }

}
public class Player : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Platformer2DUserControl platformUserControl;
    
    private AudioSource audioSource;
    private SoundEffectPlayer soundEffectPlayer;

    public PlayerStats stats = new PlayerStats();

    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";


    private AudioManager audioManager;

    [SerializeField]
    private StatusIndicator statusIndicator;


    void Start()
    {
        stats.Init();

        if (GetComponent<SoundEffectPlayer>() != null)
        {
            soundEffectPlayer = GetComponent<SoundEffectPlayer>();
        }

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (statusIndicator == null)
        {
            Debug.LogError("No status Indicator reference on player");
        }
        else
        {
            UpdateHealthUI(stats.curHealth, stats.maxHealth);
        }
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Panic! NBo audio manager in scene");
        }
    }




    void Update()
    {
        //if(transform.position.y <= fallBoundary)
        //
        //    DamagePlayer(9999999);
        
    }

    private void UpdateHealthUI(int curHealth, int maxHealth)
    {
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
   
    public void EnabledDisabledComponent(bool enable)
    {
        weapon.enabled = enable;
        platformUserControl.enabled = enable;
    }
    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            //Play death sound
            audioManager.PlaySound(deathSoundName, soundEffectPlayer.soundListGetter, audioSource);

            EnabledDisabledComponent(false);


            // Kill Player
            GameMaster.gm.KillPlayer();
        }
        else
        {
            // Play damage sound
            audioManager.PlaySound(damageSoundName, soundEffectPlayer.soundListGetter, audioSource);
        }

        UpdateHealthUI(stats.curHealth, stats.maxHealth);
    }

    public void Respawn()
    {
        stats.curHealth = stats.maxHealth;
        UpdateHealthUI(stats.curHealth, stats.maxHealth);
        EnabledDisabledComponent(true);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathArea")
        {
            DamagePlayer(stats.maxHealth);
        }
    }
}

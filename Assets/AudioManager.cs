using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f )]

    public float volume = 0.7f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;


    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    private AudioSource source;

    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {

        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.PlayOneShot(source.clip);
    }

    public void Stop()
    {
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour    
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;


    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            instance = this;
        }
        
    }
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Music");
    }

 
    public void PlaySound (string _name, List<Sound> soundList = null, AudioSource audioSource = null)
    {
        switch (soundList)
        {
            case null:
                {
                    for (int i = 0; i < sounds.Length; i++)
                    {
                        if (sounds[i].name == _name)
                        {
                            sounds[i].Play();
                            return;
                        }
                    }
                    break;
                }
            default:
                {
                    for (int i = 0; i < soundList.Count; i++)
                    {
                        soundList[i].SetSource(audioSource);
                        if (soundList[i].name == _name)
                        {
                            soundList[i].Play();
                            return;
                        }
                    }
                    break;
                }
        }  
           
        

        // no sound with _name
        Debug.LogWarning("AudioManager sound not found in list, " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager sound not found in list, " + _name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaySoundEffect
{
    public List<Sound> soundListGetter
    {
        get; 
    }
    public void playSound();

}

public class SoundEffectPlayer : MonoBehaviour, IPlaySoundEffect
{
    [SerializeField]
    List<Sound> soundList = new List<Sound>();

    public List<Sound> soundListGetter { get { return soundList; } }

    public void playSound()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

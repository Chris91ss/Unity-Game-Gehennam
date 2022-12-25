using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instanceSound { get; private set; }
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if(instanceSound == null)
        {
        instanceSound = this;
        DontDestroyOnLoad(gameObject);
        }
        else if(instanceSound != null && instanceSound != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound); 
    }
}

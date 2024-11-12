using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AudioCategory
{
    BGM,
    SFX
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                
            }
            return instance;
        }
    }

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [Serializable]
    public class AudioData
    {
        public string eventName;
        public AudioCategory category;
        public AudioClip clip;
    }

    public List<AudioData> audioDataList;

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        bgmSource = gameObject.GetComponent<AudioSource>();
        sfxSource = gameObject.GetComponent<AudioSource>();

        bgmSource.loop = true;

        foreach (var audioData in audioDataList)
        {
            audioClips.Add(audioData.eventName, audioData.clip);
        }

    }

    private void Start()
    {
        
    }
    void OnEnable()
    {
        foreach (var audioData in audioDataList)
        {
            if (audioData.category == AudioCategory.SFX)
            {
                EventBus.Subscribe(audioData.eventName, () => PlaySFX(audioData.eventName));
            }
        }
    }

    void OnDisable()
    {
        foreach (var audioData in audioDataList)
        {
            if (audioData.category == AudioCategory.SFX)
            {
                EventBus.Unsubscribe(audioData.eventName, () => PlaySFX(audioData.eventName));
            }
        }
    }

    private void PlaySFX(string eventName)
    {
        if (audioClips.TryGetValue(eventName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        PlayBGM(currentScene.name);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name);
    }

    private void PlayBGM(string sceneName)
    {
        if (audioClips.TryGetValue(sceneName, out AudioClip bgmClip))
        {
            if (bgmSource.clip != bgmClip)
            {
                bgmSource.clip = bgmClip;
                bgmSource.Play();
            }
        }
        else
        {

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

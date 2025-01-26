using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets instance;

    public SoundAudioClip[] soundAudioClipsArray;

    public AudioClip menuMusic;
    public AudioClip levelOneMusic;
    public AudioClip levelTwoMusic;
    public AudioClip levelThreeMusic;

    private AudioSource musicSource;

    public float musicVolumeModifier = 0.35f;
    public float sfxVolumeModifier = 0.5f;

    public float mainMusicVolume = 1f;
    public float mainSFXVolume = 1f;

    public Slider musicSlide;
    public Slider ambiantSlide;
    public Slider sfxSlide;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void Awake()
    {
        if (instance)
        {
            Debug.Log("Il y a deja une instance de SoundManager : Autodestruction lancee ");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 1f;
        mainMusicVolume = 1f;
        mainSFXVolume = 0.5f;
        PlayMenuMusic();
    }
    public void PlayMenuMusic()
    {
        StartCoroutine(StopMusicWithFade(0.2f));
        StartCoroutine(UpdateMusicWithFade(musicSource, menuMusic, 0.5f));
    }

    public void PlayLevelOneMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, levelOneMusic, 0.5f));
    }

    public void PlayLevelTwoMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, levelTwoMusic, 0.5f));
    }

    public void PlayLevelThreeMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, levelThreeMusic, 0.5f));
    }

    public void PlayNextMusic(int day)
    {
        StartCoroutine(StopMusicWithFade(0.2f));
        if(day < 2) PlayLevelOneMusic();
        else if (day < 4) PlayLevelTwoMusic();
        else PlayLevelThreeMusic();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.volume = mainMusicVolume * musicVolumeModifier;
        musicSource.Play();
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime)) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
    }

    public IEnumerator StopMusicWithFade(float transitionTime = 1.0f)
    {

        float t = 0f;

        for (t = 0f; t <= transitionTime; t += Time.deltaTime)
        {
            musicSource.volume = (1 - (t / transitionTime)) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }

        musicSource.Stop();
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicWithFade(1f));
    }
}
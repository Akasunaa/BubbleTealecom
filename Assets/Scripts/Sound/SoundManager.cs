using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        None,
        Humain1,
        Humain2,
        Humain3,
        Infecte1,
        Infecte2,
        Slime1,
        Slime2,
        Slime3,
        Doorbell,
        Window,
        Trash,
        SwitchButton,
        Error,
        Shaker,
        Slicer,
        SlicerLever,
        MolecularReassembler,
        Tourniquet,
        WindowClose,
        HumainHappy1,
        HumainHappy2,
        AngryHuman1,
        AngryHuman2,
        AngryHuman3,
        AngrySlime
    }

    public static void PlaySound(Sound sound, float volume = 1f)
    {

        if (sound == Sound.None) return;

        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.tag = "Sound";
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);

    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        if (sound == Sound.None) return null;

        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    public static float GetAudioClipLength(Sound sound)
    {
        if (sound == Sound.None) return 0f;

        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip.length;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return 0f;
    }
}
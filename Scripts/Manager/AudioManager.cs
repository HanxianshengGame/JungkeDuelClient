using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: BaseManager  {
    public AudioManager(GameFacade facade) : base(facade)
    {
        
    }
    private const string Sound_PathPrefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_BgFast = "Bg(fast)";
    public const string Sound_BgModerate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;

    public override void OnInit()
    {
        base.OnInit();
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        PlayBgSound(Sound_BgModerate);
    }

    public void PlayBgSound(string soundName)
    {

        PlaySound(bgAudioSource, LoadSound(soundName), 0.3f, true);
    }
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1.0f);
    }

    

    private void PlaySound(AudioSource audioSource, AudioClip clip, 
        float volume, bool loop = false)
    {

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }


    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_PathPrefix + soundsName);
    }
}

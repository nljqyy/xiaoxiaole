using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioManager
{
    private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();
    private AudioSource _source;
    private CustomAudioSource _cas;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
    }
    static AudioManager()
    {
        instance = new AudioManager();
    }
    private AudioManager()
    {
        GameObject audioSource = new GameObject("Audios");
        _source = audioSource.AddComponent<AudioSource>();
        _cas = audioSource.AddComponent<CustomAudioSource>();
    }

    public void PlayByName(string clipName,bool loop,Action acion=null)
    {
        AudioClip clip= FindAudioClip(clipName);
        _cas.Play(_source, clip, null, 1, 1, loop,acion);
        Debug.Log("播放声音---"+clipName);
    }

    public void PlayByName(string clipName,Transform emitter, bool loop, Action acion = null)
    {
        AudioClip clip = FindAudioClip(clipName);
        _cas.Play(_source, clip, emitter, 1, 1, loop, acion);
        Debug.Log("播放声音---" + clipName);
    }

    private AudioClip FindAudioClip(string clipName)
    {
        AudioClip clip;
        soundDic.TryGetValue(clipName, out clip);
        if (clip == null)
        {
            clip = Resources.Load<AudioClip>("Audio/" + clipName);
            soundDic.Add(clipName, clip);
        }
        return clip;
    }

}

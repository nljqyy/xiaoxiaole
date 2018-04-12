using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomAudioSource : MonoBehaviour
{
    private IEnumerator ie;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(StartCheckIE());
    }

    //private IEnumerator StartCheckIE()
    //{
    //    while (true)
    //    {
    //        if (ie != null && ie.MoveNext())
    //        {
    //            yield return ie.Current;
    //        }
    //        else
    //            yield return null;
    //    }
    //}
    public void Play(AudioSource audioSource, AudioClip clip, Transform emitter, float volume, float pitch, bool loop, Action action = null)
    {
        if (emitter == null)
        {
             StartCoroutine(PlayI(audioSource, clip, volume, pitch, loop, action));
            //ie = PlayI(audioSource, clip, volume, pitch, loop, action);
        }
        else
        {
            StartCoroutine(PlayII(clip, emitter, volume, pitch, loop, action));
        }

    }
    IEnumerator PlayI(AudioSource audioSource, AudioClip clip, float volume, float pitch, bool loop, Action action = null)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        if (action != null)
            action();
    }

    IEnumerator PlayII(AudioClip clip, Transform emitter, float volume, float pitch, bool loop, Action action = null)
    {
        GameObject go = new GameObject("Audio:" + clip.name);
        go.transform.SetParent(emitter);
        go.transform.localPosition = Vector3.zero;

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
        if (!loop)
        {
            DestroyObject(go, clip.length);
            yield return new WaitForSeconds(clip.length);
            if (action != null)
                action();
        }

    }



}

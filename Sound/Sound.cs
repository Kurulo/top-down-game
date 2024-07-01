using System;
using UnityEngine;

[Serializable]
public class Sound
{
    [Header("Main")]
    public string name;
    public AudioClip audioClip;

    [Space, Header("Settings")]
    [Range(0.0f, 1.0f)]
    public float volume;

    [Range(-3.0f, 3.0f)]
    public float pitch;

    [Space]
    public bool onAwake;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

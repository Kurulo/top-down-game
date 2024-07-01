using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Container for audio"), SerializeField]
    private GameObject _container;

    [Space]
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private Sound[] _music;

    private void Awake() {
        SetSounds();
        SetMusics();
        
        foreach (var music in _music)
        {
            music.source.Play();
        }
    }

    public void PlaySound(string soundName) {
        Sound sound = Array.Find(_sounds, s => s.name == soundName);
        sound.source.Play();
    }

    public void PlayMusic(string musicName) {
        Sound music = Array.Find(_music, m => m.name == musicName);
        music.source.Play();
    }

    private void SetSounds() {
        foreach (Sound s in _sounds) {
            s.source = _container.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.onAwake;
            s.source.loop = s.loop;
        }
    }

    private void SetMusics() {
        foreach (Sound m in _music) {
            m.source = _container.AddComponent<AudioSource>();
            m.source.clip = m.audioClip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.playOnAwake = m.onAwake;
            m.source.loop = m.loop;
        }
    }
}

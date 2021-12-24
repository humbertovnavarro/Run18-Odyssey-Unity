using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace Kamaii
{
    public class MusicPlayer : MonoBehaviour
    {
        private static bool didJustStop;
        public static event EventHandler OnMusicPlay;
        public static event EventHandler OnMusicStop;
        [SerializeField]
        private float _volume;
        private static float volume;
        [SerializeField]
        private AudioClip[] _music; 
        private static Dictionary<string, AudioClip> music;
        private static AudioSource _source;
        private static MusicPlayer instance;
        public static void Play(string name)
        {
            didJustStop = false;   
            OnMusicPlay?.Invoke(instance, EventArgs.Empty);
            if (instance == null)
            {
                Debug.LogError("No instance of Music Player in the scene.");
                return;
            }
            if (name == null)
            {
                Debug.LogError("No clip name provided to MusicPlayer", instance);
            }
            AudioClip clip = null;
            if (music.TryGetValue(name, out clip))
            {
                _source.volume = volume;
                _source.Stop();
                _source.loop = true;
                _source.clip = clip;
                _source.Play();
            } else
            {
                Debug.LogError("Could not find Audio Clip named: " + name);
            }
        }
        public static void PlayOneShot(string name)
        {
            if (instance == null)
            {
                Debug.LogError("No instance of Music Player in the scene.");
                return;
            }
            if (name == null)
            {
                Debug.LogError("No clip name provided to MusicPlayer", instance);
            }
            AudioClip clip = null;
            if (music.TryGetValue(name, out clip))
            {
                _source.volume = volume;
                _source.Stop();
                _source.loop = false;
                _source.clip = clip;
                _source.Play();
            }
            else
            {
                Debug.LogError("Could not find Audio Clip named: " + name);
            }
        }
        private void Update()
        {
            if (_source == null)
                return;
            if(!_source.isPlaying && !didJustStop)
            {
                didJustStop = true;
                OnMusicStop?.Invoke(instance, EventArgs.Empty);
            }
        }
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                _source = instance.GetComponent<AudioSource>();
                volume = _volume;
                music = new Dictionary<string, AudioClip>();
                foreach (AudioClip clip in _music)
                {
                    music.Add(clip.name, clip);
                }
            }
            else
            {
                Debug.LogError("Found two instances of MusicPlayer in the scene. Destroying newer one...", instance);
                GameObject.Destroy(this);
            }
        }
    }
}

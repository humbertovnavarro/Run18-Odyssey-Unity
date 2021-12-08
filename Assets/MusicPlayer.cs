using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    public enum Music
    {
        RUDEBUSTER,
        MIRRORS,
        CONSCIOUSNESS,
        SKYLINE
    }
    public enum MusicFilter { 
        OUTSIDE
    }
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0f, 2000f)]
        private float m_LowpassCutoffFrequency;
        [SerializeField]
        private AudioClip[] m_music;
        [SerializeField]
        private float m_Volume;
        public static float volume;
        private static AudioSource[] Sources { get; set; }
        private static MusicPlayer instance;
        private static AudioLowPassFilter LowPassFilter;
        public static Music Playing { get; private set; }
        private void Awake()
        {
            instance = this;
            volume = m_Volume;
            PopulateMusic();
            LowPassFilter = instance.gameObject.AddComponent<AudioLowPassFilter>();
            LowPassFilter.cutoffFrequency = m_LowpassCutoffFrequency;
            ToggleFilter(false);
        }
        public void Update()
        {
            //Remove me
            Debug();
            //
        }
        //Remove me
        public void Debug()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                Play(Music.RUDEBUSTER);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                Play(Music.CONSCIOUSNESS);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

                Play(Music.MIRRORS);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {

                Play(Music.SKYLINE);
            }
        }
        //
        public void PopulateMusic()
        {
            Sources = new AudioSource[m_music.Length];
            for (int i = 0; i < m_music.Length; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = m_music[i];
                source.playOnAwake = false;
                source.loop = true;
                source.volume = volume;
                source.playOnAwake = true;
                source.bypassEffects = false;
                Sources[i] = source;
            }
        }
        public static void Play(Music song)
        {
            Playing = song;
            for (int i = 0; i < Sources.Length; i++)
            {
                if (i != (int)song)
                {
                    Sources[i].Stop();
                } else
                {
                    Sources[i].Play();
                }
            }
        }
        public static void ToggleFilter(bool value)
        {
            LowPassFilter.enabled = value;
        }
    }
}

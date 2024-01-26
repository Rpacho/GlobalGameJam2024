using System;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using UnityEngine;
using UnityEngine.Audio;

namespace PoguScripts.Audion
{
    public class AudioSFX : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField]
        private AudioMixer _audioMixer;
        public List<AudioData> audioList;

        public void PlayAudioUsingKey(string key)
        {
            AudioData audio = audioList.Find((e) => e.key == key);
            if (audio != null)
            {
                PlayAudioClip(audio.clip);
            }
        }

        public void PlayAudioClip(AudioClip clip, float delay = 0)
        {
            _audioSource?.PlayOneShot(clip);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            GlobalEvent.OnChangedVolumeSettings.AddListener(SetVolume);
        }

        private void OnDestroy()
        {
            GlobalEvent.OnChangedVolumeSettings.RemoveListener(SetVolume);
        }

        public void SetVolume(float volume)
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10((volume)) * 20);
        }
    }

    public class AudioData
    {
        public AudioClip clip;
        public string key;
    }
}
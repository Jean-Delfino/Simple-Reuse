using System.Collections.Generic;
using Reuse.Patterns;
using UnityEngine;

namespace Reuse.Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [System.Serializable]
        public struct AudioKeyPair
        {
            public string key;
            public AudioSource source;
        }

        [SerializeField] private AudioKeyPair[] audioClipsList;
        
        private Dictionary<string, AudioSource> audioClips = new();
        
        protected override void Awake()
        {
            foreach (var pair in audioClipsList)
            {
                if (!audioClips.ContainsKey(pair.key))
                {
                    audioClips.Add(pair.key, pair.source);
                }
            }
            
            base.Awake();
        }

        public void PlayOnlyIfNotPlaying(string key)
        {
            if (!audioClips.ContainsKey(key)) return;
            
            var source = audioClips[key];
            if(!source.isPlaying) source.Play();
        }
        public void PlayAudio(string key)
        {
            if (audioClips.ContainsKey(key))
            {
                var source = audioClips[key];
                source.Play();
                return;
            }

            Debug.LogWarning($"Key {key} not found !");
        }

        public AudioClip GetClip(string key)
        {
            return audioClips.ContainsKey(key) ? audioClips[key].clip : null;
        }
    }
}
using Comfort.Common;
using EFT;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SIT.SITBetterAudio
{
    internal class SITAudioMonoBehaviour : MonoBehaviour
    {
        public void Start()
        {
        }

        //public static void PlayAudioAtPoint(Vector3 point, AudioClip clip)
        //{
        //    var sitAudio = Singleton<SITAudioMonoBehaviour>.Instance.GetOrAddComponent<SITAudioMonoBehaviour>();
            
        //    audioSource.Stop();
        //    audioSource.transform.position = point;
        //    audioSource.clip = clip;
        //    audioSource.dopplerLevel = 1f;
        //    audioSource.loop = false;
        //    audioSource.maxDistance = 100;
        //    audioSource.minDistance = 1;
        //    audioSource.pitch = 1f;
        //    audioSource.priority = 1;
        //    audioSource.reverbZoneMix = 1;
        //    audioSource.rolloffMode = AudioRolloffMode.Linear;
        //    audioSource.spatialBlend = 1;
        //    audioSource.spread = 15;
        //    audioSource.volume = 0.1f;
        //    audioSource.Play();
        //}

        public void PlayAudioAtPoint(AudioSource audioSource, Vector3 point, AudioClip clip)
        {
            //var audioSource = Singleton<GameWorld>.Instance.GetOrAddComponent<AudioSource>();
            audioSource.Stop();
            audioSource.transform.position = point;
            audioSource.clip = clip;
            audioSource.dopplerLevel = 1f;
            audioSource.loop = false;
            audioSource.maxDistance = 100;
            audioSource.minDistance = 1;
            audioSource.pitch = 1f;
            audioSource.priority = 1;
            audioSource.reverbZoneMix = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.spatialBlend = 1;
            audioSource.spread = 15;
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
    }
}

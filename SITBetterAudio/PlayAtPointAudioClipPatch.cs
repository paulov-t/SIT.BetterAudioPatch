using Comfort.Common;
using EFT;
using SIT;
using SIT.BetterAudioPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace SIT.SITBetterAudio
{
    public class PlayAtPointAudioClipPatch : ModulePatch
    {
        public static IEnumerable<MethodInfo> GetPlayAtPointMethods()
        {
            return Plugin.GetTypeByName("BetterAudio")
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == "PlayAtPoint");
        }

        protected override MethodBase GetTargetMethod()
        {
            return GetPlayAtPointMethods().FirstOrDefault(x => x.GetParameters()[1].ParameterType == typeof(AudioClip));
        }

        public static AudioListener AudioListener { get; private set; } 

        [PatchPrefix]
        public static bool PatchPrefix(
            object __instance
            , Vector3 position
            , AudioClip clip
            , AudioListener ___audioListener_0
            )
        {
            AudioListener = ___audioListener_0;

            //Logger.LogInfo("PlayAtPointAudioClipPatch played!");
            var audioSource = Singleton<GameWorld>.Instance.GetOrAddComponent<AudioSource>();
            PlayAudioAtPoint(audioSource, position, clip);

            return false;
        }

        public static void PlayAudioAtPoint
            (AudioSource audioSource
            , Vector3 point
            , AudioClip clip
            , float pitchMult = 1f
            , float volume = 0.15f
            , bool playOneShot = true)
        {
            if (audioSource == null)
            {
                audioSource = Singleton<GameWorld>.Instance.GetOrAddComponent<AudioSource>();
                audioSource.transform.position = point; 
            }

            audioSource.transform.position = point;
            audioSource.clip = clip;
            audioSource.dopplerLevel = 1f;
            audioSource.loop = false;
            audioSource.maxDistance = 50;
            audioSource.minDistance = 2;
            audioSource.pitch = 1f * pitchMult;
            audioSource.priority = 1;
            audioSource.reverbZoneMix = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.spatialBlend = 1;
            audioSource.spread = 15;
            audioSource.volume = volume;
            if (playOneShot)
                audioSource.PlayOneShot(clip, 1f);
            else
                audioSource.Play();

        }

        public static void SetupAudioSource(ref AudioSource audioSource)
        {
            audioSource.dopplerLevel = 1f;
            audioSource.priority = 1;
            audioSource.reverbZoneMix = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.spatialBlend = 1;
            audioSource.spread = 15;
        }
    }
}

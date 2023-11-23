using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using SIT;
using SIT.BetterAudioPatch;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using static BetterAudio;
using static UnityEngine.SendMouseEvents;

namespace SIT.SITBetterAudio
{
    public class PlayAtPointAudioClipPatch : ModulePatch
    {
        private static ConfigFile config;
        private static bool m_EnableCustomEcho = false;

        public PlayAtPointAudioClipPatch(ConfigFile _config)
        {
            config = _config;
            m_EnableCustomEcho = config.Bind<bool>("Audio", "EnableCustomEcho", true).Value;
        }

        public static IEnumerable<MethodInfo> GetPlayAtPointMethods()
        {
            return ReflectionHelpers.GetAllMethodsForType(typeof(BetterAudio)).Where(x=>x.Name == "PlayAtPoint");
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
            , float distance
            , AudioSourceGroupType sourceGroup
            , int rolloff
            , float volume
            , EOcclusionTest occlusionTest 
            , AudioMixerGroup forceMixerGroup
            , bool forceStereo
            , AudioListener ___audioListener_0
            )
        {
            AudioListener = ___audioListener_0;

            PlayAudioAtPoint(null, position, clip, volume: volume);
            return false;
        }

        private static GameObject GOSource { get; set; }

        public static void PlayAudioAtPoint
            (AudioSource audioSource
            , Vector3 point
            , AudioClip clip
            , float pitchMult = 1f
            , float volume = 0.75f
            , bool playOneShot = true
            , bool gunshot = false)
        {
            if (audioSource == null)
            {
                GOSource = new GameObject("as-" + Guid.NewGuid());
                audioSource = GOSource.GetOrAddComponent<AudioSource>();
            }

            audioSource.clip = clip;
            GOSource.transform.position = point;
            audioSource.transform.position = point;

            if (FPSCamera.Instance == null)
                return;

            if (FPSCamera.Instance.Camera == null)
                return;

            var cameraPosition = FPSCamera.Instance.Camera.transform.position;
            var layermask = LayerMaskClass.HighPolyWithTerrainNoGrassMask | 30 | 31;
            var vector = cameraPosition - point;
            var blocked = false;
            var blocked1 = false;
            var blocked2 = false;
            RaycastHit hitInfo;
            blocked = (Physics.Raycast(new Ray(point, cameraPosition), out hitInfo, vector.magnitude, layermask));
            blocked1 = (Physics.Raycast(new Ray(point, cameraPosition - FPSCamera.Instance.Camera.transform.right), out hitInfo, vector.magnitude, layermask));
            blocked2 = (Physics.Raycast(new Ray(point, cameraPosition + FPSCamera.Instance.Camera.transform.right), out hitInfo, vector.magnitude, layermask));

            audioSource.dopplerLevel = 1f;
            audioSource.outputAudioMixerGroup = Singleton<BetterAudio>.Instance.VeryStandartMixerGroup;
            audioSource.loop = playOneShot ? false : true;
            audioSource.maxDistance = gunshot ? 50 : 3;
            audioSource.minDistance = gunshot ? 6 : 0.75f;
            audioSource.pitch = (1.0f * pitchMult) - (float)(blocked ? 0.12f : 0) - (float)(blocked1 ? 0.12f : 0) - (float)(blocked2 ? 0.12f : 0);
            audioSource.priority = 128;
            audioSource.reverbZoneMix = 0.4f;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.spatialBlend = 1.0f;
            audioSource.spread = 0.4f;
            audioSource.volume = volume * (float)(Math.Max(0.75, Math.Min(1.0, Vector3.Distance(point, cameraPosition) > audioSource.maxDistance ? 0.9 : audioSource.maxDistance / Vector3.Distance(point, cameraPosition))));

            audioSource.Play();

        }

    }
}

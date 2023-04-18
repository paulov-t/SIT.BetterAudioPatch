using BepInEx.Configuration;
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
using UnityEngine.Audio;
using UnityEngine.UIElements;
using static BetterAudio;
using static GClass1649;

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

            // Play Audio as normal
            var audioSource = Singleton<GameWorld>.Instance.GetOrAddComponent<AudioSource>();

            if (m_EnableCustomEcho && Singleton<GameWorld>.Instance != null)
            {
                Singleton<GameWorld>.Instance.RegisteredPlayers.ForEach(x =>
                {
                    //if (x.IsYourPlayer) 
                    {
                        LayerMask highPolyWithTerrainMask = LayerMaskClass.HighPolyWithTerrainMask;
                        float maxDistance = Vector3.Distance(x.Position, position);
                        // direct line to you
                        if (Physics.SphereCast(x.Position, 1, position, out var hitInfo, maxDistance, highPolyWithTerrainMask))
                        {
                            PlayAudioAtPoint(audioSource, position, clip, volume: volume);
                        }
                        else
                        {
                            PlayAudioAtPoint(audioSource, position, clip);
                            if (!Physics.Linecast(x.Position + new Vector3(10, 0, 0), position, highPolyWithTerrainMask))
                            {
                                PlayAudioAtPoint(audioSource, x.Position + new Vector3(10, 0, 0), clip, 0.01f, volume * 0.5f, true);
                            }
                            if (!Physics.Linecast(x.Position + new Vector3(-10, 0, 0), position, highPolyWithTerrainMask))
                            {
                                PlayAudioAtPoint(audioSource, x.Position + new Vector3(-10, 0, 0), clip, 0.01f, volume * 0.5f, true);
                            }
                            if (!Physics.Linecast(x.Position + new Vector3(10, 10, 0), position, highPolyWithTerrainMask))
                            {
                                PlayAudioAtPoint(audioSource, x.Position + new Vector3(10, 10, 0), clip, 0.01f, volume * 0.5f, true);
                            }
                            if (!Physics.Linecast(x.Position + new Vector3(10, -10, 0), position, highPolyWithTerrainMask))
                            {
                                PlayAudioAtPoint(audioSource, x.Position + new Vector3(10, -10, 0), clip, 0.01f, volume * 0.5f, true);
                            }
                            if (!Physics.Linecast(x.Position + new Vector3(0, 10, 0), position, highPolyWithTerrainMask))
                            {
                                PlayAudioAtPoint(audioSource, x.Position + new Vector3(0, 10, 0), clip, 0.01f, volume * 0.5f, true);
                            }
                        }
                    }
                    //else
                    //{
                    //    PlayAudioAtPoint(audioSource, position, clip);
                    //}

                });
            }
            else
            {
                PlayAudioAtPoint(audioSource, position, clip, volume: volume);
            }


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

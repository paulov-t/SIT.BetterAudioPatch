using EFT;
using SIT.BetterAudioPatch;
using SIT.SITBetterAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.BetterAudioPatch.WeaponSoundPlayerPatches
{
    internal class WSPFireBulletPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return Plugin.GetTypeByName("WeaponSoundPlayer")
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Single(x => x.Name == "FireBullet");
        }

        [PatchPrefix]
        public static bool PatchPrefix(
            WeaponSoundPlayer __instance,
            BulletClass ammo, Vector3 shotPosition, Vector3 shotDirection, float pitchMult, bool malfunctioned = false, bool multiShot = false, bool burstOf2Start = false
            )
        {
            return true;
            //Logger.LogInfo("WSPFireBulletPatch played!");
            //SoundBank soundBank;
            //if (multiShot)
            //{
            //    soundBank = (__instance.IsSilenced ? __instance.DoubletSilenced : __instance.Doublet);
            //}
            //else
            //{
            //    soundBank = (__instance.IsSilenced ? __instance.BodySilenced : __instance.Body);
            //}
            //if (soundBank == null)
            //{
            //    return false;
            //}
            //AudioClip audioClip = null;
            //AudioClip audioClip2 = null;
            //float pr = 1;
            //float distance = (float)typeof(WeaponSoundPlayer).GetProperty("Distance", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            //soundBank.PickClipsByDistance(ref audioClip, ref audioClip2, ref pr, 1, distance);
            //var gunAudioSource = __instance.GetOrAddComponent<AudioSource>();
            //////if (gunAudioSource.isPlaying)
            //////    return false;

            ////gunAudioSource.Stop();
            ////gunAudioSource.clip = audioClip;
            ////gunAudioSource.dopplerLevel = 50f;
            ////gunAudioSource.loop = false;
            ////gunAudioSource.maxDistance = 100;
            ////gunAudioSource.minDistance = 1;
            ////gunAudioSource.pitch = 0.6f;
            ////gunAudioSource.priority = 1;
            ////gunAudioSource.reverbZoneMix = 1;
            ////gunAudioSource.rolloffMode = AudioRolloffMode.Linear;
            ////gunAudioSource.spatialBlend = 1;
            ////gunAudioSource.spread = 15;
            ////gunAudioSource.volume = 0.1f;
            ////gunAudioSource.Play();

            //PlayAtPointAudioClipPatch.PlayAudioAtPoint(gunAudioSource, shotPosition, audioClip);
            //return false;
        }
    }
}

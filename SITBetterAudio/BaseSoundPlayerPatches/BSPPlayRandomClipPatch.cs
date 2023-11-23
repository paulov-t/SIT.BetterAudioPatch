using Audio.SpatialSystem;
using Comfort.Common;
using EFT;
using SIT.BetterAudioPatch;
using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.BetterAudioPatch.BaseSoundPlayerPatches
{
    public class BSPPlayRandomClipPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(BaseSoundPlayer), "PlayRandomClip");
        }

        [PatchPrefix]
        public static bool PatchPrefix(
            BaseSoundPlayer __instance,
            Player ___Player,
            BaseSoundPlayer.SoundElement soundElement,
            GameObject ____weaponHierarchy,
            BetterSource ____clipsSource

            )
        {
            //Logger.LogInfo("BSPPlayRandomClipPatch played!");

            if (soundElement == null)
            {
                return false;
            }
            AudioClip randomSoundClip = soundElement.RandomSoundClip;
            if (randomSoundClip == null)
            {
                return false;
            }
            //if (((this.Player.PointOfView == EPointOfView.FirstPerson) ? 0f : GClass1928.Instance.Distance(this._weaponHierarchy.position)) > (float)soundElement.RollOff)
            //{
            //    return;
            //}
            var clipSource = ____clipsSource;
            if (clipSource == null)
            {
                clipSource = Singleton<BetterAudio>.Instance.GetSource(BetterAudio.AudioSourceGroupType.Weaponry, false);
                //if (this._clipsSource == null)
                //{
                //    Debug.LogWarning("GetSource(BetterAudio.AudioSourceGroupType.Weaponry) returned null, overflow?");
                //    return;
                //}
                //this._clipsSource.Awake();
                //this._clipsSource.gameObject.name = randomSoundClip.name + " " + this._weaponHierarchy.Original.gameObject.name;
                //this._clipsSource.Position = this._weaponHierarchy.position;
            }
            float volume = soundElement.Volume;
            //if (this.Player.PointOfView == EPointOfView.FirstPerson)
            //{
            //    this._clipsSource.SetMixerGroup(Singleton<BetterAudio>.Instance.VeryStandartMixerGroup);
            //}
            //else
            //{
            //    if (MonoBehaviourSingleton<SpatialAudioSystem>.Instantiated)
            //    {
            //        MonoBehaviourSingleton<SpatialAudioSystem>.Instance.ProcessSourceOcclusion(this.Player, this._clipsSource);
            //    }
            //    this._clipsSource.SetMixerGroup(MonoBehaviourSingleton<BetterAudio>.Instance.VeryStandartMixerGroup);
            //}
            //if (this._clipsSource.OcclusionVolumeFactor > 0f)
            //{
            //clipSource.gameObject.SetActive(true);
            //clipSource.Play(randomSoundClip, null, 1f, volume, false, true);
            //    this._clipsSource.SetRolloff((float)soundElement.RollOff);
            //    this._clipsSource.Play(randomSoundClip, null, 1f, volume, false, true);
            //}

            if(____weaponHierarchy != null)
                PlayAtPointAudioClipPatch.PlayAudioAtPoint(null, ____weaponHierarchy.transform.position, randomSoundClip, volume: volume);
            else
                PlayAtPointAudioClipPatch.PlayAudioAtPoint(null, ___Player.Position, randomSoundClip, volume: volume);

            return false;
            //return true;
        }
    }
}

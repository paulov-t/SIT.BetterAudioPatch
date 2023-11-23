using Comfort.Common;
using EFT;
using MonoMod.Utils;
using SIT.BetterAudioPatch;
using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace SIT.BetterAudioPatch.WeaponSoundPlayerPatches
{
    internal class WSPFireBulletPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(WeaponSoundPlayer), "FireBullet");
        }

        [PatchPrefix]
        public static bool PatchPrefix(
            WeaponSoundPlayer __instance,
            BulletClass ammo
            , Vector3 shotPosition
            , Vector3 shotDirection
            , float pitchMult
            , bool malfunctioned
            , bool multiShot
            , bool burstOf2Start
            , GClass760 ____queue
            , float ____balance
            , EFT.BifacialTransform ____weaponHierarchy
            , ref float ____pitch
            , ref float ____prevPitchMult
            , ref float ____delay
            , ref float ____startFire
            , ref float ____releaseTime
            , ref float ____occlusionReleaseTime
            , ref float ____startPlayingTime
            , ref bool ____isFiring
            , UnityEngine.Audio.AudioMixerGroup ____currentMixerGroup
            , object ___playersBridge
            )
        {
            //return true;
            var _queue = ____queue;
            var IsSilenced = __instance.IsSilenced;
            var BodySilenced = __instance.BodySilenced;
            var Body = __instance.Body;
            var DoubletSilenced = __instance.DoubletSilenced;
            var Doublet = __instance.Doublet;
            var Non_auto = __instance.Non_auto;
            var IsAutoWeapon = !Non_auto;
            var _weaponHierarchy = ____weaponHierarchy;
            var Distance = FPSCamera.Instance.Distance(_weaponHierarchy.position); 
            var _balance = ____balance;
            var _currentMixerGroup = ____currentMixerGroup;
            var _pitch = ____pitch;
            var _prevPitchMult = ____prevPitchMult;
            var _delay = ____delay;
            var _startFire = ____startFire;
            var _startPlayingTime = ____startPlayingTime;
            var _releaseTime = ____releaseTime;
            var _occlusionReleaseTime = ____occlusionReleaseTime;
            var BeatLn = __instance.BeatLn;
            var TailSilenced = __instance.TailSilenced;
            var Tail = __instance.Tail;


            // I cannot handle auto weapons very well at the moment. So resolving back to the original method.
            if (IsAutoWeapon)
            {
                return true;
                //____isFiring = true;
            }

            SoundBank soundBank = ((!multiShot) ? (IsSilenced ? BodySilenced : Body) : (IsSilenced ? DoubletSilenced : Doublet));
            if (soundBank == null)
            {
                return false;
            }
            if (____isFiring)
            {
                if (_queue != null)
                {
                    if (IsAutoWeapon)
                    {
                        ReflectionHelpers.InvokeMethodForObject(__instance, "Balance", soundBank);
                    }
                    ReflectionHelpers.InvokeMethodForObject(__instance, "UpdateMixerGroup");
                    ReflectionHelpers.InvokeMethodForObject(__instance, "UpdatePitch", pitchMult);
                }
                return false;
            }
            float distance = Distance;
            if (____queue == null)
            {
                ____queue = Singleton<BetterAudio>.Instance.BorrowWeaponAudioQueue(BetterAudio.AudioSourceGroupType.Gunshots);
                if (____queue == null)
                {
                    return false;
                }
                ____queue.Pose(_weaponHierarchy.position);
                ____queue.SetRolloff(soundBank.Rolloff);
                ____queue.SetMixerGroup(MonoBehaviourSingleton<BetterAudio>.Instance.GunshotMixerGroup);
            }
            //if (!(____queue.AudioSources[0].OcclusionVolumeFactor <= 0f))

            if (____queue == null)
            {
                return false;
            }

            {
                ____queue.SetMixerGroup(_currentMixerGroup);
                //____queue.EnableStereo(playersBridge.PointOfView == EPointOfView.FirstPerson);
                ____queue.EnableStereo(false);

                // I cannot handle auto weapons very well at the moment. So resolving back to the original method.
                if (IsAutoWeapon)
                {
                    ____isFiring = true;
                }
                AudioClip clip = null;
                AudioClip clip2 = null;
                ////soundBank.PickClipsByDistance(ref clip, ref clip2, ref _balance, (int)playersBridge.Environment, distance);
                soundBank.PickClipsByDistance(ref clip, ref clip2, ref _balance, (int)1, distance);
                var player = ReflectionHelpers.GetFieldOrPropertyFromInstance<IAIDetails>(___playersBridge, "iPlayer");
                PlayAtPointAudioClipPatch.PlayAudioAtPoint(null, player.Position, clip, pitchMult, 1.0f, true, true);
                //_pitch = pitchMult;
                //_prevPitchMult = pitchMult;
                //float num = Mathf.Max((clip != null) ? clip.length : 0f, (clip2 != null) ? clip2.length : 0f);
                //_delay = distance / 340.29f;
                //_startFire = Time.time;
                //_releaseTime = _startFire + num + 1f;
                //_occlusionReleaseTime = num / _pitch / 6f;
                ////EnableSourceOcclusion(enabledOcclusion: true);
                ////UpdateMixerGroup();
                //float sonicDelay = _delay;
                //____queue.Enqueue(clip, clip2, _balance, _startFire + _delay, Non_auto ? (num / _pitch) : 0f, soundBank.BaseVolume, _pitch);
                //if (!IsAutoWeapon)
                //{
                //    ReflectionHelpers.InvokeMethodForObject(__instance, "ReleaseOcclusion", _occlusionReleaseTime);
                //}
                ////if ((!playersBridge.isWeaponTriggerPressed || malfunctioned || burstOf2Start) && IsAutoWeapon)
                // var isWeaponTriggerPressed = ReflectionHelpers.GetFieldOrPropertyFromInstance<bool>(___playersBridge, "isWeaponTriggerPressed");
                //if (!isWeaponTriggerPressed && IsAutoWeapon)
                //{
                //    ReflectionHelpers.InvokeMethodForObject(__instance, "UpdatePitch", pitchMult);
                //    SoundBank soundBank2 = (IsSilenced ? TailSilenced : Tail);
                //    AudioClip clip3 = null;
                //    AudioClip clip4 = null;
                //    soundBank2.PickClipsByDistance(ref clip3, ref clip4, ref _balance, (int)1, distance);
                //    float num2 = Mathf.Max((clip3 != null) ? clip3.length : 0f, (clip4 != null) ? clip4.length : 0f);
                //    float num3 = (burstOf2Start ? (BeatLn * 2f) : BeatLn);
                //    sonicDelay = _delay + num3 / _pitch;
                //    _startPlayingTime = _startFire + _delay + num3 / _pitch;
                //    _queue.Enqueue(clip3, clip4, _balance, _startPlayingTime, num2 / _pitch, soundBank2.BaseVolume, _pitch);
                //    ____isFiring = false;
                //    _releaseTime = _startPlayingTime + num2 / _pitch;
                //    _occlusionReleaseTime = _delay + num3 / _pitch;
                //    ReflectionHelpers.InvokeMethodForObject(__instance, "ReleaseOcclusion", _occlusionReleaseTime);
                //    //ReleaseOcclusion(_occlusionReleaseTime);
                //}
                //__instance.FireSonicSound(sonicDelay, soundBank.Rolloff, ammo, shotPosition, shotDirection);
                ____isFiring = false;

            }

            return false;
        }
    }
}

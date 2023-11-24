using EFT;
using EFT.Interactive;
using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.BetterAudioPatch.SITBetterAudio
{
    internal class TreeInteractivePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(TreeInteractive), "method_0");
        }

        private static Dictionary<string, AudioSource> _sources = new();
        private static Dictionary<string, DateTime> _lastPlayTimes = new();

        [PatchPrefix]
        public static bool PatchPrefix(TreeInteractive __instance
            , Vector3 soundPosition, BetterSource source, object player, bool forceStereo
            , SoundBank ____soundBank)
        {
            AudioClip clip = null;
            AudioClip clip2 = null;
            float balance = 1f;
            if (FPSCamera.Instance == null)
                return false;

            if (FPSCamera.Instance.Camera == null)
                return false;

            var cameraPosition = FPSCamera.Instance.Camera.transform.position;
            var distance = Vector3.Distance(cameraPosition, soundPosition);
            var innerPlayer = ReflectionHelpers.GetFieldOrPropertyFromInstance<IAIDetails>(player, "iPlayer");

            float num = ____soundBank.PickClips(distance, ref clip, ref clip2, ref balance, EnvironmentType.Outdoor);

            AudioSource audioSource = null;
            if (!_sources.ContainsKey(__instance.gameObject.name))
            {
                var GOSource = new GameObject("as-" + Guid.NewGuid());
                audioSource = GOSource.GetOrAddComponent<AudioSource>();
                _sources.Add(__instance.gameObject.name, audioSource);
            }
            else
            {
                audioSource = _sources[__instance.gameObject.name];
            }

            if (!_lastPlayTimes.ContainsKey(__instance.gameObject.name))
            {
                _lastPlayTimes.Add(__instance.gameObject.name, DateTime.Now);
            }
            else
            {
                if (_lastPlayTimes[__instance.gameObject.name] > DateTime.Now.AddSeconds(-1))
                    return false;
            }

            PlayAtPointAudioClipPatch.PlayAudioAtPoint(audioSource, soundPosition, clip);
            return false;
        }
    }
}

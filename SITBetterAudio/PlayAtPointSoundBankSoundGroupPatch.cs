using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.SITBetterAudio
{
    internal class PlayAtPointSoundBankSoundGroupPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return PlayAtPointAudioClipPatch.GetPlayAtPointMethods().FirstOrDefault(x
                => x.GetParameters()[1].ParameterType == typeof(SoundBank)
                && x.GetParameters()[2].Name == "outputGroup"
                );
        }

        [PatchPrefix]
        public static bool PatchPrefix(
            Vector3 position
            , SoundBank bank
            , int outputGroup
            , float distance
            , float volume
            , float bankBlendValue
            , EnvironmentType env
            , EOcclusionTest occlusionTest
            , Player sourcePlayer)
        {
            //Logger.LogInfo("PlayAtPointSoundBankSoundGroupPatch played!");
            AudioClip clip = null;
            AudioClip clip2 = null;
            float balance = 1f;
            float num = bank.PickClips(distance, ref clip, ref clip2, ref balance, env);
            PlayAtPointAudioClipPatch.PlayAudioAtPoint(null, position, clip);

            return false;
        }
    }
}

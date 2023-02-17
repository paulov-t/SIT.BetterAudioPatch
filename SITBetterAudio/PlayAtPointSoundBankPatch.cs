using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static SplinePrefabInstantiator;
using UnityEngine.UIElements;
using UnityEngine;

namespace SIT.SITBetterAudio
{
    internal class PlayAtPointSoundBankPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return PlayAtPointAudioClipPatch.GetPlayAtPointMethods().FirstOrDefault(x 
                => x.GetParameters()[1].ParameterType == typeof(SoundBank)
                && x.GetParameters()[2].Name == "distance"
                );
        }

        [PatchPrefix]
        public static bool PatchPrefix(Vector3 position, SoundBank bank, float distance)
        {
            //Logger.LogInfo("PlayAtPointSoundBankPatch played!");
            AudioClip clip = null;
            AudioClip clip2 = null;
            float balance = 1f;
            float num = bank.PickClips(distance, ref clip, ref clip2, ref balance, EnvironmentType.Outdoor);
            PlayAtPointAudioClipPatch.PlayAudioAtPoint(null, position, clip);

            return false;
        }
    }
}

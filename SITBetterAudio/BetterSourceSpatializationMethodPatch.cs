using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIT.BetterAudioPatch.SITBetterAudio
{
    internal class BetterSourceSpatializationMethodPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(BetterSource), "method_2");
        }

        [PatchPrefix]
        public static bool PatchPrefix(BetterSource __instance)
        {
            return false;
        }
    }
}

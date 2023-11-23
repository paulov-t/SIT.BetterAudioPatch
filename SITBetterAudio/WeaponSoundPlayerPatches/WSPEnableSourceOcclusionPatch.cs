using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIT.BetterAudioPatch.WeaponSoundPlayerPatches
{
    internal class WSPEnableSourceOcclusionPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(WeaponSoundPlayer), "EnableSourceOcclusion");
        }

        [PatchPrefix]
        public static bool PatchPrefix(ref bool enabledOcclusion)
        {
            //Logger.LogInfo("WSPEnableSourceOcclusionPatch played!");
            enabledOcclusion = false;
            return false;
        }
    }
}

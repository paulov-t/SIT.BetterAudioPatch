using SIT.SITBetterAudio;
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
            return Plugin.GetTypeByName("WeaponSoundPlayer")
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Single(x => x.Name == "EnableSourceOcclusion");
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

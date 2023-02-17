using SIT;
using SIT.BetterAudioPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIT.SITBetterAudio
{
    internal class PlayNonSpacialPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return Plugin.GetTypeByName("BetterAudio")
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Single(x => x.Name == "PlayNonspatial");
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            //Logger.LogInfo("PlayNonSpacialPatch played!");

            return true;
        }
    }
}

using SIT;
using SIT.BetterAudioPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIT.SITBetterAudio
{
    internal class PlayAtPointDistantPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return Plugin.GetTypeByName("BetterAudio")
               .GetMethods(BindingFlags.Public | BindingFlags.Instance)
               .Single(x => x.Name == "PlayAtPointDistant");
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            //Logger.LogInfo("PlayAtPointDistant played!");

            return true;
        }
    }
}

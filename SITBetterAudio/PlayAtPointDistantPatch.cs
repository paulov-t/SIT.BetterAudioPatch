using SIT;
using SIT.BetterAudioPatch;
using StayInTarkov;
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
            return ReflectionHelpers.GetMethodForType(typeof(BetterAudio), "PlayAtPointDistant");
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            //Logger.LogInfo("PlayAtPointDistant played!");

            return true;
        }
    }
}

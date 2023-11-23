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
    internal class PlayNonSpacialPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(BetterAudio), "PlayNonspatial");
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            //Logger.LogInfo("PlayNonSpacialPatch played!");

            return true;
        }
    }
}

﻿using EFT.NPC;
using SIT.SITBetterAudio;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.BetterAudioPatch.SITBetterAudio.NPCPlayerPatches
{
    internal class NPCFootStepsSoundPlayerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(NPCFootStepsSoundPlayer), "method_0");
        }

        [PatchPrefix]
        public static bool PatchPrefix(
            NPCFootStepsSoundPlayer __instance
            //, AudioSource ____stepAudioSource
            )
        {
            Logger.LogInfo("NPCFootStepsSoundPlayerPatch:PatchPrefix");
            //___stepAudioSource.dopplerLevel = 1f;
            //___stepAudioSource.priority = 1;
            //___stepAudioSource.reverbZoneMix = 1;
            //___stepAudioSource.rolloffMode = AudioRolloffMode.Linear;
            //___stepAudioSource.spatialBlend = 1;
            //___stepAudioSource.spread = 15;
            //PlayAtPointAudioClipPatch.SetupAudioSource(ref ____stepAudioSource);
            return true;
        }
    }
}

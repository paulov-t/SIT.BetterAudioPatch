using SIT;
using SIT.BetterAudioPatch;
using StayInTarkov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SIT.SITBetterAudio
{
    internal class BetterSourcePlayPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(BetterSource), "Play");
        }

        [PatchPrefix]
        public static bool PatchPrefix(BetterSource __instance)
        {
            //Logger.LogInfo("BetterSourcePlayPatch played!");
            __instance.GetType().GetMethod("RefreshSpatialization").Invoke(__instance, new object[] { false });

            //if (this._forceStereo)
            //{
            //    this.RefreshSpatialization(false);
            //    return;
            //}
            //if (this.Preset.DirectBinaural && this.Preset.DisableBinauralByDist)
            //{
            //    float num = GClass1928.Instance.Distance(this.Position);
            //    bool enabledSpat = num > this.Preset.EnableBinauralDist;
            //    bool flag;
            //    if (flag = (num < 1.5f))
            //    {
            //        enabledSpat = false;
            //    }
            //    if (GClass1928.Instance.Camera != null && !flag)
            //    {
            //        Transform transform = GClass1928.Instance.Camera.transform;
            //        Vector3 forward = transform.forward;
            //        Vector3 vector = this.Position - transform.position;
            //        float num2 = Vector2.SignedAngle(new Vector2(forward.x, forward.z), new Vector2(vector.x, vector.z));
            //        if (num2 > 90f || num2 < -90f)
            //        {
            //            enabledSpat = true;
            //        }
            //        if (Mathf.Abs(num2) < this.Preset.AngleToAllowBinaural)
            //        {
            //            enabledSpat = false;
            //        }
            //        if (Mathf.Abs(transform.position.y - this.Position.y) > this.Preset.HeightDiffToAllowBinaural)
            //        {
            //            enabledSpat = true;
            //        }
            //    }
            //    this.RefreshSpatialization(enabledSpat);
            //    return;
            //}

            return false;
        }
    }
}

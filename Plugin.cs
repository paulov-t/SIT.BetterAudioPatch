using BepInEx;
using BepInEx.Logging;
using EFT.NPC;
using Mono.Cecil;
using SIT.BetterAudioPatch.BaseSoundPlayerPatches;
using SIT.BetterAudioPatch.SITBetterAudio.NPCPlayerPatches;
using SIT.BetterAudioPatch.WeaponSoundPlayerPatches;
using SIT.SITBetterAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SIT.BetterAudioPatch
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource MainLogger { get; set; }

        private void Awake()
        {

            new PlayAtPointAudioClipPatch(Config).Enable();
            new PlayAtPointDistantPatch().Enable();
            new PlayAtPointSoundBankPatch().Enable();
            new PlayAtPointSoundBankSoundGroupPatch().Enable();
            //new PlayNonSpacialPatch().Enable();

            //new BSPPlayRandomClipPatch().Enable();
            //new WSPEnableSourceOcclusionPatch().Enable();
            //new WSPFireBulletPatch().Enable();

            //new NPCFootStepsSoundPlayerPatch().Enable();
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            MainLogger = Logger;
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            //assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            //assemblies.Add(Assembly.GetEntryAssembly());
            foreach(var assembly in assemblies)
            {
                MainLogger.LogInfo($"Found Assembly: {assembly.FullName}");
            }

            return assemblies.AsEnumerable();
        }

        public static Assembly GetAssemblyCSharp()
        {
            var assemblyCSharp = GetAssemblies().First(x => x.FullName.Contains("Assembly-CSharp"));
            if (assemblyCSharp != null)
                MainLogger.LogInfo("Found Assembly-CSharp");
            else
                MainLogger.LogError("Unable to find Assembly-CSharp");

            return assemblyCSharp;
        }

        public static Type GetTypeByName(string name)
        {
            return typeof(EFT.TarkovApplication).Assembly.GetTypes().ToDictionary(x=>x.FullName.ToLower(), x => x).First(x => x.Key == name.ToLower()).Value;
        }
    }
}

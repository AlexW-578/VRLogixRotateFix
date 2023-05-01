using BaseX;
using HarmonyLib;
using NeosModLoader;
using FrooxEngine;
using FrooxEngine.LogiX;

namespace NMLUpdater
{
    public class VRLogixRotateFix : NeosMod
    {
        public override string Name => "VRLogixRotateFix";
        public override string Author => "AlexW-578";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/AlexW-578/VRLogixRotateFix";

        private static ModConfiguration Config;

        [AutoRegisterConfigKey] private static readonly ModConfigurationKey<bool> Enabled =
            new ModConfigurationKey<bool>("Enabled", "Enable/Disable the Mod", () => true);


        public override void OnEngineInit()
        {
            Config = GetConfiguration();
            Config.Save(true);
            Harmony harmony = new Harmony("co.uk.alexw-578.VRLogixRotateFix");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(LogixTip), "PositionSpawnedNode")]
        class NeosLogixBrowser_Patch
        {
            static void Postfix(LogixTip __instance, Slot node)
            {
                if (Config.GetValue(Enabled) && __instance.InputInterface.VR_Active)
                {
                    node.GlobalRotation = new floatQ(__instance.LocalUserRoot.Slot.GlobalRotation.x,
                        __instance.Slot.LocalRotation.y, __instance.LocalUserRoot.Slot.GlobalRotation.z,
                        __instance.LocalUserRoot.Slot.GlobalRotation.w);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace StopLockingMyMouse
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class StopLocking : BaseUnityPlugin
    {
        // Mod info
        public const string pluginGuid = "endlessvex.clickmage.unlockmouse";
        public const string pluginName = "Unlock Mouse";
        public const string pluginVersion = "0.1";

        public void Awake()
        {
            Logger.LogInfo("Unlock Mouse: Awake - Applying the patch.");

            // Create Harmony instance and apply patches
            var harmony = new Harmony(pluginGuid);
            harmony.PatchAll(typeof(StopLocking).Assembly);

            Logger.LogInfo("Unlock Mouse: Patches applied.");
        }
    }

    // We are stopping the cursor from being locked
    [HarmonyPatch(typeof(Cursor), "set_lockState")]
    public static class PatchCursorLockState
    {
        // We are just setting the Cursor Lock Mode to be None so that if its set to Locked or Confined then its nolonger locked/confined
        [HarmonyPrefix]
        public static bool Prefix(ref CursorLockMode value)
        {
            value = CursorLockMode.None;
            return true;
        }
    }
}

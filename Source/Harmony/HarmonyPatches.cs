using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = HarmonyInstance.Create("rimworld.ogliss.CyberneticWarfare.wargearweapon");
            harmony.PatchAll();
        }

    }
}
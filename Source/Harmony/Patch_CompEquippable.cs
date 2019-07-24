using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{

    static class Patch_CompEquippable
    {

        [HarmonyPatch(typeof(CompEquippable))]
        [HarmonyPatch(nameof(CompEquippable.PrimaryVerb), MethodType.Getter)]
        static class Patch_PrimaryVerb
        {

            static void Postfix(CompEquippable __instance, ref Verb __result)
            {
                if (__instance.parent.GetComp<CompWargearWeaponToggle>() is CompWargearWeaponToggle toggleComp)
                    __result = __instance.AllVerbs.First(v => v.verbProps == toggleComp.ActiveVerbProps);
            }

        }
        
    }
}
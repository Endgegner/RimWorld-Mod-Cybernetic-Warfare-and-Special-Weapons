using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{

    static class Patch_Pawn
    {

        [HarmonyPatch(typeof(Pawn))]
        [HarmonyPatch(nameof(Pawn.GetGizmos))]
        static class Patch_GetGizmos
        {

            static void Postfix(Pawn __instance, ref IEnumerable<Gizmo> __result)
            {
                // If the pawn is drafted and is a player-controlled colonist, try and add gizmos from the pawn's equipped weapon
                if (__instance.IsColonistPlayerControlled && __instance.Drafted && __instance.equipment is Pawn_EquipmentTracker equipmentTracker)
                {
                    var primaryWeapon = equipmentTracker.Primary;
                    if (primaryWeapon != null && primaryWeapon.GetComp<CompWargearWeapon>() is CompWargearWeapon wargearComp)
                        foreach (var gizmo in wargearComp.EquippedGizmos())
                            __result = __result.Add(gizmo);
                }
            }

        }
        
    }
}
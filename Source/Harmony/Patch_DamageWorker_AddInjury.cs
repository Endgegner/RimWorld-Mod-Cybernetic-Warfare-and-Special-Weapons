using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{

    static class Patch_DamageWorker_AddInjury
    {

        [HarmonyPatch(typeof(DamageWorker_AddInjury))]
        [HarmonyPatch("ApplyToPawn")]
        static class Patch_ApplyToPawn
        {

            static void Postfix(DamageInfo dinfo, Pawn pawn)
            {
                // Apply stun
                if (pawn.stances != null)
                {
                    var damageDefExtension = dinfo.Def.GetModExtension<DamageDefExtension>() ?? DamageDefExtension.defaultValues;
                    if (damageDefExtension.stunDuration > 0)
                        pawn.stances.stunner.StunFor(damageDefExtension.stunDuration, dinfo.Instigator);
                }
            }

        }
        
    }
}
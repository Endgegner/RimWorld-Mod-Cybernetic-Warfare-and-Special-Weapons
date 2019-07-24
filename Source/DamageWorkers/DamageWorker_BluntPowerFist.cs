using UnityEngine;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CyberneticWarfare
{
    public class DamageWorker_BluntKnockback : DamageWorker_Blunt
    {

        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);

            var map = pawn.Map;

            // Determine knockback distance and direction
            var damageDefExtension = dinfo.Def.GetModExtension<DamageDefExtension>() ?? DamageDefExtension.defaultValues;
            int knockbackDistance = GenMath.RoundRandom(totalDamage * damageDefExtension.knockbackDistancePerDamageDealt * (damageDefExtension.scaleKnockbackWithBodySize ? pawn.BodySize : 1));
            var knockbackDirection = CyberneticWarfareUtility.IntVec3FromDirection8Way(CyberneticWarfareUtility.Direction8WayFromAngle((pawn.Position - dinfo.Instigator.Position).AngleFlat));

            float stunDuration = damageDefExtension.stunDuration;

            // Do knockback
            for (int i = 0; i < knockbackDistance; i++)
            {
                var newPosition = pawn.Position + knockbackDirection;
                // Position out of bounds
                if (!newPosition.InBounds(map))
                    break;

                // Check if the pawn hits an impassable cell
                if (newPosition.Impassable(map))
                {
                    stunDuration *= damageDefExtension.hitBuildingStunDurationFactor;
                    break;
                }
            }

            pawn.stances.stunner.StunFor(GenMath.RoundRandom(stunDuration), dinfo.Instigator);
        }

    }
}

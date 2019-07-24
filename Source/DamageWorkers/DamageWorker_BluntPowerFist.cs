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

            var map = pawn.MapHeld;

            // Determine knockback distance and direction
            var damageDefExtension = dinfo.Def.GetModExtension<DamageDefExtension>() ?? DamageDefExtension.defaultValues;
            int knockbackDistance = GenMath.RoundRandom(totalDamage * damageDefExtension.knockbackDistancePerDamageDealt * (damageDefExtension.scaleKnockbackWithBodySize ? 1 / pawn.BodySize : 1));
            var knockbackDirection = CyberneticWarfareUtility.IntVec3FromDirection8Way(CyberneticWarfareUtility.Direction8WayFromAngle((pawn.PositionHeld - dinfo.Instigator.PositionHeld).AngleFlat));

            if (knockbackDistance > 0)
            {
                float stunDuration = damageDefExtension.stunDuration;

                // Do knockback
                for (int i = 0; i < knockbackDistance; i++)
                {
                    var newPosition = pawn.PositionHeld + knockbackDirection;

                    // Position out of bounds
                    if (!newPosition.InBounds(map))
                        break;

                    // Check if the pawn hits an impassable cell
                    if (newPosition.Impassable(map))
                    {
                        stunDuration *= damageDefExtension.hitBuildingStunDurationFactor;
                        break;
                    }

                    // Apply knockback
                    if (pawn.ParentHolder is Corpse corpse)
                        corpse.Position += knockbackDirection;
                    pawn.Position += knockbackDirection;
                }

                if (!pawn.Dead)
                    pawn.Notify_Teleported(resetTweenedPos: false);

                if (stunDuration > 0 && pawn.stances != null)
                    pawn.stances.stunner.StunFor(GenMath.RoundRandom(stunDuration), dinfo.Instigator);
            }

            
        }

    }
}

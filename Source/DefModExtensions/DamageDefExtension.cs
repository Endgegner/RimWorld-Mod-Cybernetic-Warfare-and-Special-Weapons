using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CyberneticWarfare
{
    public class DamageDefExtension : DefModExtension
    {

        public static readonly DamageDefExtension defaultValues = new DamageDefExtension();

        // For DamageWorker_BluntPowerFist
        public float knockbackDistancePerDamageDealt;
        public bool scaleKnockbackWithBodySize;
        public float hitBuildingStunDurationFactor;

        // Can be applied to any DamageDef
        public int stunDuration;

    }
}

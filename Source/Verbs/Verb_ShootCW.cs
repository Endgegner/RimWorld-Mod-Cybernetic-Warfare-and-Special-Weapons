using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Verse;

namespace CyberneticWarfare
{
    public class VerbPropertiesCW : VerbProperties
    {
        public List<ResearchProjectDef> researchPrerequisites;
        public bool rapidfire = false;
        public int pelletCount = 1;
    }

    public class Verb_ShootCW : Verb_LaunchProjectile
    {
        public VerbPropertiesCW VerbProps
        {
            get
            {
                return verbProps as VerbPropertiesCW;
            }
        }
   
        protected override int ShotsPerBurst
        {
            get
            {
                if (VerbProps.rapidfire && caster.Position.InHorDistOf(currentTarget.Cell, verbProps.range / 2))
                    return verbProps.burstShotCount * 2;

                return verbProps.burstShotCount;
            }
        }

        public override void WarmupComplete()
        {
            base.WarmupComplete();

            var pawn = currentTarget.Thing as Pawn;
			if (pawn != null && !pawn.Downed && base.CasterIsPawn && base.CasterPawn.skills != null)
			{
				float baseExp = (!pawn.HostileTo(this.caster)) ? 20f : 170f;
				float cycleTime = verbProps.AdjustedFullCycleTime(this, CasterPawn);
				CasterPawn.skills.Learn(SkillDefOf.Shooting, baseExp * cycleTime, false);
			}
        }

        protected override bool TryCastShot()
        {
            bool castShotSuccess = base.TryCastShot();
            if (castShotSuccess && CasterIsPawn)
            {
                CasterPawn.records.Increment(RecordDefOf.ShotsFired);

            }

            bool launchExtraPellets = castShotSuccess && VerbProps.pelletCount - 1 > 0;
            if (launchExtraPellets)
            {
                for (int i = 0; i < VerbProps.pelletCount - 1; i++)
                {
                    base.TryCastShot();
                }
            }

            return castShotSuccess;
        }
    }
}

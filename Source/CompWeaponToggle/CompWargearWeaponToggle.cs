using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{
    public class CompProperties_WargearWeaponToggle : CompProperties
    {
        public CompProperties_WargearWeaponToggle()
        {
            this.compClass = typeof(CompWargearWeaponToggle);
        }
    }

    public class CompWargearWeaponToggle : CompWargearWeapon
    {
        private int activeVerbIndex;

        private List<VerbProperties> VerbProps => parent.def.Verbs;

        public new CompProperties_WargearWeaponToggle Props => (CompProperties_WargearWeaponToggle)props;

        public VerbProperties ActiveVerbProps => VerbProps[activeVerbIndex];

        public override IEnumerable<Gizmo> EquippedGizmos()
        {
            int baseGroupKey = 700000101;
            for (int i = 0; i < VerbProps.Count; i++)
            {
                // This exists because otherwise activeVerbIndex will always be equal to VerbProps' count
                int j = i;

                var verbSwitchCommand = new Command_Action()
                {
                    defaultLabel = VerbProps[i].label.CapitalizeFirst(),
                    defaultDesc = "CyborgWeaponry.SwitchFireMode".Translate(VerbProps[i].label),
                    icon = VerbProps[i].defaultProjectile.uiIcon,
                    activateSound = SoundDefOf.Click,
                    groupKey = baseGroupKey + i,
                    action = () => activeVerbIndex = j
                };

                // Not all research requirements are met
                if (VerbProps[i] is VerbPropertiesCW verbPropsCW && !verbPropsCW.researchPrerequisites.NullOrEmpty())
                    foreach (var research in verbPropsCW.researchPrerequisites)
                        if (!research.IsFinished)
                            verbSwitchCommand.Disable("CyborgWeaponry.ResearchRequirementNotMet".Translate());

                yield return verbSwitchCommand;
            }

        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref activeVerbIndex, "activeVerbIndex");
        }

    }
}

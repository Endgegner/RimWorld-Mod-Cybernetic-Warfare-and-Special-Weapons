using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CyberneticWarfare
{
    public class CompProperties_WargearWeapon : CompProperties
    {
        public CompProperties_WargearWeapon()
        {
            this.compClass = typeof(CompWargearWeapon);
        }
    }

    // Token: 0x02000002 RID: 2
    public class CompWargearWeapon : ThingComp
    {
        public CompProperties_WargearWeapon Props => (CompProperties_WargearWeapon)props;

        protected virtual Pawn EquippingPawn
        {
            get
            {
                if (ParentHolder is Pawn_EquipmentTracker equipmentTracker)
                    return equipmentTracker.pawn;

                return null;
            }
        }

        public virtual IEnumerable<Gizmo> EquippedGizmos()
        {
            yield break;
        }

    }
}

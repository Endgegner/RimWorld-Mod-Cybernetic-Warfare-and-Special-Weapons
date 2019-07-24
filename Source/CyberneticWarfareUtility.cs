using System;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CyberneticWarfare
{
    public static class CyberneticWarfareUtility
    {

        public static Direction8Way Direction8WayFromAngle(float angle)
        {
            if (angle >= 337.5f || angle < 22.5f)
            {
                return Direction8Way.North;
            }
            if (angle < 67.5f)
            {
                return Direction8Way.NorthEast;
            }
            if (angle < 112.5f)
            {
                return Direction8Way.East;
            }
            if (angle < 157.5f)
            {
                return Direction8Way.SouthEast;
            }
            if (angle < 202.5f)
            {
                return Direction8Way.South;
            }
            if (angle < 247.5f)
            {
                return Direction8Way.SouthWest;
            }
            if (angle < 292.5f)
            {
                return Direction8Way.West;
            }
            return Direction8Way.NorthWest;
        }

        public static IntVec3 IntVec3FromDirection8Way(Direction8Way source)
        {
            switch (source)
            {
                case Direction8Way.North:
                    return IntVec3.North;
                case Direction8Way.NorthEast:
                    return IntVec3.NorthEast;
                case Direction8Way.East:
                    return IntVec3.East;
                case Direction8Way.SouthEast:
                    return IntVec3.SouthEast;
                case Direction8Way.South:
                    return IntVec3.South;
                case Direction8Way.SouthWest:
                    return IntVec3.SouthWest;
                case Direction8Way.West:
                    return IntVec3.West;
                case Direction8Way.NorthWest:
                    return IntVec3.NorthWest;
            }
            throw new NotImplementedException();
        }

    }
}

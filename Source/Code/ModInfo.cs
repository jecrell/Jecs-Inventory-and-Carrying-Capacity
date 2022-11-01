using System;
using Verse;

namespace JescsCarryCapacity
{
    [StaticConstructorOnStartup]
    public static class ModInfo
    {
        public static bool inventoryMass_UseBodySize = true;
        public static float inventoryMass_Mulitplier = 1f;
        
        public static bool carryingCapacity_UseBodySize = true;
        public static float carryingCapacity_Mulitplier = 1f;
    }
}

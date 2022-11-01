using System;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace JescsCarryCapacity;

static partial class HarmonyPatches
{

    static void HarmonyPatches_MassCapacity(Harmony harmony)
    {
        //Patches the mass utility capacity number after it has already finished
        harmony.Patch(AccessTools.Method(typeof(MassUtility), "Capacity"), null,
            new HarmonyMethod(typeof(HarmonyPatches), nameof(Capacity_PostFix)));
    }
    
    
    // Uses the mod settings defined under the Mod Settings page in RimWorld
    // RimWorld.MassUtility
    public static void Capacity_PostFix(Pawn p, StringBuilder explanation, ref float __result)
    {
        //the default formula for RimWorld 1.4 is...
        //float num = p.BodySize * 35f;
        //Let's use our mod settings
        float bodySizeMultiplier = ModInfo.inventoryMass_UseBodySize ? p.BodySize : 1f;
        __result = 
             Clamp(ModInfo.inventoryMass_Mulitplier * (bodySizeMultiplier * 35f), 0,
                 Int32.MaxValue);
    }
        
    //Because... clamp is not a normal function anymore I guess? So let's write it here
    private static T Clamp<T>(T value, T min, T max)
        where T : System.IComparable<T> {
        T result = value;
        if (value.CompareTo(max) > 0)
            result = max;
        if (value.CompareTo(min) < 0)
            result = min;
        return result;
    }
}
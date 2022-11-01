using System;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace JescsCarryCapacity;

[StaticConstructorOnStartup]
static partial class HarmonyPatches
{
    static HarmonyPatches()
    {
        var harmony = new Harmony("rimworld.jecrell.carryingcapacity");
        
        //There are two places where 'carrying capacity' are defined.
        //One is the 'mass' allowed by an object that defines their inventory limit
        HarmonyPatches_MassCapacity(harmony);
        
        //Another is the StatDef that defines 'Carrying Capacity' or what can be carried in a pawn's hands
        HarmonyPatches_StatCarryingCapacity(harmony);
    }

}
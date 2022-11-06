using System;
using System.Linq;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace JescsCarryCapacity;

static partial class HarmonyPatches
{
    static void HarmonyPatches_StatCarryingCapacity(Harmony harmony)
    {
        //Patches the 'Carrying Capacity' base value when using CarryingCapacity StatDef
        harmony.Patch(AccessTools.Method(typeof(StatWorker), "GetValue", new[] {typeof(StatRequest), typeof(bool)}), null,
            new HarmonyMethod(typeof(HarmonyPatches), nameof(GetValue_PostFix)));
        
        //Patches the 'Body Size' modifier when using CarryingCapacity StatDef
        harmony.Patch(AccessTools.Method(typeof(StatPart_BodySize), "TransformValue"),
            new HarmonyMethod(typeof(HarmonyPatches), nameof(TransformValue_PreFix)));  
        
        //Changes the 'Body Size' explanation for CarryingCapacity StatDef to explain mod-related changes
        harmony.Patch(AccessTools.Method(typeof(StatPart_BodySize), "ExplanationPart"),
            new HarmonyMethod(typeof(HarmonyPatches), nameof(ExplanationPart_PreFix)));
    }

    //Apply the multiplier mod setting for carrying capacity
    //StatWorker
    public static void GetValue_PostFix(StatDef ___stat, ref float __result)
    {
        if (___stat == StatDefOf.CarryingCapacity)
            __result *= ModInfo.carryingCapacity_Mulitplier;
    }

    //When 'body size' is disabled from carrying capacity, prevent it from transforming values
    //StatPart_BodySize
    public static bool TransformValue_PreFix(StatPart_BodySize __instance, StatRequest req, ref float val)
    {
        if (__instance.parentStat == StatDefOf.CarryingCapacity &&
            !ModInfo.carryingCapacity_UseBodySize)
            return false;
        
        return true;
    }

    //Change the text reporting of the stat to inform the player of mod-related changes
    //StatPart_BodySize
    public static bool ExplanationPart_PreFix(StatPart_BodySize __instance, StatRequest req, ref string __result)
    {
        if (__instance.parentStat == StatDefOf.CarryingCapacity)
        {
            StringBuilder resultString = new StringBuilder();
            
            //Inform the player when the 'body size' multiplier is disabled
            if (!ModInfo.carryingCapacity_UseBodySize)
            {
                resultString.AppendLine("JCC_StatInfoBodySizeMultiplierDisabled".Translate());
            }
            //otherwise use the base method //TODO Check this when RimWorld updates to 1.5+
            else
            {
                float bodySize = 1f;
                if (PawnOrCorpseStatUtility.TryGetPawnOrCorpseStat(req, (Pawn x) => x.BodySize, (ThingDef x) => x.race.baseBodySize, out bodySize))
                {
                    resultString.AppendLine("StatsReport_BodySize".Translate(bodySize.ToString("F2")) + ": x" +
                                            bodySize.ToStringPercent());
                }
            }
            
            //Only show the modifier if it's in effect
            if (ModInfo.carryingCapacity_Mulitplier != 1f)
            {
                resultString.AppendLine("JCC_StatInfoMultiplier".Translate(ModInfo.carryingCapacity_Mulitplier.ToString("0.00") + "x"));
            }
            __result = resultString.ToString();
            return false;
        }
        return true;

    }
    
}

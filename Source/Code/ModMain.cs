using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace JescsCarryCapacity;

public class ModMain : Mod
{
    Settings settings;
        
    public ModMain(ModContentPack content) : base(content: content)
    {
        settings = GetSettings<Settings>();
        
        ModInfo.inventoryMass_UseBodySize = settings.inventoryMass_UseBodySize;
        ModInfo.inventoryMass_Mulitplier = settings.inventoryMass_Mulitplier;
        
        ModInfo.carryingCapacity_UseBodySize = settings.carryingCapacity_UseBodySize;
        ModInfo.carryingCapacity_Mulitplier = settings.carryingCapacity_Mulitplier;
    }

    public override string SettingsCategory()
    {
        return "Jec's Capacity Settings";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {

        var fColWidth = inRect.width * 0.55f;
        var fRowHeight = inRect.height * 0.07f;
        var rowCount = 1;
        var colCount = 1;

                
        // Welcome message
        var rWelcome = new Rect(
            x: inRect.x, 
            y: inRect.y, 
            width: fColWidth * colCount, 
            height: (fRowHeight *2)
            );
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(
            rect: rWelcome, 
            label: "JCC_Welcome".Translate()
            );
        rowCount++;
        
        /////////////////////////////////////////////////////
        /// 
        /// INVENTORY CAPACITY
        ///
        ////////////////////////////////////////////////////
        
        
        // Header for Inventory Capacity
        var rHeaderInventoryMass = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount, 
            height: fRowHeight
            );
        Text.Font = GameFont.Medium;
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(
            rect: rHeaderInventoryMass, 
            label: "JCC_HeaderInventoryMass".Translate()
            );
        Text.Font = GameFont.Small;
        rowCount++;

        
        // Body Size toggle box
        var rInventoryMassBodySizeToggle = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount,
            height: fRowHeight
            );
        var rInventoryMassBodySizeToggleLeft = rInventoryMassBodySizeToggle.LeftPart(pct: 0.666f).Rounded();
        var rInventoryMassBodySizeToggleRight = rInventoryMassBodySizeToggle.RightPart(pct: 0.333f).Rounded();
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(
            rect: rInventoryMassBodySizeToggleLeft,
            label: "JCC_AllowBodySizeMultiplier".Translate()
            );
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.CheckboxLabeled(
            rect: rInventoryMassBodySizeToggleRight, 
            label: "", 
            checkOn: ref settings.inventoryMass_UseBodySize
            );
        TooltipHandler.TipRegion(
            rect: rInventoryMassBodySizeToggle,
            textGetter: () => "JCC_AllowBodySizeMultiplierICDesc".Translate(),
            uniqueId: 312356
            );
        rowCount++;

        
        // Inventory mass/capacity multiplier
        var rCarryingCapacityMultiplier1 = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount,
            height: fRowHeight
            );
        Widgets.Label(
            rect: rCarryingCapacityMultiplier1.TopHalf(), 
            label: "JCC_InventoryCapacityMultiplier".Translate()
            );
        settings.inventoryMass_Mulitplier = Widgets.HorizontalSlider(
            rect: rCarryingCapacityMultiplier1.BottomHalf(),
            value: settings.inventoryMass_Mulitplier, 
            leftValue: 0.0f, 
            rightValue: 10f, 
            middleAlignment: false,
            label: settings.inventoryMass_Mulitplier + "x", 
            leftAlignedLabel: null, 
            rightAlignedLabel: null, 
            roundTo: 0.25f);
        TooltipHandler.TipRegion(
            rect: rCarryingCapacityMultiplier1,
            textGetter: () => "JCC_InventoryCapacityMultiplierTooltip".Translate(
                35f,
                settings.inventoryMass_UseBodySize ? "JCC_BodySize".Translate() : "",
                settings.inventoryMass_Mulitplier.ToString("0.00"),
                35f * settings.inventoryMass_Mulitplier),
            uniqueId: 312357
            );
        rowCount++;
        rowCount++;
        
                
        /////////////////////////////////////////////////////
        /// 
        /// CARRYING CAPACITY
        ///
        ////////////////////////////////////////////////////
        
                
        // Header for Inventory Capacity
        var rHeaderCarryingCapacity = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount, 
            height: fRowHeight
            );
        Text.Font = GameFont.Medium;
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(
            rect: rHeaderCarryingCapacity, 
            label: "JCC_HeaderCarryingCapacity".Translate()
            );
        Text.Font = GameFont.Small;
        rowCount++;
        
        
        // Body Size toggle box
        var rBodySizeToggle = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount,
            height: fRowHeight
            );
        var rBodySizeToggleLeft = rBodySizeToggle.LeftPart(pct: 0.666f).Rounded();
        var rBodySizeToggleRight = rBodySizeToggle.RightPart(pct: 0.333f).Rounded();
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(
            rect: rBodySizeToggleLeft, 
            label: "JCC_AllowBodySizeMultiplier".Translate()
            );
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.CheckboxLabeled(
            rect: rBodySizeToggleRight,
            label: "",
            checkOn: ref settings.carryingCapacity_UseBodySize
            );
        TooltipHandler.TipRegion(
            rect: rBodySizeToggle,
            textGetter: () => "JCC_AllowBodySizeMultiplierCCDesc".Translate(),
            uniqueId: 312358
            );
        rowCount++;

        
        // Carrying capacity multiplier
        var rCarryingCapacityMultiplier = new Rect(
            x: inRect.x, 
            y: inRect.y + fRowHeight * rowCount, 
            width: fColWidth * colCount,
            height: fRowHeight
            );
        Widgets.Label(
            rect: rCarryingCapacityMultiplier.TopHalf(),
            label: "JCC_CarryCapacityMultiplier".Translate()
            );
        settings.carryingCapacity_Mulitplier = Widgets.HorizontalSlider(
            rect: rCarryingCapacityMultiplier.BottomHalf(),
            value: settings.carryingCapacity_Mulitplier,
            leftValue: 0.0f,
            rightValue: 10f,
            middleAlignment: false,
            label: settings.carryingCapacity_Mulitplier + "x", 
            leftAlignedLabel: null,
            rightAlignedLabel: null, 
            roundTo: 0.25f
            );
        TooltipHandler.TipRegion(
            rect: rCarryingCapacityMultiplier,
            textGetter: () => "JCC_CarryCapacityMultiplierTooltip".Translate(
                StatDefOf.CarryingCapacity.defaultBaseValue,
                settings.carryingCapacity_UseBodySize ? "JCC_BodySize".Translate() : "",
                settings.carryingCapacity_Mulitplier.ToString("0.00"),
                StatDefOf.CarryingCapacity.defaultBaseValue * settings.carryingCapacity_Mulitplier
            ),
            uniqueId: 312359
            );

        WriteSettings();
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        
        ModInfo.inventoryMass_UseBodySize = settings.inventoryMass_UseBodySize;
        ModInfo.inventoryMass_Mulitplier = settings.inventoryMass_Mulitplier;
        
        ModInfo.carryingCapacity_UseBodySize = settings.carryingCapacity_UseBodySize;
        ModInfo.carryingCapacity_Mulitplier = settings.carryingCapacity_Mulitplier;
    }

    public class Settings : ModSettings
    {
        public bool inventoryMass_UseBodySize = true;
        public bool carryingCapacity_UseBodySize = true;
        
        public float inventoryMass_Mulitplier = 1f;
        public float carryingCapacity_Mulitplier = 1f;

        
        public override void ExposeData()
        {
            base.ExposeData();
            
            Scribe_Values.Look(value: ref inventoryMass_Mulitplier, label: "inventoryMass_Mulitplier");
            Scribe_Values.Look(value: ref inventoryMass_UseBodySize, label: "inventoryMass_UseBodySize");
            
            Scribe_Values.Look(value: ref carryingCapacity_Mulitplier, label: "carryingCapacity_Mulitplier");
            Scribe_Values.Look(value: ref carryingCapacity_UseBodySize, label: "carryingCapacity_UseBodySize");
        }
    }
    
}
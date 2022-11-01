using System;
using UnityEngine;
using Verse;

namespace JescsCarryCapacity;

public class ModMain : Mod
{
    Settings settings;
        
    public ModMain(ModContentPack content) : base(content)
    {
        this.settings = GetSettings<Settings>();
        
        ModInfo.inventoryMass_UseBodySize = this.settings.inventoryMass_UseBodySize;
        ModInfo.inventoryMass_Mulitplier = this.settings.inventoryMass_Mulitplier;
        
        ModInfo.carryingCapacity_UseBodySize = this.settings.carryingCapacity_UseBodySize;
        ModInfo.carryingCapacity_Mulitplier = this.settings.carryingCapacity_Mulitplier;
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
        var rWelcome = new Rect(inRect.x, inRect.y, fColWidth * colCount, (fRowHeight *2));
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(rWelcome, "JCC_Welcome".Translate());
        rowCount++;
        
        /////////////////////////////////////////////////////
        /// 
        /// INVENTORY CAPACITY
        ///
        ////////////////////////////////////////////////////
        
        // Header for Inventory Capacity
        var rHeaderInventoryMass = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount, fRowHeight);
        Text.Font = GameFont.Medium;
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(rHeaderInventoryMass, "JCC_HeaderInventoryMass".Translate());
        Text.Font = GameFont.Small;
        rowCount++;

        // Body Size toggle box
        var rInventoryMassBodySizeToggle = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount,
            fRowHeight);
        var rInventoryMassBodySizeToggleLeft = rInventoryMassBodySizeToggle.LeftPart(0.666f).Rounded();
        var rInventoryMassBodySizeToggleRight = rInventoryMassBodySizeToggle.RightPart(0.333f).Rounded();
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(rInventoryMassBodySizeToggleLeft, "JCC_AllowBodySizeMultiplier".Translate());
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.CheckboxLabeled(rInventoryMassBodySizeToggleRight, "", ref this.settings.inventoryMass_UseBodySize);
        TooltipHandler.TipRegion(rInventoryMassBodySizeToggle, () => "".Translate(), 312356);
        rowCount++;

        // Inventory mass/capacity multiplier
        var rCarryingCapacityMultiplier1 = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount,
            fRowHeight);
        Widgets.Label(rCarryingCapacityMultiplier1.TopHalf(), "JCC_InventoryCapacityMultiplier".Translate());
        this.settings.inventoryMass_Mulitplier = Widgets.HorizontalSlider(rCarryingCapacityMultiplier1.BottomHalf(),
            this.settings.inventoryMass_Mulitplier, 0.0f, 10f, false, (this.settings.inventoryMass_Mulitplier.ToString()) + "x", null, null, 0.25f);
        TooltipHandler.TipRegion(rCarryingCapacityMultiplier1, () => "JCC_CarryCapacityMultiplierTooltip".Translate(),
            312357);
        rowCount++;
        rowCount++;
        
                
        /////////////////////////////////////////////////////
        /// 
        /// CARRYING CAPACITY
        ///
        ////////////////////////////////////////////////////
        
                
        // Header for Inventory Capacity
        var rHeaderCarryingCapacity = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount, fRowHeight);
        Text.Font = GameFont.Medium;
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(rHeaderCarryingCapacity, "JCC_HeaderCarryingCapacity".Translate());
        Text.Font = GameFont.Small;
        rowCount++;
        
        // Body Size toggle box
        var rBodySizeToggle = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount,
            fRowHeight);
        var rBodySizeToggleLeft = rBodySizeToggle.LeftPart(0.666f).Rounded();
        var rBodySizeToggleRight = rBodySizeToggle.RightPart(0.333f).Rounded();
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(rBodySizeToggleLeft, "JCC_AllowBodySizeMultiplier".Translate());
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.CheckboxLabeled(rBodySizeToggleRight, "", ref this.settings.carryingCapacity_UseBodySize);
        TooltipHandler.TipRegion(rBodySizeToggle, () => "".Translate(), 312358);
        rowCount++;

        // Carrying capacity multiplier
        var rCarryingCapacityMultiplier = new Rect(inRect.x, inRect.y + fRowHeight * rowCount, fColWidth * colCount,
            fRowHeight);
        Widgets.Label(rCarryingCapacityMultiplier.TopHalf(), "JCC_CarryCapacityMultiplier".Translate());
        this.settings.carryingCapacity_Mulitplier = Widgets.HorizontalSlider(rCarryingCapacityMultiplier.BottomHalf(),
            this.settings.carryingCapacity_Mulitplier, 0.0f, 10f, false, (this.settings.carryingCapacity_Mulitplier.ToString()) + "x", null, null, 0.25f);
        TooltipHandler.TipRegion(rCarryingCapacityMultiplier, () => "JCC_CarryCapacityMultiplierTooltip".Translate(),
            312359);
        rowCount++;
        rowCount++;


        this.WriteSettings();
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        
        ModInfo.inventoryMass_UseBodySize = this.settings.inventoryMass_UseBodySize;
        ModInfo.inventoryMass_Mulitplier = this.settings.inventoryMass_Mulitplier;
        
        ModInfo.carryingCapacity_UseBodySize = this.settings.carryingCapacity_UseBodySize;
        ModInfo.carryingCapacity_Mulitplier = this.settings.carryingCapacity_Mulitplier;
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
            
            Scribe_Values.Look(ref inventoryMass_Mulitplier, "inventoryMass_Mulitplier");
            Scribe_Values.Look(ref inventoryMass_UseBodySize, "inventoryMass_UseBodySize");
            
            Scribe_Values.Look(ref carryingCapacity_Mulitplier, "carryingCapacity_Mulitplier");
            Scribe_Values.Look(ref carryingCapacity_UseBodySize, "carryingCapacity_UseBodySize");
        }
    }
    
}
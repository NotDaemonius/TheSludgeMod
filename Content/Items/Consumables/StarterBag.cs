using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.Items.Zinc;

namespace TheSludgeMod.Content.Items.Consumables;

public class StarterBag : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 3;

    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.White;
    }

    public override bool CanRightClick() => true;

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBroadsword>()));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBow>()));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<WoodTreeWand>()));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<RopeWhip>()));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<WoodCrystal>()));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Shuriken, 1, 100, 100));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.WoodenArrow, 1, 100, 100));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Torch, 1, 50, 50));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Rope, 1, 50, 50));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.LesserHealingPotion, 1, 10, 10));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.RecallPotion, 1, 3, 3));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ShinePotion, 1, 2, 2));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SpelunkerPotion, 1, 2, 2));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ManaCrystal));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Chest));
    }
}
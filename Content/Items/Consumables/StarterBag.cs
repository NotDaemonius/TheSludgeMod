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
    }
}
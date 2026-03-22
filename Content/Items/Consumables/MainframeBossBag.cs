using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Items.Vanity;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.NPCs.TheMainframe;

namespace TheSludgeMod.Content.Items.Consumables;

public class MainframeBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.BossBag[Type] = true;
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = false;
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Expert;
        Item.expert = true;
    }

    public override bool CanRightClick() => true;

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<MainframeMaskHead>(), 7));
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BrainOfPerplexity>(), 1));
        itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<ThePainframe>(), ModContent.ItemType<GemshardObliterator>(), ModContent.ItemType<Laserstorm>(), ModContent.ItemType<NeuralShardStaff>()));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofSpite>(), 1, 25, 40));
        itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<TheMainframe>()));
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Content.Items.Accessories;

public class BrainOfPerplexity : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 30;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Expert;
        Item.accessory = true;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        return player.brainOfConfusionItem == null || player.brainOfConfusionItem.IsAir;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BrainOfPerplexityPlayer>().hasBrainOfPerplexity = true;
    }
}

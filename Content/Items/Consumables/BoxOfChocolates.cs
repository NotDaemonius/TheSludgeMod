using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Consumables;

public class BoxOfChocolates : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 10;

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 28;

        Item.rare = ItemRarityID.Orange;
        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
    }

    public override bool CanRightClick() => true;

    public override void OnConsumeItem(Player player)
    {
        Item chosenItem;
        while (true)
        {
            if (!ContentSamples.ItemsByType.TryGetValue(Main._rand.Next(0, ItemLoader.ItemCount), out chosenItem))
                continue;
            if (chosenItem.rare <= ItemRarityID.Orange && !chosenItem.IsAir)
                break;
        }

        player.QuickSpawnItem(Item.GetSource_FromThis(), chosenItem.type);
    }
}

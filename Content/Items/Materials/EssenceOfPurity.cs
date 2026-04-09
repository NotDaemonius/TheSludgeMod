using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Materials;

public class EssenceOfPurity : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemIconPulse[Type] = true;
        ItemID.Sets.ItemNoGravity[Type] = true;
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;

		Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(silver: 15);
        Item.material = true;
        Item.maxStack = 9999;
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Junk;

public class SpeckOfDust : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 2;

    public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.value = Item.buyPrice(copper: 1);
		Item.rare = ItemRarityID.Gray;
	}
}

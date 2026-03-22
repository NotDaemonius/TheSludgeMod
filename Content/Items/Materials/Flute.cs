using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Materials;

public class Flute : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.rare = ItemRarityID.Orange;
        Item.maxStack = 9999;
        Item.material = true;
        Item.value = 10000;
    }
}

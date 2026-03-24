using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Materials;

public class NoxiousGoo : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

    public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.rare = ItemRarityID.LightRed;
        Item.maxStack = 9999;
        Item.material = true;
        Item.value = Item.buyPrice(silver: 5);
    }
}

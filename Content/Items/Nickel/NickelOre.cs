using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items.Nickel
{
	public class NickelOre : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.DefaultToPlaceableTile(ModContent.TileType<NickelOreTile>());
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 2);
			Item.rare = ItemRarityID.White;
			Item.autoReuse = true;
			Item.useTurn = true;
            Item.material = true;
        }
	}
}

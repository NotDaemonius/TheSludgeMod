using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class ShadowvoidOre : ModItem
	{
		
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 100;
			ItemID.Sets.SortingPriorityMaterials[Type] = 58;
		}
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShadowvoidOre>());
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(gold: 15);
			Item.rare = ItemRarityID.Purple;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
	}
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class CD : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.mana = 1;
			
			Item.width = 28;
			Item.height = 28;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.CloneDefaults(ItemID.ThornChakram);
			Item.shoot = ModContent.ProjectileType<CDProj>();
			Item.DamageType = DamageClass.Magic;
		}
		public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleSpores, 10);
			recipe.AddIngredient(ItemID.Stinger, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}

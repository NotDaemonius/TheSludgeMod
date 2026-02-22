using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class Basher : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 12;
			Item.value = Item.buyPrice(gold: 2);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item153;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = 967;
			Item.shootSpeed = 8f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Hellstone, 50);
			recipe.AddIngredient(ItemID.Obsidian, 50);
			recipe.AddIngredient(ItemID.SoulofFlight, 25);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
			recipe.AddIngredient(ItemID.FrostCore, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float numberProjectiles = 10;
			float rotation = MathHelper.ToRadians(5);

			
			for (int i = 0; i < numberProjectiles; i++)
			{
				position += Vector2.Normalize(velocity) * 10f;
				Vector2 peturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation * i, rotation * i, i / (numberProjectiles - 1)));
				Projectile.NewProjectile(source, position, peturbedSpeed, type, damage, knockback, player.whoAmI);
			}
            return false;
        }
	}
}

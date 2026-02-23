using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class CoolSword : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 47;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(silver: 80);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<CoolSwordProj>();
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float numberProjectiles = 2 + Main.rand.Next(3);
			float rotation = MathHelper.ToRadians(10);

			position += Vector2.Normalize(velocity) * 10f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 peturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
				Projectile.NewProjectile(source, position, peturbedSpeed, type, damage, knockback, player.whoAmI);
			}
            return false;
        }
	}
}

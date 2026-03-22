using TheSludgeMod.Content.Projectiles.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

public class Maelthorn : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;

    public override void SetDefaults() {
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.knockBack = 5f;
		Item.width = 32;
		Item.height = 32;
		Item.damage = 15;
		Item.noUseGraphic = true;
		Item.shoot = ModContent.ProjectileType<MaelthornProjectile>();
		Item.shootSpeed = 12f;
		Item.UseSound = SoundID.Item1;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(gold: 1, silver: 50);
		Item.DamageType = DamageClass.MeleeNoSpeed;
		Item.channel = true;
		Item.noMelee = true;
	}

	public override void AddRecipes() 
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Stinger, 12);
		recipe.AddIngredient(ItemID.JungleSpores, 8);
		recipe.AddIngredient(ItemID.Vine, 2);
		recipe.AddTile(TileID.Anvils);
		recipe.Register();
	}
}
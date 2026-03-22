using TheSludgeMod.Content.Projectiles.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

	public class ThePainframe : ModItem
	{
    public override void SetStaticDefaults() => ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;

    public override void SetDefaults() {
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.knockBack = 7f;
		Item.width = 32;
		Item.height = 32;
		Item.damage = 135;
		Item.noUseGraphic = true;
		Item.shoot = ModContent.ProjectileType<ThePainframeProjectile>();
		Item.shootSpeed = 16f;
		Item.crit = 15;
		Item.UseSound = SoundID.Item1;
		Item.rare = ItemRarityID.LightPurple;
		Item.value = Item.sellPrice(gold: 5);
		Item.DamageType = DamageClass.MeleeNoSpeed; 
		Item.channel = true;
		Item.noMelee = true;
	}
}
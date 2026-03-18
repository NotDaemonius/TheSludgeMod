using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
	public class Glaive : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 93;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.crit = 16;
			Item.knockBack = 5f;
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<GlaiveProj>();
			Item.DamageType = DamageClass.Melee;
		}
		public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 3;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<SoulofSpite>(), 10)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddIngredient(ItemID.LightDisc)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}

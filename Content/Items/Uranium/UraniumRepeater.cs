using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium
{
    public class UraniumRepeater : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(gold: 2, silver: 40);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.crit = 2;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 9.5f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<UraniumBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
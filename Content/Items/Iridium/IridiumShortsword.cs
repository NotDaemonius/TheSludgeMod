using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium
{
    public class IridiumShortsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(silver: 28);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.crit = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<IridiumShortswordProj>();
            Item.shootSpeed = 2.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<IridiumBar>(6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium
{
    public class IridiumBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(silver: 28);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.crit = 1;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 6.6f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<IridiumBar>(7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
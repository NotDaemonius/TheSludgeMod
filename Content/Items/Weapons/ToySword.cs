using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalItems;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class ToySword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Glowmasks.AddGlowMask(this);
        }
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.crit = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Plastic>(10);
            recipe.AddIngredient<Battery>(1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth
{
    public class BismuthPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 7;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 4, silver: 32);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.pick = 195; 
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            Item.useTurn = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismuthBar>(18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

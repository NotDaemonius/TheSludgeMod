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
using TheSludgeMod.Content.Items.Aluminium;

namespace TheSludgeMod.Content.Items.Aluminium
{
    public class AluminiumHammer : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(silver: 16);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.hammer = 54; 
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            Item.useTurn = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AluminiumBar>(8);
            recipe.AddIngredient(ItemID.Wood, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

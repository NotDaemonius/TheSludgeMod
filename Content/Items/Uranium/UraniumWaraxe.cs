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

namespace TheSludgeMod.Content.Items.Uranium
{
    public class UraniumWaraxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Melee;
            Item.width = 58;
            Item.height = 48;
            Item.useTime = 11;
            Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 2, silver: 16);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.axe = 16; 
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            Item.useTurn = true;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class Bat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.autoReuse = true;
            Item.shootSpeed = 15;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 6;
            Item.knockBack = 8;
            Item.crit = 12;
            Item.value = Item.buyPrice(copper: 40);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 9)
            .AddTile(TileID.WorkBenches);
        }
    }
}

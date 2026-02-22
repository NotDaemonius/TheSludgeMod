using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items
{
    public class Bat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30; // The item texture's width.
            Item.height = 30; // The item texture's height.
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.useTime = 35; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 35; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.shootSpeed = 15;
            Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
            Item.damage = 6; // The damage your item deals.
            Item.knockBack = 8; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

            Item.value = Item.buyPrice(copper: 40); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.

            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Wood, 9).AddTile(TileID.WorkBenches);
        }
    }
}

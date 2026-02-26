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
    public class AmethystBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 37;
            Item.useAnimation = 37;
            Item.autoReuse = true;
            Item.shootSpeed = 6;
            Item.shoot = ProjectileID.AmethystBolt;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 15;
            Item.knockBack = 3.25f;
            Item.value = Item.buyPrice(silver: 4);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Amethyst, 10)
            .AddTile(TileID.Anvils);
        }
    }
}

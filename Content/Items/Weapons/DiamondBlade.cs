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
    public class DiamondBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.autoReuse = true;
            Item.shootSpeed = 9.5f;
            Item.shoot = ProjectileID.DiamondBolt;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 23;
            Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(silver: 60);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Diamond, 10)
            .AddTile(TileID.Anvils);
        }
    }
}

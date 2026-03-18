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
    public class TopazBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.autoReuse = true;
            Item.shootSpeed = 6.5f;
            Item.shoot = ProjectileID.TopazBolt;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 16;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(silver: 6);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Topaz, 10)
            .AddTile(TileID.Anvils);
        }
    }
}

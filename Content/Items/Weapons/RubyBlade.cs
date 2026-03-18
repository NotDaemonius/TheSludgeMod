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
    public class RubyBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.autoReuse = true;
            Item.shootSpeed = 9f;
            Item.shoot = ProjectileID.RubyBolt;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 21;
            Item.knockBack = 4.75f;
            Item.value = Item.buyPrice(silver: 40);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ruby, 10)
            .AddTile(TileID.Anvils);
        }
    }
}

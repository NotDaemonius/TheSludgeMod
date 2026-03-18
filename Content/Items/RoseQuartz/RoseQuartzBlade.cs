using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartzBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.autoReuse = true;
            Item.shootSpeed = 7.5f;
            Item.shoot = ModContent.ProjectileType<RoseQuartz_Bolt>();
            Item.DamageType = DamageClass.Melee;
            Item.damage = 18;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(silver: 20);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<RoseQuartz>(), 10)
            .AddTile(TileID.Anvils);
        }
    }
}

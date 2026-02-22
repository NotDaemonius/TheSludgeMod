using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class ItemRoseQuartzStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.mana = 6;
            Item.UseSound = SoundID.Item43;
            Item.useStyle = ItemUseStyleID.Shoot;  // same as 5, just clearer
            Item.damage = 18;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.width = 40;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<RoseQuartz_Bolt>();
            Item.shootSpeed = 7.5f;
            Item.knockBack = 4f;
            Item.value = 10000;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Blue;  // same as 1, just clearer
            Item.noMelee = true;
            Item.channel = true;           // ← this is what was missing
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RoseQuartz>(), 8)
                .AddIngredient(ItemID.IronBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

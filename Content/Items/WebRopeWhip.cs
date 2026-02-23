using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
    public class WebRopeWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<WebRopeWhipProjectile>(), 8, 2, 4);
            Item.reuseDelay = 14;
            Item.rare = ItemRarityID.White;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.WebRope, 100);
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}

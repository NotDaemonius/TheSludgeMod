using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class VineRopeWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<VineRopeWhipProjectile>(), 8, 2, 4);
            Item.reuseDelay = 14;
            Item.rare = ItemRarityID.White;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.VineRope, 100);
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}

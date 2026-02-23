using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class PinkPhaseblade : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BluePhaseblade);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<RoseQuartz>(), 15)
            .AddIngredient(ItemID.MeteoriteBar, 15)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

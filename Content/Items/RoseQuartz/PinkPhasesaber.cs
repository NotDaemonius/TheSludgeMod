using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class PinkPhasesaber : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.OrangePhasesaber);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PinkPhaseblade>())
                .AddIngredient(ItemID.CrystalShard, 25)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

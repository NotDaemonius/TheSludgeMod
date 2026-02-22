using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartzStoneBlock : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.EmeraldStoneBlock);
            Item.createTile = ModContent.TileType<RoseQuartzStoneTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RoseQuartz>(), 1)
                .AddIngredient(ItemID.StoneBlock, 1)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }
    }
}

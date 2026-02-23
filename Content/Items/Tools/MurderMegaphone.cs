using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.Systems;

namespace TheSludgeMod.Content.Items.Tools
{
    public class MurderMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 54;
            Item.useAnimation = 54;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.PlayerKilled;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 4f * player.direction;
            player.itemLocation.Y += 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroups.AnyTombstone, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
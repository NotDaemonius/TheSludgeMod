using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools
{
    public class MoonLordMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 250;
            Item.useAnimation = 250;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Zombie92;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 4f * player.direction;
            player.itemLocation.Y += 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
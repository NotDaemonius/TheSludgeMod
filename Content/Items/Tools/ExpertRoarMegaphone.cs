using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools
{
    public class ExpertRoarMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 76;
            Item.useAnimation = 76;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Roar with { Pitch = 0.6f };
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 4f * player.direction;
            player.itemLocation.Y += 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
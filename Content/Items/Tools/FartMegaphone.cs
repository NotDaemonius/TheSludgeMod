using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools;

public class FartMegaphone : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 72;
        Item.useAnimation = 72;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.NPCDeath64;
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        player.itemLocation.X -= 4f * player.direction;
        player.itemLocation.Y += 10f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GelBalloon, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
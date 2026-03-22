using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools;

public class DeathWhistleMegaphone : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 150;
        Item.useAnimation = 150;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = new SoundStyle("TheSludgeMod/Content/Items/Tools/Roar_2.wav");
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        player.itemLocation.X -= 4f * player.direction;
        player.itemLocation.Y += 10f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BeetleShell, 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
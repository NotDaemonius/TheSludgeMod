using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools
{
    public class GiggleMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 155;
            Item.useAnimation = 155;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Zombie123;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 4f * player.direction;
            player.itemLocation.Y += 10f;
        }
    }
}
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.MushroomBiome;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomGrassSeeds : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 25;

    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTime = 10;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = Item.CommonMaxStack;
        Item.value = Item.buyPrice(silver: 1, copper: 50);
    }

    public override bool? UseItem(Player player) => true;

    public override bool ConsumeItem(Player player)
    {
        var tileX = Player.tileTargetX;
        var tileY = Player.tileTargetY;
        var tile = Framing.GetTileSafely(tileX, tileY);

        if (tile.HasTile && tile.TileType == TileID.Mud && player.IsInTileInteractionRange(tileX, tileY, TileReachCheckSettings.Simple))
        {
            tile.TileType = (ushort)ModContent.TileType<MushroomGrass>();
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(player.whoAmI, tileX, tileY);
            }
            SoundEngine.PlaySound(SoundID.Dig, player.Center);
            return true;
        }

        return false;
    }
}
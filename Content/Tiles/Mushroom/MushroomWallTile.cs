using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomWallTile : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(237, 160, 69));
        DustType = DustID.Pumpkin;
        VanillaFallbackOnModDeletion = WallID.Mushroom;
        HitSound = SoundID.Dig;
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomBeamTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoSunLight[Type] = false;
        Main.tileFrameImportant[Type] = false;
        Main.tileLighted[Type] = true;
        TileID.Sets.IsBeam[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        DustType = DustID.Pumpkin;
        HitSound = SoundID.Dig;
        AddMapEntry(new Color(237, 160, 69));
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomBlockTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileMergeDirt[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(237, 160, 69));
        DustType = DustID.Shadowflame;
        VanillaFallbackOnModDeletion = TileID.MushroomBlock;
        HitSound = SoundID.Dig;
    }
}

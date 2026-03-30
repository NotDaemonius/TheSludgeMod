using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Tiles;

public class GreenStickyNoteTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSign[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;
        TileID.Sets.AvoidedByNPCs[Type] = true;
        TileID.Sets.TileInteractRead[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;
        VanillaFallbackOnModDeletion = TileID.Signs;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(161, 230, 115), CreateMapEntryName());
        DustType = DustID.Confetti_Green;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail && !effectOnly) Sign.KillSign(i, j);
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
}
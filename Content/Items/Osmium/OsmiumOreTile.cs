using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumOreTile : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.Ore[Type] = true;
		TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 635;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 975;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(118, 126, 184), name);
		DustType = DustID.Lead;
		VanillaFallbackOnModDeletion = TileID.Orichalcum;
		HitSound = SoundID.Tink;
		MinPick = 110;
		MineResist = 3;
	}
}

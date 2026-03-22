using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles;

public class ShadowvoidOre : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.Ore[Type] = true;
		TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 1010;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 975;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(102, 0, 255), name);
		DustType = DustID.Shadowflame;
		VanillaFallbackOnModDeletion = TileID.Obsidian;
		HitSound = SoundID.Tink;
		MineResist = 40f;
		MinPick = 225;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.2f;
		g = 0f;
		b = 1f;
	}
}

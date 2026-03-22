using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles;

public class ShadowvoidBrick : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(102, 0, 255), name);
		DustType = DustID.Shadowflame;
		VanillaFallbackOnModDeletion = TileID.Obsidian;
		HitSound = SoundID.Tink;
		MineResist = 20f;
		MinPick = 225;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.2f;
		g = 0f;
		b = 1f;
	}
}

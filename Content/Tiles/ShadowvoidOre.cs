using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Tiles
{
	public class ShadowvoidOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 1010; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
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
}

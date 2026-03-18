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

namespace TheSludgeMod.Content.Items.Nickel
{
	public class NickelOreTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 235;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 975;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(130, 156, 114), name);
			DustType = DustID.Tungsten;
			VanillaFallbackOnModDeletion = TileID.Lead;
			HitSound = SoundID.Tink;
		}
	}
}

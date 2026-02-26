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
using Terraria.ObjectData;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items.Uranium
{
	public class UraniumBarTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileShine[Type] = 1100;
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			VanillaFallbackOnModDeletion = TileID.MetalBars;

			AddMapEntry(new Color(144, 212, 42));
		}
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak) {

			if (!WorldGen.SolidTileAllowBottomSlope(i, j + 1)) {
				WorldGen.KillTile(i, j);
			}

			return true;
		}
	}
}

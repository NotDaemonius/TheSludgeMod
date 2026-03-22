using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumOreTile : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.Ore[Type] = true;
		TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 615;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 975;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(144, 212, 42), name);
		DustType = DustID.Chlorophyte;
		VanillaFallbackOnModDeletion = TileID.Palladium;
		HitSound = SoundID.Tink;
		MinPick = 100;
		MineResist = 2;
	}
}

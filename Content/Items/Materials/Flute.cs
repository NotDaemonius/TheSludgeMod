using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Materials
{
	public class Flute : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Orange;
            Item.maxStack = 9999;
            Item.material = true;
            Item.value = 10000;
        }
    }
}

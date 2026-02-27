using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories;
using TheSludgeMod.Content.Items.Tools;

namespace TheSludgeMod.Common.Systems
{
    public class ChestItemWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {
            void appendChestItem(int itemID, Chest chest)
            {
                for (int slot = 0; slot < chest.item.Length; slot++)
                {
                    if (chest.item[slot].type == ItemID.None)
                    {
                        chest.item[slot].SetDefaults(itemID);
                        break;
                    }
                }
            }

            void replaceChestItem(int slot, int itemID, Chest chest)
            {
                chest.item[slot].SetDefaults(itemID);
            }

            for (int i = 0; i < Main.maxChests; i++)
            {
                Chest chest = Main.chest[i];
                if (chest == null)
                    continue;

                Tile tile = Main.tile[chest.x, chest.y];

                // Granite
                if (tile.TileType == TileID.Containers2 && tile.TileFrameX / 36 == 11)
                {
                    replaceChestItem(0, ModContent.ItemType<OrbitalRadiance>(), chest);
                } else if (tile.TileType == TileID.Containers && tile.TileFrameX == 4 * 36) // Shadow
                {
                    if (Random.Shared.Next(3) == 0)
                    {
                        appendChestItem(ModContent.ItemType<MagnetPickaxe>(), chest);
                    }
                }
            }
        }
    }
}
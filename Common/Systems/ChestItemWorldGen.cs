using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

namespace TheSludgeMod.Common.Systems
{
    public class ChestItemWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {
            int[] itemsToPlaceInFrozenChests = [ModContent.ItemType<MagnetPickaxe>()];
            int itemsToPlaceInFrozenChestsChoice = 0;
            int itemsPlaced = 0;
            int maxItems = 6;

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];

                if (chest == null)
                {
                    continue;
                }

                Tile chestTile = Main.tile[chest.x, chest.y];

                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 4 * 36)
                {
                    if (chest.item[0].type == ItemID.None)
                        continue;

                    if (WorldGen.genRand.NextBool(3))
                        continue;

                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInFrozenChests[itemsToPlaceInFrozenChestsChoice]);
                            itemsToPlaceInFrozenChestsChoice = (itemsToPlaceInFrozenChestsChoice + 1) % itemsToPlaceInFrozenChests.Length;
                            itemsPlaced++;
                            break;
                        }
                    }
                }

                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }
}
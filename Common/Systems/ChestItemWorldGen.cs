using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories;
using TheSludgeMod.Content.Items.Accessories.Balloons;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Items.Nickel;
using TheSludgeMod.Content.Items.Tools;
using TheSludgeMod.Content.Items.Zinc;

namespace TheSludgeMod.Common.Systems;

public class ChestItemWorldGen : ModSystem
{
    public override void PostWorldGen()
    {
        List<(int, int, int)> barValues = new List<(int, int, int)>() {
            (ItemID.CopperBar, ItemID.TinBar, ModContent.ItemType<ZincBar>()),
            (ItemID.IronBar, ItemID.LeadBar, ModContent.ItemType<NickelBar>()),
            (ItemID.SilverBar, ItemID.TungstenBar, ModContent.ItemType<AluminiumBar>()),
            (ItemID.GoldBar, ItemID.PlatinumBar, ModContent.ItemType<IridiumBar>()),
        };

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
            chest.item[slot].stack = 1;
        }

        for (int i = 0; i < Main.maxChests; i++)
        {
            Chest chest = Main.chest[i];
            if (chest == null) continue;
            Tile tile = Main.tile[chest.x, chest.y];

            for (int slot = 0; slot < chest.item.Length; slot++)
            {
                for (int barThing = 0; barThing < barValues.Count; barThing++)
                {
                    if (chest.item[slot].type == barValues[barThing].Item1 || chest.item[slot].type == barValues[barThing].Item2)
                    {
                        if (Main.rand.NextBool(3))
                        {
                            int stack = chest.item[slot].stack;
                            chest.item[slot].SetDefaults(barValues[barThing].Item3);
                            chest.item[slot].stack = stack;
                        }
                    }
                }
            }

            if (tile.TileType == TileID.Containers2 && tile.TileFrameX / 36 == 11) replaceChestItem(0, ModContent.ItemType<OrbitalRadiance>(), chest);

            else if (tile.TileType == TileID.Containers && tile.TileFrameX == 4 * 36)
            {
                if (Random.Shared.Next(3) == 0) appendChestItem(ModContent.ItemType<MagnetPickaxe>(), chest);
            }
            else if (tile.TileType == TileID.Containers && tile.TileFrameX / 36 == 13)
            {
                for (int slot = 0; slot < chest.item.Length; slot++)
                {
                    if (chest.item[slot].type == ItemID.ShinyRedBalloon)
                    {
                        int[] balloons = new int[]
                        {
                            ItemID.ShinyRedBalloon,
                            ModContent.ItemType<ShinyBlackBalloon>(),
                            ModContent.ItemType<ShinyBlueBalloon>(),
                            ModContent.ItemType<ShinyBrownBalloon>(),
                            ModContent.ItemType<ShinyCyanBalloon>(),
                            ModContent.ItemType<ShinyGreenBalloon>(),
                            ModContent.ItemType<ShinyLimeBalloon>(),
                            ModContent.ItemType<ShinyOrangeBalloon>(),
                            ModContent.ItemType<ShinyPinkBalloon>(),
                            ModContent.ItemType<ShinyPurpleBalloon>(),
                            ModContent.ItemType<ShinySilverBalloon>(),
                            ModContent.ItemType<ShinySkyBlueBalloon>(),
                            ModContent.ItemType<ShinyTealBalloon>(),
                            ModContent.ItemType<ShinyVioletBalloon>(),
                            ModContent.ItemType<ShinyYellowBalloon>()
                        };

                        replaceChestItem(slot, balloons[Main.rand.Next(balloons.Length)], chest);
                        break;
                    }
                }
            }
        }
    }
}
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Bismuth;
using TheSludgeMod.Content.Items.Osmium;
using TheSludgeMod.Content.Items.Uranium;

namespace TheSludgeMod.Common.Systems
{
    public class ModifyAlterOreSpawns : ModSystem
    {
        public override void Load() => On_WorldGen.SmashAltar += OnSmashAltar;
        public override void Unload() => On_WorldGen.SmashAltar -= OnSmashAltar;

        private void OnSmashAltar(On_WorldGen.orig_SmashAltar orig, int x, int y)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (!Main.hardMode) return;
            if (WorldGen.generatingWorld) return;

            int uranium = ModContent.TileType<UraniumOreTile>();
            int osmium = ModContent.TileType<OsmiumOreTile>();
            int bismuth = ModContent.TileType<BismuthOreTile>();

            int tierIndex = WorldGen.altarCount % 3;
            int altarNum = WorldGen.altarCount / 3 + 1;
            int num4 = 1;

            double count = (double)Main.maxTilesX / 4200.0;
            count = count * 310.0 - 85.0 * tierIndex;
            count *= 0.85;
            count /= altarNum;

            if (Main.drunkWorld)
            {
                if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite) WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium) WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
            }

            bool flag = false;
            int vanilla, alt, third;
            string vanillaName, altName, thirdName;

            if (tierIndex == 0)
            {
                if (Main.drunkWorld)
                {
                    if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt) WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                    else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium) WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                }
                if (WorldGen.SavedOreTiers.Cobalt == -1)
                {
                    flag = true;
                    WorldGen.SavedOreTiers.Cobalt = WorldGen.genRand.Next(2) == 0 ? TileID.Palladium : TileID.Cobalt;
                }

                vanilla = WorldGen.SavedOreTiers.Cobalt;
                alt = vanilla == TileID.Cobalt ? TileID.Palladium : TileID.Cobalt;
                third = uranium;
                vanillaName = vanilla == TileID.Cobalt ? "Cobalt" : "Palladium";
                altName = alt == TileID.Cobalt ? "Cobalt" : "Palladium";
                thirdName = "Uranium";

                if (vanilla == TileID.Palladium) count *= 0.9;
                count *= 1.05;
            }
            else if (tierIndex == 1)
            {
                if (Main.drunkWorld)
                {
                    if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril) WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                    else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum) WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                }
                if (WorldGen.SavedOreTiers.Mythril == -1)
                {
                    flag = true;
                    WorldGen.SavedOreTiers.Mythril = WorldGen.genRand.Next(2) == 0 ? TileID.Orichalcum : TileID.Mythril;
                }

                vanilla = WorldGen.SavedOreTiers.Mythril;
                alt = vanilla == TileID.Mythril ? TileID.Orichalcum : TileID.Mythril;
                third = osmium;
                vanillaName = vanilla == TileID.Mythril ? "Mythril" : "Orichalcum";
                altName = alt == TileID.Mythril ? "Mythril" : "Orichalcum";
                thirdName = "Osmium";

                if (vanilla == TileID.Orichalcum) count *= 0.9;
            }
            else
            {
                if (Main.drunkWorld)
                {
                    if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt) WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                    else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium) WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                }
                if (WorldGen.SavedOreTiers.Adamantite == -1)
                {
                    flag = true;
                    WorldGen.SavedOreTiers.Adamantite = WorldGen.genRand.Next(2) == 0 ? TileID.Titanium : TileID.Adamantite;
                }

                vanilla = WorldGen.SavedOreTiers.Adamantite;
                alt = vanilla == TileID.Adamantite ? TileID.Titanium : TileID.Adamantite;
                third = bismuth;
                vanillaName = vanilla == TileID.Adamantite ? "Adamantite" : "Titanium";
                altName = alt == TileID.Adamantite ? "Adamantite" : "Titanium";
                thirdName = "Bismuth";

                if (vanilla == TileID.Titanium) count *= 0.9;
            }

            if (flag)
                NetMessage.SendData(MessageID.WorldData);

            Color color = new Color(50, 255, 130);
            string message = $"Your world has been blessed with {vanillaName}, {altName}, and {thirdName}!";

            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(message, color.R, color.G, color.B);
            else if (Main.netMode == NetmodeID.Server)
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);

            int strengthMax = Main.tenthAnniversaryWorld ? 11 + num4 : 9 + num4;

            void RunOre(int type, double oreCount)
            {
                int n = 0;
                while ((double)n < oreCount)
                {
                    int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);

                    double minDepth = Main.worldSurface;
                    if (type == TileID.Mythril || type == TileID.Orichalcum || type == osmium)
                        minDepth = Main.rockLayer;
                    if (type == TileID.Adamantite || type == TileID.Titanium || type == bismuth)
                        minDepth = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0;

                    int j2 = WorldGen.genRand.Next((int)minDepth, Main.maxTilesY - 150);

                    if (Main.remixWorld)
                    {
                        double remixMin = Main.maxTilesX - 350;
                        if (type == TileID.Mythril || type == TileID.Orichalcum || type == osmium)
                            remixMin = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY - 350.0) / 3.0;
                        if (type == TileID.Adamantite || type == TileID.Titanium || type == bismuth)
                            remixMin = Main.rockLayer - 25.0;
                        j2 = WorldGen.genRand.Next((int)Main.worldSurface + 15, (int)remixMin);
                    }

                    WorldGen.OreRunner(i2, j2,
                        WorldGen.genRand.Next(5, strengthMax),
                        WorldGen.genRand.Next(5, strengthMax),
                        (ushort)type);
                    n++;
                }
            }

            RunOre(vanilla, count);
            RunOre(alt, count);
            RunOre(third, count);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int wraiths = Main.rand.Next(2) + 1;
                for (int k = 0; k < wraiths; k++)
                    NPC.SpawnOnPlayer((int)Player.FindClosest(new Vector2(x * 16, y * 16), 16, 16), NPCID.Wraith);
            }

            WorldGen.altarCount++;
            AchievementsHelper.NotifyProgressionEvent(6);
        }
    }
}

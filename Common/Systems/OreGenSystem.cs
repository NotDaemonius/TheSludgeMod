using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Items.Nickel;
using TheSludgeMod.Content.Items.Zinc;

namespace TheSludgeMod.Common.Systems
{
    public class OreGenSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int idx = tasks.FindIndex(pass => pass.Name == "Shinies");
            if (idx != -1)
                tasks.Insert(idx + 1, new PassLegacy("Generate Alternate Ores", GenerateAlternateOres));
        }

        private void GenerateAlternateOres(GenerationProgress progress, GameConfiguration config)
        {
            if (WorldGen.drunkWorldGen)
                return;

            progress.Message = "Generating alternate ores...";

            int zinc = ModContent.TileType<ZincOreTile>();
            int nickel = ModContent.TileType<NickelOreTile>();
            int aluminium = ModContent.TileType<AluminiumOreTile>();
            int iridium = ModContent.TileType<IridiumOreTile>();

            int tiles = Main.maxTilesX * Main.maxTilesY;
            int surfLow = (int)GenVars.worldSurfaceLow;
            int surfHigh = (int)GenVars.worldSurfaceHigh;
            int rockLow = (int)GenVars.rockLayerLow;
            int rockHigh = (int)GenVars.rockLayerHigh;
            int W = Main.maxTilesX;
            int H = Main.maxTilesY;

            int altCopper = GenVars.copper == TileID.Copper ? TileID.Tin : TileID.Copper;
            int altIron = GenVars.iron == TileID.Iron ? TileID.Lead : TileID.Iron;
            int altSilver = GenVars.silver == TileID.Silver ? TileID.Tungsten : TileID.Silver;
            int altGold = GenVars.gold == TileID.Gold ? TileID.Platinum : TileID.Gold;

            void Run(int type, int count, int x1, int x2, int y1, int y2, int sMin, int sMax, int stMin, int stMax)
            {
                for (int i = 0; i < count; i++)
                    WorldGen.TileRunner(
                        WorldGen.genRand.Next(x1, x2),
                        WorldGen.genRand.Next(y1, y2),
                        WorldGen.genRand.Next(sMin, sMax),
                        WorldGen.genRand.Next(stMin, stMax),
                        type, false, 0f, 0f, false, true);
            }

            Run(altCopper, (int)(tiles * 6E-05), 0, W, surfLow, surfHigh, 3, 6, 2, 6);
            Run(altCopper, (int)(tiles * 8E-05), 0, W, surfHigh, rockHigh, 3, 7, 3, 7);
            Run(altCopper, (int)(tiles * 0.0002), 0, W, rockLow, H, 4, 9, 4, 8);
            Run(zinc, (int)(tiles * 6E-05), 0, W, surfLow, surfHigh, 3, 6, 2, 6);
            Run(zinc, (int)(tiles * 8E-05), 0, W, surfHigh, rockHigh, 3, 7, 3, 7);
            Run(zinc, (int)(tiles * 0.0002), 0, W, rockLow, H, 4, 9, 4, 8);
            progress.Set(0.25);

            Run(altIron, (int)(tiles * 3E-05), 0, W, surfLow, surfHigh, 3, 7, 2, 5);
            Run(altIron, (int)(tiles * 8E-05), 0, W, surfHigh, rockHigh, 3, 6, 3, 6);
            Run(altIron, (int)(tiles * 0.0002), 0, W, rockLow, H, 4, 9, 4, 8);
            Run(nickel, (int)(tiles * 3E-05), 0, W, surfLow, surfHigh, 3, 7, 2, 5);
            Run(nickel, (int)(tiles * 8E-05), 0, W, surfHigh, rockHigh, 3, 6, 3, 6);
            Run(nickel, (int)(tiles * 0.0002), 0, W, rockLow, H, 4, 9, 4, 8);
            progress.Set(0.5);

            Run(altSilver, (int)(tiles * 2.6E-05), 0, W, surfHigh, rockHigh, 3, 6, 3, 6);
            Run(altSilver, (int)(tiles * 0.00015), 0, W, rockLow, H, 4, 9, 4, 8);
            Run(altSilver, (int)(tiles * 0.00017), 0, W, 0, surfLow, 4, 9, 4, 8);
            Run(aluminium, (int)(tiles * 2.6E-05), 0, W, surfHigh, rockHigh, 3, 6, 3, 6);
            Run(aluminium, (int)(tiles * 0.00015), 0, W, rockLow, H, 4, 9, 4, 8);
            Run(aluminium, (int)(tiles * 0.00017), 0, W, 0, surfLow, 4, 9, 4, 8);
            progress.Set(0.75);

            Run(altGold, (int)(tiles * 0.00012), 0, W, rockLow, H, 4, 8, 4, 8);
            Run(altGold, (int)(tiles * 0.00012), 0, W, 0, surfLow - 20, 4, 8, 4, 8);
            Run(iridium, (int)(tiles * 0.00012), 0, W, rockLow, H, 4, 8, 4, 8);
            Run(iridium, (int)(tiles * 0.00012), 0, W, 0, surfLow - 20, 4, 8, 4, 8);
            progress.Set(1.0);
        }
    }
}

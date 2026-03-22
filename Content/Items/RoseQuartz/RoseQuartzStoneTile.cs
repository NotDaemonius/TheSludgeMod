using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TheSludgeMod.Content.Items.RoseQuartz;

public class RoseQuartzStoneTile : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 975;
        Main.tileMergeDirt[Type] = true;
        Main.tileSolid[Type] = true;
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(205, 113, 151), name);
        DustType = DustID.Stone;
        VanillaFallbackOnModDeletion = TileID.Emerald;
        HitSound = SoundID.Tink;
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j) => new List<Item>() { new Item(ModContent.ItemType<RoseQuartz>()) };
}

public class ExampleOreSystem : ModSystem
{
    public static LocalizedText ExampleOrePassMessage { get; private set; }

    public static LocalizedText BlessedWithExampleOreMessage { get; private set; }

    public override void SetStaticDefaults()
    {
        ExampleOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(ExampleOrePassMessage)}");
        BlessedWithExampleOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithExampleOreMessage)}");
    }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
        if (ShiniesIndex != -1) tasks.Insert(ShiniesIndex + 1, new ExampleOrePass("Example Mod Ores", 237.4298f));
    }
}

public class ExampleOrePass : GenPass
{
    public ExampleOrePass(string name, float loadWeight) : base(name, loadWeight){}

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = ExampleOreSystem.ExampleOrePassMessage.Value;

        for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
        {
            int x = WorldGen.genRand.Next(0, Main.maxTilesX);
            int y = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);
            WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<RoseQuartzStoneTile>());
        }
    }
}

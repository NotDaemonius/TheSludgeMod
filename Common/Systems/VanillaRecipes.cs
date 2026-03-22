using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Bismuth;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.Items.Zinc;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Common.Systems;

public class VanillaRecipes : ModSystem
{
    public override void AddRecipes()
    {
        Recipe paa = Recipe.Create(ItemID.PaperAirplaneA);
        paa.AddIngredient<Paper>();
        paa.Register();

        Recipe pab = Recipe.Create(ItemID.PaperAirplaneB);
        pab.AddIngredient<Paper>(1;
        pab.Register();

        Recipe necromantic = Recipe.Create(ItemID.NecromanticScroll);
        necromantic.AddIngredient<Paper>(1);
        necromantic.AddIngredient(ItemID.SpookyWood, 100);
        necromantic.AddTile(TileID.TinkerersWorkbench);
        necromantic.Register();

        Recipe trifold = Recipe.Create(ItemID.TrifoldMap);
        trifold.AddIngredient(ItemID.LightShard);
        trifold.AddIngredient<Paper>(3);
        trifold.AddTile(TileID.TinkerersWorkbench);
        trifold.Register();

        Recipe kwad = Recipe.Create(ItemID.JimsDrone);
        kwad.AddIngredient(ItemID.Ectoplasm);
        kwad.AddIngredient<Electronics>();
        kwad.AddIngredient<Battery>(3);
        kwad.AddIngredient(ItemID.TitaniumBar, 5);
        kwad.AddIngredient<Plastic>(20);
        kwad.AddTile<ElectronicsTable>();
        kwad.Register();

        Recipe FPV = Recipe.Create(ItemID.JimsDroneVisor);
        FPV.AddIngredient<Electronics>();
        FPV.AddIngredient<Battery>();
        FPV.AddIngredient(ItemID.Ectoplasm);
        FPV.AddIngredient(ItemID.Glass, 10);
        FPV.AddIngredient<Plastic>(10);
        FPV.AddTile<ElectronicsTable>();
        FPV.Register();

        Recipe fastclock = Recipe.Create(ItemID.FastClock);
        fastclock.AddIngredient(ItemID.GoldWatch);
        fastclock.AddIngredient<Battery>(3);
        fastclock.AddIngredient(ItemID.PixieDust, 10);
        fastclock.AddTile<ElectronicsTable>();
        fastclock.Register();

        Recipe fastclock1 = Recipe.Create(ItemID.FastClock);
        fastclock1.AddIngredient(ItemID.PlatinumWatch);
        fastclock1.AddIngredient<Battery>(3);
        fastclock1.AddIngredient(ItemID.PixieDust, 10);
        fastclock1.AddTile<ElectronicsTable>();
        fastclock1.Register();

        Recipe fastclock2 = Recipe.Create(ItemID.FastClock);
        fastclock2.AddIngredient<IridiumWatch>();
        fastclock2.AddIngredient<Battery>(3);
        fastclock2.AddIngredient(ItemID.PixieDust, 10);
        fastclock2.AddTile<ElectronicsTable>();
        fastclock2.Register();

        Recipe zinc5 = Recipe.Create(ItemID.Timer5Second);
        zinc5.AddIngredient(ItemID.Wire);
        zinc5.AddIngredient<ZincWatch>();
        zinc5.AddTile(TileID.Anvils);
        zinc5.Register();

        Recipe aluminium3 = Recipe.Create(ItemID.Timer3Second);
        aluminium3.AddIngredient(ItemID.Wire);
        aluminium3.AddIngredient<AluminiumWatch>();
        aluminium3.AddTile(TileID.Anvils);
        aluminium3.Register();

        Recipe iridium1 = Recipe.Create(ItemID.Timer1Second);
        iridium1.AddIngredient(ItemID.Wire);
        iridium1.AddIngredient<IridiumWatch>();
        iridium1.AddTile(TileID.Anvils);
        iridium1.Register();

        Recipe iridiumgps = Recipe.Create(ItemID.GPS);
        iridiumgps.AddIngredient<IridiumWatch>();
        iridiumgps.AddIngredient(ItemID.DepthMeter);
        iridiumgps.AddIngredient(ItemID.Compass);
        iridiumgps.AddTile(TileID.TinkerersWorkbench);
        iridiumgps.Register();

        Recipe iridiumsc = Recipe.Create(ItemID.SlimeCrown);
        iridiumsc.AddIngredient(ItemID.Gel, 20);
        iridiumsc.AddIngredient<IridiumCrownHead>();
        iridiumsc.AddTile(TileID.DemonAltar);
        iridiumsc.Register();

        Recipe iridiummm = Recipe.Create(ItemID.MagicMirror);
        iridiummm.AddIngredient(ItemID.Glass, 10);
        iridiummm.AddIngredient<IridiumBar>(8);
        iridiummm.AddIngredient(ItemID.Diamond, 3);
        iridiummm.AddTile(TileID.Furnaces);
        iridiummm.Register();

        Recipe iridiumt = Recipe.Create(ItemID.Throne);
        iridiumt.AddIngredient<IridiumBar>(30);
        iridiumt.AddIngredient(ItemID.Silk, 20);
        iridiumt.AddTile(TileID.Anvils);
        iridiumt.Register();

        Recipe iridiumffc = Recipe.Create(ItemID.FlinxFurCoat);
        iridiumffc.AddIngredient(ItemID.Silk, 10);
        iridiumffc.AddIngredient(ItemID.FlinxFur, 10);
        iridiumffc.AddIngredient<IridiumBar>(8);
        iridiumffc.AddTile(TileID.Loom);
        iridiumffc.Register();

        Recipe iridiumfs = Recipe.Create(ItemID.FlinxStaff);
        iridiumfs.AddIngredient<IridiumBar>(10);
        iridiumfs.AddIngredient(ItemID.FlinxFur, 6);
        iridiumfs.AddTile(TileID.WorkBenches);
        iridiumfs.Register();

        Recipe iridiumpc = Recipe.Create(ItemID.PeaceCandle);
        iridiumpc.AddIngredient<IridiumBar>(2);
        iridiumpc.AddIngredient(ItemID.PinkTorch);
        iridiumpc.AddTile(TileID.WorkBenches);
        iridiumpc.Register();

        Recipe hf = Recipe.Create(ItemID.Hellforge);
        hf.AddIngredient(ItemID.Hellstone, 30);
        hf.AddIngredient(ItemID.Furnace);
        hf.AddTile(TileID.WorkBenches);
        hf.Register();

        Recipe bfm = Recipe.Create(ItemID.AncientBattleArmorHat);
        bfm.AddIngredient<BismuthBar>(10);
        bfm.AddIngredient(ItemID.AncientBattleArmorMaterial);
        bfm.AddTile(TileID.MythrilAnvil);
        bfm.Register();

        Recipe bfr = Recipe.Create(ItemID.AncientBattleArmorShirt);
        bfr.AddIngredient<BismuthBar>(20);
        bfr.AddIngredient(ItemID.AncientBattleArmorMaterial);
        bfr.AddTile(TileID.MythrilAnvil);
        bfr.Register();

        Recipe bft = Recipe.Create(ItemID.AncientBattleArmorPants);
        bft.AddIngredient<BismuthBar>(16);
        bft.AddIngredient(ItemID.AncientBattleArmorMaterial);
        bft.AddTile(TileID.MythrilAnvil);
        bft.Register();

        Recipe bfrb = Recipe.Create(ItemID.FrostHelmet);
        bfrb.AddIngredient<BismuthBar>(10);
        bfrb.AddIngredient(ItemID.FrostCore);
        bfrb.AddTile(TileID.MythrilAnvil);
        bfrb.Register();

        Recipe bfrh = Recipe.Create(ItemID.FrostBreastplate);
        bfrh.AddIngredient<BismuthBar>(20);
        bfrh.AddIngredient(ItemID.FrostCore);
        bfrh.AddTile(TileID.MythrilAnvil);
        bfrh.Register();

        Recipe bfrl = Recipe.Create(ItemID.FrostLeggings);
        bfrl.AddIngredient<BismuthBar>(16);
        bfrl.AddIngredient(ItemID.FrostCore);
        bfrl.AddTile(TileID.MythrilAnvil);
        bfrl.Register();

        Recipe zen = Recipe.Create(ItemID.Zenith);
        zen.AddIngredient<ToyZenith>();
        zen.AddTile(TileID.MythrilAnvil);
        zen.Register();

        Recipe dm = Recipe.Create(ItemID.DepthMeter);
        dm.AddIngredient(ItemID.StoneBlock, 100);
        dm.AddRecipeGroup("IronBar", 10);
        dm.AddIngredient<Electronics>(4);
        dm.AddTile<ElectronicsTable>();
        dm.Register();

        Recipe com = Recipe.Create(ItemID.Compass);
        com.AddRecipeGroup("IronBar", 12);
        com.AddIngredient<Electronics>(3);
        com.AddIngredient(ItemID.TatteredCloth);
        com.AddTile<ElectronicsTable>();
        com.Register();

        Recipe rad = Recipe.Create(ItemID.Radar);
        rad.AddIngredient(ItemID.GreenTorch, 10);
        rad.AddRecipeGroup("IronBar", 8);
        rad.AddIngredient<Electronics>(4);
        rad.AddIngredient<Battery>();
        rad.AddTile<ElectronicsTable>();
        rad.Register();

        Recipe la = Recipe.Create(ItemID.LifeformAnalyzer);
        la.AddIngredient(ItemID.Glass, 10);
        la.AddRecipeGroup("IronBar", 10);
        la.AddIngredient<Electronics>(3);
        la.AddIngredient<Battery>();
        la.AddTile<ElectronicsTable>();
        la.Register();

        Recipe tc = Recipe.Create(ItemID.TallyCounter);
        tc.AddIngredient(ItemID.Bone, 20);
        tc.AddRecipeGroup("IronBar", 12);
        tc.AddIngredient<Electronics>(2);
        tc.AddIngredient<Plastic>();
        tc.AddTile<ElectronicsTable>();
        tc.Register();

        Recipe md = Recipe.Create(ItemID.MetalDetector);
        md.AddRecipeGroup("IronBar", 10);
        md.AddIngredient<Electronics>(5);
        md.AddIngredient(ItemID.LifeCrystal);
        md.AddTile<ElectronicsTable>();
        md.Register();

        Recipe stop = Recipe.Create(ItemID.Stopwatch);
        stop.AddRecipeGroup("IronBar", 8);
        stop.AddIngredient<Electronics>(3);
        stop.AddIngredient(ItemID.Aglet);
        stop.AddTile<ElectronicsTable>();
        stop.Register();

        Recipe dps = Recipe.Create(ItemID.DPSMeter);
        dps.AddIngredient(ItemID.Silk, 10);
        dps.AddIngredient<Electronics>(3);
        dps.AddIngredient(ItemID.Ruby);
        dps.AddTile<ElectronicsTable>();
        dps.Register();

        Recipe fpg = Recipe.Create(ItemID.FishermansGuide);
        fpg.AddIngredient<Paper>(100);
        fpg.AddIngredient(ItemID.Silk);
        fpg.AddTile<ElectronicsTable>();
        fpg.Register();

        Recipe wr = Recipe.Create(ItemID.WeatherRadio);
        wr.AddRecipeGroup("IronBar", 6);
        wr.AddIngredient<Electronics>(6);
        wr.AddIngredient(ItemID.Lens, 3);
        wr.AddTile<ElectronicsTable>();
        wr.Register();

        Recipe sx = Recipe.Create(ItemID.Sextant);
        sx.AddIngredient(ItemID.GoldBar, 12);
        wr.AddIngredient(ItemID.Lens, 3);
        sx.AddTile<ElectronicsTable>();
        sx.Register();
    }
    public override void PostAddRecipes()
    {
        foreach (Recipe recipe in Main.recipe.Take(Recipe.numRecipes))
        {
            if (recipe.HasResult(ItemID.CloudinaBalloon) && recipe.HasIngredient(ItemID.ShinyRedBalloon))
            {
                recipe.RemoveIngredient(ItemID.ShinyRedBalloon);
                recipe.AddRecipeGroup("TheSludgeMod:AnyShinyBalloon");
            }
            if (recipe.HasResult(ItemID.BlizzardinaBalloon) && recipe.HasIngredient(ItemID.ShinyRedBalloon))
            {
                recipe.RemoveIngredient(ItemID.ShinyRedBalloon);
                recipe.AddRecipeGroup("TheSludgeMod:AnyShinyBalloon");
            }
            if (recipe.HasResult(ItemID.FartInABalloon) && recipe.HasIngredient(ItemID.ShinyRedBalloon))
            {
                recipe.RemoveIngredient(ItemID.ShinyRedBalloon);
                recipe.AddRecipeGroup("TheSludgeMod:AnyShinyBalloon");
            }
            if (recipe.HasResult(ItemID.HoneyBalloon) && recipe.HasIngredient(ItemID.ShinyRedBalloon))
            {
                recipe.RemoveIngredient(ItemID.ShinyRedBalloon);
                recipe.AddRecipeGroup("TheSludgeMod:AnyShinyBalloon");
            }
            if (recipe.HasResult(ItemID.SandstorminaBalloon) && recipe.HasIngredient(ItemID.ShinyRedBalloon))
            {
                recipe.RemoveIngredient(ItemID.ShinyRedBalloon);
                recipe.AddRecipeGroup("TheSludgeMod:AnyShinyBalloon");
            }
        }
    }
}
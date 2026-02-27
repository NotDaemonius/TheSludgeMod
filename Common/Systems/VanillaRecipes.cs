using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Items.Zinc;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Common.Systems
{
    public class VanillaRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe paa = Recipe.Create(ItemID.PaperAirplaneA);
            paa.AddIngredient<Paper>(1);
            paa.Register();

            Recipe pab = Recipe.Create(ItemID.PaperAirplaneB);
            pab.AddIngredient<Paper>(1);
            pab.Register();

            Recipe necromantic = Recipe.Create(ItemID.NecromanticScroll);
            necromantic.AddIngredient<Paper>(1);
            necromantic.AddIngredient(ItemID.SpookyWood, 100);
            necromantic.AddTile(TileID.TinkerersWorkbench);
            necromantic.Register();

            Recipe trifold = Recipe.Create(ItemID.TrifoldMap);
            trifold.AddIngredient(ItemID.LightShard, 1);
            trifold.AddIngredient<Paper>(3);
            trifold.AddTile(TileID.TinkerersWorkbench);
            trifold.Register();

            Recipe kwad = Recipe.Create(ItemID.JimsDrone);
            kwad.AddIngredient(ItemID.Ectoplasm, 1);
            kwad.AddIngredient<Electronics>(1);
            kwad.AddIngredient<Battery>(3);
            kwad.AddIngredient(ItemID.TitaniumBar, 5);
            kwad.AddIngredient<Plastic>(20);
            kwad.AddTile<ElectronicsTable>();
            kwad.Register();

            Recipe FPV = Recipe.Create(ItemID.JimsDroneVisor);
            FPV.AddIngredient<Electronics>(1);
            FPV.AddIngredient<Battery>(1);
            FPV.AddIngredient(ItemID.Ectoplasm, 1);
            FPV.AddIngredient(ItemID.Glass, 10);
            FPV.AddIngredient<Plastic>(10);
            FPV.AddTile<ElectronicsTable>();
            FPV.Register();

            Recipe fastclock = Recipe.Create(ItemID.FastClock);
            fastclock.AddIngredient(ItemID.GoldWatch, 1);
            fastclock.AddIngredient<Battery>(3);
            fastclock.AddIngredient(ItemID.PixieDust, 10);
            fastclock.AddTile<ElectronicsTable>();
            fastclock.Register();

            Recipe fastclock1 = Recipe.Create(ItemID.FastClock);
            fastclock1.AddIngredient(ItemID.PlatinumWatch, 1);
            fastclock1.AddIngredient<Battery>(3);
            fastclock1.AddIngredient(ItemID.PixieDust, 10);
            fastclock1.AddTile<ElectronicsTable>();
            fastclock1.Register();

            Recipe fastclock2 = Recipe.Create(ItemID.FastClock);
            fastclock2.AddIngredient<IridiumWatch>(1);
            fastclock2.AddIngredient<Battery>(3);
            fastclock2.AddIngredient(ItemID.PixieDust, 10);
            fastclock2.AddTile<ElectronicsTable>();
            fastclock2.Register();

            Recipe zinc5 = Recipe.Create(ItemID.Timer5Second);
            zinc5.AddIngredient(ItemID.Wire, 1);
            zinc5.AddIngredient<ZincWatch>(1);
            zinc5.AddTile(TileID.Anvils);
            zinc5.Register();

            Recipe aluminium3 = Recipe.Create(ItemID.Timer3Second);
            aluminium3.AddIngredient(ItemID.Wire, 1);
            aluminium3.AddIngredient<AluminiumWatch>(1);
            aluminium3.AddTile(TileID.Anvils);
            aluminium3.Register();

            Recipe iridium1 = Recipe.Create(ItemID.Timer1Second);
            iridium1.AddIngredient(ItemID.Wire, 1);
            iridium1.AddIngredient<IridiumWatch>(1);
            iridium1.AddTile(TileID.Anvils);
            iridium1.Register();

            Recipe iridiumgps = Recipe.Create(ItemID.GPS);
            iridiumgps.AddIngredient<IridiumWatch>(1);
            iridiumgps.AddIngredient(ItemID.DepthMeter, 1);
            iridiumgps.AddIngredient(ItemID.Compass, 1);
            iridiumgps.AddTile(TileID.TinkerersWorkbench);
            iridiumgps.Register();

            Recipe iridiumsc = Recipe.Create(ItemID.SlimeCrown);
            iridiumsc.AddIngredient(ItemID.Gel, 20);
            iridiumsc.AddIngredient<IridiumCrownHead>(1);
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
            iridiumpc.AddIngredient(ItemID.PinkTorch, 1);
            iridiumpc.AddTile(TileID.WorkBenches);
            iridiumpc.Register();

            Recipe hf = Recipe.Create(ItemID.Hellforge);
            hf.AddIngredient(ItemID.Hellstone, 30);
            hf.AddIngredient(ItemID.Furnace, 1);
            hf.AddTile(TileID.WorkBenches);
            hf.Register();
        }
    }
}
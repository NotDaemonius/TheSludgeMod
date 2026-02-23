using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

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
            kwad.AddTile(TileID.HeavyWorkBench);
            kwad.Register();

            Recipe FPV = Recipe.Create(ItemID.JimsDroneVisor);
            kwad.AddIngredient<Electronics>(1);
            FPV.AddIngredient<Battery>(1);
            FPV.AddIngredient(ItemID.Ectoplasm, 1);
            FPV.AddIngredient(ItemID.Glass, 10);
            FPV.AddIngredient<Plastic>(10);
            FPV.AddTile(TileID.HeavyWorkBench);
            FPV.Register();

            Recipe fastclock = Recipe.Create(ItemID.FastClock);
            fastclock.AddIngredient(ItemID.GoldWatch, 1);
            fastclock.AddIngredient<Battery>(3);
            fastclock.AddIngredient(ItemID.PixieDust, 10);
            fastclock.AddTile(TileID.TinkerersWorkbench);
            fastclock.Register();
        }
    }
}
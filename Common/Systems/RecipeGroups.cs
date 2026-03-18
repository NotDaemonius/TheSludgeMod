using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories.Balloons;

namespace TheSludgeMod.Common.Systems
{
    public class RecipeGroups : ModSystem
    {
        public const string AnyTombstone = "TheSludgeMod:AnyTombstone";
        public const string AnyShinyBalloon = "TheSludgeMod:AnyShinyBalloon";
        public override void AddRecipeGroups()
        {
            RecipeGroup tombstoneGroup = new RecipeGroup(
                () => $"{Lang.misc[37].Value} Tombstone",
                ItemID.Tombstone,
                ItemID.GraveMarker,
                ItemID.CrossGraveMarker,
                ItemID.Headstone,
                ItemID.Gravestone,
                ItemID.Obelisk,
                ItemID.RichGravestone1,
                ItemID.RichGravestone2,
                ItemID.RichGravestone3,
                ItemID.RichGravestone4,
                ItemID.RichGravestone5
            );

            RecipeGroup.RegisterGroup(AnyTombstone, tombstoneGroup);

            RecipeGroup balloonGroup = new RecipeGroup(
                () => $"{Lang.misc[37].Value} Shiny Balloon",
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
            );

            RecipeGroup.RegisterGroup(AnyShinyBalloon, balloonGroup);
        }
    }
}
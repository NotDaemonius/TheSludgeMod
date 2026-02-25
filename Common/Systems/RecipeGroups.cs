using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Systems
{
    public class RecipeGroups : ModSystem
    {
        public const string AnyTombstone = "YourModName:AnyTombstone";
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
        }
    }
}
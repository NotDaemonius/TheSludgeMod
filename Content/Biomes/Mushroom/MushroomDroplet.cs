using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Biomes.Mushroom;

public class MushroomDroplet : ModGore
{
    public override void SetStaticDefaults()
    {
        ChildSafety.SafeGore[Type] = true;
        GoreID.Sets.LiquidDroplet[Type] = true;

        UpdateType = GoreID.WaterDrip;
    }
}

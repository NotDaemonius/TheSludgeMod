using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Rarities;

public class Black : ModRarity
{
    public override Color RarityColor => new Color(0, 0, 0);

    public override int GetPrefixedRarity(int offset, float valueMult) => offset switch
    {
        -2 => ModContent.RarityType<NeonOrange>(),
        -1 => ModContent.RarityType<Indigo>(),
        1 => ModContent.RarityType<Black>(),
        2 => ModContent.RarityType<Black>(),
        _ => Type,
    };
}
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Rarities;

public class NeonOrange : ModRarity
{
    public override Color RarityColor => new Color(255, 80, 0);

    public override int GetPrefixedRarity(int offset, float valueMult) => offset switch
    {
        -2 => ItemRarityID.Red,
        -1 => ItemRarityID.Purple,
        1 => ModContent.RarityType<Indigo>(),
        2 => ModContent.RarityType<Black>(),
        _ => Type,
    };
}
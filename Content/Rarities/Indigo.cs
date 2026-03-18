using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Rarities
{
    public class Indigo : ModRarity
    {
        public override Color RarityColor => new Color(50, 0, 255);

        public override int GetPrefixedRarity(int offset, float valueMult) => offset switch
        {
            -2 => ItemRarityID.Purple,
            -1 => ModContent.RarityType<NeonOrange>(),
            1 => ModContent.RarityType<Black>(),
            2 => ModContent.RarityType<Black>(),
            _ => Type,
        };
    }
}
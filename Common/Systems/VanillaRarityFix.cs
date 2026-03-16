using MonoMod.RuntimeDetour;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Rarities;

public class VanillaRarityFix : ModSystem
{
    private Hook _prefixHook;
    private delegate bool orig_Prefix(Item self, int prefixWeWant);

    public override void Load()
    {
        var method = typeof(Item).GetMethod(
            "Prefix",
            BindingFlags.Public | BindingFlags.Instance,
            null,
            new[] { typeof(int) },
            null
        );
        _prefixHook = new Hook(method, OnPrefix);
    }

    public override void Unload()
    {
        _prefixHook?.Dispose();
        _prefixHook = null;
    }

    private static bool OnPrefix(orig_Prefix orig, Item self, int prefixWeWant)
    {
        int baseRarity = self.rare;
        int baseValue = self.value;

        bool result = orig(self, prefixWeWant);

        if (!result || baseValue <= 0)
            return result;

        if (baseRarity != ItemRarityID.Red && baseRarity != ItemRarityID.Purple)
            return result;

        // Infer the prefix tier from the value multiplier.
        // Prefix() applies: value = (int)(value * num * num)
        // So numSq = finalValue / baseValue approximates num²
        float numSq = (float)self.value / baseValue;
        int offset = numSq switch
        {
            >= 1.44f => 2,   // num >= 1.2
            >= 1.1025f => 1,   // num >= 1.05
            <= 0.7225f => -2,   // num <= 0.85
            <= 0.9025f => -1,   // num <= 0.95
            _ => 0
        };

        if (baseRarity == ItemRarityID.Red)
        {
            self.rare = offset switch
            {
                -2 => ItemRarityID.Yellow,
                -1 => ItemRarityID.Cyan,
                1 => ItemRarityID.Purple,
                2 => ModContent.RarityType<NeonOrange>(),
                _ => ItemRarityID.Red
            };
        }
        else // Purple
        {
            self.rare = offset switch
            {
                -2 => ItemRarityID.Cyan,
                -1 => ItemRarityID.Red,
                1 => ModContent.RarityType<NeonOrange>(),
                2 => ModContent.RarityType<Indigo>(),
                _ => ItemRarityID.Purple
            };
        }

        return result;
    }
}
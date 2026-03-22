using System;
using System.Reflection;
using MonoMod.RuntimeDetour;
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
        try
        {
            var method = typeof(Item).GetMethod("Prefix", BindingFlags.Public | BindingFlags.Instance, null, [typeof(int)], null);

            if (method == null)
            {
                Mod.Logger.Error("Failed to find Item.Prefix method for hooking");
                return;
            }

            _prefixHook = new Hook(method, OnPrefix);
        }
        catch (Exception ex)
        {
            Mod.Logger.Error($"Failed to create Item.Prefix hook: {ex.Message}");
        }
    }

    public override void Unload()
    {
        try
        {
            if (_prefixHook != null)
            {
                _prefixHook.Dispose();
                _prefixHook = null;
            }
        }
        catch (Exception ex)
        {
            Mod.Logger.Error($"Failed to dispose Item.Prefix hook: {ex.Message}");
        }
    }

    private static bool OnPrefix(orig_Prefix orig, Item self, int prefixWeWant)
    {
        int baseRarity = self.rare;
        int baseValue = self.value;
        bool result = orig(self, prefixWeWant);
        if (!result || baseValue <= 0) return result;
        if (baseRarity != ItemRarityID.Red && baseRarity != ItemRarityID.Purple) return result;

        float numSq = (float)self.value / baseValue;
        int offset = numSq switch
        {
            >= 1.44f => 2,
            >= 1.1025f => 1,
            <= 0.7225f => -2,
            <= 0.9025f => -1,
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
        else
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
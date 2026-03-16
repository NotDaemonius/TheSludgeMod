using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Common.Players;
public class BrainOfPerplexityPlayer : ModPlayer
{
    public bool hasBrainOfPerplexity;

    private const float DodgeChance = 1f / 3f;
    private const int ConfusionDuration = 600;
    private const float ConfusionRadius = 500f;
    private const int BuffDuration = 240; // 4 seconds

    public override void ResetEffects()
    {
        hasBrainOfPerplexity = false;
    }

    public override bool FreeDodge(Player.HurtInfo info)
    {
        if (!hasBrainOfPerplexity || Player.HasBuff(ModContent.BuffType<PerplexityBuff>()))
            return false;

        if (Main.rand.NextFloat() >= DodgeChance)
            return false;

        Player.AddBuff(ModContent.BuffType<PerplexityBuff>(), BuffDuration);

        Player.BrainOfConfusionDodge();

        CombatText.NewText(Player.Hitbox, new Color(225, 35, 55), "DODGE!", true, false);

        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.active || npc.friendly || npc.dontTakeDamage)
                continue;
            if (Vector2.Distance(Player.Center, npc.Center) <= ConfusionRadius)
                npc.AddBuff(BuffID.Confused, ConfusionDuration);
        }

        return true;
    }
}

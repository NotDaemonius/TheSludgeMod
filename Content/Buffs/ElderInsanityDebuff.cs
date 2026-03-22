using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class ElderInsanityDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

    public override void Update(Player player, ref int buffIndex) => player.GetModPlayer<ElderInsanityPlayer>().elderInsanity = true;

    public override void Update(NPC npc, ref int buffIndex) => npc.GetGlobalNPC<ElderInsanityNPC>().elderInsanity = true;
}

public class ElderInsanityPlayer : ModPlayer
{
    public bool elderInsanity;
    private float damageAccumulator;

    public override void ResetEffects() => elderInsanity = false;

    public override void PostUpdate()
    {
        if (!elderInsanity) return;
        damageAccumulator += 100f / 60f;

        if (damageAccumulator >= 1f)
        {
            int dmg = (int)damageAccumulator;
            damageAccumulator -= dmg;
            Player.statLife -= dmg;
            Player.lifeRegenTime = 0;
            if (Player.statLife <= 0) Player.KillMe(new PlayerDeathReason { CustomReason = NetworkText.FromLiteral(Player.name + " succumbed to elder insanity.") }, dmg, 0);

            for (int i = 0; i < 2; i++)
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Terra, Scale: Main.rand.NextFloat(1.2f, 1.8f));
                d.noGravity = true; 
                d.color = new Color(0, 255, 80);
            }
        }
    }
}

public class ElderInsanityNPC : GlobalNPC
{
    public bool elderInsanity;

    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc) => elderInsanity = false;

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (!elderInsanity) return;
        if (npc.lifeRegen > 0) npc.lifeRegen = 0;
        npc.lifeRegen -= 200;
        damage = System.Math.Max(damage, 100);
    }
}
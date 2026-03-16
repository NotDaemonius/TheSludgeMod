using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;
public class PerplexityBuff : ModBuff
{
public override void SetStaticDefaults()
{
    Main.debuff[Type] = false;
    Main.buffNoSave[Type] = true;
    Main.buffNoTimeDisplay[Type] = false;
}

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance(DamageClass.Generic) += 15;
        player.GetDamage(DamageClass.Summon) += 0.15f;
    }
}
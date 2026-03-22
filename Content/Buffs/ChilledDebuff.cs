using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class ChilledDebuff : ModBuff
{
    public override void SetStaticDefaults() => Main.debuff[Type] = true;

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.velocity.X *= 0.75f;

        if (Main.rand.NextBool(3))
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.IceTorch);
            dust.noGravity = true;
            dust.velocity *= 0.3f;
            dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
        }
    }
}
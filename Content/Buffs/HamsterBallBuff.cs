using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class HamsterBallBuff : ModBuff
{
    public override void SetStaticDefaults() => Main.buffNoTimeDisplay[Type] = true;
}
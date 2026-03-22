using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class ScoutingBuff : ModBuff
{
    public override void SetStaticDefaults() => Main.buffNoTimeDisplay[Type] = false;
}
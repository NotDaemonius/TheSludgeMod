using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class DeodorantBuff : ModBuff
{
    public override void SetStaticDefaults() => Main.buffNoSave[Type] = true;
}
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Content.Buffs;

public class SteroidsBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) => player.GetModPlayer<SteroidsPlayer>().hasSteroidsBuff = true;
}
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Content.Buffs
{
    public class SteroidsDebuff : ModBuff
    {
        public override void SetStaticDefaults() => Main.debuff[Type] = true;

        public override void Update(Player player, ref int buffIndex) => player.GetModPlayer<SteroidsPlayer>().hasSteroidsDebuff = true;
    }
}
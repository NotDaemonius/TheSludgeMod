using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs
{
    public class SlimeHoardBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            bool anyAlive = false;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<Projectiles.SlimeHoardProjectile>())
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            player.DelBuff(buffIndex);
        }
    }
}
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs;

public class NeuralShardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.buffTime[buffIndex] = 18000;
        int projType = ModContent.ProjectileType<Content.Projectiles.Weapons.NeuralShardProj>();
        bool minionExists = false;

        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile p = Main.projectile[i];

            if (p.active && p.owner == player.whoAmI && p.type == projType)
            {
                minionExists = true;
                break;
            }
        }

        if (!minionExists) player.DelBuff(buffIndex);
        else player.buffTime[buffIndex] = 18000;
    }
}
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Buffs;

public class ShadewoodCrystalBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<ShadewoodCrystalProj>()] > 0) player.buffTime[buffIndex] = 18;
    }
}
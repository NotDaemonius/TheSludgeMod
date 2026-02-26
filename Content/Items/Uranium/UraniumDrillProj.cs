using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium
{
    public class UraniumDrillProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.aiStyle = ProjAIStyleID.Drill;
            Projectile.hide = true;
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Pets
{
    public class NeuralCrystalProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            Main.projPet[Type] = true;
            ProjectileID.Sets.CharacterPreviewAnimations[Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Type], 8).WithOffset(0f, 0f).WithCode(DelegateMethods.CharacterPreview.Float);
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            AIType = ProjectileID.ZephyrFish;
            Projectile.width = 46;
            Projectile.height = 26;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.zephyrfish = false;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<NeuralCrystalBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}
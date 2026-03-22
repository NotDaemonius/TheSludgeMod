using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.NPCs.TheMainframe;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class GemshardObliterator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.autoReuse = true;
            Item.shootSpeed = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 56;
            Item.knockBack = 2;
            Item.crit = 12;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item157;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TheMainframeLaser>();

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 5f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 aimDir = Vector2.Normalize(velocity);
            Vector2 perpDir = new Vector2(-aimDir.Y, aimDir.X) * player.direction;

            // GetFrontHandPosition returns the actual world-space hand position for the
            // current aim angle. MountedCenter is the body center — the distance from it
            // to the muzzle tip changes as the arm rotates, so no single BARREL_LENGTH
            // can satisfy both horizontal and vertical aiming simultaneously.
            // The hand position IS the gun's rotation pivot, so BARREL_LENGTH measured
            // from here is consistent at every angle.
            Vector2 handPos = player.GetFrontHandPosition(
                Player.CompositeArmStretchAmount.Full, velocity.ToRotation());

            const float BARREL_LENGTH = 32f; // retune this — now measured from hand, not body center
            const float BARREL_PERP = -5f;

            Vector2 muzzlePos = handPos
                + aimDir * BARREL_LENGTH
                + perpDir * BARREL_PERP;

            int projIndex = Projectile.NewProjectile(source, muzzlePos, velocity,
                ModContent.ProjectileType<TheMainframeLaser>(), damage, knockback, player.whoAmI);

            Main.projectile[projIndex].friendly = true;
            Main.projectile[projIndex].hostile = false;
            Main.projectile[projIndex].DamageType = DamageClass.Ranged;
            Main.projectile[projIndex].usesLocalNPCImmunity = true;
            Main.projectile[projIndex].localNPCHitCooldown = 10;

            return false;
        }
    }
}

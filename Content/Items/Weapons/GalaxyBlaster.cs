using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class GalaxyBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.autoReuse = true;
            Item.shootSpeed = 20;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 47;
            Item.knockBack = -1;
            Item.crit = 6;
            Item.mana = 2;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item157;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.GreenLaser;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = HelperFunctions.AdjustMuzzleOffset(player, ref position, velocity, -28f);
        }
    }
}   

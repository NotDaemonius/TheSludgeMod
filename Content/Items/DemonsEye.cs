using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
    public class DemonsEye : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40; // The item texture's width.
            Item.height = 40; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.shootSpeed = 15;
            Item.DamageType = DamageClass.Magic; // Whether your item is part of the melee class.
            Item.damage = 13; // The damage your item deals.
            Item.knockBack = -1; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

            Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.

            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<DemonsEyeEye>();
        }
    }
}
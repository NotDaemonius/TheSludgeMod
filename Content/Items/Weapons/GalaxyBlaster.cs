using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;

namespace TheSludgeMod.Content.Items.Weapons;

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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LaserRifle);
            recipe.AddIngredient(ItemID.SpaceGun);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
    }

    public override Vector2? HoldoutOffset() => new Vector2(-4f, -2f);

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) => position = HelperFunctions.AdjustMuzzleOffset(player, ref position, velocity, -28f);
}


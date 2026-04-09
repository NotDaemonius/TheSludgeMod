using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class ThePurifier : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 66;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 20;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 5f;
        Item.value = Item.buyPrice(gold: 7);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<ThePurifierProjectile>();
        Item.shootSpeed = 12f;
        Item.crit = 20;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LihzahrdBrick, 100);
        recipe.AddIngredient(ItemID.BeetleHusk, 2);
        recipe.AddIngredient(ItemID.GoldDust, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= 0.66f;

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) =>
        position = HelperFunctions.AdjustMuzzleOffset(player, ref position, velocity, 28f);
}
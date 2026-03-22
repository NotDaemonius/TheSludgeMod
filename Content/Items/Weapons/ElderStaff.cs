using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class ElderStaff : ModItem
{
    private const int MinProj = 7;
    private const int MaxProj = 11;
    private const float MinSpreadDeg = 5f;
    private const float MaxSpreadDeg = 10f;

    public override void SetStaticDefaults() => Item.staff[Type] = true;

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 65;
        Item.knockBack = 3.5f;
        Item.mana = 18;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.value = 100000;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item43;
        Item.shoot = ModContent.ProjectileType<ElderStaffProj>();
        Item.shootSpeed = 14f;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(); //TO BE CHANGED WHEN ELDER WASTES BIOME IS IMPLEMENTED
        recipe.AddIngredient(ItemID.VenomStaff, 1);
        recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
        recipe.AddIngredient(ItemID.ToxicFlask, 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int projCount = Main.rand.Next(MinProj, MaxProj + 1);
        float totalSpreadRad = MathHelper.ToRadians(Main.rand.NextFloat(MinSpreadDeg, MaxSpreadDeg));
        float baseAngle = velocity.ToRotation();
        float speed = velocity.Length();

        for (int i = 0; i < projCount; i++)
        {
            float t = projCount == 1 ? 0f : (i / (float)(projCount - 1)) - 0.5f;
            float angle = baseAngle + t * totalSpreadRad + Main.rand.NextFloat(-0.03f, 0.03f);
            Vector2 projVelocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            Projectile.NewProjectile(source, position, projVelocity, type, damage, knockback, player.whoAmI);
        }

        for (int d = 0; d < 10; d++)
        {
            int dust = Dust.NewDust(position, 4, 4, DustID.Terra, velocity.X * 0.5f + Main.rand.NextFloat(-2f, 2f), velocity.Y * 0.5f + Main.rand.NextFloat(-2f, 2f), 0, default, Main.rand.NextFloat(1.2f, 2f));
            Main.dust[dust].noGravity = true;
            Main.dust[dust].color = new Color(57, 255, 20);
        }

        return false;
    }
}
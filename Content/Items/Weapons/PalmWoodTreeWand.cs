using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class PalmWoodTreeWand : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.damage = 8;
        Item.DamageType = DamageClass.Magic;
        Item.knockBack = 2f;
        Item.mana = 2;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<PalmWoodTreeWandProj>();
        Item.shootSpeed = 8f;
        Item.value = Item.buyPrice(copper: 50);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float spread = MathHelper.ToRadians(1f);
        float angle = velocity.ToRotation() + Main.rand.NextFloat(-spread, spread);
        velocity = angle.ToRotationVector2() * velocity.Length();
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PalmWood, 20);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
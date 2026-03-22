using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class GunGnir : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 118;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 50;
        Item.height = 28;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 7f;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<GungnirBullet>();
        Item.shootSpeed = 32f;
        Item.crit = 12;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Gungnir);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items.Weapons;

public class SapphireBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 38;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 34;
        Item.useAnimation = 34;
        Item.autoReuse = true;
        Item.shootSpeed = 7.5f;
        Item.shoot = ProjectileID.SapphireBolt;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 18;
        Item.knockBack = 4f;
        Item.value = Item.buyPrice(silver: 20);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Sapphire, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}

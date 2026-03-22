using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items.Weapons;

public class EmeraldBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 38;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.autoReuse = true;
        Item.shootSpeed = 8f;
        Item.shoot = ProjectileID.EmeraldBolt;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 19;
        Item.knockBack = 4.25f;
        Item.value = Item.buyPrice(silver: 30);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Emerald, 10);
        recipe.AddTile(TileID.Anvils);
    }
}

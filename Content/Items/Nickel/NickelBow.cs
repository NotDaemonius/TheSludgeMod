using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel;

public class NickelBow : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(silver: 5, copper: 60);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.crit = 1;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 6.6f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<NickelBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
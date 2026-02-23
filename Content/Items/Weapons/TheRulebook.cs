using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class TheRulebook : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 95;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7f;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Yellow;
            Item.mana = 10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TheRulebookProj>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item43;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ItemID.Football, 10);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}
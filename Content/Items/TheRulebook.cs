using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
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

            // Makes it look like a magic book when held
            Item.UseSound = SoundID.Item43; // Magic book sound
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
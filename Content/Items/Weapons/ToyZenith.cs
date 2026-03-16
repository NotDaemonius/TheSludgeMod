using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class ToyZenith : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 400;
            Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(platinum: 3);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Zenith)
            .AddTile(TileID.MythrilAnvil);
        }
    }
}

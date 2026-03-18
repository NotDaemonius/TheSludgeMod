using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class FrostflamingMace : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FlamingMace);
            Item.rare = ItemRarityID.Green;
            Item.damage = 22;
            Item.knockBack = 8f;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.value = Item.buyPrice(gold: 3);
            Item.shoot = ModContent.ProjectileType<FrostflamingMaceProj>();
            Item.shootSpeed = 11f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlamingMace)
            .AddIngredient(ItemID.IceTorch, 99)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
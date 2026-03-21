using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class LifeSickle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 95;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6.5f;
            Item.crit = 8;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.shoot = ModContent.ProjectileType<LifeSickleProj>();
            Item.shootSpeed = 14f;
            Item.value = Item.buyPrice(gold: 25);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DeathSickle)
                .AddIngredient(ItemID.ChlorophyteBar, 10)
                .AddIngredient(ItemID.VialofVenom, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium
{
    public class OsmiumChainsaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsChainsaw[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 32;
            Item.height = 12;
            Item.useTime = 5;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 3, silver: 24);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<OsmiumChainsawProj>();
            Item.shootSpeed = 32f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.axe = 16;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OsmiumBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class GiantToothbrush : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.width = 66;
            Item.height = 66;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(silver: 40);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GiantToothbrushProj>();
            Item.shootSpeed = 4f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Plastic>(), 15)
            .AddIngredient(ItemID.Bone, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
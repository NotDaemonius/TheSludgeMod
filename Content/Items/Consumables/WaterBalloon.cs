using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Consumables;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod.Content.Items.Consumables
{
    public class WaterBalloon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.damage = 0;
            Item.DamageType = DamageClass.Generic;
            Item.shoot = ModContent.ProjectileType<WaterBalloonProj>();
            Item.shootSpeed = 8f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(copper: 5);
        }
        public override void AddRecipes()
        {
            CreateRecipe(20)
            .AddIngredient(ModContent.ItemType<Plastic>(), 1)
            .AddCondition(Condition.NearWater)
            .Register();
        }
    }
}
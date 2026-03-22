using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class MagicalFlute : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.autoReuse = true;
            Item.shootSpeed = 11f;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 45;
            Item.knockBack = 2;
            Item.crit = 6;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = new SoundStyle("TheSludgeMod/Assets/Sounds/FluteC");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SilentQuarterNote>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = HelperFunctions.AdjustMuzzleOffset(player, ref position, velocity, 50f);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrystalShard, 20)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ModContent.ItemType<Flute>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
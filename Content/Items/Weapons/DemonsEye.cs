using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class DemonsEye : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.autoReuse = true;
        Item.shootSpeed = 10;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 13;
        Item.knockBack = -1;
        Item.crit = 6;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<DemonsEyeEye>();
    }
}
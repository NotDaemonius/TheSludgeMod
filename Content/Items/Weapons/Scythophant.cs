using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class Scythophant : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 196;
        Item.DamageType = DamageClass.Melee;
        Item.width = 64;
        Item.height = 64;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 7f;
        Item.value = Item.buyPrice(gold: 11);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.channel = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.shoot = ModContent.ProjectileType<ScythophantProj>();
        Item.shootSpeed = 1f;
        Item.crit = 32;
    }
}
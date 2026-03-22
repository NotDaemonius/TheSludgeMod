using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class TungstenEnforcedUmbrella : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 18;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 44;
        Item.height = 44;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 60);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = false;
        Item.crit = 6;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<TungstenEnforcedUmbrellaProj>();
        Item.shootSpeed = 2.1f;
    }
}

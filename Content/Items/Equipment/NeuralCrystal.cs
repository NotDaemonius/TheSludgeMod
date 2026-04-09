using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Pets;

namespace TheSludgeMod.Content.Items.Equipment;

public class NeuralCrystal : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DukeFishronPetItem);
        Item.shoot = ModContent.ProjectileType<NeuralCrystalProj>();
        Item.buffType = ModContent.BuffType<NeuralCrystalBuff>();
    }

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer) player.AddBuff(Item.buffType, 3600);
        return true;
    }
}
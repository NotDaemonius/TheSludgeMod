using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class MerchantDollPlayer : ModPlayer
{
    public bool voodooMerchant = false;

    public override void ResetEffects() => voodooMerchant = false;
}

public class MerchantVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<MerchantDollPlayer>().voodooMerchant = true;
}

public class MerchantDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Merchant && player.GetModPlayer<MerchantDollPlayer>().voodooMerchant) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Merchant) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<MerchantDollPlayer>().voodooMerchant) return true;
        return null;
    }
}
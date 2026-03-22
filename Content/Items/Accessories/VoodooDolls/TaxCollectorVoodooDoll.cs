using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class TaxCollectorDollPlayer : ModPlayer
{
    public bool voodooTaxCollector = false;

    public override void ResetEffects() => voodooTaxCollector = false;
}

public class TaxCollectorVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<TaxCollectorDollPlayer>().voodooTaxCollector = true;
}

public class TaxCollectorDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.TaxCollector && player.GetModPlayer<TaxCollectorDollPlayer>().voodooTaxCollector) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.TaxCollector) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<TaxCollectorDollPlayer>().voodooTaxCollector) return true;
        return null;
    }
}
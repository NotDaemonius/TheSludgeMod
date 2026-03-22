using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class CyborgDollPlayer : ModPlayer
{
    public bool voodooCyborg = false;

    public override void ResetEffects() => voodooCyborg = false;
}

public class CyborgVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<CyborgDollPlayer>().voodooCyborg = true;
}

public class CyborgDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Cyborg && player.GetModPlayer<CyborgDollPlayer>().voodooCyborg) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Cyborg) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<CyborgDollPlayer>().voodooCyborg) return true;
        return null;
    }
}
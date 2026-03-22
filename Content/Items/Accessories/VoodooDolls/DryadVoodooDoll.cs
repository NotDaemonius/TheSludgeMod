using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class DryadDollPlayer : ModPlayer
{
    public bool voodooDryad = false;

    public override void ResetEffects() => voodooDryad = false;
}

public class DryadVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<DryadDollPlayer>().voodooDryad = true;
}

public class DryadDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Dryad && player.GetModPlayer<DryadDollPlayer>().voodooDryad) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Dryad) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<DryadDollPlayer>().voodooDryad) return true;
        return null;
    }
}
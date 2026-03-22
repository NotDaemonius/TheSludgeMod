using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class PirateDollPlayer : ModPlayer
{
    public bool voodooPirate = false;

    public override void ResetEffects() => voodooPirate = false;
}

public class PirateVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<PirateDollPlayer>().voodooPirate = true;
}

public class PirateDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Pirate && player.GetModPlayer<PirateDollPlayer>().voodooPirate) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Pirate) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<PirateDollPlayer>().voodooPirate) return true;
        return null;
    }
}
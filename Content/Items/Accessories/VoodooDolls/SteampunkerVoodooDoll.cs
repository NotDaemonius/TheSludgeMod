using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class SteampunkerDollPlayer : ModPlayer
{
    public bool voodooSteampunker = false;

    public override void ResetEffects() => voodooSteampunker = false;
}

public class SteampunkerVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<SteampunkerDollPlayer>().voodooSteampunker = true;
}

public class SteampunkerDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Steampunker && player.GetModPlayer<SteampunkerDollPlayer>().voodooSteampunker) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Steampunker) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<SteampunkerDollPlayer>().voodooSteampunker) return true;
        return null;
    }
}
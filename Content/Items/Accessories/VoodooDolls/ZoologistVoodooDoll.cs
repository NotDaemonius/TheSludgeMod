using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class ZoologistDollPlayer : ModPlayer
{
    public bool voodooZoologist = false;

    public override void ResetEffects() => voodooZoologist = false;
}

public class ZoologistVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<ZoologistDollPlayer>().voodooZoologist = true;
}

public class ZoologistDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.BestiaryGirl && player.GetModPlayer<ZoologistDollPlayer>().voodooZoologist) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.BestiaryGirl) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<ZoologistDollPlayer>().voodooZoologist) return true;
        return null;
    }
}
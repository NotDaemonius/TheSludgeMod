using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class TavernkeepDollPlayer : ModPlayer
{
    public bool voodooTavernkeep = false;

    public override void ResetEffects() => voodooTavernkeep = false;
}

public class TavernkeepVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<TavernkeepDollPlayer>().voodooTavernkeep = true;
}

public class TavernkeepDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.DD2Bartender && player.GetModPlayer<TavernkeepDollPlayer>().voodooTavernkeep) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.DD2Bartender) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<TavernkeepDollPlayer>().voodooTavernkeep) return true;
        return null;
    }
}
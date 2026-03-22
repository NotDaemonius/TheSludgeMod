using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class GoblinTinkererDollPlayer : ModPlayer
{
    public bool voodooGoblinTinkerer = false;

    public override void ResetEffects() => voodooGoblinTinkerer = false;
}

public class GoblinTinkererVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<GoblinTinkererDollPlayer>().voodooGoblinTinkerer = true;
}

public class GoblinTinkererDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.GoblinTinkerer && player.GetModPlayer<GoblinTinkererDollPlayer>().voodooGoblinTinkerer) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.GoblinTinkerer) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<GoblinTinkererDollPlayer>().voodooGoblinTinkerer) return true;
        return null;
    }
}
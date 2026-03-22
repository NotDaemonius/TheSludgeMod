using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class AnglerDollPlayer : ModPlayer
{
    public bool voodooAngler = false;

    public override void ResetEffects() => voodooAngler = false;
}

public class AnglerVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<AnglerDollPlayer>().voodooAngler = true;
}

public class AnglerDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Angler && player.GetModPlayer<AnglerDollPlayer>().voodooAngler) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Angler) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<AnglerDollPlayer>().voodooAngler) return true;
        return null;
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class TruffleDollPlayer : ModPlayer
{
    public bool voodooTruffle = false;

    public override void ResetEffects() => voodooTruffle = false;
}

public class TruffleVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<TruffleDollPlayer>().voodooTruffle = true;
}

public class TruffleDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Truffle && player.GetModPlayer<TruffleDollPlayer>().voodooTruffle) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Truffle) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<TruffleDollPlayer>().voodooTruffle) return true;
        return null;
    }
}
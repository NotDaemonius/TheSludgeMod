using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class NurseDollPlayer : ModPlayer
{
    public bool voodooNurse = false;

    public override void ResetEffects() => voodooNurse = false;
}

public class NurseVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<NurseDollPlayer>().voodooNurse = true;
}

public class NurseDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.Nurse && player.GetModPlayer<NurseDollPlayer>().voodooNurse) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.Nurse) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<NurseDollPlayer>().voodooNurse) return true;
        return null;
    }
}
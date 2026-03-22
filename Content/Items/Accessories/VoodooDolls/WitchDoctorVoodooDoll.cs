using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls;

public class WitchDoctorDollPlayer : ModPlayer
{
    public bool voodooWitchDoctor = false;

    public override void ResetEffects() => voodooWitchDoctor = false;
}

public class WitchDoctorVoodooDoll : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.accessory = true;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<WitchDoctorDollPlayer>().voodooWitchDoctor = true;
}

public class WitchDoctorDollNPC : GlobalNPC
{
    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (npc.type == NPCID.WitchDoctor && player.GetModPlayer<WitchDoctorDollPlayer>().voodooWitchDoctor) return true;
        return null;
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (npc.type != NPCID.WitchDoctor) return null;
        Player owner = Main.player[projectile.owner];
        if (owner.GetModPlayer<WitchDoctorDollPlayer>().voodooWitchDoctor) return true;
        return null;
    }
}
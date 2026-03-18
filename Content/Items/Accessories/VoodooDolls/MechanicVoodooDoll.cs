using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class MechanicDollPlayer : ModPlayer
    {
        public bool voodooMechanic = false;

        public override void ResetEffects()
        {
            voodooMechanic = false;
        }
    }
    public class MechanicVoodooDoll : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MechanicDollPlayer>().voodooMechanic = true;
        }
    }
    public class MechanicDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.Mechanic && player.GetModPlayer<MechanicDollPlayer>().voodooMechanic)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.Mechanic)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<MechanicDollPlayer>().voodooMechanic)
                return true;

            return null;
        }
    }
}
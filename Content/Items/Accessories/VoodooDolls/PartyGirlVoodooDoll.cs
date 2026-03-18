using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class PartyGirlDollPlayer : ModPlayer
    {
        public bool voodooPartyGirl = false;

        public override void ResetEffects()
        {
            voodooPartyGirl = false;
        }
    }
    public class PartyGirlVoodooDoll : ModItem
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
            player.GetModPlayer<PartyGirlDollPlayer>().voodooPartyGirl = true;
        }
    }
    public class PartyGirlDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.PartyGirl && player.GetModPlayer<PartyGirlDollPlayer>().voodooPartyGirl)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.PartyGirl)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<PartyGirlDollPlayer>().voodooPartyGirl)
                return true;

            return null;
        }
    }
}
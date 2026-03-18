using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class DemolitionistDollPlayer : ModPlayer
    {
        public bool voodooDemolitionist = false;

        public override void ResetEffects()
        {
            voodooDemolitionist = false;
        }
    }
    public class DemolitionistVoodooDoll : ModItem
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
            player.GetModPlayer<DemolitionistDollPlayer>().voodooDemolitionist = true;
        }
    }
    public class DemolitionistDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.Demolitionist && player.GetModPlayer<DemolitionistDollPlayer>().voodooDemolitionist)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.Demolitionist)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<DemolitionistDollPlayer>().voodooDemolitionist)
                return true;

            return null;
        }
    }
}
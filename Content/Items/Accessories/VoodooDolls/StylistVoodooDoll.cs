using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class StylistDollPlayer : ModPlayer
    {
        public bool voodooStylist = false;

        public override void ResetEffects()
        {
            voodooStylist = false;
        }
    }
    public class StylistVoodooDoll : ModItem
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
            player.GetModPlayer<StylistDollPlayer>().voodooStylist = true;
        }
    }
    public class StylistDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.Stylist && player.GetModPlayer<StylistDollPlayer>().voodooStylist)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.Stylist)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<StylistDollPlayer>().voodooStylist)
                return true;

            return null;
        }
    }
}
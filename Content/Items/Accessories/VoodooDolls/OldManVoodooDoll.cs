using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class OldManDollPlayer : ModPlayer
    {
        public bool voodooOldMan = false;

        public override void ResetEffects()
        {
            voodooOldMan = false;
        }
    }
    public class OldManVoodooDoll : ModItem
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
            player.GetModPlayer<OldManDollPlayer>().voodooOldMan = true;
        }
    }
    public class OldManDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.OldMan && player.GetModPlayer<OldManDollPlayer>().voodooOldMan)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.OldMan)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<OldManDollPlayer>().voodooOldMan)
                return true;

            return null;
        }
    }
}
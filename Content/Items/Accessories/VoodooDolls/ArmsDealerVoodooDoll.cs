using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class ArmsDealerDollPlayer : ModPlayer
    {
        public bool voodooArmsDealer = false;

        public override void ResetEffects()
        {
            voodooArmsDealer = false;
        }
    }
    public class ArmsDealerVoodooDoll : ModItem
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
            player.GetModPlayer<ArmsDealerDollPlayer>().voodooArmsDealer = true;
        }
    }
    public class ArmsDealerDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.ArmsDealer && player.GetModPlayer<ArmsDealerDollPlayer>().voodooArmsDealer)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.ArmsDealer)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<ArmsDealerDollPlayer>().voodooArmsDealer)
                return true;

            return null;
        }
    }
}
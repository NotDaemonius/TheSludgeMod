using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class SkeletonMerchantDollPlayer : ModPlayer
    {
        public bool voodooSkeletonMerchant = false;

        public override void ResetEffects()
        {
            voodooSkeletonMerchant = false;
        }
    }
    public class SkeletonMerchantVoodooDoll : ModItem
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
            player.GetModPlayer<SkeletonMerchantDollPlayer>().voodooSkeletonMerchant = true;
        }
    }
    public class SkeletonMerchantDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.SkeletonMerchant && player.GetModPlayer<SkeletonMerchantDollPlayer>().voodooSkeletonMerchant)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.SkeletonMerchant)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<SkeletonMerchantDollPlayer>().voodooSkeletonMerchant)
                return true;

            return null;
        }
    }
}
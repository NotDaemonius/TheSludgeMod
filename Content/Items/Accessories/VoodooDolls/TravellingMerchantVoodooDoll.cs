using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class TravellingMerchantDollPlayer : ModPlayer
    {
        public bool voodooTravellingMerchant = false;

        public override void ResetEffects()
        {
            voodooTravellingMerchant = false;
        }
    }
    public class TravellingMerchantVoodooDoll : ModItem
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
            player.GetModPlayer<TravellingMerchantDollPlayer>().voodooTravellingMerchant = true;
        }
    }
    public class TravellingMerchantDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.TravellingMerchant && player.GetModPlayer<TravellingMerchantDollPlayer>().voodooTravellingMerchant)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.TravellingMerchant)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<TravellingMerchantDollPlayer>().voodooTravellingMerchant)
                return true;

            return null;
        }
    }
}
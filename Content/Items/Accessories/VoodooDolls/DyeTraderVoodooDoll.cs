using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class DyeTraderDollPlayer : ModPlayer
    {
        public bool voodooDyeTrader = false;

        public override void ResetEffects()
        {
            voodooDyeTrader = false;
        }
    }
    public class DyeTraderVoodooDoll : ModItem
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
            player.GetModPlayer<DyeTraderDollPlayer>().voodooDyeTrader = true;
        }
    }
    public class DyeTraderDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.DyeTrader && player.GetModPlayer<DyeTraderDollPlayer>().voodooDyeTrader)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.DyeTrader)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<DyeTraderDollPlayer>().voodooDyeTrader)
                return true;

            return null;
        }
    }
}
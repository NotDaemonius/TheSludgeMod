using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class PrincessDollPlayer : ModPlayer
    {
        public bool voodooPrincess = false;

        public override void ResetEffects()
        {
            voodooPrincess = false;
        }
    }
    public class PrincessVoodooDoll : ModItem
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
            player.GetModPlayer<PrincessDollPlayer>().voodooPrincess = true;
        }
    }
    public class PrincessDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.Princess && player.GetModPlayer<PrincessDollPlayer>().voodooPrincess)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.Princess)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<PrincessDollPlayer>().voodooPrincess)
                return true;

            return null;
        }
    }
}
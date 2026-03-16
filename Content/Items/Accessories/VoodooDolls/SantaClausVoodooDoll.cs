using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class SantaClausDollPlayer : ModPlayer
    {
        public bool voodooSantaClaus = false;

        public override void ResetEffects()
        {
            voodooSantaClaus = false;
        }
    }
    public class SantaClausVoodooDoll : ModItem
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
            player.GetModPlayer<SantaClausDollPlayer>().voodooSantaClaus = true;
        }
    }
    public class SantaClausDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.SantaClaus && player.GetModPlayer<SantaClausDollPlayer>().voodooSantaClaus)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.SantaClaus)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<SantaClausDollPlayer>().voodooSantaClaus)
                return true;

            return null;
        }
    }
}
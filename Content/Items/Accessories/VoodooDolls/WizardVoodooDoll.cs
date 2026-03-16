using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.VoodooDolls
{
    public class WizardDollPlayer : ModPlayer
    {
        public bool voodooWizard = false;

        public override void ResetEffects()
        {
            voodooWizard = false;
        }
    }
    public class WizardVoodooDoll : ModItem
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
            player.GetModPlayer<WizardDollPlayer>().voodooWizard = true;
        }
    }
    public class WizardDollNPC : GlobalNPC
    {
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.type == NPCID.Wizard && player.GetModPlayer<WizardDollPlayer>().voodooWizard)
                return true;

            return null;
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type != NPCID.Wizard)
                return null;

            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<WizardDollPlayer>().voodooWizard)
                return true;

            return null;
        }
    }
}
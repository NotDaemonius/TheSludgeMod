using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Content.Items.Junk
{
    public class DodgyBuilder : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Gray;
            Item.maxStack = 1;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<DodgyBuilderPlayer>().hasDodgyBuilder = true;
        }
    }
}
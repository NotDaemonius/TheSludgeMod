using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class MainframeMaskHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(silver: 75);
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }
    }
}
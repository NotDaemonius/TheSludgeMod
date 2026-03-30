using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Consumables;

namespace TheSludgeMod.Common.Players
{
    public class InventoryPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            yield return new Item(ModContent.ItemType<StarterBag>());
        }
    }
}
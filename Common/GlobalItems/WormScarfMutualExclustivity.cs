using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories;

namespace TheSludgeMod.Common.GlobalItems;

public class WormScarfMutualExclustivity : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.WormScarf;
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        return !HelperFunctions.PlayerHasAccesory(player, ModContent.ItemType<DestroyerScarf>());
    }
}

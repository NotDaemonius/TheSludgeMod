using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Common.GlobalItems;

public class BrainOfPreplexityMutualExclustivity : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BrainOfConfusion;

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        bool boppExist = player.TryGetModPlayer<BrainOfPerplexityPlayer>(out BrainOfPerplexityPlayer self2);
        if (boppExist) return !self2.hasBrainOfPerplexity;
        return true;
    }
}

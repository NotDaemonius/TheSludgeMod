using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Accessories.Balloons;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Bismuth;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Items.Nickel;
using TheSludgeMod.Content.Items.Osmium;
using TheSludgeMod.Content.Items.Uranium;
using TheSludgeMod.Content.Items.Zinc;

namespace TheSludgeMod.Common.GlobalItems;

public class ModifiyCrateLoot : GlobalItem
{
    private static readonly int[] PreHMCrates =
    {
        ItemID.WoodenCrate,
        ItemID.IronCrate,
        ItemID.GoldenCrate,
        ItemID.JungleFishingCrate,
        ItemID.DungeonFishingCrate,
        ItemID.CrimsonFishingCrate,
        ItemID.CorruptFishingCrate,
        ItemID.FloatingIslandFishingCrate,
        ItemID.HallowedFishingCrate,
    };

    private static readonly int[] HMCrates =
    {
        ItemID.WoodenCrateHard,
        ItemID.IronCrateHard,
        ItemID.GoldenCrateHard,
        ItemID.HallowedFishingCrateHard,
        ItemID.FloatingIslandFishingCrateHard,
        ItemID.CorruptFishingCrateHard,
        ItemID.CrimsonFishingCrateHard,
        ItemID.DungeonFishingCrateHard,
        ItemID.JungleFishingCrateHard,
    };

    public override void ModifyItemLoot(Item item, ItemLoot loot)
    {
        if (Array.Exists(PreHMCrates, id => id == item.type))
        {
            loot.Add(ItemDropRule.Common(ModContent.ItemType<ZincOre>(), 6, 8, 14));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<NickelOre>(), 6, 8, 14));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<AluminiumOre>(), 6, 8, 14));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<IridiumOre>(), 6, 8, 14));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<ZincBar>(), 8, 3, 7));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<NickelBar>(), 8, 3, 7));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<AluminiumBar>(), 8, 3, 7));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<IridiumBar>(), 8, 3, 7));
        }

        if (Array.Exists(HMCrates, id => id == item.type))
        {
            loot.Add(ItemDropRule.Common(ModContent.ItemType<UraniumOre>(), 5, 6, 12));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<OsmiumOre>(), 5, 6, 12));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<BismuthOre>(), 5, 6, 12));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<UraniumBar>(), 7, 2, 5));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<OsmiumBar>(), 7, 2, 5));
            loot.Add(ItemDropRule.Common(ModContent.ItemType<BismuthBar>(), 7, 2, 5));
        }

        if (item.type == ItemID.FloatingIslandFishingCrate || item.type == ItemID.FloatingIslandFishingCrateHard)
        {
            loot.RemoveWhere(rule =>
                rule is CommonDrop cd &&
                (cd.itemId == ItemID.Starfury ||
                 cd.itemId == ItemID.LuckyHorseshoe ||
                 cd.itemId == ItemID.CelestialMagnet ||
                 cd.itemId == ItemID.ShinyRedBalloon)
            );

            int[] balloons = new int[]
            {
            ItemID.ShinyRedBalloon,
            ModContent.ItemType<ShinyBlackBalloon>(),
            ModContent.ItemType<ShinyBlueBalloon>(),
            ModContent.ItemType<ShinyBrownBalloon>(),
            ModContent.ItemType<ShinyCyanBalloon>(),
            ModContent.ItemType<ShinyGreenBalloon>(),
            ModContent.ItemType<ShinyLimeBalloon>(),
            ModContent.ItemType<ShinyOrangeBalloon>(),
            ModContent.ItemType<ShinyPinkBalloon>(),
            ModContent.ItemType<ShinyPurpleBalloon>(),
            ModContent.ItemType<ShinySilverBalloon>(),
            ModContent.ItemType<ShinySkyBlueBalloon>(),
            ModContent.ItemType<ShinyTealBalloon>(),
            ModContent.ItemType<ShinyVioletBalloon>(),
            ModContent.ItemType<ShinyYellowBalloon>()
            };

            loot.Add(new OneFromRulesRule(1,
                ItemDropRule.Common(ItemID.Starfury, 1),
                ItemDropRule.Common(ItemID.LuckyHorseshoe, 1),
                ItemDropRule.Common(ItemID.CelestialMagnet, 1),
                ItemDropRule.OneFromOptions(1, balloons)
            ));
        }
    }
}

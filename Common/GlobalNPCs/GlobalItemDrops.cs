using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Consumables;
using TheSludgeMod.Content.Items.Junk;
using TheSludgeMod.Content.Items.Tools;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.Items.Accessories.VoodooDolls;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod.Common.GlobalNPCs;

public class SkeletronDefeatedCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;

    public bool CanShowItemDropInUI() => true;

    public string GetConditionDescription() => "Drops after Skeletron has been defeated";
}
public class GlobalItemDrops : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (npc.type == NPCID.DemonEye || npc.type == NPCID.DemonEye2)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DemonsEye>(), 10));
        }

        if (npc.type == NPCID.PirateCrossbower)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheRedRobin>(), 10));
        } 

        if (npc.type == NPCID.Plantera) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PeaShooter>(), 2));
        }

        if (npc.type == NPCID.CultistBoss)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Scythophant>(), 2));
        }

        if (npc.type == NPCID.Golem)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Autohammer, 1));
        }

        if (npc.type == NPCID.Clown)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GiggleMegaphone>(), 10));
        }

        if (npc.type == NPCID.UmbrellaSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlimyUmbrella>(), 50));
        }

        if (npc.type == NPCID.Skeleton)
        {
            var skeletronDefeated = new LeadingConditionRule(new SkeletronDefeatedCondition());
            skeletronDefeated.OnSuccess(ItemDropRule.Common(ItemID.Bone, 1, 1, 2));
            npcLoot.Add(skeletronDefeated);
        }

        if (npc.type == NPCID.Frog)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FrogLeg, 200));
        }

        if (npc.type == NPCID.GoldFrog)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FrogLeg, 4));
        }

        if (npc.type == NPCID.Plantera)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TheAxe, 1));
        }

        if (npc.type == NPCID.EyeofCthulhu)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CthulhuEyeStaff>(), 4));
        }

        if (npc.type == NPCID.SkeletronHead)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheBowne>(), 4));
        }

        if (npc.type == NPCID.ToxicSludge)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NoxiousGoo>(), 10));
        }

        // === VOODOO DOLLS === //

        if (npc.type == NPCID.Crab)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AnglerVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.Shark)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArmsDealerVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.DungeonSpirit)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CyborgVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.UndeadMiner)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DemolitionistVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.GreenSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DryadVoodooDoll>(), 1000));
        }
        if (npc.type == NPCID.CochinealBeetle || npc.type == NPCID.CyanBeetle || npc.type == NPCID.LacBeetle)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DyeTraderVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.GoblinScout)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinTinkererVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.Antlion)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GolferVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.CursedSkull)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MechanicVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.CaveBat)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MerchantVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.Pinky)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NurseVoodooDoll>(), 50));
        }
        if (npc.type == NPCID.DungeonSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OldManVoodooDoll>(), 50));
        }
        if (npc.type == NPCID.AngryNimbus)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PainterVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.ShimmerSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PartyGirlVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.PirateCaptain)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PirateVoodooDoll>(), 50));
        }
        if (npc.type == NPCID.RainbowSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrincessVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.Krampus)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SantaClausVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.Skeleton)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SkeletonMerchantVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.PossessedArmor)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteampunkerVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.WallCreeper)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StylistVoodooDoll>(), 500));
        }
        if (npc.type == NPCID.DD2GoblinT1 || npc.type == NPCID.DD2GoblinT2 || npc.type == NPCID.DD2GoblinT3)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TavernkeepVoodooDoll>(), 200));
        }
        if (npc.type == NPCID.DemonTaxCollector)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TaxCollectorVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.BlueSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TravellingMerchantVoodooDoll>(), 1000));
        }
        if (npc.type == NPCID.GiantFungiBulb)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TruffleVoodooDoll>(), 100));
        }
        if (npc.type == NPCID.Bee)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WitchDoctorVoodooDoll>(), 1000));
        }
        if (npc.type == NPCID.Tim || npc.type == NPCID.RuneWizard)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WizardVoodooDoll>(), 50));
        }
        if (npc.type == NPCID.Bunny)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZoologistVoodooDoll>(), 1000));
        }

    }
    public override void OnKill(NPC npc)
    {
        if (Main.eclipse && npc.type == NPCID.OrangeDragonfly)
        {
            if (npc.lastInteraction != -1 && Main.projectile[npc.lastInteraction].type == ProjectileID.Chik)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DefaultItem>(), Main.rand.Next(10, 51));
            }
        }

        if (Main.dayRate > 30 && npc.type == NPCID.NebulaBeast)
        {
            if (npc.lastInteraction != -1 && Main.projectile[npc.lastInteraction].type == ProjectileID.SporeCloud)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PlaceholderItem>(), Main.rand.Next(10, 51));
            }
        }
    }

}

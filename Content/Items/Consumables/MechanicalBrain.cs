using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.NPCs.TheMainframe;

namespace TheSludgeMod.Content.Items.Consumables;

public class MechanicalBrain : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
    }

    public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.rare = ItemRarityID.Orange;
        Item.maxStack = 9999;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = false;
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) => itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;

    public override bool CanUseItem(Player player) => !Main.IsItDay() && !NPC.AnyNPCs(ModContent.NPCType<TheMainframe>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            int type = ModContent.NPCType<TheMainframe>();
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, type);
            else NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
        }

        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofFright);
        recipe.AddIngredient(ItemID.SoulofMight);
        recipe.AddIngredient(ItemID.SoulofSight);
        recipe.AddRecipeGroup("IronBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

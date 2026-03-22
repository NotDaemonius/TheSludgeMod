using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Tools;

public class SolunarChronowarper : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.useAnimation = 8;
        Item.useTime = 8;
        Item.rare = ItemRarityID.Cyan;
        Item.autoReuse = false;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item67;
    }

    public override bool CanUseItem(Player player)
    {
        for (int i = 0; i < Main.maxNPCs; i++) if (Main.npc[i].active && Main.npc[i].boss) return false;
        return true;
    }

    public override bool? UseItem(Player player)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient) return true;
        if (Main.dayTime && Main.time < 27000) Main.SkipToTime(27000, true);
        else if (Main.dayTime) Main.SkipToTime(0, false);
        else if (!Main.dayTime && Main.time < 16200) Main.SkipToTime(16200, false);
        else Main.SkipToTime(0, true);
        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddIngredient(ItemID.SoulofLight, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz;

[AutoloadEquip(EquipType.Body)]

public class RoseQuartzRobe : ModItem
{
    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server) return;
        EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
    }

    public override void SetStaticDefaults() => ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 14;
        Item.rare = ItemRarityID.Green;
        Item.defense = 3;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 80;
        player.manaCost *= 0.91f;
    }

    public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
    {
        robes = true;
        equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Robe);
        recipe.AddIngredient(ModContent.ItemType<RoseQuartz>(), 10);
        recipe.Register();
    }
}

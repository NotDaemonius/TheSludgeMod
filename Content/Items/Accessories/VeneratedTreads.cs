using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TheSludgeMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shoes, EquipType.Wings)]

public class VeneratedTreads : ModItem
{
    public override void SetStaticDefaults() => ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(150, 7f, 2.5f);

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 22;
        Item.value = Item.sellPrice(gold: 15);
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.accRunSpeed = Math.Max(player.accRunSpeed, 7.5f);
        player.fireWalk = true;
        player.waterWalk = true;
        player.waterWalk2 = true;
        player.noFallDmg = true;
        player.iceSkate = true;
        player.lavaMax += 420;
        player.lavaRose = true;
        player.moveSpeed += 0.3f;
        player.accFlipper = true;
        player.hasMagiluminescence = true;
        if (!hideVisual) Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.9f, 0.05f, 0.2f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TerrasparkBoots);
        recipe.AddIngredient(ItemID.Magiluminescence);
        recipe.AddIngredient(ItemID.Flipper);
        recipe.AddIngredient(ItemID.SoulofFlight, 10);
        recipe.AddIngredient(ItemID.HallowedBar, 10);
        recipe.AddIngredient(ItemID.SoulofFright, 3);
        recipe.AddIngredient(ItemID.SoulofMight, 3);
        recipe.AddIngredient(ItemID.SoulofSight, 3);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Materials;

public class SoulofSpite : ModItem
{
    public override void SetStaticDefaults()
    {
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
        ItemID.Sets.AnimatesAsSoul[Type] = true;
        ItemID.Sets.ItemIconPulse[Type] = true;
        ItemID.Sets.ItemNoGravity[Type] = true;
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;

        Item.rare = ItemRarityID.Pink;
        Item.value = 8000;
        Item.material = true;
        Item.maxStack = Item.CommonMaxStack;
    }

    public override void PostUpdate() => Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);

    public override Color? GetAlpha(Color lightColor) => new Color(255, 0, 0, 50);
}
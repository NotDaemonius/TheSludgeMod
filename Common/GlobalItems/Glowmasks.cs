using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalItems;
[ReinitializeDuringResizeArrays]
public class Glowmasks : GlobalItem
{
    static readonly short[] itemGlowmasks = ItemID.Sets.Factory.CreateCustomSet<short>(-1);
    public static short AddGlowMask(string texture)
    {
        if (Main.netMode != NetmodeID.Server)
        {
            string name = texture;
            if (ModContent.RequestIfExists(name, out Asset<Texture2D> asset))
            {
                int index = TextureAssets.GlowMask.Length;
                Array.Resize(ref TextureAssets.GlowMask, index + 1);
                TextureAssets.GlowMask[^1] = asset;
                return (short)index;
            }
        }
        return -1;
    }
    public static short AddGlowMask(ModItem item, string suffix = "Glowmask")
    {
        short slot = AddGlowMask(item.Texture + suffix);
        itemGlowmasks[item.Type] = slot;
        return slot;
    }
    public static short AddGlowMask(ModTexturedType content, string suffix = "Glowmask")
    {
        return AddGlowMask(content.Texture + suffix);
    }
    public override void SetDefaults(Item item)
    {
        if (itemGlowmasks.IndexInRange(item.type) && itemGlowmasks?[item.type] is not 0 and not -1 and not null) item.glowMask = itemGlowmasks[item.type];
    }
    public override void Unload() => Array.Resize(ref TextureAssets.GlowMask, GlowMaskID.Count);
}
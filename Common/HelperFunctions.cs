using Microsoft.Xna.Framework;
using Terraria;

namespace TheSludgeMod.Common;

public static class HelperFunctions
{
    public static bool PlayerHasAccesory(Player player, short ItemID)
    {
        for (int i = 0; i < player.armor.Length; i++)
        {
            if (player.armor[i] != null && !player.armor[i].IsAir && player.armor[i].type == ItemID)
            {
                return true;
            }
        }

        return false;
    }

    public static bool PlayerHasAccesory(Player player, int ItemID)
    {
        for (int i = 0; i < player.armor.Length; i++)
        {
            if (player.armor[i] != null && !player.armor[i].IsAir && player.armor[i].type == ItemID)
            {
                return true;
            }
        }

        return false;
    }

    public static Vector2 AdjustMuzzleOffset(Player player, ref Vector2 position, Vector2 velocity, float intensity)
    {
        Vector2 muzzleOffset = Vector2.Normalize(velocity) * intensity;

        if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
        {
            position += muzzleOffset;
        }
        return position;
    }
}


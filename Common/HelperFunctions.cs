using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TheSludgeMod.Common
{
    public static class HelperFunctions
    {
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
}

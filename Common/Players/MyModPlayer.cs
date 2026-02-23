using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Players
{
    public class MyModPlayer : ModPlayer
    {
        public override void PostUpdateRunSpeeds()
        {
            Player.maxRunSpeed = 4.5f;
            Player.runAcceleration = 0.09f;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

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

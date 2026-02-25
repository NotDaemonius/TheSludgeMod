using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class TungstenEnforcedUmbrella : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.rare = 1;
            Item.value = 10000;
            Item.holdStyle = 2;
            Item.useStyle = 3;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.damage = 10;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
        }
    }
}

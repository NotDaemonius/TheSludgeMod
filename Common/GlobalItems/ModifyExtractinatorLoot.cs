using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalItems
{
    public class ModifyExtractinatorLoot : ModItem
    {
        public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            base.ExtractinatorUse(extractinatorBlockType, ref resultType, ref resultStack);
        }
    }
}

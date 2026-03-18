using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class DeodorantGlobalNPC : GlobalNPC
    {
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            Player player = Main.LocalPlayer;

            if (!player.HasBuff(ModContent.BuffType<DeodorantBuff>()))
                return;

            foreach (Item item in items)
            {
                if (item == null || item.IsAir)
                    continue;

                int basePrice = item.shopCustomPrice ?? item.value;
                item.shopCustomPrice = (int)(basePrice * 0.90f);
            }
        }
    }
}
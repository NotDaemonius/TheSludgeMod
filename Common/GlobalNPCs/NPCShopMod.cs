using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.Items.Ammo;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod;

public class NPCShopMod : GlobalNPC
{
    public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
    {
        if (npc.type == NPCID.TravellingMerchant)
        {
            if (NPC.downedBoss1)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == null || items[i].type == ItemID.None)
                    {
                        items[i] = new Item(ModContent.ItemType<XShotBlaster>());
                        break; 
                    }
                }
            }
        }

        if (npc.type == NPCID.ArmsDealer)
        {
            if (Main.hardMode)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == null || items[i].type == ItemID.None)
                    {
                        items[i] = new Item(ModContent.ItemType<BigBullet>());
                        break;
                    }
                }
            }
        }

        if (npc.type == NPCID.Wizard)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].type == ItemID.None)
                {
                    items[i] = new Item(ModContent.ItemType<Flute>());
                    break;
                }
            }
        }

        if (npc.type == NPCID.ArmsDealer)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].type == ItemID.None)
                {
                    items[i] = new Item(ItemID.ZombieArm);
                    break;
                }
            }
        }
    }
}
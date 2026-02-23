using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Tools
{
    public class MagnetPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.pick = 115; 
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; 
        }
        public override void HoldItem(Player player)
        {
            foreach (Item item in Main.ActiveItems)
            {
                if (item.noGrabDelay == 0 && item.playerIndexTheItemIsReservedFor == player.whoAmI && (player.Center - item.Center).Length() < 400)
                {
                    item.beingGrabbed = true;
                    if (player.Center.X > item.Center.X)
                    {
                        if (item.velocity.X < 90f + player.velocity.X)
                        {
                            item.velocity.X += 9f;
                        }
                        if (item.velocity.X < 0f)
                        {
                            item.velocity.X += 9f * 0.75f;
                        }
                    }
                    else
                    {
                        if (item.velocity.X > -90f + player.velocity.X)
                        {
                            item.velocity.X -= 9f;
                        }
                        if (item.velocity.X > 0f)
                        {
                            item.velocity.X -= 9f * 0.75f;
                        }
                    }

                    if (player.Center.Y > item.Center.Y)
                    {
                        if (item.velocity.Y < 90f)
                        {
                            item.velocity.Y += 9f;
                        }
                        if (item.velocity.Y < 0f)
                        {
                            item.velocity.Y += 9f * 0.75f;
                        }
                    }
                    else
                    {
                        if (item.velocity.Y > -90f)
                        {
                            item.velocity.Y -= 9f;
                        }
                        if (item.velocity.Y > 0f)
                        {
                            item.velocity.Y -= 9f * 0.75f;
                        }
                    }
                }
            }
        }
    }
}

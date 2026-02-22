using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.RoseQuartz;

namespace TheSludgeMod.Common.Systems
{
    public class FixLargeGems :  ModSystem
    {
        public override void Load()
        {
            On_Player.Update += On_Player_Update;
            On_PlayerDrawLayers.DrawPlayer_36_CTG += On_PlayerDrawLayers_DrawPlayer_36_CTG;
        }
        //hii
        private void On_PlayerDrawLayers_DrawPlayer_36_CTG(On_PlayerDrawLayers.orig_DrawPlayer_36_CTG orig, ref PlayerDrawSet drawinfo)
        {
            if (drawinfo.shadow == 0f && drawinfo.drawPlayer.ownedLargeGems > 0)
            {
                bool flag = false;
                BitsByte ownedLargeGems = drawinfo.drawPlayer.ownedLargeGems;
                float num = 0f;
                for (int i = 0; i < 8; i++)
                {
                    if (ownedLargeGems[i])
                    {
                        num += 1f;
                    }
                }
                float num2 = 1f - num * 0.06f;
                float num3 = (num - 1f) * 4f;
                switch ((int)num)
                {
                    case 2:
                        num3 += 10f;
                        break;
                    case 3:
                        num3 += 8f;
                        break;
                    case 4:
                        num3 += 6f;
                        break;
                    case 5:
                        num3 += 6f;
                        break;
                    case 6:
                        num3 += 2f;
                        break;
                    case 7:
                        num3 += 0f;
                        break;
                    case 8:
                        num3 += 0f;
                        break;
                }
                float num4 = (float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f;
                if (num > 0f)
                {
                    float num5 = 6.2831855f / num;
                    float num6 = 0f;
                    Vector2 one = new Vector2(1.3f, 0.65f);
                    if (!flag)
                    {
                        one = Vector2.One;
                    }
                    List<DrawData> list = new List<DrawData>();
                    for (int j = 0; j < 8; j++)
                    {
                        if (!ownedLargeGems[j])
                        {
                            num6 += 1f;
                        }
                        else
                        {
                            Vector2 vector = (num4 + num5 * ((float)j - num6)).ToRotationVector2();
                            float num7 = num2;
                            if (flag)
                            {
                                num7 = MathHelper.Lerp(num2 * 0.7f, 1f, vector.Y / 2f + 0.5f);
                            }
                            Texture2D value;
                            if (j == 7)
                            {
                                 value = (Texture2D)ModContent.Request<Texture2D>("TheSludgeMod/Content/Items/RoseQuartz/GiantRoseQuartz", AssetRequestMode.ImmediateLoad);
                            } else
                            {
                                 value = TextureAssets.Gem[j].Value;
                            }
                            DrawData item = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - 80f))) + vector * one * num3, null, new Color(250, 250, 250, (int)(Main.mouseTextColor / 2)), 0f, value.Size() / 2f, ((float)Main.mouseTextColor / 1000f + 0.8f) * num7, SpriteEffects.None, 0f);
                            list.Add(item);
                        }
                    }
                    if (flag)
                    {
                        list.Sort(new Comparison<DrawData>(DelegateMethods.CompareDrawSorterByYScale));
                    }
                    drawinfo.DrawDataCache.AddRange(list);
                }
            }
        }

        private void On_Player_Update(On_Player.orig_Update orig, Player self, int i)
        {
            self.gem = -1;
            self.ownedLargeGems = 0;
            self.gemCount = 0;
            for (int num20 = 0; num20 <= 58; num20++)
            {
                if (self.inventory[num20].type == 0 || self.inventory[num20].stack == 0)
                {
                    self.inventory[num20].TurnToAir(false);
                }
                if (self.inventory[num20].type >= 1522 && self.inventory[num20].type <= 1527)
                {
                    self.gem = self.inventory[num20].type - 1522;
                    self.ownedLargeGems[self.gem] = true;
                }

                if (self.inventory[num20].type == ModContent.ItemType<GiantRoseQuartz>())
                {
                    self.gem = 7;
                    self.ownedLargeGems[self.gem] = true;
                }
                if (self.inventory[num20].type == 3643)
                {
                    self.gem = 6;
                    self.ownedLargeGems[self.gem] = true;
                }
            }

            orig.Invoke(self, i);
        }
    }
}

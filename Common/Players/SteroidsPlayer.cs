using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Common.Players
{
    public class SteroidsPlayer : ModPlayer
    {
        public bool hasSteroidsBuff;
        public bool hasSteroidsDebuff;
        private bool hadSteroidsBuff;
        public int steroidsCurseTimer;
        public override void ResetEffects()
        {
            hadSteroidsBuff = hasSteroidsBuff;
            hasSteroidsBuff = false;
            hasSteroidsDebuff = false;
        }
        public override void PostUpdateBuffs()
        {
            if (hadSteroidsBuff && !hasSteroidsBuff && steroidsCurseTimer <= 0)
            {
                steroidsCurseTimer = 60 * 60;
            }

            if (steroidsCurseTimer > 0)
            {
                steroidsCurseTimer--;
                Player.AddBuff(ModContent.BuffType<SteroidsDebuff>(), steroidsCurseTimer + 1);
            }
        }
        public override void PostUpdateEquips()
        {
            if (hasSteroidsBuff)
            {
                Player.GetDamage(DamageClass.Generic) += 0.50f;
            }

            if (steroidsCurseTimer > 0)
            {
                Player.GetDamage(DamageClass.Generic) -= 0.20f;
                Player.moveSpeed -= 0.10f;
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag["steroidsCurseTimer"] = steroidsCurseTimer;
        }
        public override void LoadData(TagCompound tag)
        {
            steroidsCurseTimer = tag.GetInt("steroidsCurseTimer");
        }
    }
}
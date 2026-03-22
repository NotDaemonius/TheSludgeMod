using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs;

public class OrangeSlime : ModNPC
{
    private const float MaxJumpVelocity = -2f;
    private const int JumpCooldownFrames = 15;
    private static readonly Color OrangeDustColor = new Color(255, 120, 0);
    private const int HellBorderTiles = 300;

    private ref float JumpTimer => ref NPC.localAI[0];

    public override void SetStaticDefaults() => Main.npcFrameCount[Type] = 4;

    public override void SetDefaults()
    {
        NPC.width = 44;
        NPC.height = 44;
        NPC.lifeMax = 600;
        NPC.damage = 35;
        NPC.defense = 15;
        NPC.knockBackResist = 0.5f;
        NPC.alpha = 50;
        NPC.friendly = false;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = Item.buyPrice(silver: 20);
        NPC.aiStyle = NPCAIStyleID.Slime;
        AnimationType = NPCID.RainbowSlime;
        NPC.noGravity = false;
    }

    public override void AI()
    {
        NPC.TargetClosest(faceTarget: true);
        base.AI();
        if (NPC.velocity.Y < MaxJumpVelocity)NPC.velocity.Y = MaxJumpVelocity;
        bool onGround = NPC.collideY && NPC.velocity.Y == 0f;

        if (onGround)
        {
            JumpTimer--;
            if (JumpTimer <= 0f)
            {
                NPC.velocity.Y = MaxJumpVelocity;
                JumpTimer = JumpCooldownFrames;
            }
        }
        else
        {
            JumpTimer = JumpCooldownFrames;
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.dedServ) return;
        int dustCount = Main.rand.Next(3, 6);
        for (int i = 0; i < dustCount; i++)
        {
            Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection * Main.rand.NextFloat(1f, 3f), Main.rand.NextFloat(-2f, -0.5f), newColor: OrangeDustColor);
            d.alpha = 150;
            d.scale = Main.rand.NextFloat(0.9f, 1.4f);
            d.noGravity = false;
        }
    }

    public override void OnKill()
    {
        if (Main.dedServ) return;
        int dustCount = Main.rand.Next(30, 50);
        for (int i = 0; i < dustCount; i++)
        {
            float angle = MathHelper.TwoPi * i / dustCount;
            float speed = Main.rand.NextFloat(2f, 6f);
            float velX = (float)Math.Cos(angle) * speed;
            float velY = (float)Math.Sin(angle) * speed;
            Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.TintableDust, velX, velY, newColor: OrangeDustColor);
            d.alpha = 150;
            d.scale = Main.rand.NextFloat(1.1f, 1.8f);
            d.noGravity = true;
        }
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns, new FlavorTextBestiaryInfoElement("Mods.TheSludgeMod.Bestiary.OrangeSlime")});

    public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 15, 25));

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        int tileY = (int)(spawnInfo.Player.position.Y / 16f);
        bool nearHell = tileY >= Main.UnderworldLayer - HellBorderTiles && tileY < Main.UnderworldLayer;
        bool notInHell = !spawnInfo.Player.ZoneUnderworldHeight;
        return (nearHell && notInHell) ? 0.05f : 0f;
    }
}
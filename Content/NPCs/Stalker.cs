using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs;

public class Stalker : ModNPC
{

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            Position = new Vector2(0, 5),
            Velocity = 5f,

        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
    }

    public override void SetDefaults()
    {
        NPC.width = 30;
        NPC.height = 68;

        NPC.value = Item.buyPrice(silver: 2);

        DrawOffsetY = -4f;

        NPC.damage = 25;
        NPC.defense = 10;
        NPC.lifeMax = 70;
        NPC.knockBackResist = 0.6f;


        NPC.friendly = false;
        NPC.noGravity = false;


        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath2;


    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> { BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption, new FlavorTextBestiaryInfoElement("Mods.TheSludgeMod.Bestiary.Stalker") });
    }

    public override void FindFrame(int frameHeight)
    {
        int startFrame = 0;
        int finalFrame = 3;

        if (NPC.velocity.Y == 0)
        {
            if (NPC.frame.Y / frameHeight > finalFrame)
            {
                NPC.frameCounter = 0f;
                NPC.frame.Y = startFrame * frameHeight;
            }


            NPC.frameCounter += MathF.Abs(NPC.velocity.X) / 10 + 0.1f;

            if (NPC.frameCounter >= 2)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        } else
        {
            startFrame = 4;
            finalFrame = 5;

            if (NPC.frame.Y / frameHeight < startFrame)
            {
                NPC.frameCounter = 0f;
                NPC.frame.Y = startFrame * frameHeight;
            }

            NPC.frameCounter += 0.5f;
            if (NPC.frameCounter >= 2)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
    }

    public override void AI()
    {
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest();
        }

        if (Main.rand.Next(700) == 0)
        {
            SoundEngine.PlaySound(SoundID.Zombie24, NPC.Center);
        }

        Player player = Main.player[NPC.target];

        NPC.GravityMultiplier *= 1.5f;
        int targetDirection = player.Center.X > NPC.Center.X ? 1 : -1;
        if (targetDirection != NPC.direction && NPC.localAI[0] <= 0)
        {
            NPC.direction = targetDirection;
            NPC.localAI[0] = 40;
        }

        NPC.spriteDirection = NPC.direction;
        NPC.velocity = new Vector2((NPC.velocity.X + NPC.direction * 0.3f) * 0.96f, NPC.velocity.Y);

        if (NPC.localAI[0] > 0)
            NPC.localAI[0]--;

        if (NPC.localAI[1] > 0)
            NPC.localAI[1]--;

        NPC.noTileCollide = player.Center.Y > NPC.Bottom.Y + 16f && NPC.velocity.Y == 0f && !NPC.collideX;

        bool tileCollideLeft = NPC.velocity.X < 0 && NPC.collideX;
        bool tileCollideRight = NPC.velocity.X > 0 && NPC.collideX;

        if ((tileCollideLeft || tileCollideRight) && NPC.velocity.Y == 0)
        {
            if (NPC.localAI[1] <= 0)
            {
                NPC.velocity.Y = -6f;
                NPC.localAI[1] = 100;
            } else
            {
                NPC.velocity.Y = -13f;
                NPC.localAI[1] = 0;
            }
        } else if (!Main.tile[(int) (NPC.Bottom.X + NPC.direction * 16) / 16, (int) (NPC.Bottom.Y + 2) / 16].HasTile && !Main.tile[(int) (NPC.Bottom.X + NPC.direction * 32) / 16, (int) (NPC.Bottom.Y + 18) / 16].HasTile && NPC.velocity.Y == 0 && NPC.collideY && !NPC.collideX && !(player.Center.Y > NPC.Center.Y && MathF.Abs(player.Center.Y - NPC.Center.Y) > 300))
        {
            NPC.velocity.Y = -13f;
            Main.NewText(MathF.Abs(player.Center.Y - NPC.Center.Y));
        }

        Collision.StepUp(
            ref NPC.position,
            ref NPC.velocity,
            NPC.width,
            NPC.height,
            ref NPC.stepSpeed,
            ref NPC.gfxOffY
        );
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.Burger, 100));
        npcLoot.Add(ItemDropRule.OneFromOptions(525, ItemID.AncientArmorHat, ItemID.AncientArmorPants, ItemID.AncientBattleArmorHat));
        npcLoot.Add(ItemDropRule.Common(ItemID.RottenChunk, 2));
        npcLoot.Add(ItemDropRule.Common(ItemID.TentacleSpike, 525));
        npcLoot.Add(ItemDropRule.Common(ItemID.PigPetItem, 1500));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        bool boob = spawnInfo.SpawnTileY - 8 * 16 < spawnInfo.Player.Center.Y;
        return spawnInfo.Player.ZoneCorrupt && boob ? 0.5f : 0f;
    }

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        modifiers.Knockback *= 2f;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.dedServ)
            return;

        int dustCount = Main.rand.Next(5, 9);
        for (int i = 0; i < dustCount; i++)
        {
            Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, hit.HitDirection * Main.rand.NextFloat(1f, 3f), Main.rand.NextFloat(-2f, -0.5f));
            d.alpha = 125;
            d.scale = Main.rand.NextFloat(0.8f, 1.2f);
            d.noGravity = false;
        }

        if (NPC.life <= 0)
        {
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}Gore_1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}Gore_2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}Gore_3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}Gore_3").Type, NPC.scale);
        }
    }
}

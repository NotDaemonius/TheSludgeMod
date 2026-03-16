using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Consumables;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Items.Placeable;
using TheSludgeMod.Content.Items.Vanity;
using TheSludgeMod.Content.Items.Weapons;
using TheSludgeMod.Content.Pets;

namespace TheSludgeMod.Content.NPCs.TheMainframe
{
    public enum BossState
    {
        Homing,
        Circle,
        SwitchToSecondPhase,
        Spiral,
        NA,
    }

    [AutoloadBossHead]
    public class TheMainframe : ModNPC
    {
        // Boss stage values 
        private float bossTeleportSpeed
        {
            get => SwitchedToSecondPhase ? 32 : 20;
        }
        private float minBossSpeed
        {
            get => SwitchedToSecondPhase ? 14 : 10;
        }
        private float directionLerpSpeed
        {
            get => SwitchedToSecondPhase ? 0.025f : 0.05f;
        }
        private float rotationSinIntenisty
        {
            get => SwitchedToSecondPhase ? 100f : 150f;
        }
        private float rotationSinSpeed
        {
            get => SwitchedToSecondPhase ? 10f : 15f;
        }
        private float laserShootAmount
        {
            get => SwitchedToSecondPhase ? 40f : 50f;
        }
        // Circling and spiral values
        private float circlingRadius
        {
            get => SwitchedToSecondPhase ? 600f : 350f;
        }
        private float circlingSpeed
        {
            get => SwitchedToSecondPhase ? 0.3f : 0.25f;
        }
        private float circlingDecel
        {
            get => SwitchedToSecondPhase ? 0.998f : 0.995f;
        }
        private float circlingLaserFireSpeed
        {
            get => SwitchedToSecondPhase ? 12f : 20f;
        }

        private int laserDamage = 20;

        public BossState CurrentStage
        {
            get => (BossState) NPC.ai[0];
            set => NPC.ai[0] = (int) value;
        }

        public ref float Timer => ref NPC.ai[1];

        public Vector2 TargetPos
        {
            get => new Vector2(NPC.ai[2], NPC.ai[3]);
            set
            {
                NPC.ai[2] = value.X;
                NPC.ai[3] = value.Y;
            }
        }

        public int rotDir
        {
            get => (int) NPC.localAI[0];
            set => NPC.localAI[0] = (int) value;
        }

        public bool SwitchedToSecondPhase
        {
            get => NPC.localAI[1] == 1f;
            set => NPC.localAI[1] = value ? 1f : 0f;
        }

        public bool DoSpiralNext
        {
            get => NPC.localAI[2] == 1f;
            set => NPC.localAI[2] = value ? 1f : 0f;
        }

        public float CurrentBossSpeed;
        public float CirclingStartOffset;
        public float StartRadius;
        public Vector2 BossMoveDirection;
        public int HomingStageRanCount;
        public Vector2 MoveAwayVelocity;
        public float TeleportingTimer;
        public float CurrentBossRadius;
        private bool doDeath;

        private float _musicVolume = 1f;
        private SlotId _transitionLoopSlot;
        private bool _transitionLoopPlaying = false;

        private TheMainframeHand[] hands = new TheMainframeHand[2];

        private bool inSecondPhase
        {
            get => NPC.life < NPC.lifeMax * 0.5f;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                PortraitScale = 0.5f,
                PortraitPositionYOverride = -50f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 242;
            NPC.height = 182;

            NPC.damage = 60;
            NPC.knockBackResist = 0f;


            NPC.defense = 25;
            NPC.lifeMax = 35000;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.value = Item.buyPrice(gold: 18);
            NPC.rarity = 3;

            NPC.SpawnWithHigherTime(30);

            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheMainframeTheme");
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MainframeTrophy>(), 10));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MainframeMaskHead>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulofSpite>(), 1, 25, 40));
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<ThePainframe>(), ModContent.ItemType<GemshardObliterator>(), ModContent.ItemType<Laserstorm>(), ModContent.ItemType<NeuralShardStaff>()));
            npcLoot.Add(notExpertRule);
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<MainframeBossBag>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<MainframeRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<NeuralCrystal>(), 4));
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 3;

            if (SwitchedToSecondPhase)
            {
                startFrame = 4;
                finalFrame = Main.npcFrameCount[Type] - 1;

                if (NPC.frame.Y < startFrame * frameHeight)
                {
                    NPC.frameCounter = 0f;
                    NPC.frame.Y = startFrame * frameHeight;
                }
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

        public override void OnSpawn(IEntitySource source)
        {
            SpawnHands();
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (CurrentStage == BossState.Homing)
            {
                Player player = Main.player[NPC.target];
                if (player.dead)
                {
                    return;
                }

                Vector2 directionToPlayer = (player.Center - NPC.Center);
                directionToPlayer.Normalize();

                MoveAwayVelocity = directionToPlayer * -20f;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int goreType1 = Mod.Find<ModGore>("TheMainframeBody_1").Type;
                int goreType2 = Mod.Find<ModGore>("TheMainframeBody_2").Type;
                int goreType3 = Mod.Find<ModGore>("TheMainframeBody_3").Type;
                int goreType4 = Mod.Find<ModGore>("TheMainframeWing").Type;
                int goreType5 = Mod.Find<ModGore>("TheMainframeArm").Type;
                var entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), goreType1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), goreType2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), goreType3);

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), goreType4);
                }

                for (int i = 0; i < hands.Length; i++)
                {
                    if (hands[i] == null || !hands[i].NPC.active)
                    {
                        continue;
                    }

                    Gore.NewGore(entitySource, hands[i].NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), goreType5);
                }

                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                // Screen shake
                PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float) Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
                Main.instance.CameraModifiers.Add(modifier);
            }
        }
        public override void AI()
        {
            bool needsRecovery = false;
            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i] == null || !hands[i].NPC.active)
                {
                    needsRecovery = true;
                    break;
                }
            }
            if (needsRecovery)
                TryRecoverHands();


            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.dead || doDeath)
            {
                if (!doDeath)
                {
                    TeleportingTimer = 0;
                    if (NPC.velocity == Vector2.Zero)
                    {
                        NPC.velocity = Main.rand.NextVector2Unit() * Main.rand.Next(10, 20);
                    }
                    doDeath = true;
                }
                TeleportingTimer += 1;
                NPC.velocity *= 0.95f;
                if (TeleportingTimer >= 60)
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float) Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
                    Main.instance.CameraModifiers.Add(modifier);

                    for (int i = 0; i < hands.Length; i++)
                    {
                        if (hands[i] == null || !hands[i].NPC.active)
                        {
                            continue;
                        }

                        hands[i].NPC.active = false;
                    }
                    NPC.active = false;
                }
                return;
            }

            if (Main.dayTime)
            {
                NPC.damage = 1000;
                laserDamage = 1000;
                NPC.defense = 1000;
            }

            if (!Main.dedServ)
            {
                float targetVolume;

                if (CurrentStage == BossState.SwitchToSecondPhase)
                {
                    if (Timer < 100f)
                    {
                        targetVolume = MathHelper.Lerp(1f, 0f, Timer / 100f);
                    } else if (Timer >= 300f)
                    {
                        if (_transitionLoopPlaying)
                        {
                            SoundEngine.TryGetActiveSound(_transitionLoopSlot, out ActiveSound activeSound);
                            activeSound?.Stop();
                            _transitionLoopPlaying = false;
                        }

                        targetVolume = MathHelper.Lerp(0f, 1f, (Timer - 300f) / 100f);
                    } else
                    {
                        if (!_transitionLoopPlaying)
                        {
                            SoundStyle loopStyle = new SoundStyle("TheSludgeMod/Assets/Sounds/MainframeTransitionLoop")
                            {
                                IsLooped = true,
                                Volume = 1f
                            };
                            _transitionLoopSlot = SoundEngine.PlaySound(loopStyle, NPC.Center);
                            _transitionLoopPlaying = true;
                        } else
                        {
                            if (SoundEngine.TryGetActiveSound(_transitionLoopSlot, out ActiveSound activeSound))
                                activeSound.Position = NPC.Center;
                        }

                        targetVolume = 0f;
                    }
                } else
                {
                    if (_transitionLoopPlaying)
                    {
                        SoundEngine.TryGetActiveSound(_transitionLoopSlot, out ActiveSound activeSound);
                        activeSound?.Stop();
                        _transitionLoopPlaying = false;
                    }

                    targetVolume = 1f;
                }

                _musicVolume = MathHelper.Lerp(_musicVolume, targetVolume, 0.15f);
                Main.musicFade[Music] = _musicVolume;
            }

            switch (CurrentStage)
            {
                case BossState.Homing:
                    if (Timer == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            //Runs on boss spawn only
                            if (TargetPos == Vector2.Zero)
                            {
                                TargetPos = player.Center + Main.rand.NextVector2Unit() * Main.rand.Next(30, 500);
                                NPC.netUpdate = true;
                            }
                        }

                        CurrentBossSpeed = bossTeleportSpeed;

                        MoveAwayVelocity = Vector2.Zero;

                        NPC.Center = TargetPos;
                        NPC.velocity = Vector2.Zero;

                        BossMoveDirection = (player.Center - NPC.Center).SafeNormalize(Vector2.One);

                        TargetPos = Vector2.Zero;

                        HomingStageRanCount += 1;
                    }

                    CurrentBossSpeed = MathF.Max(minBossSpeed, CurrentBossSpeed * 0.99f);

                    Vector2 directionToPlayer = (player.Center - NPC.Center);
                    float distanceToPlayer = directionToPlayer.Length();
                    directionToPlayer.Normalize();

                    Vector2 moveDir = Vector2.Lerp(BossMoveDirection, directionToPlayer, directionLerpSpeed);
                    BossMoveDirection = moveDir;
                    moveDir.Normalize();

                    NPC.velocity = moveDir * CurrentBossSpeed * Math.Max(1f, distanceToPlayer / 500 + 0.5f) + MoveAwayVelocity;
                    if (SwitchedToSecondPhase)
                    {
                        NPC.velocity += new Vector2(0, MathF.Sin(Timer * 0.1f) * 10);
                    }

                    float dirChange = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed;
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;

                    MoveAwayVelocity *= 0.97f;
                    Timer += 1;

                    // Second phase switch
                    if (Timer >= 20 && inSecondPhase && !SwitchedToSecondPhase)
                    {
                        ChangeState(BossState.SwitchToSecondPhase);
                        return;
                    }

                    // Teleportation
                    if (Timer >= 260 && HomingStageRanCount < 2)
                    {
                        Timer = 0;
                        TeleportingTimer = 0;
                        return;
                    }

                    if (Timer >= 200)
                    {
                        if (HomingStageRanCount < 2)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (TargetPos == Vector2.Zero)
                                {
                                    TargetPos = player.Center + Main.rand.NextVector2Unit() * Main.rand.Next(30, 500);
                                    NPC.netUpdate = true;
                                }
                            }

                            TeleportingTimer += 1;
                            for (int i = 0; i < 5; i++)
                            {
                                Vector2 dustSpawn = Utils.RandomVector2(Main.rand, -50f, 50f);
                                Dust d = Dust.NewDustPerfect(TargetPos + dustSpawn, DustID.GemRuby, null, Scale: 1.5f);
                                d.noGravity = true;
                            }

                            return;
                        }

                        if (DoSpiralNext)
                        {
                            ChangeState(BossState.Spiral);
                        } else
                        {
                            ChangeState(BossState.Circle);
                        }

                        return;
                    }

                    if (Main.rand.NextBool(100))
                    {
                        Vector2 dustSpawn = new Vector2(Main.rand.Next(-121, 121), Main.rand.Next(-91, 91));
                        Vector2 dustVelocity = Main.rand.NextVector2Unit() * Main.rand.Next(2, 10);
                        Dust.NewDustPerfect(NPC.Center + dustSpawn, DustID.FireworkFountain_Yellow, dustVelocity);
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(200) && SwitchedToSecondPhase)
                    {
                        int dir = Main.rand.NextBool() ? -1 : 1;
                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == null || !hands[i].NPC.active)
                                continue;
                            if (hands[i].CurrentStage == ArmState.Spinny)
                                break;
                            hands[i].DoArmRotation(30, 150, 6, 5f, dir);
                        }
                        NPC.netUpdate = true;
                        return;
                    }

                    if (Timer % laserShootAmount == 0)
                    {
                        ShootFromHands(player, 15f, 0);
                    } else if ((Timer + laserShootAmount / 2) % laserShootAmount == 0 && (Timer + laserShootAmount / 2) < 200)
                    {
                        ShootFromHands(player, 15f, (int) (laserShootAmount / 2));
                    }

                    break;
                case BossState.Circle:
                    if (Timer == 0)
                    {
                        NPC.velocity = Vector2.Zero;

                        CurrentBossSpeed = circlingSpeed;
                        rotDir = (player.Center.X > NPC.Center.X) ? 1 : -1;
                        CirclingStartOffset = (NPC.Center - player.Center).ToRotation();
                        StartRadius = Vector2.Distance(NPC.Center, player.Center);
                    }

                    float spinRadius = MathHelper.Lerp(StartRadius, circlingRadius, Math.Min(1f, Timer / 40));

                    TargetPos = player.Center + new Vector2(MathF.Cos((Timer * CurrentBossSpeed * rotDir) + CirclingStartOffset) * spinRadius, MathF.Sin((Timer * CurrentBossSpeed * rotDir) + CirclingStartOffset) * spinRadius);
                    NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.2f);

                    // Rotation
                    float dirChange2 = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange2 - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed;
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;

                    Timer += 1;
                    CurrentBossSpeed *= circlingDecel;

                    if (Timer % circlingLaserFireSpeed == 0 && Timer >= 24)
                    {
                        ShootFromHands(player, 10f);
                    } else if ((Timer + circlingLaserFireSpeed / 2) % circlingLaserFireSpeed == 0)
                    {
                        ShootFromHands(player, 10f, (int) (circlingLaserFireSpeed / 2));
                    }

                    if (Timer >= 120)
                    {
                        if (SwitchedToSecondPhase)
                        {
                            DoSpiralNext = true;
                        }
                            
                        ChangeState(BossState.Homing);
                        TargetPos = NPC.Center;
                    }

                    break;
                case BossState.Spiral:
                    if (Timer == 0)
                    {
                        NPC.velocity = Vector2.Zero;

                        CurrentBossRadius = 1400;

                        CurrentBossSpeed = circlingSpeed;
                        rotDir = (player.Center.X > NPC.Center.X) ? 1 : -1;
                        CirclingStartOffset = (NPC.Center - player.Center).ToRotation();
                        StartRadius = Vector2.Distance(NPC.Center, player.Center);
                    }

                    float spinRadius2 = MathHelper.Lerp(StartRadius, CurrentBossRadius, Math.Min(1f, Timer / 40));
                    CurrentBossRadius = Math.Max(CurrentBossRadius * 0.995f, 200);

                    TargetPos = player.Center + new Vector2(MathF.Cos((Timer * CurrentBossSpeed * rotDir) + CirclingStartOffset) * spinRadius2, MathF.Sin((Timer * CurrentBossSpeed * rotDir) + CirclingStartOffset) * spinRadius2);
                    NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.2f);

                    //  ROTATION
                    float dirChange3 = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange3 - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed;
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;

                    Timer += 1;
                    CurrentBossSpeed *= 0.9997f;

                    if (Timer % circlingLaserFireSpeed == 0 && Timer >= 24)
                    {
                        ShootFromHands(player, 10f);
                    } else if ((Timer + circlingLaserFireSpeed / 2) % circlingLaserFireSpeed == 0)
                    {
                        ShootFromHands(player, 10f, (int) (circlingLaserFireSpeed / 2));
                    }

                    int jitler = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Main.rand.NextVector2Unit() * 2, ModContent.ProjectileType<MainframeGhostSpooky>(), 0, 0);
                    Main.projectile[jitler].rotation = NPC.rotation;

                    if (Timer >= 200)
                    {
                        if (SwitchedToSecondPhase)
                        {
                            DoSpiralNext = false;
                        }

                        ChangeState(BossState.Homing);
                        TargetPos = NPC.Center;
                    }

                    break;
                case BossState.SwitchToSecondPhase:
                    if (Timer == 0)
                    {
                        NPC.GetImmuneTime(0, 400);
                    }

                    NPC.velocity *= 0.98f;
                    Timer += 1;

                    NPC.rotation = MathHelper.Lerp(NPC.rotation, 0, 0.01f);

                    if (Timer >= 400)
                    {
                        ChangeState(BossState.Homing);
                        TargetPos = NPC.Center;
                        return;
                    }

                    if (Timer >= 300)
                    {
                        if (Timer >= 300 && NPC.localAI[3] == 0f)
                        {
                            TargetPos = TargetPos - new Vector2(0, 200);
                            NPC.localAI[3] = 1f;
                        }

                        NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.05f);
                        SwitchedToSecondPhase = true;

                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == null || !hands[i].NPC.active)
                            {
                                continue;
                            }

                            hands[i].InSecondStage = true;
                        }
                        return;
                    }

                    if (Timer == 200)
                    {
                        for (int i = 0; i < hands.Length; i++)
                        {
                            if (hands[i] == null || !hands[i].NPC.active)
                            {
                                continue;
                            }

                            hands[i].DoArmRotation(100, 150, 6, 0.4f, 1);
                        }
                    }

                    if (Timer > 50)
                    {
                        if (Main.rand.NextBool((int) MathF.Max(1, 150 - Timer)))
                        {
                            NPC.Center = TargetPos + Utils.RandomVector2(Main.rand, -20f, 20f) * (Timer / 100);

                            if (Main.rand.NextBool(3))
                            {
                                Vector2 dustSpawn = new Vector2(Main.rand.Next(-121, 121), Main.rand.Next(-91, 91));
                                Vector2 dustVelocity = Main.rand.NextVector2Unit() * Main.rand.Next(2, 10);
                                Dust.NewDustPerfect(NPC.Center + dustSpawn, DustID.FireworkFountain_Yellow, dustVelocity);
                            }

                        } else
                        {
                            NPC.Center = TargetPos;
                        }
                        return;
                    }

                    if (Timer == 50)
                    {
                        TargetPos = NPC.Center;
                        return;
                    }

                    break;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            if (TeleportingTimer <= 0f)
                return;
            SpriteEffects spriteEffects = (SpriteEffects) 0;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = (SpriteEffects) 1;
            }
            float height = Main.NPCAddHeight(NPC);
            Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
            spriteBatch.Draw(glow, NPC.Bottom - screenPos + new Vector2((float) (-TextureAssets.Npc[Type].Width()) * NPC.scale / 2f + halfSize.X * NPC.scale, (float) (-TextureAssets.Npc[Type].Height()) * NPC.scale / (float) Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + height + NPC.gfxOffY), (Rectangle?) NPC.frame, new Color(200, 200, 200, 100), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);


            Texture2D glow2 = ModContent.Request<Texture2D>(
                "TheSludgeMod/Content/NPCs/TheMainframe/Gloww",
                AssetRequestMode.ImmediateLoad).Value;

            float progress = TeleportingTimer / 60f;
            float scale = MathHelper.Lerp(0.3f, 2.5f, progress);
            float alpha = MathHelper.Lerp(0.1f, 0.9f, progress);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive,
                Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            spriteBatch.Draw(
                glow2,
                NPC.Center - screenPos - new Vector2(0, 30f),
                null,
                Color.Red * alpha,
                0f,
                glow2.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);



        }
        public void SpawnHands()
        {
            int amountOfArms = Main.expertMode ? 4 : 2;
            hands = new TheMainframeHand[amountOfArms];

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            for (int i = 0; i < amountOfArms; i++)
            {
                TheMainframeHand handyMandy = (TheMainframeHand) NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, ModContent.NPCType<TheMainframeHand>(), NPC.whoAmI).ModNPC;
                handyMandy.Parent = NPC;
                handyMandy.Side = ((MathF.PI * 2f / amountOfArms) * i) + MathF.PI / 4;
                handyMandy.ParentWhoAmI = NPC.whoAmI;
                hands[i] = handyMandy;

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: handyMandy.NPC.whoAmI);
                }
            }
        }
        private void TryRecoverHands()
        {
            int found = 0;
            for (int i = 0; i < Main.maxNPCs && found < hands.Length; i++)
            {
                if (Main.npc[i].active
                    && Main.npc[i].ModNPC is TheMainframeHand hand
                    && (int) hand.ParentWhoAmI == NPC.whoAmI)
                {
                    hands[found] = hand;
                    found++;
                }
            }
        }
        private void ShootFromHands(Player player, float speed = 10, int windupTime = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            if (windupTime > 0)
            {
                for (int i = 0; i < hands.Length; i++)
                {
                    if (hands[i] == null || !hands[i].NPC.active)
                    {
                        continue;
                    }

                    hands[i].WindupTimer = 0;
                    hands[i].WindupDuration = windupTime;
                    hands[i].NPC.netUpdate = true;
                }
                return;
            }

            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i] == null || !hands[i].NPC.active)
                {
                    continue;
                }

                hands[i].WindupTimer = 0;
                hands[i].WindupDuration = 1;
                hands[i].pushback = 50;
                hands[i].NPC.netUpdate = true;


                Projectile.NewProjectile(
                    NPC.GetSource_FromAI(),
                    hands[i].NPC.Center,
                    (player.oldPosition - hands[i].NPC.Center).SafeNormalize(Vector2.One) * speed,
                    ModContent.ProjectileType<TheMainframeLaser>(), laserDamage, 2
                );
            }
        }
        public void ChangeState(BossState newState)
        {
            TargetPos = Vector2.Zero;
            CurrentStage = newState;
            Timer = 0;
            TeleportingTimer = 0;
            HomingStageRanCount = 0;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.TheSludgeMod.Bestiary.TheMainframe")
            });
        }
    }
}

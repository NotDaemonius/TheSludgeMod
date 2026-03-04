using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.RoseQuartz;

namespace TheSludgeMod.Content.NPCs.TheMainframe
{
    // Add boss map icon
    public class TheMainframe : ModNPC
    {
        //public override LocalizedText DeathMessage => Language.GetText("Announcement.HasBeenDefeated_Plural").WithFormatArgs(this.GetLocalization("BossFightName")); Add Death message

        public enum BossState
        {
            Hoaming,
            Circle,
            SwitchToSecondPhase,
            CircleThing2,
            NA,
        }

        public BossState CurrentStage
        {
            get => (BossState)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
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
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = (int)value;
        }

        public float BossSpeed;

        public float SinOffset;

        public float StartRadius { get; private set; }

        public Vector2 BossMoveDirection;

        public int TeleCount;

        public Vector2 MoveAwayVelocity;

        private TheMainframeHand[] hands = new TheMainframeHand[2];

        private bool inSecondPhase { get => NPC.life < NPC.lifeMax * 0.99f; }

        public bool switchedToSecondPhase;

        private float currentBossRadius;

        // boss vars
        private float bossTeleportSpeed { get => switchedToSecondPhase ? 32 : 20; }
        private float minBossSpeed { get => switchedToSecondPhase ? 14f : 10; }
        private float directionLerpSpeed { get => switchedToSecondPhase ? 0.025f : 0.05f; }
        private float rotationSinIntenisty { get => switchedToSecondPhase ? 100f : 150f; }
        private float rotationSinSpeed { get => switchedToSecondPhase ? 10f : 15f; }

        // circlin
        private float circlingRadius { get => switchedToSecondPhase ? 600f : 350f; }
        private float circlingSpeed { get => switchedToSecondPhase ? 0.3f : 0.25f; }
        private float circlingDecel { get => switchedToSecondPhase ? 0.998f : 0.995f; }
        private float circlingLaserFireSpeed { get => switchedToSecondPhase ? 8f : 12f; }

        private bool doCircle2;
            
        public override void SetStaticDefaults()
        {
            // Add beastiary shit
            Main.npcFrameCount[Type] = 2;
        }
        public override void SetDefaults()
        {
            NPC.width = 242;
            NPC.height = 186;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 65000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);

            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheMainframeTheme");
            }

            // Add boss bar
        }

        public override void FindFrame(int frameHeight)
        {
            if (switchedToSecondPhase)
            {
                NPC.frame.Y = 1 * frameHeight;
            }  else
            {
                NPC.frame.Y = 0;
            }
        }

        /*
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

        }
        */


        /*
        public override void OnKill()
        {
            if (!DownedBossSystem.downedMinionBoss)
            {
                ModContent.GetInstance<ExampleOreSystem>().BlessWorldWithExampleOre();
            }

        	NPC.SetEventFlagCleared(ref DownedBossSystem.downedMinionBoss, -1);
        }
        */

        

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (CurrentStage == BossState.Hoaming)
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

        public override void BossLoot(ref int potionType)
        {
            // Here you'd want to change the potion type that drops when the boss is defeated. Because this boss is early pre-hardmode, we keep it unchanged
            // (Lesser Healing Potion). If you wanted to change it, simply write "potionType = ItemID.HealingPotion;" or any other potion type
        }

        // boss imunity frame shit
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SpawnHands();
        }

        private void TryRecoverHands()
        {
            int found = 0;
            for (int i = 0; i < Main.maxNPCs && found < 2; i++)
            {
                if (Main.npc[i].active
                    && Main.npc[i].ModNPC is TheMainframeHand hand
                    && (int)hand.ParentWhoAmI == NPC.whoAmI)
                {
                    hands[found] = hand;
                    found++;
                }
            }
        }

        // this is gonna be hitler
        public override void AI()
        {
            if (hands[0] == null || !hands[0].NPC.active || hands[1] == null || !hands[1].NPC.active)
            {
                TryRecoverHands();
            }


            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.dead) // fix this despawn shit
            {
                NPC.velocity.Y -= 0.04f;
                NPC.EncourageDespawn(10);
                return;
            }

            switch (CurrentStage)
            {
                case BossState.Hoaming:
                    if (Timer == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (TargetPos == Vector2.Zero)
                            {
                                Vector2 offsetDir = Main.rand.NextVector2Unit();
                                TargetPos = player.Center + offsetDir * Main.rand.Next(30, 500);
                                NPC.netUpdate = true; // force sync the new TargetPos to clients
                            }
                        }

                        BossSpeed = bossTeleportSpeed;  // v 1
                        MoveAwayVelocity = Vector2.Zero;

                        NPC.Center = TargetPos;
                        NPC.velocity = Vector2.Zero;

                        BossMoveDirection = (player.Center - NPC.Center);
                        BossMoveDirection.Normalize();

                        TargetPos = Vector2.Zero;

                        TeleCount += 1;
                    }

                    BossSpeed = MathF.Max(minBossSpeed, BossSpeed * 0.99f); // v2

                    // Move towards player
                    Vector2 directionToPlayer = (player.Center - NPC.Center);
                    float len = directionToPlayer.Length();
                    directionToPlayer.Normalize();

                    Vector2 moveDir = Vector2.Lerp(BossMoveDirection, directionToPlayer, directionLerpSpeed); // v3
                    BossMoveDirection = moveDir;
                    moveDir.Normalize(); // idk if this will do shit but might as well

                    NPC.velocity = moveDir * BossSpeed * Math.Max(1f, len / 500 + 0.5f) + MoveAwayVelocity;
                    MoveAwayVelocity *= 0.97f;

                    // Rotation shit
                    float dirChange = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed; // EASING OLLy
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;
                    Timer += 1;

                    // add second phase wiggle

                    if (Timer >= 200)
                    {
                        if (TeleCount < 2)
                        {
                            if (Timer >= 260)
                            {
                                Timer = 0; // Teleport
                            }
                            else
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    if (TargetPos == Vector2.Zero)
                                    {
                                        Vector2 offsetDir = Main.rand.NextVector2Unit();
                                        TargetPos = player.Center + offsetDir * Main.rand.Next(30, 500);
                                        NPC.netUpdate = true; // force sync the new TargetPos to clients
                                    }
                                }

                                for (int i = 0; i < 3; i++)
                                {
                                    Vector2 dustSpawn = Utils.RandomVector2(Main.rand, -20f, 20f);
                                    Dust d = Dust.NewDustPerfect(TargetPos + dustSpawn, DustID.GemRuby, null, Scale: 1.5f);
                                    d.noGravity = true;
                                }
                            }
                        } else
                        {
                            if (doCircle2)
                            {
                                ChangeState(BossState.CircleThing2);
                            } else
                            {
                                ChangeState(BossState.Circle);
                            }
                        }
                    } else {
                        if (Timer >= 20)
                        {
                            if (inSecondPhase && !switchedToSecondPhase)
                            {
                                ChangeState(BossState.SwitchToSecondPhase);
                                break;
                            }
                        }

                        if (Timer % 40 == 0)
                        {
                            ShootFromHands(player, 15f, 0);
                        } else if ((Timer + 20) % 40 == 0)
                        {
                            Main.NewText("agha");
                            ShootFromHands(player, 15f, 10);
                        }
                    }

                    break;
                case BossState.Circle:
                    if (Timer == 0)
                    {
                        BossSpeed = circlingSpeed;
                        NPC.velocity = Vector2.Zero;
                        rotDir = (player.Center.X > NPC.Center.X) ? 1 : -1;
                        SinOffset = (NPC.Center - player.Center).ToRotation();
                        StartRadius = Vector2.Distance(NPC.Center, player.Center);
                    }

                    float spinRadius = MathHelper.Lerp(StartRadius, circlingRadius, Math.Min(1f, Timer / 40));

                    TargetPos = player.Center + new Vector2(MathF.Cos((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius, MathF.Sin((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius);
                    BossSpeed *= circlingDecel;

                    NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.2f);

                    //  ROTATION
                    float dirChange2 = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange2 - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed; // EASING OLLy
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;

                    Timer += 1;

                    if (Timer % circlingLaserFireSpeed == 0 && Timer >= 24)
                    {
                        ShootFromHands(player, 10f);
                        //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.One) * 10, ProjectileID.EyeLaser, 20, 2);
                    }

                    if (Timer >= 120)
                    {
                        if (switchedToSecondPhase)
                        {
                            doCircle2 = true;
                        }

                        ChangeState(BossState.Hoaming);
                        TargetPos = NPC.Center;
                    }

                    break;
                case BossState.CircleThing2:
                    if (Timer == 0)
                    {
                        BossSpeed = circlingSpeed;
                        NPC.velocity = Vector2.Zero;
                        rotDir = (player.Center.X > NPC.Center.X) ? 1 : -1;
                        SinOffset = (NPC.Center - player.Center).ToRotation();
                        StartRadius = Vector2.Distance(NPC.Center, player.Center);
                        currentBossRadius = 1400;
                    }

                    float spinRadius2 = MathHelper.Lerp(StartRadius, currentBossRadius, Math.Min(1f, Timer / 40));

                    currentBossRadius = Math.Max(currentBossRadius * 0.995f, 200);

                    TargetPos = player.Center + new Vector2(MathF.Cos((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius2, MathF.Sin((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius2);
                    BossSpeed *= 0.9997f;

                    NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.2f);

                    //  ROTATION
                    float dirChange3 = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange3 - (NPC.rotation - MathF.Sin((Timer - 1) / rotationSinSpeed) / rotationSinIntenisty)) / rotationSinSpeed; // EASING OLLy
                    NPC.rotation += MathF.Sin(Timer / rotationSinSpeed) / rotationSinIntenisty;

                    Timer += 1;

                    if (Timer % circlingLaserFireSpeed == 0 && Timer >= 24)
                    {
                        ShootFromHands(player, 10f);
                        //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.One) * 10, ProjectileID.EyeLaser, 20, 2);
                    }

                    if (Timer >= 200)
                    {
                        if (switchedToSecondPhase)
                        {
                            doCircle2 = false;
                        }

                        ChangeState(BossState.Hoaming);
                        TargetPos = NPC.Center;
                    }

                    break;
                case BossState.SwitchToSecondPhase:
                    NPC.velocity *= 0.96f;

                    if (Timer == 0)
                    {
                        TargetPos = NPC.Center;
                    }

                    Timer += 1;

                    if (Timer >= 150) // add effects.
                    {
                        ChangeState(BossState.Hoaming);
                        TargetPos = NPC.Center;
                        break;
                    } else if (Timer >= 100)
                    {
                        NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.05f);
                        switchedToSecondPhase = true;
                    } else
                    {
                        NPC.Center = TargetPos + new Vector2(MathF.Sin(Timer * 0.005f * Timer) * Timer, MathF.Cos(Timer * 0.007f * Timer) * Timer);
                    }
                    break;
            }
        }

        public override void DrawBehind(int index)
        {
        }

        private void ShootFromHands(Player player, float speed = 10, int windupTime=0)
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

                    Main.NewText("wa");
                    hands[i].WindupTimer = 0;
                    hands[i].WindupDuration = 20;
                }
                return;
            }

            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i] == null || !hands[i].NPC.active) { 
                    continue; 
                }
                Main.NewText("pam");
                hands[i].WindupTimer = 0;
                hands[i].WindupDuration = 1;
                hands[i].pushback = 50;

                Projectile.NewProjectile(
                    NPC.GetSource_FromAI(),
                    hands[i].NPC.Center,
                    (player.Center - player.oldVelocity * 2 - hands[i].NPC.Center).SafeNormalize(Vector2.One) * speed,
                    ProjectileID.EyeLaser, 20, 2
                );
            }
        }

        public void ChangeState(BossState newState)
        {
            TargetPos = Vector2.Zero;
            CurrentStage = newState;
            Timer = 0;
            TeleCount = 0;
        }

        public void SpawnHands()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                TheMainframeHand handyMandy = (TheMainframeHand)NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, ModContent.NPCType<TheMainframeHand>(), NPC.whoAmI).ModNPC;
                handyMandy.Parent = NPC;
                handyMandy.Side = i * 2 - 1;
                handyMandy.ParentWhoAmI = NPC.whoAmI;

                hands[i] = handyMandy;

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: handyMandy.NPC.whoAmI);
                }
            }
        }
    }
}

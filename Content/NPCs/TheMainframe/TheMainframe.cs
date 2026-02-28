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
            NA
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

        public float BossSpeed;

        public float SinOffset;

        public float StartRadius { get; private set; }

        public Vector2 BossMoveDirection;

        public int TeleCount;

        public Vector2 MoveAwayVelocity;

        public int rotDir;

        public override void SetStaticDefaults()
        {
            // Add beastiary shit
        }
        public override void SetDefaults()
        {
            NPC.width = 242;
            NPC.height = 186;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 65000;
            NPC.HitSound = SoundID.NPCHit1;
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

        // this is gonna be hitler
        public override void AI()
        {
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
                        if (TargetPos == Vector2.Zero)
                        {
                            Vector2 offsetDir = Main.rand.NextVector2Unit();
                            TargetPos = player.Center + offsetDir * Main.rand.Next(30, 500);


                        }

                        BossSpeed = 20f;
                        MoveAwayVelocity = Vector2.Zero;

                        NPC.Center = TargetPos;
                        NPC.velocity = Vector2.Zero;

                        BossMoveDirection = (player.Center - NPC.Center);
                        BossMoveDirection.Normalize();

                        TargetPos = Vector2.Zero;

                        TeleCount += 1;
                    }

                    BossSpeed = MathF.Max(10f, BossSpeed * 0.99f);

                    // Move towards player
                    Vector2 directionToPlayer = (player.Center - NPC.Center);
                    float len = directionToPlayer.Length();
                    directionToPlayer.Normalize();

                    Vector2 moveDir = Vector2.Lerp(BossMoveDirection, directionToPlayer, 0.05f);
                    BossMoveDirection = moveDir;
                    moveDir.Normalize(); // idk if this will do shit but might as well

                    NPC.velocity = moveDir * BossSpeed * Math.Max(1f, len / 500 + 0.5f) + MoveAwayVelocity;
                    MoveAwayVelocity *= 0.97f;

                    // Rotation shit
                    float dirChange = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange - (NPC.rotation - MathF.Sin((Timer - 1) / 15) / 150)) / 15; // EASING OLLy
                    NPC.rotation += MathF.Sin(Timer / 15) / 150;
                    Timer += 1;

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
                                if (TargetPos == Vector2.Zero)
                                {
                                    Vector2 offsetDir = Main.rand.NextVector2Unit();
                                    TargetPos = player.Center + offsetDir * Main.rand.Next(30, 500);
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
                            ChangeState(BossState.Circle);
                        }
                    }

                    break;
                case BossState.Circle:
                    if (Timer == 0)
                    {
                        BossSpeed = 0.25f;
                        NPC.rotation = 0;
                        NPC.velocity = Vector2.Zero;
                        rotDir = (player.Center.X > NPC.Center.X) ? 1 : -1;
                        SinOffset = (NPC.Center - player.Center).ToRotation(); 
                        StartRadius = Vector2.Distance(NPC.Center, player.Center); 
                    }

                    float spinRadius = MathHelper.Lerp(StartRadius, 350f, Math.Min(1f, Timer / 40));

                    TargetPos = player.Center + new Vector2( MathF.Cos((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius, MathF.Sin((Timer * BossSpeed * rotDir) + SinOffset) * spinRadius  );
                    BossSpeed *= 0.995f;

                    NPC.Center = Vector2.Lerp(NPC.Center, TargetPos, 0.2f); // Soft follow instead of instant

                    //  ROTATION
                    float dirChange2 = (player.Center.X >= (NPC.Center + NPC.velocity).X) ? 0.0872665f : -0.0872665f;
                    NPC.rotation += (dirChange2 - (NPC.rotation - MathF.Sin((Timer - 1) / 15) / 150)) / 15; // EASING OLLy
                    NPC.rotation += MathF.Sin(Timer / 15) / 150;

                    Timer += 1;

                    if (Timer % 12 == 0 && Timer >= 24)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.One) * 10, ProjectileID.EyeLaser, 20, 2);
                    }

                    if (Timer >= 120)
                    {
                        ChangeState(BossState.Hoaming);
                        TargetPos = NPC.Center;
                    }

                    break;
            }
        }

        public void ChangeState(BossState newState)
        {
            TargetPos = Vector2.Zero;
            CurrentStage = newState;
            Timer = 0;
            TeleCount = 0;
        }
    }
}

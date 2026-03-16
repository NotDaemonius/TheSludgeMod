using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe
{
    public enum ArmState
    {
        Normal,
        Spinny
    }

    public class TheMainframeHand : ModNPC
    {
        public NPC Parent;

        public bool InSecondStage;

        public float OrbitAmount = 75f;

        public int WindupTimer = 0;
        public int WindupDuration = 1;

        public float pushback = 0;

        public float RotationThing = 0;

        float currentProgress = 0;
        public float rotSpeed = 0.4f;

        public float Side
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public float Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float ParentWhoAmI
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public ArmState CurrentStage
        {
            get => (ArmState) NPC.ai[3];
            set => NPC.ai[3] = (int) value;
        }

        private float spinRadius;
        private float spinTime;
        private float spinMaxSpeed;
        private float spinSpeedUp;
        private float spinDirection;
        float poo;

        float currentSpin;

        private const int ChainPoints = 50;
        private Vector2[] _chain = new Vector2[ChainPoints];
        private bool _chainInitialized = false;

        private static readonly Rectangle LargeRect = new Rectangle(0, 0, 8, 18);
        private static readonly Rectangle SmallRect = new Rectangle(8, 2, 8, 14);

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 2;

            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 26;
            NPC.damage = 7;
            NPC.defense = 0;
            NPC.lifeMax = 500000;
            NPC.DiscourageDespawn(1000);
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
        }

        public override void FindFrame(int frameHeight)
        {
            if (InSecondStage)
            {
                NPC.frame.Y = 1 * frameHeight;
            } else
            {
                NPC.frame.Y = 0;
            }
        }

        public override void AI()
        {
            if (Parent == null || !Parent.active)
            {
                int id = (int) ParentWhoAmI;
                if (id >= 0 && id < Main.maxNPCs && Main.npc[id].active && Main.npc[id].ModNPC is TheMainframe)
                    Parent = Main.npc[id];
                else
                {
                    NPC.active = false;
                    return;
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                float locDistance = OrbitAmount + MathF.Sin((Timer + Side * 2) * 0.05f) * 20;
                Vector2 targetPos = Parent.Center + new Vector2(locDistance, 0).RotatedBy(RotationThing) * new Vector2(3, 2);
                NPC.Center = Vector2.Lerp(NPC.Center, targetPos, rotSpeed);
                UpdateChain();

                return;
            }

            switch (CurrentStage)
            {

                case ArmState.Normal:
                    NPC.velocity = Vector2.Zero;

                    rotSpeed = MathF.Min(0.7f, rotSpeed + 0.04f);

                    float sideOffset = Side;
                    float rotationToPlayer = (player.Center - Parent.Center).ToRotation();

                    float locDistance = OrbitAmount + MathF.Sin((Timer + Side * 2) * 0.05f) * 20;
                    float angleOscillation = MathF.Cos((Timer + Side * 2) * 0.05f) * 0.1f;
                    pushback *= 0.9f;

                    RotationThing = rotationToPlayer + sideOffset + angleOscillation;
                    Vector2 targetPos = Parent.Center + new Vector2(locDistance, 0).RotatedBy(RotationThing) * new Vector2(3, 2) - (player.Center - NPC.Center).SafeNormalize(Vector2.One) * pushback;

                    NPC.Center = Vector2.Lerp(NPC.Center, targetPos, rotSpeed);
                    NPC.rotation = (player.Center - NPC.Center).ToRotation();

                    Timer += 1;

                    if (WindupDuration != 1)
                    {
                        WindupTimer += 1;
                        if (WindupTimer >= WindupDuration)
                        {
                            WindupTimer = WindupDuration;
                        }
                    }
                    UpdateChain();
                    break;
                case ArmState.Spinny:

                    NPC.velocity = Vector2.Zero;
                    pushback *= 0.9f;

                    rotSpeed = MathF.Min(0.2f, rotSpeed + 0.01f);
                    poo = MathF.Min(spinMaxSpeed, poo + spinSpeedUp);
                    currentSpin += poo;

                    Vector2 targetPos2 = Parent.Center + new Vector2(spinRadius, 0).RotatedBy(RotationThing + currentSpin * spinDirection) * new Vector2(3, 3);
                    NPC.Center = Vector2.Lerp(NPC.Center, targetPos2, rotSpeed);
                    NPC.rotation = RotationThing + currentSpin;

                    Timer += 1;
                    UpdateChain();

                    if (Timer >= spinTime)
                    {
                        Timer = 0;
                        CurrentStage = ArmState.Normal;
                        rotSpeed = 0;
                    }

                    break;
            }
        }

        public override void SendExtraAI(System.IO.BinaryWriter writer)
        {
            writer.Write(WindupTimer);
            writer.Write(WindupDuration);
        }

        public override void ReceiveExtraAI(System.IO.BinaryReader reader)
        {
            WindupTimer = reader.ReadInt32();
            WindupDuration = reader.ReadInt32();
        }

        public void DoArmRotation(float time, float radius, float rotSpeede, float rotSpeedup, int roDir)
        {
            WindupTimer = 0;
            WindupDuration = 1;
            Timer = 0;
            CurrentStage = ArmState.Spinny;
            rotSpeed = 0;

            spinTime = time;
            spinRadius = radius;
            spinMaxSpeed = rotSpeede;
            spinSpeedUp = rotSpeedup;
            spinDirection = roDir;
            currentSpin = 0;
            poo = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!_chainInitialized || Parent == null || !Parent.active)
                return true;



            Texture2D tex = ModContent.Request<Texture2D>(
                "TheSludgeMod/Content/NPCs/TheMainframe/TheMainframeWire",
                ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < ChainPoints - 1; i++)
                {
                    bool isLarge = (i % 2 == 0);

                    if (pass == 0 && isLarge)
                        continue;
                    if (pass == 1 && !isLarge)
                        continue;

                    Vector2 from = _chain[i];
                    Vector2 to = _chain[i + 1];
                    Vector2 center = (from + to) * 0.5f;

                    float rot = (to - from).ToRotation();
                    if (i == 0)
                    {
                        rot = NPC.rotation;
                    }

                    Rectangle src = isLarge ? LargeRect : SmallRect;
                    if (InSecondStage)
                    {
                        src.X += 16;
                    }
                    Vector2 origin = new Vector2(src.Width / 2f, src.Height / 2f);

                    spriteBatch.Draw(
                        tex,
                        center - screenPos,
                        src,
                        drawColor,
                        rot,
                        origin,
                        1f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            currentProgress = MathHelper.Lerp(currentProgress, WindupTimer / (float) WindupDuration, 0.4f);

            bool shouldGlow = WindupDuration > 1 || currentProgress > 0.01f;
            if (!shouldGlow)
                return;


            Texture2D glow2 = ModContent.Request<Texture2D>(
                "TheSludgeMod/Content/NPCs/TheMainframe/Gloww",
                ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            float progress = currentProgress;
            float scale = MathHelper.Lerp(0.1f, 1.5f, progress);
            float alpha = MathHelper.Lerp(0.1f, 0.8f, progress);
            float alpha2 = MathHelper.Lerp(0.05f, 0.4f, progress);

            Texture2D glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            SpriteEffects spriteEffects = (SpriteEffects) 0;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = (SpriteEffects) 1;
            }
            float height = Main.NPCAddHeight(NPC);
            Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
            spriteBatch.Draw(glow, NPC.Bottom - screenPos + new Vector2((float) (-TextureAssets.Npc[Type].Width()) * NPC.scale / 2f + halfSize.X * NPC.scale, (float) (-TextureAssets.Npc[Type].Height()) * NPC.scale / (float) Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + height + NPC.gfxOffY), (Rectangle?) NPC.frame, Color.White * alpha2, NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive,
                Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            spriteBatch.Draw(
                glow2,
                NPC.Center - screenPos,
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

        private void UpdateChain()
        {
            Vector2 dirToHand = (NPC.Center - Parent.Center).SafeNormalize(Vector2.UnitX);
            float angleToHand = MathF.Atan2(dirToHand.Y, dirToHand.X);
            Vector2 anchor = Parent.Center + new Vector2(MathF.Cos(angleToHand), MathF.Sin(angleToHand)) * new Vector2(75, 25);

            Vector2 tip = NPC.Center;

            if (!_chainInitialized)
            {
                for (int i = 0; i < ChainPoints; i++)
                    _chain[i] = Vector2.Lerp(anchor, tip, i / (float) (ChainPoints - 1));
                _chainInitialized = true;
            }

            float distance = Vector2.Distance(anchor, tip);
            float totalNaturalLength = ChainPoints * 6f;
            float scale = Math.Max(0.7f, (distance * 1.2f) / totalNaturalLength);

            float largeSize = 8f * scale;
            float smallSize = 4f * scale;

            for (int x = 0; x < 10; x++)
            {
                _chain[ChainPoints - 1] = tip;
                for (int y = ChainPoints - 2; y >= 0; y--)
                {
                    float segSize = (y % 2 == 0) ? largeSize : smallSize;
                    Vector2 dir = (_chain[y] - _chain[y + 1]).SafeNormalize(Vector2.UnitY);
                    _chain[y] = _chain[y + 1] + dir * segSize;
                }

                _chain[0] = anchor;
                for (int y = 1; y < ChainPoints; y++)
                {
                    float segSize = (y % 2 == 0) ? largeSize : smallSize;
                    Vector2 dir = (_chain[y] - _chain[y - 1]).SafeNormalize(Vector2.UnitY);
                    _chain[y] = _chain[y - 1] + dir * segSize;
                }
            }
        }
    }
}
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Enemies.VK
{
    [AutoloadBossHead]
    public class VK : ModNPC
    {
        private int ai;
        private bool despawn;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            { SpecificallyImmuneTo = new int[] { BuffID.Poisoned, BuffID.Confused } };

            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            //NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            //{
            //    CustomTexturePath = "",
            //    PortraitScale = 0.5f,
            //    PortraitPositionYOverride = 0f,
            //};
            //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetDefaults()
        {
            NPC.width = 118;
            NPC.height = 78;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.lifeMax = 2000;
            NPC.value = 100000f;
            //NPC.HitSound = SoundID.NPCHit4;
            //NPC.DeathSound = SoundID.Item14;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            //NPC.noTileCollide = true;
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            if (!Main.dedServ) { Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/Bosses/Scavengeance"); }
            //Main.npcFrameCount[NPC.type] = 4;
            //AnimationType = 49;
        }
            public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
            {
                bestiaryEntry.Info.AddRange
                (
                    new List<IBestiaryInfoElement>
                    {
                    new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                    new FlavorTextBestiaryInfoElement("Mods.AetherMod.Bestiary.VultureKing")
                    }
                );
            }

            public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
            {
                NPC.lifeMax = (int)((float)NPC.lifeMax * (numPlayers * 0.6f));
            }

            public override void OnKill()
            {
                if (!BossDownedList.downedVultureKingBoss)
                {
                    BossDownedList.downedVultureKingBoss = true;
                }
            }

            //public override void ModifyNPCLoot(NPCLoot npcLoot)
            //{
            
            //}
            public override void AI()
            {

                NPC.TargetClosest(true);
                Player player = Main.player[NPC.target];
                Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;

                NPC.rotation = 0f;
                NPC.netAlways = true;
                NPC.TargetClosest(true);

                if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
                {
                    if (!despawn)
                    {
                        NPC.timeLeft = 180;
                        despawn = true;
                    }

                    else
                    {
                        NPC.TargetClosest(false);
                        NPC.velocity.Y = NPC.velocity.Y - 0.4f;
                        NPC.EncourageDespawn(20);
                    }
                    return;
                }

                ai++;
                NPC.ai[0] = (float)ai * 1f;
                int distance = (int)Vector2.Distance(target, NPC.Center);


                //movement
                if ((double)NPC.ai[0] < 600)
                {
                    MoveTowards(NPC, target, (float)(distance > 1000 ? 40f : 8f), 40f);
                    NPC.netUpdate = true;
                }
            }
            private void MoveTowards(NPC npc, Vector2 playerTarget, float speed, float turnResistance)
            {
                var move = playerTarget - npc.Center;
                float length = move.Length();

                if (length > speed)
                {
                    move *= speed / length;
                }

                move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
                length = move.Length();

                if (length > speed)
                {
                    move *= speed / length;
                }

                npc.velocity = move;
            }

        }
    }
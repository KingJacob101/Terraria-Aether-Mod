using AetherMod.Enemies.VultureMinion;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AetherMod.Enemies.VK
{
    [AutoloadBossHead]
    public class VK : ModNPC
    {
        private int ai,phase;
        private bool despawn,phase2trigger = false, phase3trigger = false;
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
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item14;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
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

            //switch between phase

            //phase 1
            if (NPC.life > (NPC.lifeMax / 3) * 2)
            {
                phase = 1;
            }
            //phase 2
            if (NPC.life <= (NPC.lifeMax / 3) * 2 && NPC.life > NPC.lifeMax / 3)
            {
                phase = 2;
            }
            //phase 3
            if (NPC.life <= NPC.lifeMax / 3)
            {
                phase = 3;
            }


            //phase 1 
            if (phase == 1)
            {
                // pattern movement
                if ((double)NPC.ai[0] < 800)
                {
                    MoveTowards(NPC, new Vector2(target.X, target.Y - 300), (float)(distance > 1000 ? 8f : 6f), 60f);
                    NPC.netUpdate = true;
                }
                else if ((double)NPC.ai[0] == 800)
                {
                    //Dash into the player
                    float speed = 15f;
                    Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float x = player.position.X + (float)(player.width / 2) - vector.X;
                    float y = player.position.Y + (float)(player.height / 2) - vector.Y;
                    float distance2 = (float)Math.Sqrt(x * x + y * y);
                    float factor = (speed / (distance * 2));
                    NPC.velocity.X = x * factor;
                    NPC.velocity.Y = y * factor;
                    NPC.netUpdate = true;
                }
                else if ((double)NPC.ai[0] == 845)
                {
                    //Dash backwards
                    float speed = 15f;
                    Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float x = player.position.X + (float)(player.width / 2) - vector.X;
                    float y = player.position.Y + (float)(player.height / 2) - vector.Y;
                    float distance2 = (float)Math.Sqrt(x * x + y * y);
                    float factor = speed / (distance * 3);
                    NPC.velocity.X = -x * factor;
                    NPC.velocity.Y = -10f;
                    NPC.netUpdate = true;
                }
                else if ((double)NPC.ai[0] == 885)
                {
                    ai = 0;
                }


                //Summon the 1-2 minions every 2 seconds
                if ((double)NPC.ai[0] % 120 == 0)
                {
                    if (Main.rand.NextBool())
                    {
                        NPC minionNPC = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, number: minionNPC.whoAmI);
                        }
                    }
                    else
                    {
                        NPC minionNPC = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                        NPC minionNPC2 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, number: minionNPC.whoAmI);
                        }
                    }
                }
                NPC.netUpdate = true;
            }


            //phase 2 
            if (phase == 2)
            {
                if (!phase2trigger)
                {
                    phase2trigger = true;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                    ai = 0;
                }

                if ((double)NPC.ai[0] < 60)
                {
                    NPC.velocity.X = 0f; NPC.velocity.Y = 0f;
                }
                else if ((double)NPC.ai[0] > 60)
                {
                    NPC.velocity.Y = 10f;
                }

                if ((double)NPC.ai[0] % 120 == 0)
                {
                    NPC minionNPC1 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, number: minionNPC1.whoAmI);
                    }
                    NPC minionNPC2 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X - 25, (int)NPC.Center.Y - 25, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, number: minionNPC2.whoAmI);
                    }
                    NPC minionNPC3 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X + 25 , (int)NPC.Center.Y + 25, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, number: minionNPC3.whoAmI);
                    }
                    NPC minionNPC4 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X - 25, (int)NPC.Center.Y + 25, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, number: minionNPC4.whoAmI);
                    }
                    NPC minionNPC5 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X + 25, (int)NPC.Center.Y - 25, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, number: minionNPC5.whoAmI);
                    }
                    ai = 60;
                }
            }
            
            //phase 3
            if (phase == 3)
            {
                if (!phase3trigger)
                {
                    phase3trigger = true;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                    ai = 0;
                    NPC.velocity.X = 0f; NPC.velocity.Y = 0f;
                }

                if ((double)NPC.ai[0] > 60)
                {
                    if ((double)NPC.ai[0] < 860)
                    {
                        MoveTowards(NPC, new Vector2(target.X, target.Y - 300), (float)(distance > 1000 ? 8f : 6f), 60f);
                        NPC.netUpdate = true;
                    }
                    else if ((double)NPC.ai[0] == 860)
                    {
                        //Dash into the ground
                        SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                        NPC.velocity.X = 0f;
                        NPC.velocity.Y = 15f;
                        NPC.netUpdate = true;
                    }

                    else if ((double)NPC.ai[0] < 905 && (double)NPC.ai[0] > 860)
                    {
                        if (NPC.velocity.Y == 0f)
                        {
                            SoundEngine.PlaySound(SoundID.Item14);
                            if (distance < 320)
                            {
                                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + Language.GetOrRegister("Mods.AetherMod.NPCs.VK.hardattack")), 60, player.direction * -1);
                            }
                        }
                        
                    }

                    else if ((double)NPC.ai[0] == 905)
                    {
                        //Dash backwards
                        NPC.velocity.Y = -15f;
                        NPC.netUpdate = true;
                    }

                    else if ((double)NPC.ai[0] == 945)
                    {
                        ai = 61;
                    }

                    if ((double)NPC.ai[0] % 120 == 0)
                    {
                        if (Main.rand.NextBool())
                        {
                            NPC minionNPC = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, number: minionNPC.whoAmI);
                            }
                        }
                        else
                        {
                            NPC minionNPC = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                            NPC minionNPC2 = NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VultureMinion.VultureMinion>(), NPC.whoAmI);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, number: minionNPC.whoAmI);
                                NetMessage.SendData(MessageID.SyncNPC, number: minionNPC2.whoAmI);
                            }
                        }
                    }

                }
            }
        }

        
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (phase == 3 && (double)NPC.ai[0] < 905 && (double)NPC.ai[0] > 860)
            {
                Player player = Main.player[NPC.target];
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + Language.GetOrRegister("Mods.AetherMod.NPCs.VK.reflect")), (damageDone * (int)1.5), player.direction * -1);
            }
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (phase == 3 && (double)NPC.ai[0] < 905 && (double)NPC.ai[0] > 860)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + Language.GetOrRegister("Mods.AetherMod.NPCs.VK.reflect")), (damageDone * (int)1.5), player.direction * -1);
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
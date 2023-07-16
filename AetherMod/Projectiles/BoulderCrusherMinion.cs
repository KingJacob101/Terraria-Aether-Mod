using AetherMod.Buffs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace AetherMod.Projectiles;

public class BoulderCrusherMinion : ModProjectile
{

    bool rush = false;
    int rushtimer = 0;
    Vector2 target;
    bool randangle = false;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 1;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
    }

    public sealed override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.tileCollide = false;

        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.minionSlots = 1f;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override bool MinionContactDamage()
    {
        return true;
    }

    public override void AI()
    {
        double deg = (double)Projectile.ai[1];
        double rad = deg * (Math.PI / 180);
        double dist = 64;

        Player player = Main.player[Projectile.owner];

        if (!CheckActive(player))
        {
            return;
        }

        if (Main.mouseRight && !rush)
        {
            target = Main.MouseWorld;
            rush = true;
        }

        if (!rush)
        {
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.ai[1] += 3f;
        }
        else
        {
            rushtimer++;
            if (rushtimer > 40 || Projectile.position == target)
            {
                Projectile.Kill();
            }
            Projectile.velocity = (target - Projectile.Center).SafeNormalize(Vector2.Zero) * rushtimer;

            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
    private bool CheckActive(Player owner)
    {
        if (owner.dead || !owner.active)
        {
            owner.ClearBuff(ModContent.BuffType<BoulderCrusherMinionBuff>());

            return false;
        }

        if (owner.HasBuff(ModContent.BuffType<BoulderCrusherMinionBuff>()))
        {
            Projectile.timeLeft = 2;
        }

        return true;
    }

}
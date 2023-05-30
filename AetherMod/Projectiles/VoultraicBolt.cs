using Terraria;
using Terraria.ModLoader;

namespace AetherMod.Projectiles;

public class VoultraicBolt : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.height = 10;
        Projectile.width = 5;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 600;
    }
    public override void AI()
    {
        // this is solely to make the projectile not look awkward
        Projectile.rotation = Projectile.velocity.ToRotation();
    }
}
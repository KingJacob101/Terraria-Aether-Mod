using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Projectiles
{
    public class VoultraicChainsawProj : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
		}

		public override void SetDefaults() 
		{
			Projectile.width = 26;
			Projectile.height = 58;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ownerHitCheck = true;
			Projectile.aiStyle = 20;
			Projectile.hide = true; 
		}
	}
}

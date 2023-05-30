using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;


namespace AetherMod.Items.Weapons.Voultraic;

public class VoultraicPistol : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Ranged;
        Item.useAnimation = 18;
        Item.useTime = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAmmo = AmmoID.Bullet;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 20;
        Item.UseSound = SoundID.Item11;
        Item.noMelee = true;
        Item.width = 27;
        Item.height = 18;
        Item.knockBack = 2f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if(Main.rand.NextBool(5))
        {
            // gives a 1/5 chance to shoot a Voltraic Bolt
            // ofcourse change the type parameter to aether mods projectile path
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.VoultraicBolt>(), damage, knockback, Main.myPlayer);  

        }
        return true;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
        // makes this gun shoot out of its barrel instead of the players center
        if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
        {
            position += muzzleOffset;
        }
        
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4f, 0f);
    }
}
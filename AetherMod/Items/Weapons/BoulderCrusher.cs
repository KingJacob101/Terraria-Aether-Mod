using AetherMod.Buffs;
using AetherMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Weapons
{
	public class BoulderCrusher : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
        }
        public override void SetDefaults()
		{
			Item.damage = 9;
			Item.crit = 4;
			Item.DamageType = DamageClass.Summon;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1750;
            Item.mana = 5;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.buffType = ModContent.BuffType<BoulderCrusherMinionBuff>();
            Item.shoot = ModContent.ProjectileType<BoulderCrusherMinion>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;
            return false;
        }

        public override void AddRecipes()
		{
			//Placeholder
			CreateRecipe()
			.AddIngredient(ItemID.DirtBlock, 1)
			.AddTile(TileID.WorkBenches)
			.Register();
		}

    }
}

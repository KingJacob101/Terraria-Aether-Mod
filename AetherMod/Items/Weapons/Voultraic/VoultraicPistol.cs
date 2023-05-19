using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Weapons.Voultraic
{
	public class VoultraicPistol : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.crit = 4;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
			Item.value = 1750;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Bullet;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10f;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Items.Voultraic.VoultraicCell>(), 2)
			.AddIngredient(ModContent.ItemType<Items.Voultraic.VoultraicFeather>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
		}

	}
}

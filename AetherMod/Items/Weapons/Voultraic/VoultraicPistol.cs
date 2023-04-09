using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Weapons.Voultraic
{
	public class VoultraicPistol : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.crit = 4;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = 5;
			Item.knockBack = 2;
			Item.value = 1750;
			Item.rare = 2;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Bullet;
			Item.shoot = 10;
			Item.shootSpeed = 10f;
		}
		
	public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Items.Voultraic.VoultraicCell>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Items.Voultraic.VoultraicFeather>(), 4);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

	}
}

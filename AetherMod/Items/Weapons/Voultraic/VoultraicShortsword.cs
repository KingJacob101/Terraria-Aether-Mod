using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Weapons.Voultraic
{
	public class VoultraicShortsword : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 31;
			Item.crit = 4;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack = 5;
			Item.value = 1750;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
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

using AetherMod.Items.Materials.Voultraic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Tools
{
    public class VoultraicChainsaw : ModItem
	{
		public override void SetStaticDefaults() 
		{
			ItemID.Sets.IsChainsaw[Type] = true;
		}

		public override void SetDefaults() 
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 58;
			Item.height = 26;
			Item.useTime = 4;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0.5f;
			Item.value = 0;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item23;
			Item.shoot = ModContent.ProjectileType<Projectiles.VoultraicChainsawProj>();
			Item.shootSpeed = 32f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.axe = 14;
			
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient<VoultraicCell>(5)
				.AddIngredient<VoultraicFeather>(5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Dev
{
	public class VoidReaper : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 2300;
			Item.crit = 4;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack = 8;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Voultraic
{
	public class VoultraicCell: ModItem
	{
			public override void SetDefaults()
			{
				Item.height = 40;
				Item.value = 150;
				Item.rare = ItemRarityID.LightRed;
				Item.maxStack = 999;
			}
			
	}
}
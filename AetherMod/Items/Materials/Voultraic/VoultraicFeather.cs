using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Materials.Voultraic
{
	public class VoultraicFeather: ModItem
	{
			public override void SetDefaults()
			{
				Item.height = 30;
				Item.width = 22;
				Item.value = 150;
				Item.rare = ItemRarityID.LightRed;
				Item.maxStack = 9999;
			}
			
	}
}
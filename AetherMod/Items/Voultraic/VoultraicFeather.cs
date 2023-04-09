using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AetherMod.Items.Voultraic
{
	public class VoultraicFeather: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Titanium Scythe"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A feather that fell from the Vulture King himself.");
		}

			public override void SetDefaults()
			{
				Item.height = 40;
				Item.value = 150;
				Item.rare = 4;
				Item.maxStack = 999;
			}
			
	}
}

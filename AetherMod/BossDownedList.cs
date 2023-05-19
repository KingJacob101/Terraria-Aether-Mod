using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AetherMod
{
	public class BossDownedList : ModSystem
	{
		public static bool downedVultureKingBoss = false;

		public override void OnWorldLoad() 
		{
			downedVultureKingBoss = false; 
		}

		public override void OnWorldUnload() 
		{
			downedVultureKingBoss = false;
		}

		public override void SaveWorldData(TagCompound tag) 
		{
			if (downedVultureKingBoss) 
			{
				tag["downedVultureKingBoss"] = true;
			} 
		}

		public override void LoadWorldData(TagCompound tag) 
		{
			downedVultureKingBoss = tag.ContainsKey("downedVultureKingBoss"); 
		}

		public override void NetSend(BinaryWriter writer) 
		{
			var flags = new BitsByte(); 
			flags[0] = downedVultureKingBoss;		
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader) 
		{
			BitsByte flags = reader.ReadByte();
			downedVultureKingBoss = flags[0];
		}

	}
}
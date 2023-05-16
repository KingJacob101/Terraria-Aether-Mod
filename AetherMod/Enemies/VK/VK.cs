using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace AetherMod.Enemies.VK
{
    public class VK : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulture King");
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 15;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.lifeMax = 2000;
            NPC.value = 100000f;
            //Main.npcFrameCount[NPC.type] = 4;
            //AnimationType = 49;
        }
    }
}
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace AetherMod.Enemies.VultureMinion
{
    public class VultureMinion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulture King's Minion");
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 15;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.lifeMax = 64;
            NPC.value = 5f;
            NPC.aiStyle = 2;
            Main.npcFrameCount[NPC.type] = 4;
            AnimationType = 49;
        }
    }
}
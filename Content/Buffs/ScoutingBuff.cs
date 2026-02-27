using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Buffs
{
    public class ScoutingBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Display name and tooltip are pulled from Localization files,
            // or you can set them via .hjson. Alternatively, hardcode below:
            // DisplayName.SetDefault("Scouting");
            // Description.SetDefault("Bound town NPCs spawn 10x more frequently");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }
    }
}
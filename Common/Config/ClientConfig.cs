using Terraria.ModLoader.Config;

namespace TheSludgeMod.Common;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    public bool DisableScreenShake { get; set; } = false;

    public bool DisableBossSpawnSuppression { get; set; }
}
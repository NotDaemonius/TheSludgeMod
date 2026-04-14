using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Systems;

public class HardmodeEvilSwap : ModSystem
{
    public override void Load()
    {
        On_WorldGen.smCallBack += SwapHardmodeEvil;
    }

    public override void Unload()
    {
        On_WorldGen.smCallBack -= SwapHardmodeEvil;
    }

    // this code could be so shit but idc it works
    private static void SwapHardmodeEvil(On_WorldGen.orig_smCallBack orig, object threadContext)
    {
        WorldGen.crimson = !WorldGen.crimson;
        orig.Invoke(threadContext);
        WorldGen.crimson = !WorldGen.crimson;
    }
}

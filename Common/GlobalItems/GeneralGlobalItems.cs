using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalItems;

public class GeneralGlobalItems : GlobalItem
{
    public override void SetDefaults(Item entity)
    {
        // Make the cellphone teleport you faster. 
        if (entity.type == ItemID.CellPhone ||
            entity.type == ItemID.Shellphone ||
            entity.type == ItemID.ShellphoneDummy ||
            entity.type == ItemID.ShellphoneHell ||
            entity.type == ItemID.ShellphoneOcean ||
            entity.type == ItemID.ShellphoneSpawn
        )
            entity.useTime = (entity.useAnimation = 30);

    }
}

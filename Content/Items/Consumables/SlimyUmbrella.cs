using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Consumables;

public class SlimyUmbrella : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = false;
        Item.useAnimation = 45;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override bool CanUseItem(Player player) => !Main.slimeRain;

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            Main.slimeRain = false;
            Main.StartSlimeRain();
            Main.slimeRainTime = 54000.0;
            Main.slimeWarningDelay = 0;
            Main.slimeWarningTime = 0;
            NetworkText message = Language.GetText("LegacyWorldGen.74").ToNetworkText();
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(message.ToString(), 50, 255, 130);
            else ChatHelper.BroadcastChatMessage(message, new Microsoft.Xna.Framework.Color(50, 255, 130));
            NetMessage.SendData(MessageID.WorldData);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
        }

        return true;
    }
}
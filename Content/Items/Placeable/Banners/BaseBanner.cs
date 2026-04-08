using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalTiles;
using TheSludgeMod.Content.Tiles.Banners;

namespace TheSludgeMod.Content.Items.Placeable.Banners;

public abstract class BaseBanner : ModItem
{

    public virtual int BannerTileID => ModContent.TileType<MonsterBanner>();
    public virtual int BannerTileStyle => 0;
    public virtual int BannerKillRequirement => ItemID.Sets.DefaultKillsForBannerNeeded;
    public virtual int BonusNPCID => MonsterBanner.GetBannerNPC(BannerTileStyle);
    public new string LocalizationCategory => "Items.Placeables";
    public virtual LocalizedText NPCName => NPCLoader.GetNPC(BonusNPCID).DisplayName;
    public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs(NPCName.ToString());
    public override LocalizedText Tooltip => FurnitureCommon.GetText($"{LocalizationCategory}.FormattedBannerTooltip").WithFormatArgs(NPCName.ToString());
    public override void SetStaticDefaults() => ItemID.Sets.KillsToBanner[Type] = BannerKillRequirement;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(BannerTileID, BannerTileStyle);
        Item.width = 10;
        Item.height = 24;
        Item.value = Item.sellPrice(silver: 2);
        Item.rare = ItemRarityID.Blue;
    }
}
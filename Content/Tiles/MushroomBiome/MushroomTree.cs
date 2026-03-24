/*
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.MushroomBiome
{
    public class MushroomTree : ModTree
    {
        private Asset<Texture2D> texture;
        private Asset<Texture2D> topsTexture;

        // This is a blind copy-paste from Vanilla's PurityPalmTree settings.
        // TODO: This needs some explanations
        public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            // Makes Example Tree grow on ExampleBlock
            GrowsOnTileId = [ModContent.TileType<MushroomGrass>()];
            texture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Tiles/MushroomBiome/MushroomTree");
            topsTexture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Tiles/MushroomBiome/MushroomTree_Tops");
        }

        // This is the primary texture for the trunk. Branches and foliage use different settings.
        public override Asset<Texture2D> GetTexture()
        {
            return texture;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<MushroomSapling>();
        }

        public override void SetTreeFoliageSettings(int i, int j, Tile tile, int xoffset, ref int treeFrame, int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            Main.NewText(floorY);
            // This is where fancy code could go, but let's save that for an advanced example
        }

        // Branch Textures
        public override Asset<Texture2D> GetBranchTextures() => null;

        // Top Textures
        public override Asset<Texture2D> GetTopTextures() => topsTexture;

        public override int DropWood()
        {
            return ItemID.Mushroom;
        }

        /*
        public override bool Shake(int x, int y, ref bool createLeaves)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Placeable.ExampleBlock>());
            return false;
        }
        
        public override int TreeLeaf()
        {
            return ModContent.GoreType<ExampleTreeLeaf>();
        }
    }
}
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;

namespace TheSludgeMod.Content.Skies;

public class MushroomSky : CustomSky
{
    private bool isActive = false;
    private float intensity = 0f;

    public override void Update(GameTime gameTime)
    {
        if (isActive && intensity < 1f)
        {
            intensity += 0.02f;
        } else if (!isActive && intensity > 0f)
        {
            intensity -= 0.1f;
        }
    }


    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
        {
            spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(102, 40, 28) * intensity);
        }
    }

    public override float GetCloudAlpha() => 0f;
    public override void Activate(Vector2 position, params object[] args) => isActive = true;
    public override void Deactivate(params object[] args) => isActive = false;
    public override void Reset() => isActive = false;
    public override bool IsActive() => isActive || intensity > 0f;
}

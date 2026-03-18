using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod
{
	public class TheSludgeMod : Mod
	{
        private Asset<Texture2D> _vanillaSkeletronHead;
        private Asset<Texture2D> _modSkeletronHead;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _vanillaSkeletronHead = TextureAssets.Npc[NPCID.SkeletronHead];
                _modSkeletronHead = ModContent.Request<Texture2D>("TheSludgeMod/Common/GlobalNPCs/Skeletron_Head", AssetRequestMode.ImmediateLoad);

                if (_modSkeletronHead != null)
                {
                    TextureAssets.Npc[NPCID.SkeletronHead] = _modSkeletronHead;
                }
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                if (_vanillaSkeletronHead != null &&
                    TextureAssets.Npc[NPCID.SkeletronHead] == _modSkeletronHead)
                {
                    TextureAssets.Npc[NPCID.SkeletronHead] = _vanillaSkeletronHead;
                }

                _vanillaSkeletronHead = null;
                _modSkeletronHead = null;
            }
            base.Unload();
        }
    }
}

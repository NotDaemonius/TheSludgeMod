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
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class TheSludgeMod : Mod
	{
        private Asset<Texture2D> _vanillaSkeletronHead;


        public override void Load()
        {
            if (!Main.dedServ)
            {
                _vanillaSkeletronHead = TextureAssets.Npc[NPCID.SkeletronHead];
                TextureAssets.Npc[NPCID.SkeletronHead] = ModContent.Request<Texture2D>("TheSludgeMod/Common/GlobalNPCs/Skeletron_Head", AssetRequestMode.ImmediateLoad);
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                if (_vanillaSkeletronHead != null) {
                    TextureAssets.Npc[NPCID.SkeletronHead] = _vanillaSkeletronHead;
                }

                _vanillaSkeletronHead = null;
            }

            base.Unload();
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
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
        private readonly List<Action> _unloadActions = new();
        public override void Load()
        {
            if (Main.dedServ) return;

            try
            {
                _vanillaSkeletronHead = TextureAssets.Npc[NPCID.SkeletronHead];

                _modSkeletronHead = ModContent.Request<Texture2D>(
                    "TheSludgeMod/Common/GlobalNPCs/Skeletron_Head",
                    AssetRequestMode.ImmediateLoad);

                if (_modSkeletronHead?.IsLoaded == true)
                {
                    TextureAssets.Npc[NPCID.SkeletronHead] = _modSkeletronHead;

                    _unloadActions.Add(() =>
                    {
                        if (TextureAssets.Npc[NPCID.SkeletronHead] == _modSkeletronHead)
                            TextureAssets.Npc[NPCID.SkeletronHead] = _vanillaSkeletronHead;
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load custom Skeletron texture: {ex.Message}");
            }
        }
        public override void Unload()
        {
            if (!Main.dedServ)
            {
                try
                {
                    foreach (var action in _unloadActions)
                        action.Invoke();
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to restore vanilla Skeletron texture: {ex.Message}");
                }
                finally
                {
                    _unloadActions.Clear();
                    _vanillaSkeletronHead = null;
                    _modSkeletronHead = null;
                }
            }

            base.Unload();
        }
    }
}
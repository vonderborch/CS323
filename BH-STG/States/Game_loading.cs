/*
 * Component: States - Game Loading
 * Version: 1.0.4
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/


using BH_STG.BarrageEngine.Screen;
using BH_STG.Particles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BH_STG.States
{
    class Game_Loading : Screen
    {
        Texture2D BGIMAGE;
        SpriteFont font;
        int loadTick = 0;
        string loadText = "";
        Color fontColor = Color.White;

        public Main GameMain { get; set; }
        public bool loading()
        {
            //////// Load Fade Texture ////////
            this.FadeTexture = GameMain.Content.Load<Texture2D>("Graphics\\Other\\FadeBase.png");


            font = GameMain.Content.Load<SpriteFont>("Fonts\\General");

            /////// load bg image ////////
            this.BGIMAGE = GameMain.Content.Load<Texture2D>("Graphics\\MenuBackgrounds\\bsg-stars.png");

            return true;
        }
        public bool update(out Main.GameStates state, Game_Main lGame, Defeat lDefeat, Victory lVictory, 
                            Game_Main.Levels selLevel, Game_Main.Players selPlayer, Explosion explode, Flame flame,
                            string bgmusic, string explosion, string getitem, string bomb, 
                            int difficulty, bool useReplay, string filename = "")
        {
            state = Main.GameStates.game_loading;

            string GameMusic = "GoCart",
                   GameExplosion = "55830__sergenious__explosio", GameGetItem = "39041__wildweasel__itemup2", GameBomb = "155235__zangrutz__bomb-small";

            if (selLevel == Game_Main.Levels.secret_bob_level || selLevel == Game_Main.Levels.test_level)
                GameMusic = "Spazzmatica Polka";
            else
                GameMusic = "Theme for Harold var 3";

            if (loadTick == 0)
            {
                lGame = new Game_Main();
                loadText = "Resetting the Game";
            }
            else if (loadTick == 1)
            {
                lGame.GameMain = GameMain;
                lGame.def = lDefeat;
                lGame.vic = lVictory;
                loadText = "Assigning Classes";
            }
            else if (loadTick == 2)
            {
                lGame.loading(selLevel, selPlayer, difficulty, explode, flame,
                              GameMusic, GameExplosion, GameGetItem, GameBomb, 
                              useReplay, filename);
                loadText = "Loading the Level";
            }
            else
            {
                loadTick = 0;
                state = Main.GameStates.game;
            }
            loadTick++;

            return true;
        }

        public bool draw(SpriteBatch spriteBatch)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw temp text
            spriteBatch.DrawString(font, "Loading level...", new Vector2(GameMain.videosettings.returnModifiedX(GameMain.videosettings.width / 2),
                                   GameMain.videosettings.returnModifiedY(GameMain.videosettings.height / 2)), fontColor);
            spriteBatch.DrawString(font, loadText, new Vector2(GameMain.videosettings.returnModifiedX(GameMain.videosettings.width / 2),
                                   GameMain.videosettings.returnModifiedY(GameMain.videosettings.height / 2 + 20)), fontColor);
            return true;
        }
    }
}

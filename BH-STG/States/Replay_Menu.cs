/*
 * Component: States - Replay Menu
 * Version: 1.0.5
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Replay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace BH_STG.States
{
    class Replay_Menu : BH_STG.BarrageEngine.Screen.Screen
    {
        Texture2D BGIMAGE;
        SpriteFont font;

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
        public string update(out Main.GameStates state, InputCommon input)
        {
            state = Main.GameStates.replay_menu;
            OpenFileDialog loader = new OpenFileDialog();
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            filename += "\\Team Christian\\BH-STG\\Replays\\";
            loader.InitialDirectory = filename;
            loader.Filter = "Replay Files (*.bhr)|*.bhr";
            loader.RestoreDirectory = true;

            if (loader.ShowDialog() == DialogResult.OK)
            {
                if (Replay.checkFileVersion(loader.FileName))
                {
                    state = Main.GameStates.game_loading;
                    return loader.FileName;
                }
                else
                    MessageBox.Show("Cannot load replay file, invalid version!", "Replay File Check", MessageBoxButtons.OK);
            }
            else if (loader.ShowDialog() == DialogResult.Cancel || loader.ShowDialog() == DialogResult.None)
            {
                state = Main.GameStates.main_menu;
                return "";
            }
            return "";
        }

        public bool draw(SpriteBatch spriteBatch)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            return true;
        }
    }
}

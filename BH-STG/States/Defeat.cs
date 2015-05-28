/*
 * Component: States - Defeat
 * Version: 1.1.4
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Windows.Forms;

namespace BH_STG.States
{
    class Defeat : BH_STG.BarrageEngine.Screen.Menu
    {
        public Game_Main game { get; set; }
        string score, difficulty, character, level, user;
        int scre, diff;

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.defeat;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("Exit to Main Menu", true));
            options.Add(new Option("Save High Score"));
            options.Add(new Option("Save Replay"));
            options.Add(new Option("Save High Score and Replay"));
        }

        public override Main.GameStates updateEnter(InputCommon input)
        {
            Main.GameStates state = this.thisState;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.main_menu;
                else if (selectedOption == 2) // save replay
                {
                    if (!game.isUsingReplay())
                    {
                        game.saveReplay();
                        MessageBox.Show("Replay saved!", "Save Confirmation", MessageBoxButtons.OK);
                    }
                }
                else if (selectedOption == 1) // save high score
                {
                    MessageBox.Show("Score saved!", "Save Confirmation", MessageBoxButtons.OK);
                    GameMain.Offlinescores.saveScore(user, level, character, diff, scre);
                }
                else if (selectedOption == 3) // save replay and high score
                {
                    GameMain.Offlinescores.saveScore(user, level, character, diff, scre);
                    if (!game.isUsingReplay())
                    {
                        game.saveReplay();
                        MessageBox.Show("Replay and score saved!", "Save Confirmation", MessageBoxButtons.OK);
                    }
                }
            }
            #endregion

            return state;
        }

        public void setMatchInfo(string nUser, string nScore, string nDifficulty, string nCharacter, string nLevel, int nDiff, int nScre)
        {
            user = nUser;
            score = nScore;
            difficulty = nDifficulty;
            character = nCharacter;
            level = nLevel;
            diff = nDiff;
            scre = nScre;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Defeat!", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            // Draw Player Score and other info
            spriteBatch.DrawString(this.titleFont, "Score: " + score, new Vector2(main.videosettings.returnModifiedX(30),
                                   main.videosettings.returnModifiedY(200)), this.fontColor);
            spriteBatch.DrawString(this.itemFont, "Difficulty: " + difficulty, new Vector2(main.videosettings.returnModifiedX(30),
                                   main.videosettings.returnModifiedY(230)), this.fontColor);
            spriteBatch.DrawString(this.itemFont, "Character: " + character, new Vector2(main.videosettings.returnModifiedX(30),
                                   main.videosettings.returnModifiedY(250)), this.fontColor);
            spriteBatch.DrawString(this.itemFont, "Level: " + level, new Vector2(main.videosettings.returnModifiedX(30),
                                   main.videosettings.returnModifiedY(270)), this.fontColor);


            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

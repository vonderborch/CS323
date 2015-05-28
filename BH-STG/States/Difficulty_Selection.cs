/*
 * Component: States - Difficulty Selection
 * Version: 1.1.6
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Screen;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.States
{
    class Difficulty_Selection : Menu
    {
        public override void loadMenu()
        {
            this.thisState = Main.GameStates.difficulty_selection;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("(back)", true));
            options.Add(new Option("Easy"));
            options.Add(new Option("Normal"));
            options.Add(new Option("Hard"));
            options.Add(new Option("Insanity"));
            if (GameMain.gamesettings.testmode || GameMain.debugmode)
                options.Add(new Option("Test Mode (Invulnerability)"));
        }

        public int update(out Main.GameStates state, InputCommon input)
        {
            int difficulty = 0;

            updateSelection(input);
            state = updateEnter(input, out difficulty);

            return difficulty;
        }

        public Main.GameStates updateEnter(InputCommon input, out int difficulty)
        {
            Main.GameStates state = this.thisState;
            difficulty = 0;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.level_selection;
                else if (selectedOption == 1)
                {
                    state = Main.GameStates.character_selection;
                    difficulty = 1;
                }
                else if (selectedOption == 2)
                {
                    state = Main.GameStates.character_selection;
                    difficulty = 2;
                }
                else if (selectedOption == 3)
                {
                    state = Main.GameStates.character_selection;
                    difficulty = 3;
                }
                else if (selectedOption == 4)
                {
                    state = Main.GameStates.character_selection;
                    difficulty = 4;
                }
                else if (selectedOption == 5)
                {
                    state = Main.GameStates.character_selection;
                    difficulty = 5;
                }
            }
            #endregion

            return state;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Difficulty Selection", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            int itemx = 800;
            if (selectedOption == 1)
            {
                spriteBatch.DrawString(this.itemFont, " - Item Drop Ratio: 1x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Enemy Shooting Frequency Ratio: 1x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Lives: 5", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(100)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Health: 10", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(120)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: 1", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(140)), this.fontColor);
            }
            else if (selectedOption == 2)
            {
                spriteBatch.DrawString(this.itemFont, " - Item Drop Ratio: 1/2x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Enemy Shooting Frequency Ratio: 2x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Lives: 3", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(100)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Health: 10", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(120)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: 2", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(140)), this.fontColor);
            }
            else if (selectedOption == 3)
            {
                spriteBatch.DrawString(this.itemFont, " - Item Drop Ratio: 1/3x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Enemy Shooting Frequency Ratio: 3x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Lives: 3", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(100)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Health: 5", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(120)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: 3", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(140)), this.fontColor);
            }
            else if (selectedOption == 4)
            {
                spriteBatch.DrawString(this.itemFont, " - Item Drop Ratio: 1/4x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Enemy Shooting Frequency Ratio: 4x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Lives: 0", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(100)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Health: 1", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(120)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: 4", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(140)), this.fontColor);
            }
            else if (selectedOption == 5)
            {
                spriteBatch.DrawString(this.itemFont, " - Item Drop Ratio: 1x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Enemy Shooting Frequency Ratio: 1x", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Lives: 1000000", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(100)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Base Health: 1000000", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(120)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: 1", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(140)), this.fontColor);
            }

            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

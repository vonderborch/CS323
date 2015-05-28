/*
 * Component: States - Level Selection
 * Version: 1.1.10
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Level;
using BH_STG.BarrageEngine.Screen;
using BH_STG.Levels;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace BH_STG.States
{
    class Level_Selection : Menu
    {
        TestLevel testlevel;

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.level_selection;

            // set classes
            Random rand = new Random();
            testlevel = new TestLevel(rand, 0, 0, 0);

            // add menu options
            selectedOption = 0;
        }

        public void createMenu()
        {
            options = new List<Option>();
            options.Add(new Option("(back)", true));
            
            if (GameMain.gamesettings.testmode || GameMain.debugmode)
                options.Add(new Option(testlevel.returnName()));
        }

        public Game_Main.Levels update(out Main.GameStates state, InputCommon input)
        {
            Game_Main.Levels level = Game_Main.Levels.secret_bob_level;

            updateSelection(input);
            state = updateEnter(input, out level);

            return level;
        }

        public Main.GameStates updateEnter(InputCommon input, out Game_Main.Levels level)
        {
            Main.GameStates state = this.thisState;
            level = Game_Main.Levels.secret_bob_level;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.main_menu;
                else if (selectedOption == 1)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level1;
                }
                else if (selectedOption == 2)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level2;
                }
                else if (selectedOption == 3)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level3;
                }
                else if (selectedOption == 4)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level4;
                }
                else if (selectedOption == 5)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level5;
                }
                else if (selectedOption == 6)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level6;
                }
                else if (selectedOption == 7)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.level7;
                }
                else if (selectedOption == 8)
                {
                    state = Main.GameStates.difficulty_selection;
                    level = Game_Main.Levels.test_level;
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
            spriteBatch.DrawString(this.titleFont, "BH-STG: Level Selection", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            Level templevel = null;
            if (selectedOption == 1) // test level
                templevel = testlevel;

            if (templevel != null)
            {
                List<string> leveldescription = templevel.getLevelDescription();
                int ypos = 40, itemx = 800;

                foreach (string str in leveldescription)
                {
                    spriteBatch.DrawString(this.itemFont, str, new Vector2(main.videosettings.returnModifiedX(itemx),
                                           main.videosettings.returnModifiedY(ypos)), this.fontColor);
                    ypos += 20;
                }
            }

            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

/*
 * Component: States - High-score Menu
 * Version: 1.1.6
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.HighScores;
using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Screen;
using BH_STG.Levels;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace BH_STG.States
{
    class HighScore_Menu : Menu
    {
        TestLevel testlevel;

        List<Option> scores = new List<Option>();
        bool hasUpdated = false;

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.highscore_menu;

            // set classes
            Random rand = new Random();
            testlevel = new TestLevel(rand, 0, 0, 0);

            // add menu options
            selectedOption = 0;
            options.Add(new Option("(back)", true));
            options.Add(new Option(testlevel.returnName()));
        }

        public override bool update(out Main.GameStates state, InputCommon input)
        {
            updateMain(input);
            state = updateEnter(input);

            #region display scores
            if (selectedOption == 1 && hasUpdated == false) // test level
            {
                #region load level high scores
                List<OfflineScores.Score> HighScores = GameMain.Offlinescores.getTwentyScores(testlevel.returnName());
                hasUpdated = true;

                foreach (OfflineScores.Score sc in HighScores)
                {
                    string diff = "Easy";
                    if (sc.difficulty == 2)
                        diff = "Normal";
                    else if (sc.difficulty == 3)
                        diff = "Hard";
                    else if (sc.difficulty == 4)
                        diff = "Insanity";
                    scores.Add(new Option("User: " + sc.user + ", Character: " + sc.character + ", Difficulty: " + diff + ", Score: " + sc.score.ToString()));
                }

                #endregion
            }

            #endregion

            return true;
        }

        public void updateMain(InputCommon input)
        {
            bool updateOptions = false;

            #region Menu Item Selection
            if (input.Left.pressed && input.Left.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                updateOptions = true;
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = options.Count - 1;
            }
            if (input.Right.pressed && input.Right.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                updateOptions = true;
                selectedOption++;
                if (selectedOption == options.Count)
                    selectedOption = 0;
            }

            if (updateOptions)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    string txt = options[i].text;
                    bool sel = false;
                    hasUpdated = false;
                    scores = new List<Option>();
                    if (i == selectedOption)
                        sel = true;
                    options[i] = new Option(txt, sel);
                }
            }

            #endregion
        }

        public override Main.GameStates updateEnter(InputCommon input)
        {
            Main.GameStates state = this.thisState;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                {
                    state = Main.GameStates.main_menu;
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
            spriteBatch.DrawString(this.titleFont, "BH-STG: High Scores", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.X += 75;
            }
            // draw scores
            pos = new Vector2(50, 80);
            foreach (Option score in scores)
            {
                score.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }


            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

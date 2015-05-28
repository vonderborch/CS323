/*
 * Component: States - Main Menu
 * Version: 1.1.5
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Forms;
using BH_STG.BarrageEngine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.IO;
using System.Windows.Forms;

namespace BH_STG.States
{
    class Main_Menu : BH_STG.BarrageEngine.Screen.Menu
    {
        EnhancedTextBox log = null;
        bool logopen = false;

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.main_menu;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("Play", true));
            options.Add(new Option("Replay"));
            options.Add(new Option("High Scores"));
            options.Add(new Option("Options"));
            options.Add(new Option("Cheats"));
            options.Add(new Option("Help"));
            options.Add(new Option("About"));
            options.Add(new Option("Changelog"));
            options.Add(new Option("Known Issues"));
            options.Add(new Option("To-Do List"));
            options.Add(new Option("Exit"));
        }

        public override Main.GameStates updateEnter(InputCommon input)
        {
            Main.GameStates state = this.thisState;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.level_selection;
                else if (selectedOption == 1)
                    state = Main.GameStates.replay_menu;
                else if (selectedOption == 2)
                    state = Main.GameStates.highscore_menu;
                else if (selectedOption == 3)
                    state = Main.GameStates.options_menu;
                else if (selectedOption == 4)
                    state = Main.GameStates.cheats;
                else if (selectedOption == 5)
                    state = Main.GameStates.help;
                else if (selectedOption == 6)
                    state = Main.GameStates.about;
                else if (selectedOption == 7)
                {
                    if (logopen == false)
                    {
                        string file = "Changelog";
                        log = new EnhancedTextBox();
                        StreamReader reader = new StreamReader("Content\\Common\\" + file + ".txt");
                        string text = reader.ReadToEnd();
                        reader.Close();
                        log.setText(text);
                        log.setTitle(file);
                        log.Show();
                        logopen = true;
                    }
                    else
                        logopen = false;
                }
                else if (selectedOption == 8)
                {
                    if (logopen == false)
                    {
                        string file = "KnownIssues";
                        log = new EnhancedTextBox();
                        StreamReader reader = new StreamReader("Content\\Common\\" + file + ".txt");
                        string text = reader.ReadToEnd();
                        reader.Close();
                        log.setText(text);
                        log.setTitle(file);
                        log.Show();
                        logopen = true;
                    }
                    else
                        logopen = false;
                }
                else if (selectedOption == 9)
                {
                    if (logopen == false)
                    {
                        string file = "ToDo";
                        log = new EnhancedTextBox();
                        StreamReader reader = new StreamReader("Content\\Common\\" + file + ".txt");
                        string text = reader.ReadToEnd();
                        reader.Close();
                        log.setText(text);
                        log.setTitle(file);
                        log.Show();
                        logopen = true;
                    }
                    else
                        logopen = false;
                }
                else if (selectedOption == 10)
                    state = Main.GameStates.exit;
            }
            #endregion

            return state;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Main Menu", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);
            
            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);

            return true;
        }
    }
}

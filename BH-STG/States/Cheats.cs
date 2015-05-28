/*
 * Component: States - Cheats Screen
 * Version: 1.0.4
 * Created: April 14th, 2014
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
    class Cheats : BH_STG.BarrageEngine.Screen.Menu
    {
        Initial_Loading loader;
        bool isReload = false;

        public void loadExtra(Initial_Loading iLoading)
        {
            loader = iLoading;
        }

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.cheats;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("(back)", true));
            options.Add(new Option("Enter New Code"));
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
                else if (selectedOption == 1)
                {
                    string inputstr = Microsoft.VisualBasic.Interaction.InputBox("Cheat Code: ", "Enter Cheat Code", "");

                    if (inputstr == "UnlockSecret")
                    {
                        isReload = true;
                        GameMain.gamesettings.setSecret(true);
                        GameMain.gamesettings.saveSettings();
                        loader.loadGameSettings(loader.basename + "\\GameSettings.bhe");
                        MessageBox.Show("Unlocked Secret Level!", "Cheat Code Confirmation", MessageBoxButtons.OK);
                    }
                    else if (inputstr == "DebugMode")
                    {
                        isReload = true;
                        GameMain.gamesettings.setTestMode(true);
                        GameMain.gamesettings.saveSettings();
                        loader.loadGameSettings(loader.basename + "\\GameSettings.bhe");
                        MessageBox.Show("Unlocked Debug Mode!", "Cheat Code Confirmation", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Invalid cheat code!", "Cheat Code Confirmation", MessageBoxButtons.OK);
                }
            }
            #endregion

            return state;
        }

        public bool reload()
        {
            if (isReload)
            {
                isReload = false;
                return true;
            }
            return false;
        }

        public void resetReload()
        {
            isReload = false;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Help", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            pos.Y += 100;
            if (GameMain.debugmode == true)
            {
                spriteBatch.DrawString(this.itemFont, "Available Cheats:", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, " - UnlockSecret: Unlocks secret character and level.", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, " - DebugMode: Sets game into debug mode.", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 40;
                spriteBatch.DrawString(this.itemFont, "Available Easter Egg Characters (set usernames to one of these):", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, " - von der borch: unlock the von der borch character.", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, " - antidavid: unlock the antidavid character.", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, " - Alazandar: unlock the Alazandar character.", new Vector2(main.videosettings.returnModifiedX(10),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
            }

            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

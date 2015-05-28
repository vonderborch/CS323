/*
 * Component: States - Pause Menu
 * Version: 1.1.7
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
    class Pause_Menu : BH_STG.BarrageEngine.Screen.Menu
    {
        Main.GameSettings gSettings = new Main.GameSettings();
        Main.VideoSettings vSettings = new Main.VideoSettings();

        public Game_Main game { get; set; }

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.pause_menu;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("Return to Game", true));
            options.Add(new Option("Input Method: "));
            options.Add(new Option("Music Volume: "));
            options.Add(new Option("Sound Effects Volume: "));
            options.Add(new Option("Performence: "));
            options.Add(new Option("Multisampling: "));
            options.Add(new Option("VSync: "));
            options.Add(new Option("Save Settings"));
            options.Add(new Option("Exit to Main Menu"));
        }

        public override Main.GameStates updateEnter(InputCommon input)
        {
            Main.GameStates state = this.thisState;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.game;
                else if (selectedOption == 8)
                    state = Main.GameStates.main_menu;
                else if (selectedOption == 7) // save settings
                {
                    GameMain.gamesettings = gSettings;
                    GameMain.videosettings = vSettings;
                    GameMain.gamesettings.saveSettings();
                    GameMain.videosettings.saveSettings();
                    MessageBox.Show("Settings saved!", "Save Confirmation", MessageBoxButtons.OK);
                }
            }
            #endregion

            #region left/right arrows
            if (input.Left.pressed && input.Left.pressedTicks == 0)
            {
                #region left
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 1) // input
                {
                    if (gSettings.inputMethod == "keyboard")
                        gSettings.setInputMethod("gamepad");
                    else if (gSettings.inputMethod == "gamepad")
                        gSettings.setInputMethod("keyboard");
                }
                else if (selectedOption == 2) // music
                    gSettings.setMusicVolume(gSettings.musicvolume - 5);
                else if (selectedOption == 3) // sound
                    gSettings.setSoundEffectsVolume(gSettings.soundeffectsvolume - 5);
                else if (selectedOption == 4) // performence
                {
                    int fps = vSettings.baseFPS;
                    fps -= 15;
                    if (fps <= 0)
                        fps = 120;
                    vSettings.setFPS(fps);
                }
                else if (selectedOption == 5) // Multisampling
                {
                    if (vSettings.multisample)
                        vSettings.setMULTISAMPLE(false);
                    else
                        vSettings.setMULTISAMPLE(true);
                }
                else if (selectedOption == 6) // VSync
                {
                    if (vSettings.vsync)
                        vSettings.setVSYNC(false);
                    else
                        vSettings.setVSYNC(true);
                }

                #endregion
            }
            else if (input.Right.pressed && input.Right.pressedTicks == 0)
            {
                #region right
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 1) // input
                {
                    if (gSettings.inputMethod == "keyboard")
                        gSettings.setInputMethod("gamepad");
                    else if (gSettings.inputMethod == "gamepad")
                        gSettings.setInputMethod("keyboard");
                }
                else if (selectedOption == 2) // music
                    gSettings.setMusicVolume(gSettings.musicvolume + 5);
                else if (selectedOption == 3) // sound
                    gSettings.setSoundEffectsVolume(gSettings.soundeffectsvolume + 5);
                else if (selectedOption == 4) // performence
                {
                    int fps = vSettings.baseFPS;
                    fps += 15;
                    if (fps >= 120)
                        fps = 15;
                    vSettings.setFPS(fps);
                }
                else if (selectedOption == 5) // Multisampling
                {
                    if (vSettings.multisample)
                        vSettings.setMULTISAMPLE(false);
                    else
                        vSettings.setMULTISAMPLE(true);
                }
                else if (selectedOption == 6) // VSync
                {
                    if (vSettings.vsync)
                        vSettings.setVSYNC(false);
                    else
                        vSettings.setVSYNC(true);
                }
                #endregion
            }

            #endregion

            #region update option fields
            if (selectedOption == 1)
                options[1] = new Option("Input Method: " + gSettings.inputMethod, true);
            else
                options[1] = new Option("Input Method: " + gSettings.inputMethod);
            if (selectedOption == 2)
                options[2] = new Option("Music Volume: " + gSettings.musicvolume.ToString(), true);
            else
                options[2] = new Option("Music Volume: " + gSettings.musicvolume.ToString());
            if (selectedOption == 3)
                options[3] = new Option("Sound Effects Volume: " + gSettings.soundeffectsvolume.ToString(), true);
            else
                options[3] = new Option("Sound Effects Volume: " + gSettings.soundeffectsvolume.ToString());
            if (selectedOption == 4)
                options[4] = new Option("Performence: " + vSettings.baseFPS.ToString(), true);
            else
                options[4] = new Option("Performence: " + vSettings.baseFPS.ToString());
            if (selectedOption == 5)
                options[5] = new Option("Multisampling: " + vSettings.multisample.ToString(), true);
            else
                options[5] = new Option("Multisampling: " + vSettings.multisample.ToString());
            if (selectedOption == 6)
                options[6] = new Option("VSync: " + vSettings.vsync.ToString(), true);
            else
                options[6] = new Option("VSync: " + vSettings.vsync.ToString());

            #endregion

            return state;
        }

        public void updateSettings()
        {
            gSettings = GameMain.gamesettings;
            vSettings = GameMain.videosettings;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Pause Menu", new Vector2(main.videosettings.returnModifiedX(10),
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

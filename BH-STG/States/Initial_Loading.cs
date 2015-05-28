/*
 * Component: States - Initial Loading
 * Version: 1.1.7
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BH_STG.States
{
    class Initial_Loading : BH_STG.BarrageEngine.Screen.Screen
    {
        Texture2D BGIMAGE;
        SpriteFont font;
        Color fontColor = Color.White;
        int loadTick = 0;
        string loadText = "";
        public string basename;

        public Main GameMain { get; set; }
        public bool loading(InputCommon linput)
        {
            //////// Load Fade Texture ////////
            this.FadeTexture = GameMain.Content.Load<Texture2D>("Graphics\\Other\\FadeBase.png");
            basename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();


            font = GameMain.Content.Load<SpriteFont>("Fonts\\General");
            linput.loading();

            /////// load bg image ////////
            this.BGIMAGE = GameMain.Content.Load<Texture2D>("Graphics\\MenuBackgrounds\\bsg-stars.png");

            return true;
        }
        public bool update(out Main.GameStates state, GraphicsDeviceManager graphics, Main_Menu lMainMenu, Replay_Menu lReplayMenu, 
                           HighScore_Menu lHighScores, Options_Menu lOptionsMenu, Game_Loading lGameLoading,
                           Pause_Menu lPauseMenu, Level_Selection lLevelSelection, Difficulty_Selection lDifficultySelection,
                           Victory lVictory, Defeat lDefeat, Character_Selection lCharacterSelection, About lAbout, Help lHelp, Cheats lCheats)
        {
            state = Main.GameStates.initial_loading;

            string MenuMusic = "Home Base Groove", MenuSoundEffect = "BEEP1A",
                   VictoryMusic = "Show Your Moves", VictorySoundEffect = MenuSoundEffect,
                   DefeatMusic = "Show Your Moves", DefeatSoundEffect = MenuSoundEffect;

            if (loadTick == 0)
            {
                // GoCart = Music
                // BEEP1A = the beep that plays when you use the arrow keys
                lMainMenu.loading(MenuMusic, MenuSoundEffect); // MUSIC HERE
                loadText = "Currently Loading: Main Menu";
            }
            else if (loadTick == 1)
            {
                lReplayMenu.loading();
                loadText = "Currently Loading: Replay Menu";
            }
            else if (loadTick == 2)
            {
                lHighScores.loading(MenuMusic, "BEEP1A");
                loadText = "Currently Loading: High Scores Screen";
            }
            else if (loadTick == 3)
            {
                lOptionsMenu.loading(MenuMusic, "BEEP1A");
                lOptionsMenu.loadExtra(this);
                loadText = "Currently Loading: Options Menu";
            }
            else if (loadTick == 4)
            {
                lGameLoading.loading();
                loadText = "Currently Loading: Game Loading Screen";
            }
            else if (loadTick == 5)
            {
                lPauseMenu.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: Pause Menu";
            }
            else if (loadTick == 6)
            {
                lLevelSelection.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: Level Selection Menu";
            }
            else if (loadTick == 7)
            {
                lDifficultySelection.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: Difficulty Selection Menu";
            }
            else if (loadTick == 8)
            {
                lCharacterSelection.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: Character Selection Menu";
            }
            else if (loadTick == 9)
            {
                lVictory.loading(VictoryMusic, VictorySoundEffect);
                lVictory.loadExtra(this);
                loadText = "Currently Loading: Victory Menu";
            }
            else if (loadTick == 10)
            {
                lDefeat.loading(DefeatMusic, DefeatSoundEffect);
                loadText = "Currently Loading: Defeat Menu";
            }
            else if (loadTick == 11)
            {
                lAbout.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: About Screen";
            }
            else if (loadTick == 12)
            {
                lHelp.loading(MenuMusic, MenuSoundEffect);
                loadText = "Currently Loading: Help Screen";
            }
            else if (loadTick == 13)
            {
                lCheats.loading(MenuMusic, MenuSoundEffect);
                lCheats.loadExtra(this);
                loadText = "Currently Loading: Cheats Screen";
            }
            else if (loadTick == 14)
            {
                Directory.CreateDirectory(basename + "\\Team Christian");
                basename += "\\Team Christian\\BH-STG";
                Directory.CreateDirectory(basename);
                Directory.CreateDirectory(basename + "\\Replays");
                loadText = "Creating File Directories";
            }
            else if (loadTick == 15)
            {
                string file = basename + "\\VideoSettings.bhe";
                GameMain.videosettings.setFilename(file);
                if (File.Exists(file))
                {
                    loadVideoSettings(file, graphics);
                    loadTick++;
                }
                loadText = "Loading Video Settings";
            }
            else if (loadTick == 16)
            {
                string file = basename + "\\VideoSettings.bhe";
                GameMain.videosettings.saveSettings();
                loadText = "Creating Default Video Settings";
            }
            else if (loadTick == 17)
            {
                string file = basename + "\\GameSettings.bhe";
                GameMain.gamesettings.setFilename(file);
                if (File.Exists(file))
                {
                    loadGameSettings(file);
                    loadTick++;
                }
                loadText = "Loading Game Settings";
            }
            else if (loadTick == 18)
            {
                string file = basename + "\\GameSettings.bhe";
                GameMain.gamesettings.saveSettings();
                loadText = "Creating Default Game Settings";
            }
            else
                state = Main.GameStates.main_menu;
            loadTick++;

            return true;
        }

        public void loadVideoSettings(string file, GraphicsDeviceManager graphics)
        {
            XmlNodeList[] settings = new XmlNodeList[7];
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            settings[0] = doc.GetElementsByTagName("width");
            settings[1] = doc.GetElementsByTagName("height");
            settings[2] = doc.GetElementsByTagName("fps");
            settings[3] = doc.GetElementsByTagName("fullscreen");
            settings[4] = doc.GetElementsByTagName("vsync");
            settings[5] = doc.GetElementsByTagName("multisample");
            settings[6] = doc.GetElementsByTagName("settingsversion");

            if (settings[6].Count == 0)
            {
                GameMain.videosettings.saveSettings(); // reset settings to new version
                MessageBox.Show("Old video settings file outdated, resetting to new defaults!", "Bad Settings file", MessageBoxButtons.OK);
            }
            else if (settings[6].Item(0).InnerText != GameMain.returnSettingsVersion())
            {
                GameMain.videosettings.saveSettings(); // reset settings to new version
                MessageBox.Show("Old video settings file outdated, resetting to new defaults!", "Bad Settings file", MessageBoxButtons.OK);
            }
            else
            {
                GameMain.videosettings.changeResolution(Convert.ToInt32(settings[0].Item(0).InnerText), Convert.ToInt32(settings[1].Item(0).InnerText));
                GameMain.videosettings.setFPS(Convert.ToInt32(settings[2].Item(0).InnerText));
                GameMain.videosettings.setFullscreen(Convert.ToBoolean(settings[3].Item(0).InnerText));
                GameMain.videosettings.setVSYNC(Convert.ToBoolean(settings[4].Item(0).InnerText));
                GameMain.videosettings.setMULTISAMPLE(Convert.ToBoolean(settings[5].Item(0).InnerText));
            }
        }

        public void loadGameSettings(string file)
        {
            XmlNodeList[] settings = new XmlNodeList[7];
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            settings[0] = doc.GetElementsByTagName("user");
            settings[1] = doc.GetElementsByTagName("inputMethod");
            settings[2] = doc.GetElementsByTagName("musicvolume");
            settings[3] = doc.GetElementsByTagName("soundeffectsvolume");
            settings[4] = doc.GetElementsByTagName("unlockedsecret");
            settings[5] = doc.GetElementsByTagName("testmode");
            settings[6] = doc.GetElementsByTagName("settingsversion");


            if (settings[6].Count == 0)
            {
                GameMain.gamesettings.saveSettings(); // reset settings to new version
                MessageBox.Show("Old game settings file outdated, resetting to new defaults!", "Bad Settings file", MessageBoxButtons.OK);
            }
            else if (settings[6].Item(0).InnerText != GameMain.returnSettingsVersion())
            {
                GameMain.gamesettings.saveSettings(); // reset settings to new version
                MessageBox.Show("Old game settings file outdated, resetting to new defaults!", "Bad Settings file", MessageBoxButtons.OK);
            }
            else
            {
                GameMain.gamesettings.setUser(settings[0].Item(0).InnerText);
                GameMain.gamesettings.setInputMethod(settings[1].Item(0).InnerText);
                GameMain.gamesettings.setMusicVolume(Convert.ToInt32(settings[2].Item(0).InnerText));
                GameMain.gamesettings.setSoundEffectsVolume(Convert.ToInt32(settings[3].Item(0).InnerText));
                GameMain.gamesettings.setSecret(Convert.ToBoolean(settings[4].Item(0).InnerText));
                GameMain.gamesettings.setTestMode(Convert.ToBoolean(settings[5].Item(0).InnerText));
            }
        }

        public bool draw(SpriteBatch spriteBatch)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw temp text
            spriteBatch.DrawString(font, "Loading the game...", new Vector2(GameMain.videosettings.returnModifiedX(GameMain.videosettings.width / 2),
                                   GameMain.videosettings.returnModifiedY(GameMain.videosettings.height / 2)), fontColor);
            spriteBatch.DrawString(font, loadText, new Vector2(GameMain.videosettings.returnModifiedX(GameMain.videosettings.width / 2),
                                   GameMain.videosettings.returnModifiedY(GameMain.videosettings.height / 2 + 20)), fontColor);
            return true;
        }
    }
}

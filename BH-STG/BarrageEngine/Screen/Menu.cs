/*
 * Component: Screen System - Menu Base
 * Version: 1.0.2
 * Created: March 27th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Audio;
using BH_STG.BarrageEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BH_STG.BarrageEngine.Screen
{
    class Menu : Screen
    {
        protected Texture2D BGIMAGE;
        protected SpriteFont titleFont, itemFont;
        protected Music bgMusic = new Music();
        protected SoundFX beep = new SoundFX();
        protected string music_name, beep_name;
        bool hasStartedMusic;
        protected Color fontColor = Color.White;

        #region menu options
        #region options variable
        public struct Option
        {
            public bool selected;
            public string text;
            string basetext;

            public Option(string txt, bool sel = false)
            {
                selected = sel;
                text = txt;
                basetext = txt;
            }

            public void updateText(string txt)
            {
                text = basetext + txt;
            }

            public void newText(string txt)
            {
                text = txt;
                basetext = txt;
            }

            public void isSelected()
            {
                selected = true;
            }
            public void isNotSelected()
            {
                selected = false;
            }

            public void draw(SpriteFont font, SpriteBatch spriteBatch, Vector2 pos)
            {
                Color color = Color.White;
                if (selected)
                    color = Color.Gold;

                spriteBatch.DrawString(font, text, pos, color);
            }
        }
        #endregion

        protected List<Option> options = new List<Option>();
        protected int selectedOption = 0;

        #endregion

        protected Main GameMain;

        public void setGameMain(Main main)
        {
            this.GameMain = main;
        }
        
        public Menu(int max_frames = 30)
        {
            this.transition_frames = max_frames;
            useGameMainFunctionSetter = true;
        }

        public void loading(string musicName, string beepName)
        {
            //////// Load Fade Texture ////////
            this.FadeTexture = GameMain.Content.Load<Texture2D>("Graphics\\Other\\FadeBase.png");

            /////// load bg image ////////
            this.BGIMAGE = GameMain.Content.Load<Texture2D>("Graphics\\MenuBackgrounds\\bsg-stars.png");

            // load fonts
            titleFont = GameMain.Content.Load<SpriteFont>("Fonts\\Menu Title");
            itemFont = GameMain.Content.Load<SpriteFont>("Fonts\\Menu Item");

            // load music
            bgMusic.GameMain = this.GameMain;
            music_name = musicName;
            bgMusic.loadContent(music_name);
            hasStartedMusic = false;

            // load soundfx
            beep.GameMain = this.GameMain;
            beep_name = beepName;
            beep.loadContent(beep_name);

            loadMenu();
        }

        public virtual void loadMenu() { }

        public virtual bool update(out Main.GameStates state, InputCommon input)
        {
            updateSelection(input);
            state = updateEnter(input);

            return true;
        }

        public void updateSelection(InputCommon input)
        {
            bool updateOptions = false;

            #region Menu Item Selection
            if (input.Up.pressed && input.Up.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                updateOptions = true;
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = options.Count - 1;
            }
            if (input.Down.pressed && input.Down.pressedTicks == 0)
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
                    if (i == selectedOption)
                        sel = true;
                    options[i] = new Option(txt, sel);
                }
            }

            #endregion
        }

        public virtual Main.GameStates updateEnter(InputCommon input)
        {
            return this.thisState;
        }

        public bool isPlayingMusic()
        {
            return hasStartedMusic;
        }

        public void playMusic(int volume)
        {
            if (!hasStartedMusic)
            {
                bgMusic.playSong(music_name, true, volume, 0.0f);
                hasStartedMusic = true;
            }
        }

        public void stopMusic()
        {
            hasStartedMusic = false;
            bgMusic.stopMusic();
        }

        public void updateMusicVolume(int volume)
        {
            if (hasStartedMusic)
                bgMusic.changeVolume(volume, 0.0f);
        }
    }
}

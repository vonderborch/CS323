/*
 * Component: Screen System - Screen Base
 * Version: 1.0.2
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.BarrageEngine.Screen
{
    class Screen
    {
        protected int transition_tick = 0,
                      transition_frames = 30;
        protected Texture2D FadeTexture;
        protected Main.GameStates thisState;
        protected bool useGameMainFunctionSetter = false;

        public enum Screens
        {
            initial_loading,
            main_menu,
            help,
            about,
            cheats,
            options_menu,
            highscore_menu,
            replay_menu,
            level_selection,
            difficulty_selection,
            character_selection,
            game_loading,
            game,
            pause_menu,
            victory,
            defeat,
            exit
        }

        public Screen(int max_frames = 30)
        {
            transition_frames = max_frames;
        }

        public void setState(Main.GameStates state)
        {
            thisState = state;
        }

        public bool returnGameMainFunctionSetter()
        {
            return useGameMainFunctionSetter;
        }

        public bool UpdateTransitionIn()
        {
            transition_tick++;
            if (transition_tick == transition_frames)
            {
                transition_tick = 0;
                return false;
            }
            return true;
        }

        public bool UpdateTransitionOut()
        {
            transition_tick++;
            if (transition_tick == transition_frames)
            {
                transition_tick = 0;
                return false;
            }
            return true;
        }

        public void DrawTransitionIn(SpriteBatch spritebatch)
        {
            float alphaPercent = (float)transition_tick / transition_frames;
            Color alphaColor = new Color(Color.Black, 1 - alphaPercent);

            spritebatch.Draw(FadeTexture, new Rectangle(0, 0, 1280, 720), alphaColor);
        }

        public void DrawTransitionOut(SpriteBatch spritebatch)
        {
            float alphaPercent = (float)transition_tick / transition_frames;
            Color alphaColor = new Color(Color.Black, alphaPercent);

            spritebatch.Draw(FadeTexture, new Rectangle(0, 0, 1280, 720), alphaColor);
        }
    }
}

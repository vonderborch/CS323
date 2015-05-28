/*
 * Component: States - About Screen
 * Version: 1.0.7
 * Created: March 28th, 2014
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
    class Help : Menu
    {
        public override void loadMenu()
        {
            this.thisState = Main.GameStates.help;

            // add menu options
            selectedOption = 0;
            options.Add(new Option("(back)", true));
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

            pos.Y += 10;
            spriteBatch.DrawString(this.itemFont, "Goal: Destroy all enemies and bosses!", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                   main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
            pos.Y += 20;
            spriteBatch.DrawString(this.itemFont, "Different items will occassionally drop, providing some form of a benefit.", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                   main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
            pos.Y += 20;
            spriteBatch.DrawString(this.itemFont, "To lose the game, you must lose all of your health AND all your lives.", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                   main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
            pos.Y += 40;
            if (GameMain.gamesettings.inputMethod == "keyboard")
            {
                spriteBatch.DrawString(this.itemFont, "Default Keyboard Controls: ", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 30;
                spriteBatch.DrawString(this.itemFont, "   - WASD or Arrow Keys: Move player in direction", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, "   - Spacebar: Use bomb", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 30;
            }
            else
            {
                spriteBatch.DrawString(this.itemFont, "Default Gamepad Controls: ", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 30;
                spriteBatch.DrawString(this.itemFont, "   - Left thumbstick: Move player in direction", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 20;
                spriteBatch.DrawString(this.itemFont, "   - A: Use bomb", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                       main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);
                pos.Y += 30;
            }
            spriteBatch.DrawString(this.itemFont, "NOTE: input settings can be modified in the Content\\Common\\input.xml file!", new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                   main.videosettings.returnModifiedY((int)pos.Y)), this.fontColor);


            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

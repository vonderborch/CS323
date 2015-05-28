/*
 * Component: Input System - Mouse
 * Version: 1.2.1
 * Created: September 9th, 2013
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BH_STG.BarrageEngine.Input
{
    class InputMouse
    {
        public MouseState mouseState;
        public int scrollValue = 0;
        public Vector2 coords = new Vector2(0,0);
        public InputCommon.Key LeftButton, RightButton, MiddleButton, XButton1, XButton2;

        public Main GameMain { get; set; }
        int maxLastPressedTicks, maxPressedTicks, resetPressedTicks;

        public InputMouse(int lastPressedTicks = 600, int pressedTicks = 600, int resetTicks = -1)
        {
            maxLastPressedTicks = lastPressedTicks;
            maxPressedTicks = pressedTicks;
            resetPressedTicks = resetTicks;
        }

        public void update()
        {
            mouseState = Mouse.GetState();

            #region update scroll value
            // scroll value
            scrollValue = mouseState.ScrollWheelValue;

            #endregion

            #region update cursor position
            // cursor position
            coords.X = mouseState.X;
            coords.Y = mouseState.Y;

            #endregion

            #region update buttons
            // left button pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
                LeftButton.isPressed(maxPressedTicks, resetPressedTicks);
            else
                LeftButton.isNotPressed(maxLastPressedTicks);

            // right button pressed
            if (mouseState.RightButton == ButtonState.Pressed)
                RightButton.isPressed(maxPressedTicks, resetPressedTicks);
            else
                RightButton.isNotPressed(maxLastPressedTicks);

            // middle button pressed
            if (mouseState.MiddleButton == ButtonState.Pressed)
                MiddleButton.isPressed(maxPressedTicks, resetPressedTicks);
            else
                MiddleButton.isNotPressed(maxLastPressedTicks);

            // XButton1 pressed
            if (mouseState.XButton1 == ButtonState.Pressed)
                XButton1.isPressed(maxPressedTicks, resetPressedTicks);
            else
                XButton1.isNotPressed(maxLastPressedTicks);

            // XButton2 pressed
            if (mouseState.XButton2 == ButtonState.Pressed)
                XButton2.isPressed(maxPressedTicks, resetPressedTicks);
            else
                XButton2.isNotPressed(maxLastPressedTicks);

            #endregion

            Mouse.SetPosition(GameMain.videosettings.width / 2, GameMain.videosettings.height / 2);
        }

        public bool checkKey(string key)
        {
            bool isPressed = false;

            switch (key)
            {
                #region special keys
                case "LeftClick":
                    if (LeftButton.pressed)
                        isPressed = true;
                    break;
                case "RightClick":
                    if (RightButton.pressed)
                        isPressed = true;
                    break;
                case "MiddleClick":
                    if (MiddleButton.pressed)
                        isPressed = true;
                    break;
                case "XButton1":
                    if (XButton1.pressed)
                        isPressed = true;
                    break;
                case "XButton2":
                    if (XButton2.pressed)
                        isPressed = true;
                    break;
                #endregion
            }

            return isPressed;
        }
    }
}

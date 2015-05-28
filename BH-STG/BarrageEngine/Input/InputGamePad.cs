/*
 * Component: Input System - GamePad (Xbox 360-style)
 * Version: 1.1.6
 * Created: Febraury 21st, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BH_STG.BarrageEngine.Input
{
    class InputGamePad
    {
        public GamePadState padState;
        public Vector2 LeftThumbStick = new Vector2(0, 0), RightThumbStick = new Vector2(0, 0);
        public float LeftTrigger = 0.0f, RightTrigger = 0.0f;
        public InputCommon.Key Back, Start, A, B, X, Y, BigButton, LeftBumper, RightBumper,
                               LeftStick, RightStick, DPadUp, DPadDown, DPadLeft, DPadRight;
        public bool connected = false;

        int maxLastPressedTicks, maxPressedTicks, resetPressedTicks;

        public InputGamePad(int lastPressedTicks = 600, int pressedTicks = 600, int resetTicks = -1)
        {
            maxLastPressedTicks = lastPressedTicks;
            maxPressedTicks = pressedTicks;
            resetPressedTicks = resetTicks;
        }

        public void update()
        {
            connected = false;
            padState = GamePad.GetState(PlayerIndex.One);
            if (padState.IsConnected)
            {
                connected = true;

                #region update thumbsticks
                LeftThumbStick = padState.ThumbSticks.Left;
                RightThumbStick = padState.ThumbSticks.Right;

                #endregion

                #region update triggers
                LeftTrigger = padState.Triggers.Left;
                RightTrigger = padState.Triggers.Right;

                #endregion

                #region update dPad
                // Up
                if (padState.DPad.Up == ButtonState.Pressed)
                    DPadUp.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    DPadUp.isNotPressed(maxLastPressedTicks);
                // Down
                if (padState.DPad.Down == ButtonState.Pressed)
                    DPadDown.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    DPadDown.isNotPressed(maxLastPressedTicks);
                // Left
                if (padState.DPad.Left == ButtonState.Pressed)
                    DPadLeft.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    DPadLeft.isNotPressed(maxLastPressedTicks);
                // Right
                if (padState.DPad.Right == ButtonState.Pressed)
                    DPadRight.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    DPadRight.isNotPressed(maxLastPressedTicks);
                #endregion

                #region update buttons
                // A
                if (padState.Buttons.A == ButtonState.Pressed)
                    A.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    A.isNotPressed(maxLastPressedTicks);

                // B
                if (padState.Buttons.B == ButtonState.Pressed)
                    B.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    B.isNotPressed(maxLastPressedTicks);

                // X
                if (padState.Buttons.X == ButtonState.Pressed)
                    X.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    X.isNotPressed(maxLastPressedTicks);

                // Y
                if (padState.Buttons.Y == ButtonState.Pressed)
                    Y.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    Y.isNotPressed(maxLastPressedTicks);

                // Start
                if (padState.Buttons.Start == ButtonState.Pressed)
                    Start.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    Start.isNotPressed(maxLastPressedTicks);

                // Back
                if (padState.Buttons.Back == ButtonState.Pressed)
                    Back.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    Back.isNotPressed(maxLastPressedTicks);

                // Big Button
                if (padState.Buttons.BigButton == ButtonState.Pressed)
                    BigButton.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    BigButton.isNotPressed(maxLastPressedTicks);

                // Left Shoulder
                if (padState.Buttons.LeftShoulder == ButtonState.Pressed)
                    LeftBumper.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    LeftBumper.isNotPressed(maxLastPressedTicks);

                // Right Shoulder
                if (padState.Buttons.RightShoulder == ButtonState.Pressed)
                    RightBumper.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    RightBumper.isNotPressed(maxLastPressedTicks);

                // Left Stick
                if (padState.Buttons.LeftStick == ButtonState.Pressed)
                    LeftStick.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    LeftStick.isNotPressed(maxLastPressedTicks);

                // Right Stick
                if (padState.Buttons.RightStick == ButtonState.Pressed)
                    RightStick.isPressed(maxPressedTicks, resetPressedTicks);
                else
                    RightStick.isNotPressed(maxLastPressedTicks);
                #endregion
            }
        }

        public void setVibration(float leftMotor, float rightMotor)
        {
            try
            {
                SharpDX.XInput.Controller cont = new SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.One);

                SharpDX.XInput.Vibration vib;
                var result = cont.SetVibration(new SharpDX.XInput.Vibration
                {
                    LeftMotorSpeed = (ushort)(leftMotor * ushort.MaxValue),
                    RightMotorSpeed = (ushort)(rightMotor * ushort.MaxValue),
                });
            }
            catch { }
        }

        public bool checkKey(string key)
        {
            bool isPressed = false;

            switch (key)
            {
                #region DPad
                case "DPadUp":
                    if (DPadUp.pressed)
                        isPressed = true;
                    break;
                case "DPadDown":
                    if (DPadDown.pressed)
                        isPressed = true;
                    break;
                case "DPadLeft":
                    if (DPadLeft.pressed)
                        isPressed = true;
                    break;
                case "DPadRight":
                    if (DPadRight.pressed)
                        isPressed = true;
                    break;
                #endregion

                #region Buttons
                case "A":
                    if (A.pressed)
                        isPressed = true;
                    break;
                case "B":
                    if (B.pressed)
                        isPressed = true;
                    break;
                case "X":
                    if (X.pressed)
                        isPressed = true;
                    break;
                case "Y":
                    if (Y.pressed)
                        isPressed = true;
                    break;
                case "Start":
                    if (Start.pressed)
                        isPressed = true;
                    break;
                case "Back":
                    if (Back.pressed)
                        isPressed = true;
                    break;
                case "BigButton":
                    if (BigButton.pressed)
                        isPressed = true;
                    break;
                case "LeftBumper":
                    if (LeftBumper.pressed)
                        isPressed = true;
                    break;
                case "RightBumper":
                    if (RightBumper.pressed)
                        isPressed = true;
                    break;
                case "LeftStick":
                    if (LeftStick.pressed)
                        isPressed = true;
                    break;
                case "RightStick":
                    if (RightStick.pressed)
                        isPressed = true;
                    break;
                #endregion
            }

            return isPressed;
        }
    }
}

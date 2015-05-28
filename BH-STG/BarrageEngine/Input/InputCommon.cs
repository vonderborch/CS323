/*
 * Component: Input System - Common
 * Version: 1.1.4
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: April 25th, 2014
 * Last Updated By: Jacob
*/

using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Xml;

namespace BH_STG.BarrageEngine.Input
{
    public class InputCommon
    {
        #region key variable type
        public struct Key
        {
            public bool pressed;
            public int pressedTicks, sinceLastPressedTicks;

            public Key(bool p = false, int pt = 0, int slpt = 0)
            {
                pressed = p;
                pressedTicks = pt;
                sinceLastPressedTicks = slpt;
            }
            public void isPressed(int MaxPressedTicks = 600, int resetTicks = -1)
            {
                if (!pressed)
                {
                    pressedTicks = -1;
                    sinceLastPressedTicks = 0;
                }
                pressed = true;
                pressedTicks++;
                if (pressedTicks > MaxPressedTicks)
                    pressedTicks = MaxPressedTicks;
                if (resetTicks != -1 && pressedTicks == resetTicks)
                    pressedTicks = 0;
            }
            public void isNotPressed(int lastPressedTicks = 600)
            {
                if (pressed)
                {
                    pressedTicks = 0;
                    sinceLastPressedTicks = -1;
                }
                pressed = false;
                sinceLastPressedTicks++;
                if (sinceLastPressedTicks > lastPressedTicks)
                    sinceLastPressedTicks = lastPressedTicks;
            }
        }

        #endregion

        #region InputCommand variable type
        struct InputCommand
        {
            public string command, key;
            public char type; // K = keyboard, M = Mouse, G = Gamepad

            public InputCommand(string cmd, char tpe, string ky)
            {
                command = cmd;
                type = tpe;
                key = ky;
            }
        }

        #endregion

        #region Input Method enum
        public enum InputMethod
        {
            keyboard,
            mouse,
            gamepad
        }

        #endregion

        List<InputCommand> inputs = new List<InputCommand>();
        InputMouse mouse;
        InputGamePad pad;
        InputKeyboard keyboard;
        InputMethod defaultInput = InputMethod.keyboard;
        public Key Up, Down, Left, Right, 
                   UseItem, UseSafety,
                   Accept, Cancel;
        public Vector2 AnalogMovement, LastAnalogMovement, CommandVector;
        Vector2 lastMouseMovement = new Vector2(0,0);
        static int vibrationBase = 20;

        int maxLastPressedTicks, maxPressedTicks, resetPressedTicks, vibrationTicks;
        public Main GameMain { get; set; }

        public InputCommon(int lastPressedTicks = 600, int pressedTicks = 600, int resetTicks = -1)
        {
            maxLastPressedTicks = lastPressedTicks;
            maxPressedTicks = pressedTicks;
            resetPressedTicks = resetTicks;
            vibrationTicks = 0;
        }

        public void loading()
        {
            #region define the input method variables
            mouse = new InputMouse(maxLastPressedTicks, maxPressedTicks, resetPressedTicks);
            mouse.GameMain = GameMain;
            pad = new InputGamePad(maxLastPressedTicks, maxPressedTicks, resetPressedTicks);
            keyboard = new InputKeyboard(maxLastPressedTicks, maxPressedTicks, resetPressedTicks);

            #endregion

            #region define the various accessable input command variables
            Up = new Key();
            Down = new Key();
            Left = new Key();
            Right = new Key();
            UseItem = new Key();
            UseSafety = new Key();
            Accept = new Key();
            Cancel = new Key();
            AnalogMovement = new Vector2();
            LastAnalogMovement = new Vector2();
            CommandVector = new Vector2();

            #endregion

            #region load the XML doc containing input binding settings
            XmlNodeList[] output = new XmlNodeList[9];

            // create and load an XML document variable
            XmlDocument doc = new XmlDocument();
            doc.Load("Content\\Common\\Input.xml");

            // Get NodeLists for each component of the XML File
            output[0] = doc.GetElementsByTagName("Up");
            output[1] = doc.GetElementsByTagName("Down");
            output[2] = doc.GetElementsByTagName("Left");
            output[3] = doc.GetElementsByTagName("Right");
            output[4] = doc.GetElementsByTagName("UseItem");
            output[5] = doc.GetElementsByTagName("UseSafety");
            output[6] = doc.GetElementsByTagName("Accept");
            output[7] = doc.GetElementsByTagName("Cancel");
            output[8] = doc.GetElementsByTagName("AnalogMovement");

            //// process the inputs
            // up
            addInputs(output[0], "Up");
            // down
            addInputs(output[1], "Down");
            // Left
            addInputs(output[2], "Left");
            // Right
            addInputs(output[3], "Right");
            // UseItem
            addInputs(output[4], "UseItem");
            // UseSafety
            addInputs(output[5], "UseSafety");
            // Accept
            addInputs(output[6], "Accept");
            // Cancel
            addInputs(output[7], "Cancel");
            // AnalogMovement
            addInputs(output[8], "AnalogMovement");

            #endregion
        }

        private void addInputs(XmlNodeList xmlinputs, string command)
        {
            for (int i = 0; i < xmlinputs.Count; i++)
            {
                foreach (XmlNode node in xmlinputs.Item(i))
                {
                    if (node.Name == "Keyboard")
                        inputs.Add(new InputCommand(command, 'K', node.InnerText));
                    else if (node.Name == "Mouse")
                        inputs.Add(new InputCommand(command, 'M', node.InnerText));
                    else if (node.Name == "GamePad")
                        inputs.Add(new InputCommand(command, 'G', node.InnerText));
                }
            }
        }

        public void update(InputMethod InputMode = InputMethod.keyboard, float LeftMotorVibration = 1.0f, float RightMotorVibration = 1.0f)
        {
            keyboard.update();
            if (InputMode == InputMethod.mouse)
                mouse.update();
            pad.update();

            bool up = checkKeys("Up", InputMode), down = checkKeys("Down", InputMode), left = checkKeys("Left", InputMode), right = checkKeys("Right", InputMode),
                 useitem = checkKeys("UseItem", InputMode), usesafety = checkKeys("UseSafety", InputMode),
                 accept = checkKeys("Accept", InputMode), cancel = checkKeys("Cancel", InputMode);

            #region update button commands
            // Up
            if (up)
                Up.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Up.isNotPressed(maxLastPressedTicks);
            // Down
            if (down)
                Down.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Down.isNotPressed(maxLastPressedTicks);
            // Left
            if (left)
                Left.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Left.isNotPressed(maxLastPressedTicks);
            // Right
            if (right)
                Right.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Right.isNotPressed(maxLastPressedTicks);
            // UseItem
            if (useitem)
                UseItem.isPressed(maxPressedTicks, resetPressedTicks);
            else
                UseItem.isNotPressed(maxLastPressedTicks);
            // UseSafety
            if (usesafety)
                UseSafety.isPressed(maxPressedTicks, resetPressedTicks);
            else
                UseSafety.isNotPressed(maxLastPressedTicks);
            // Accept
            if (accept)
                Accept.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Accept.isNotPressed(maxLastPressedTicks);
            // Cancel
            if (cancel)
                Cancel.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Cancel.isNotPressed(maxLastPressedTicks);

            #endregion

            #region update analog movement controls
            // AnalogMovement
            LastAnalogMovement = AnalogMovement;
            AnalogMovement = checkVector2("AnalogMovement", InputMode);

            #endregion

            #region update controller vibration
            if (pad.connected && InputMode == InputMethod.gamepad)
            {
                if (vibrationTicks > 0)
                {
                    vibrationTicks--;
                    pad.setVibration(LeftMotorVibration, RightMotorVibration);
                }
                else
                    pad.setVibration(0, 0);
            }

            #endregion

            #region Update CommandVector (movement vector)
            CommandVector.X = 0.0f;
            CommandVector.Y = 0.0f;
            if (InputMode == InputMethod.keyboard)
            {
                if (up && !down)
                    CommandVector.Y = -1.0f;
                else if (down && !up)
                    CommandVector.Y = 1.0f;
                if (left && !right)
                    CommandVector.X = -1.0f;
                else if (right && !left)
                    CommandVector.X = 1.0f;

                if (up && left)
                {
                    CommandVector.X = -0.707106781f;
                    CommandVector.Y = -0.707106781f;
                }
                else if (up && right)
                {
                    CommandVector.X = 0.707106781f;
                    CommandVector.Y = -0.707106781f;
                }
                if (down && left)
                {
                    CommandVector.X = -0.707106781f;
                    CommandVector.Y = 0.707106781f;
                }
                else if (down && right)
                {
                    CommandVector.X = 0.707106781f;
                    CommandVector.Y = 0.707106781f;
                }
            }
            else if (InputMode == InputMethod.mouse)
            {
                // TODO: Make this a -1.0 to 1.0 representation of the current direction.
                CommandVector.X = AnalogMovement.X;
                CommandVector.Y = AnalogMovement.Y;
            }
            else if (InputMode == InputMethod.gamepad)
            {
                CommandVector.X = AnalogMovement.X;
                CommandVector.Y = AnalogMovement.Y;
            }

            #endregion
        }

        public void controllerVibration(InputMethod InputMode = InputMethod.keyboard, float LeftMotorVibration = 0.0f, float RightMotorVibration = 0.0f)
        {
            if (pad != null)
            {
                if (pad.connected && InputMode == InputMethod.gamepad)
                    vibrationTicks = vibrationBase;
            }
        }

        public void resetVibration()
        {
            vibrationTicks = 0;
        }

        private bool checkKeys(string command, InputMethod SelectedInputMode)
        {
            bool isPressed = false;
            foreach (InputCommand input in inputs)
            {
                if (input.command == command)
                {
                    // check keyboard input
                    if (input.type == 'K' && (SelectedInputMode == InputMethod.keyboard || defaultInput == InputMethod.keyboard))
                    {
                        isPressed = keyboard.checkKey(input.key);
                    }
                    // check mouse input
                    else if (input.type == 'M' && (SelectedInputMode == InputMethod.mouse || defaultInput == InputMethod.mouse))
                    {
                        isPressed = mouse.checkKey(input.key);
                    }
                    // check gamepad input
                    else if (input.type == 'G' && (SelectedInputMode == InputMethod.gamepad || defaultInput == InputMethod.gamepad))
                    {
                        isPressed = pad.checkKey(input.key);
                    }

                    if (isPressed)
                        break;
                }
            }
            return isPressed;
        }

        private Vector2 checkVector2(string command, InputMethod SelectedInputMode)
        {
            Vector2 mousemovement = new Vector2(), padmovement = new Vector2();
            foreach (InputCommand input in inputs)
            {
                if (input.command == command)
                {
                    // check mouse input
                    if (input.type == 'M' && SelectedInputMode == InputMethod.mouse)
                    {
                        mousemovement.X = mouse.coords.X - lastMouseMovement.X;
                        mousemovement.Y = mouse.coords.Y - lastMouseMovement.Y;
                        mousemovement.Normalize();
                        if (float.IsNaN(mousemovement.X))
                            mousemovement.X = 0;
                        if (float.IsNaN(mousemovement.Y))
                            mousemovement.Y = 0;

                        lastMouseMovement = mouse.coords;
                    }
                    // check gamepad input
                    else if (input.type == 'G' && SelectedInputMode == InputMethod.gamepad)
                    {
                        if (input.key == "LeftThumbStick")
                            padmovement = pad.LeftThumbStick;
                        else if (input.key == "RightThumbStick")
                            padmovement = pad.RightThumbStick;
                        padmovement.Y = -padmovement.Y;
                    }
                }
            }
            if (SelectedInputMode == InputMethod.mouse)
                return mousemovement;
            else
                return padmovement;
        }
    }
}

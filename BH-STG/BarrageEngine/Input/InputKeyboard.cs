/*
 * Component: Input System - Keyboard
 * Version: 1.2.1
 * Created: September 9th, 2013
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework.Input;

namespace BH_STG.BarrageEngine.Input
{
    class InputKeyboard
    {

        KeyboardState keyState;
        #region keys
        public InputCommon.Key Enter = new InputCommon.Key();
        public InputCommon.Key Escape = new InputCommon.Key();
        public InputCommon.Key SpaceBar = new InputCommon.Key();
        public InputCommon.Key Up = new InputCommon.Key();
        public InputCommon.Key Down = new InputCommon.Key();
        public InputCommon.Key Left = new InputCommon.Key();
        public InputCommon.Key Right = new InputCommon.Key();
        public InputCommon.Key A = new InputCommon.Key();
        public InputCommon.Key B = new InputCommon.Key();
        public InputCommon.Key C = new InputCommon.Key();
        public InputCommon.Key D = new InputCommon.Key();
        public InputCommon.Key E = new InputCommon.Key();
        public InputCommon.Key F = new InputCommon.Key();
        public InputCommon.Key G = new InputCommon.Key();
        public InputCommon.Key H = new InputCommon.Key();
        public InputCommon.Key I = new InputCommon.Key();
        public InputCommon.Key J = new InputCommon.Key();
        public InputCommon.Key K = new InputCommon.Key();
        public InputCommon.Key L = new InputCommon.Key();
        public InputCommon.Key M = new InputCommon.Key();
        public InputCommon.Key N = new InputCommon.Key();
        public InputCommon.Key O = new InputCommon.Key();
        public InputCommon.Key P = new InputCommon.Key();
        public InputCommon.Key Q = new InputCommon.Key();
        public InputCommon.Key R = new InputCommon.Key();
        public InputCommon.Key S = new InputCommon.Key();
        public InputCommon.Key T = new InputCommon.Key();
        public InputCommon.Key U = new InputCommon.Key();
        public InputCommon.Key V = new InputCommon.Key();
        public InputCommon.Key W = new InputCommon.Key();
        public InputCommon.Key X = new InputCommon.Key();
        public InputCommon.Key Y = new InputCommon.Key();
        public InputCommon.Key Z = new InputCommon.Key();

        #endregion

        #region global variables
        int maxLastPressedTicks;
        int maxPressedTicks;
        int resetPressedTicks;

        #endregion

        public InputKeyboard(int lastPressedTicks = 600, int pressedTicks = 600, int resetTicks = -1)
        {
            maxLastPressedTicks = lastPressedTicks;
            maxPressedTicks = pressedTicks;
            resetPressedTicks = resetTicks;
        }

        public void update()
        {
            keyState = Keyboard.GetState();

            //// Special Keys ////
            #region special keys
            // Escape
            if (keyState.IsKeyDown(Keys.Escape))
                Escape.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Escape.isNotPressed(maxLastPressedTicks);
            // Enter
            if (keyState.IsKeyDown(Keys.Enter))
                Enter.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Enter.isNotPressed(maxLastPressedTicks);
            // SpaceBar
            if (keyState.IsKeyDown(Keys.Space))
                SpaceBar.isPressed(maxPressedTicks, resetPressedTicks);
            else
                SpaceBar.isNotPressed(maxLastPressedTicks);

            #endregion

            //// Arrow Keys ////
            #region arrow keys
            // Up
            if (keyState.IsKeyDown(Keys.Up))
                Up.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Up.isNotPressed(maxLastPressedTicks);
            // Down
            if (keyState.IsKeyDown(Keys.Down))
                Down.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Down.isNotPressed(maxLastPressedTicks);
            // Left
            if (keyState.IsKeyDown(Keys.Left))
                Left.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Left.isNotPressed(maxLastPressedTicks);
            // Right
            if (keyState.IsKeyDown(Keys.Right))
                Right.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Right.isNotPressed(maxLastPressedTicks);

            #endregion

            //// Letter Keys ////
            #region letter keys
            // A
            if (keyState.IsKeyDown(Keys.A))
                A.isPressed(maxPressedTicks, resetPressedTicks);
            else
                A.isNotPressed(maxLastPressedTicks);
            // B
            if (keyState.IsKeyDown(Keys.B))
                B.isPressed(maxPressedTicks, resetPressedTicks);
            else
                B.isNotPressed(maxLastPressedTicks);
            // C
            if (keyState.IsKeyDown(Keys.C))
                C.isPressed(maxPressedTicks, resetPressedTicks);
            else
                C.isNotPressed(maxLastPressedTicks);
            // D
            if (keyState.IsKeyDown(Keys.D))
                D.isPressed(maxPressedTicks, resetPressedTicks);
            else
                D.isNotPressed(maxLastPressedTicks);
            // E
            if (keyState.IsKeyDown(Keys.E))
                E.isPressed(maxPressedTicks, resetPressedTicks);
            else
                E.isNotPressed(maxLastPressedTicks);
            // F
            if (keyState.IsKeyDown(Keys.F))
                F.isPressed(maxPressedTicks, resetPressedTicks);
            else
                F.isNotPressed(maxLastPressedTicks);
            // G
            if (keyState.IsKeyDown(Keys.G))
                G.isPressed(maxPressedTicks, resetPressedTicks);
            else
                G.isNotPressed(maxLastPressedTicks);
            // H
            if (keyState.IsKeyDown(Keys.H))
                H.isPressed(maxPressedTicks, resetPressedTicks);
            else
                H.isNotPressed(maxLastPressedTicks);
            // I
            if (keyState.IsKeyDown(Keys.I))
                I.isPressed(maxPressedTicks, resetPressedTicks);
            else
                I.isNotPressed(maxLastPressedTicks);
            // J
            if (keyState.IsKeyDown(Keys.J))
                J.isPressed(maxPressedTicks, resetPressedTicks);
            else
                J.isNotPressed(maxLastPressedTicks);
            // K
            if (keyState.IsKeyDown(Keys.K))
                K.isPressed(maxPressedTicks, resetPressedTicks);
            else
                K.isNotPressed(maxLastPressedTicks);
            // L
            if (keyState.IsKeyDown(Keys.L))
                L.isPressed(maxPressedTicks, resetPressedTicks);
            else
                L.isNotPressed(maxLastPressedTicks);
            // M
            if (keyState.IsKeyDown(Keys.M))
                M.isPressed(maxPressedTicks, resetPressedTicks);
            else
                M.isNotPressed(maxLastPressedTicks);
            // N
            if (keyState.IsKeyDown(Keys.N))
                N.isPressed(maxPressedTicks, resetPressedTicks);
            else
                N.isNotPressed(maxLastPressedTicks);
            // O
            if (keyState.IsKeyDown(Keys.O))
                O.isPressed(maxPressedTicks, resetPressedTicks);
            else
                O.isNotPressed(maxLastPressedTicks);
            // P
            if (keyState.IsKeyDown(Keys.P))
                P.isPressed(maxPressedTicks, resetPressedTicks);
            else
                P.isNotPressed(maxLastPressedTicks);
            // Q
            if (keyState.IsKeyDown(Keys.Q))
                Q.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Q.isNotPressed(maxLastPressedTicks);
            // R
            if (keyState.IsKeyDown(Keys.R))
                R.isPressed(maxPressedTicks, resetPressedTicks);
            else
                R.isNotPressed(maxLastPressedTicks);
            // S
            if (keyState.IsKeyDown(Keys.S))
                S.isPressed(maxPressedTicks, resetPressedTicks);
            else
                S.isNotPressed(maxLastPressedTicks);
            // T
            if (keyState.IsKeyDown(Keys.T))
                T.isPressed(maxPressedTicks, resetPressedTicks);
            else
                T.isNotPressed(maxLastPressedTicks);
            // U
            if (keyState.IsKeyDown(Keys.U))
                U.isPressed(maxPressedTicks, resetPressedTicks);
            else
                U.isNotPressed(maxLastPressedTicks);
            // V
            if (keyState.IsKeyDown(Keys.V))
                V.isPressed(maxPressedTicks, resetPressedTicks);
            else
                V.isNotPressed(maxLastPressedTicks);
            // W
            if (keyState.IsKeyDown(Keys.W))
                W.isPressed(maxPressedTicks, resetPressedTicks);
            else
                W.isNotPressed(maxLastPressedTicks);
            // X
            if (keyState.IsKeyDown(Keys.X))
                X.isPressed(maxPressedTicks, resetPressedTicks);
            else
                X.isNotPressed(maxLastPressedTicks);
            // Y
            if (keyState.IsKeyDown(Keys.Y))
                Y.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Y.isNotPressed(maxLastPressedTicks);
            // Z
            if (keyState.IsKeyDown(Keys.Z))
                Z.isPressed(maxPressedTicks, resetPressedTicks);
            else
                Z.isNotPressed(maxLastPressedTicks);

            #endregion
        }

        public bool checkKey(string key)
        {
            bool isPressed = false;

            switch (key)
            {
                #region special keys
                case "Enter":
                    if (Enter.pressed)
                        isPressed = true;
                    break;
                case "Escape":
                    if (Escape.pressed)
                        isPressed = true;
                    break;
                case "Spacebar":
                    if (SpaceBar.pressed)
                        isPressed = true;
                    break;
                #endregion

                #region arrow keys
                case "Left":
                    if (Left.pressed)
                        isPressed = true;
                    break;
                case "Right":
                    if (Right.pressed)
                        isPressed = true;
                    break;
                case "Up":
                    if (Up.pressed)
                        isPressed = true;
                    break;
                case "Down":
                    if (Down.pressed)
                        isPressed = true;
                    break;
                #endregion

                #region letter keys
                case "A":
                    if (A.pressed)
                        isPressed = true;
                    break;
                case "B":
                    if (B.pressed)
                        isPressed = true;
                    break;
                case "C":
                    if (C.pressed)
                        isPressed = true;
                    break;
                case "D":
                    if (D.pressed)
                        isPressed = true;
                    break;
                case "E":
                    if (E.pressed)
                        isPressed = true;
                    break;
                case "F":
                    if (F.pressed)
                        isPressed = true;
                    break;
                case "G":
                    if (G.pressed)
                        isPressed = true;
                    break;
                case "H":
                    if (H.pressed)
                        isPressed = true;
                    break;
                case "I":
                    if (I.pressed)
                        isPressed = true;
                    break;
                case "J":
                    if (J.pressed)
                        isPressed = true;
                    break;
                case "K":
                    if (K.pressed)
                        isPressed = true;
                    break;
                case "L":
                    if (L.pressed)
                        isPressed = true;
                    break;
                case "M":
                    if (M.pressed)
                        isPressed = true;
                    break;
                case "N":
                    if (N.pressed)
                        isPressed = true;
                    break;
                case "O":
                    if (O.pressed)
                        isPressed = true;
                    break;
                case "P":
                    if (P.pressed)
                        isPressed = true;
                    break;
                case "Q":
                    if (Q.pressed)
                        isPressed = true;
                    break;
                case "R":
                    if (R.pressed)
                        isPressed = true;
                    break;
                case "S":
                    if (S.pressed)
                        isPressed = true;
                    break;
                case "T":
                    if (T.pressed)
                        isPressed = true;
                    break;
                case "U":
                    if (U.pressed)
                        isPressed = true;
                    break;
                case "V":
                    if (V.pressed)
                        isPressed = true;
                    break;
                case "W":
                    if (W.pressed)
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
                case "Z":
                    if (Z.pressed)
                        isPressed = true;
                    break;
                #endregion
            }

            return isPressed;
        }
    }
}

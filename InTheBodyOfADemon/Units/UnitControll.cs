using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace InTheBodyOfADemon.Units
{
    class UnitControll
    {
        Unit _unit;

        private KeyboardState _oldKBState;
        private GamePadState _oldGPState;

        public UnitControll(Unit unit)
        {
            _unit = unit;
        }

        private void KBUpdate(KeyboardState kState, GameTime gameTime)
        {
            if (kState.IsKeyDown(Keys.Space) && _oldKBState.IsKeyUp(Keys.Space))
            {
                _unit.Jump(gameTime);
            }

            if (kState.IsKeyDown(Keys.V) && _oldKBState.IsKeyUp(Keys.V))
            {
                _unit.Magick(gameTime);
            }

            if (kState.IsKeyDown(Keys.Left))
            {
                _unit.MoveLeft(gameTime);
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                _unit.MoveRight(gameTime);
            }
            else
            {
                _unit.Stop(gameTime);
            }

            if (kState.IsKeyDown(Keys.C) && _oldKBState.IsKeyUp(Keys.C))
            {
                _unit.Attack(gameTime);
            }

            _oldKBState = kState;


        }

        private void GPUpdate(GamePadState gamePad, GameTime gameTime)
        {
            if (gamePad.IsConnected)
            {
                if (gamePad.DPad.Left == ButtonState.Pressed)
                {
                    _unit.MoveLeft(gameTime);
                }
                if (gamePad.IsButtonDown(Buttons.DPadRight))
                {
                    _unit.MoveRight(gameTime);
                }
                if (gamePad.IsButtonDown(Buttons.A) && _oldGPState.IsButtonUp(Buttons.A))
                {
                    _unit.Jump(gameTime);
                }
                if (gamePad.IsButtonDown(Buttons.X) && _oldGPState.IsButtonUp(Buttons.X))
                {
                    _unit.Attack(gameTime);
                }
                if (gamePad.IsButtonDown(Buttons.Y) && _oldGPState.IsButtonUp(Buttons.Y))
                {
                    _unit.Magick(gameTime);
                }
                _oldGPState = gamePad;
            }
        }
        public void Update(KeyboardState kState, GamePadState gamePad, GameTime gameTime)
        {
            KBUpdate(kState, gameTime);
            GPUpdate(gamePad, gameTime);
            _unit.Update(gameTime);
        }
    }
}

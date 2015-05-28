/*
 * Component: Character System - Boss
 * Version: 1.1.7
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: April 28th, 2014
 * Last Updated By: Jacob
*/

using BH_STG.BarrageEngine.Level;
using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace BH_STG.BarrageEngine.Characters
{
    class Boss : Character
    {
        protected int currentFireTicks = 0, difficulty = 0, health = 1, flipTick = 0, maxFlipTicks = 0;
        protected float speed = 0.0f;

        protected string basicweapon;
        protected List<int> ticksBetweenFiring = new List<int>(), numBullets = new List<int>();
        protected List<Weapon.WeaponType> weapons = new List<Weapon.WeaponType>();
        protected bool canFlip = false;
        protected SpriteFont font;


        public Boss()
        {
            this.basepath = "Graphics\\Sprites\\Bosses\\";
            mX = 0;
            mY = 0;
            this.useVectorCoords = true;
        }

        public override void extraloading(Main GameMain)
        {
            font = GameMain.Content.Load<SpriteFont>("Fonts\\General");
        }

        public bool update(Vector2 towardsCoords, bool towardsPlayer = false)
        {
            bool returnBool = false;
            float dX = towardsCoords.X - (this.coords.X + this.size.X / 2),
                  dY = towardsCoords.Y - (this.coords.Y + this.size.Y / 2);
            double dT = Math.Sqrt(dX * dX + dY * dY);

            double mDX = dX / dT, mDY = dY / dT;
            mX += mDX;
            mY += mDY;
            double tM = Math.Sqrt(mX * mX + mY * mY);
            mX = this.speed * mX / tM;
            mY = this.speed * mY / tM;

            if (double.IsNaN(mX) || mX == double.MaxValue || mX == double.MinValue || mX == double.NegativeInfinity || mX == double.PositiveInfinity)
            {
                if (!towardsPlayer)
                {
                    mX = 0;
                    nanticks++;
                }
                else
                {
                    mX = 0;
                    returnBool = true;
                }
            }
            if (double.IsNaN(mY) || mY == double.MaxValue || mY == double.MinValue || mY == double.NegativeInfinity || mY == double.PositiveInfinity)
            {
                if (!towardsPlayer)
                {
                    mY = 0;
                    nanticks++;
                }
                else
                {
                    mY = 0;
                    returnBool = true;
                }
            }

            this.coords.X += (float)Math.Ceiling(mX);
            this.coords.Y += (float)Math.Ceiling(mY);

            if (nanticks > 2)
            {
                this.coords.X += 0;
                this.coords.Y += 0;
            }

            this.angle = 180 * Math.Atan2(mY, mX) / Math.PI;

            if (this.coords.Y >= 720)
                returnBool = true;

            this.renderPos.X = (int)this.coords.X;
            this.renderPos.Y = (int)this.coords.Y;

            return returnBool;
        }

        public bool shouldFireWeapon()
        {
            int tickdiff = difficulty;
            if (tickdiff == 5)
                tickdiff = 1;
            if (this.coords.Y >= -50)
            {
                currentFireTicks++;
                if (currentFireTicks >= (ticksBetweenFiring[health % ticksBetweenFiring.Count] / tickdiff))
                {
                    currentFireTicks = 0;
                    return true;
                }
            }
            return false;
        }

        public int getNumberBullets()
        {
            return numBullets[health % numBullets.Count];
        }

        public bool shouldFlipScreen(bool current)
        {
            if (this.coords.Y >= -50)
            {
                if (canFlip)
                {
                    flipTick++;
                    if (flipTick >= (maxFlipTicks / difficulty))
                    {
                        flipTick = 0;
                        if (current)
                            return false;
                        return true;
                    }
                }
            }
            return current;
        }

        public bool updateHealth(int Damage)
        {
            health -= Damage;

            if (health <= 0)
                return true;
            return false;
        }

        public string returnBasicWeaponType()
        {
            return basicweapon;
        }

        public bool withinRange(Vector2 ncoords)
        {
            CollisionDetection.Detection detection = new CollisionDetection.Detection();
            Rectangle rec = new Rectangle((int)coords.X, (int)coords.Y, (int)size.X, (int)size.Y);
            if (detection.RectangleCircle(rec, ncoords, 1))
                return true;

            return false;
        }

        public Weapon.WeaponType returnWeaponType()
        {
            return this.weapons[health % weapons.Count];
        }

        public int returnHealth()
        {
            return this.health;
        }

        public void setLast()
        {
            lastcoords = coords;
        }

        public bool shouldRemove()
        {
            if (this.coords.Y >= 720)
                return true;
            return false;
        }

        public override void postDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, bool isFlipped = false)
        {
            if (isFlipped)
            {
                Vector2 newRec = this.GetMidPoint();
                newRec.Y = -(int)(coords.Y) + 720;
                spriteBatch.DrawString(font, health.ToString(), newRec, Color.White);
            }
            else
                spriteBatch.DrawString(font, health.ToString(), this.GetMidPoint(), Color.White);
        }
    }
}

/*
 * Component: Character System - Enemy
 * Version: 1.1.7
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: April 28th, 2014
 * Last Updated By: Jacob
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;

using System;

namespace BH_STG.BarrageEngine.Characters
{
    public class Enemy : Character
    {
        protected int currentFireTicks = 0, ticksBetweenFiring = 0, difficulty = 0, health = 1;
        protected float speed = 0.0f;

        protected string basicweapon;
        protected Weapon.WeaponType weapon;

        public Enemy()
        {
            this.basepath = "Graphics\\Sprites\\BasicEnemies\\";
            mX = 0;
            mY = 0;
            this.useVectorCoords = true;
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

            this.coords.X += (float)mX;
            this.coords.Y += (float)mY;

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
            if (this.renderPos.Y >= -50)
            {
                currentFireTicks++;
                if (currentFireTicks >= (ticksBetweenFiring / tickdiff))
                {
                    currentFireTicks = 0;
                    return true;
                }
            }
            return false;
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
            return this.weapon;
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
    }
}

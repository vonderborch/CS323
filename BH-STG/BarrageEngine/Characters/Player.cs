/*
 * Component: Character System - Player
 * Version: 1.0.10
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;
using BH_STG.Particles;

using Microsoft.Xna.Framework;

using System;

namespace BH_STG.BarrageEngine.Characters
{
    class Player : Character
    {
        protected int currentFireTicks = 0, ticksBetweenFiring = 0, BaseDamage = 1, BaseBombs = 0;

        protected string basicweapon, name;
        protected Weapon.WeaponType weapon;
        protected float scoremultiplier;

        public Player()
        {
            this.basepath = "Graphics\\Sprites\\Players\\";
            isEnemy = false;
        }

        public void update(Rectangle constraints, Vector2 playertempvec, float PlayerSpeed)
        {
            Rectangle playertemp = GetSize();
            playertempvec.X *= PlayerSpeed;
            playertempvec.Y *= PlayerSpeed;
            playertemp.X += (int)playertempvec.X;
            playertemp.Y += (int)playertempvec.Y;

            if (playertemp.X < constraints.X)
                playertemp.X = constraints.X;
            if (playertemp.X + playertemp.Width > constraints.Width)
                playertemp.X = constraints.Width - playertemp.Width;
            if (playertemp.Y < constraints.Y)
                playertemp.Y = constraints.Y;
            if (playertemp.Y + playertemp.Height > constraints.Height)
                playertemp.Y = constraints.Height - playertemp.Height;
            this.renderPos = playertemp;
        }

        public bool checkCollision(CollisionDetection.Detection collision,
                                              Explosion explosion, Random rand,
                                              Vector2 bulletCoords, int bulletRadius, bool isEnemy = true)
        {
            if (collision.Circle(bulletCoords, bulletRadius,
                                 GetMidPoint(), getRadius()))
            {
                if (isEnemy)
                    explosion.AddParticles(this.GetMidPoint(), rand);
                return true;
            }
            return false;
        }

        public bool checkLaserCD(CollisionDetection.Detection collision,
                                              Explosion explosion, Random rand,
                                              Rectangle bulletCoords, bool isEnemy = true)
        {
            if (collision.RectangleCircle(bulletCoords,
                                 GetMidPoint(), getRadius()))
            {
                if (isEnemy)
                    explosion.AddParticles(this.GetMidPoint(), rand);
                return true;
            }
            return false;
        }

        public bool shouldFireWeapon(int ReloadMod)
        {
            if (ticksBetweenFiring > 0)
            {
                currentFireTicks += 1 * ReloadMod;
                if (currentFireTicks >= ticksBetweenFiring)
                {
                    currentFireTicks = 0;
                    return true;
                }
            }
            return false;
        }

        public Weapon.WeaponType returnWeaponType()
        {
            return this.weapon;
        }

        public string returnBasicWeaponType()
        {
            return basicweapon;
        }

        public string returnName()
        {
            return name;
        }

        public int returnBaseDamage()
        {
            return BaseDamage;
        }

        public int returnBaseBombs()
        {
            return BaseBombs;
        }

        public int returnCooldown()
        {
            return ticksBetweenFiring;
        }

        public float returnScoreMultiplier()
        {
            return scoremultiplier;
        }
    }
}

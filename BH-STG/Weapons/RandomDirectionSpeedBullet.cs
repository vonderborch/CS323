/*
 * Component: Weapon - RandomDirectionSpeed
 * Version: 1.0.7
 * Created: April 10th, 2014
 * Created By: Christian
 * Last Updated: April 30th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class RandomDirectionSpeedBullet : Weapon
    {
        int randtick = 30, randMaxTick = 30, randDirect;
        Random rand;
        float baseSpeed = 3.0f;

        public RandomDirectionSpeedBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                                          bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet5.png");
            }
            else
            {
                this.maxlifeticks = 1000;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet6.png");
            }
            this.position = basePosition;
            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.randomdirectionspeed;
            rand = random;
            randDirect = rand.Next(0, 359) + 90;
            this.speed.X = 0;
            this.speed.Y = 0;
            this.name = "Random Direction and Speed Weapon";
            this.description = "A weapon which shoots bullets in random directions and at random (and changing) speeds.";
        }

        public override void updatePosition()
        {
            randtick++;
            if (randtick >= randMaxTick)
            {
                baseSpeed = (float)(rand.NextDouble() + rand.Next(2, 5));

                this.speed.X = (float)Math.Cos(randDirect * Math.PI / 180) * baseSpeed;
                this.speed.Y = (float)Math.Sin(randDirect * Math.PI / 180) * baseSpeed;

                randtick = 0;
                randMaxTick = rand.Next(3, 50);
            }

            Vector2 next = this.speed;
            if (isPlayers)
            {
                next.X = -next.X;
                next.Y = -next.Y;
            }

            this.position.X += next.X;
            this.position.Y += next.Y;

            this.lifeticks++;
        }
    }
}

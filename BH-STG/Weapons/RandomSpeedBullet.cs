/*
 * Component: Weapon - RandomSpeed
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
    class RandomSpeedBullet : Weapon
    {
        int randtick = 30, randMaxTick = 30;
        Random rand;

        public RandomSpeedBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                                 bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet3.png");
            }
            else
            {
                this.maxlifeticks = 1000;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet4.png");
            }
            this.position = basePosition;
            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.randomspeed;
            rand = random;
            this.speed = new Vector2(0, 0);
            this.name = "Random Speed Weapon";
            this.description = "A bullet which shoots weapons at a random (and changing) speed.";
        }

        public override void updatePosition()
        {
            randtick++;
            if (randtick >= randMaxTick)
            {
                this.speed.Y = (float)(rand.NextDouble() + rand.Next(2, 5));
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

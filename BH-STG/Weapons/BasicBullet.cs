/*
 * Component: Weapon - Basic
 * Version: 1.0.7
 * Created: April 10th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Jacob
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class BasicBullet : Weapon
    {
        public BasicBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                           bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            this.position = basePosition;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet2.png");
            }
            else
            {
                this.maxlifeticks = 1000;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet1.png");
            }

            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.basic;
            this.speed = new Vector2(0, 3.0f);
            this.name = "Basic Weapon";
            this.description = "A basic gun. Shoots bullets that travel in a straight line ahead of the player.";
        }

        public override void updatePosition()
        {
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

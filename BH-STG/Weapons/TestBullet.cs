/*
 * Component: Weapon System - Test Bullet
 * Version: 1.1.6
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: April 18th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class TestBullet : Weapon
    {
        public TestBullet (Main gamemain, Color tint, Vector2 basePosition, Random random,
                           bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\testbullet.png");
            this.position = basePosition;
            this.maxlifeticks = 120;
            this.scale = 0.05f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 4;
            this.type = WeaponType.test;
            this.speed = new Vector2(0, 3);
            this.name = "Test Weapon";
            this.description = "The test weapon, fires bullets in a straight line.";
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

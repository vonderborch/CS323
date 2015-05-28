/*
 * Component: Weapon System - Fake Bullet
 * Version: 1.0.0
 * Created: April 28th, 2014
 * Created By: Christian
 * Last Updated: April 28th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class FakeBullet : Weapon
    {
        public FakeBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                           bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\testbullet.png");
            this.position = basePosition;
            this.maxlifeticks = 0;
            this.scale = 0.05f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 4;
            this.type = WeaponType.fake;
            this.speed = new Vector2(0, 3);
            this.name = "Fake Weapon";
            this.description = "This weapon doesn't actually shoot anything :)";
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

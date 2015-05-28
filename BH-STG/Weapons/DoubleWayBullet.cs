/*
 * Component: Weapon - DoubleWay
 * Version: 1.0.5
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
    class DoubleWayBullet : Weapon
    {
        Vector2 Move = new Vector2(0, 0);

        public DoubleWayBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                               bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet9.png");
            }
            else
            {
                this.maxlifeticks = 1000;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet10.png");
            }
            this.position = basePosition;
            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.douubleway;
            this.speed = new Vector2(0, 3.0f);
            this.name = "DoubleWay Weapon";
            this.description = "A weapon which targest the nearest enemy and tracks it at an angle.";
        }

        public override void updatePosition()
        {
            float dx = this.playerCoords.X - this.position.X,
                  dy = this.playerCoords.Y - this.position.Y,
                  dt = (float)Math.Sqrt(dx * dx + dy * dy),
                  mdx = 0.2f * dx / dt, mdy = 0.2f * dy / dt;
            Move.X += mdx;
            Move.Y += mdy;

            float tm = (float)Math.Sqrt(Move.X * Move.X + Move.Y * Move.Y);
            Move.X = this.speed.Y * Move.X / tm;
            Move.Y = this.speed.Y * Move.Y / tm;

            this.position.X += Move.X;
            this.position.Y += Move.Y;

            this.lifeticks++;
        }
    }
}

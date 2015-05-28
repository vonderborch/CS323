/*
 * Component: Weapon - Badger
 * Version: 1.0.0
 * Created: April 29th, 2014
 * Created By: Christian
 * Last Updated: April 29th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class BadgerBullet : Weapon
    {
        Vector2 Move = new Vector2(0,0);

        public BadgerBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                               bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet13.png");
            }
            else
            {
                this.maxlifeticks = 1000;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bullet14.png");
            }
            this.position = basePosition;
            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.singleway;
            this.speed = new Vector2(0, 4.0f);
            this.name = "Badger Weapon";
            this.description = "A weapon worthy of a honey badger.";
        }

        public override void updatePosition()
        {
            float dx = this.playerCoords.X - this.position.X,
                  dy = this.playerCoords.Y - this.position.Y,
                  dt = (float)Math.Sqrt(dx * dx + dy * dy),
                  mdx = dx / dt, mdy = dy / dt;
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

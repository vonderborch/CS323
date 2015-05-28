/*
 * Component: Weapon - Secret Player Weapon
 * Version: 1.0.5
 * Created: April 14th, 2014
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
    class SecretPlayerBullet : Weapon
    {
        Vector2 Move = new Vector2(0,0);

        public SecretPlayerBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                                  bool isFlipped, bool isPlayerFired = false)
        {
            this.color = Color.Blue;
            this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\bob.png");
            this.position = basePosition;
            this.maxlifeticks = 120;
            this.scale = 8.0f / 119.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.secretplayer;
            this.speed = new Vector2(0, 3.0f);
            this.name = "Secret Player Weapon";
            this.description = "The player's secret weapon.";
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

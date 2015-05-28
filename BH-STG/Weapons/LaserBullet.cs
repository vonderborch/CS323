/*
 * Component: Weapon - Laser
 * Version: 1.0.6
 * Created: April 10th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Weapons
{
    class LaserBullet : Weapon
    {
        public LaserBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                           bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired)
            {
                this.maxlifeticks = 30;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\laser2.png");
            }
            else
            {
                this.maxlifeticks = 30;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\laser3.png");
            }
            this.scale = 1.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);

            if (!isPlayerFired)
                this.position = new Vector2(basePosition.X - (size.X / 2), basePosition.Y);
            else
                this.position = new Vector2(basePosition.X - (size.X / 2), basePosition.Y - size.Y);

            this.isPlayers = isPlayerFired;
            this.radius = 4;
            this.type = WeaponType.laser;
            this.name = "Laser Weapon";
            this.description = "A weapon which shoots a laser in a straight line ahead of the player.";
        }

        public override void updatePosition()
        {
            this.lifeticks++;
        }
    }
}

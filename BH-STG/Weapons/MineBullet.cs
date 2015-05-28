/*
 * Component: Weapon - Mine
 * Version: 1.0.0
 * Created: April 30th, 2014
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
    class MineBullet : Weapon
    {
        public MineBullet(Main gamemain, Color tint, Vector2 basePosition, Random random,
                           bool isFlipped, bool isPlayerFired = false)
        {
            this.color = tint;
            if (isPlayerFired == true)
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\mine.png");
            }
            else
            {
                this.maxlifeticks = 500;
                this.sprite = gamemain.Content.Load<Texture2D>("Graphics\\Sprites\\Weapons\\mine.png");
            }
            this.position = basePosition;
            this.scale = 1.0f / 8.0f;
            this.size = new Vector2(sprite.Width * this.scale,
                                    sprite.Height * this.scale);
            this.isPlayers = isPlayerFired;
            this.radius = 8;
            this.type = WeaponType.basic;
            this.speed = new Vector2(0, 0.0f);
            this.name = "Mine Weapon";
            this.description = "A weapon that lays mines.";
        }

        public override void updatePosition()
        {
            this.lifeticks++;
        }
    }
}

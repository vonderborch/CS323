﻿/*
 * Component: Enemies - Basic Enemy
 * Version: 1.0.7
 * Created: April 14th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Enemies
{
    class BasicEnemy : Enemy
    {
        public BasicEnemy(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor, int diff)
        {
            this.spritepath = this.basepath + "badguy2.png";
            this.speed = nSpeed;
            this.coords.X = baseRenderPos.X;
            this.coords.Y = baseRenderPos.Y;
            this.scale = 1.0f / 4.0f;
            this.color = baseColor;
            this.ticksBetweenFiring = 90;
            this.weapon = BarrageEngine.Weapons.Weapon.WeaponType.basic;
            this.difficulty = diff;
            this.radius = 32;
            this.health = 1;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
        }
    }
}

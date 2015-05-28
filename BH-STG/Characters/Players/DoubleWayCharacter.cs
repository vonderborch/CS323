﻿/*
 * Component: Players - DoubleWay Character
 * Version: 1.0.3
 * Created: April 14th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Players
{
    class DoubleWayCharacter : Player
    {
        public DoubleWayCharacter(Vector2 baseRenderPos, float rscale, Color baseColor, bool menu = false)
        {
            this.spritepath = this.basepath + "mericaone.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 0.5f;
            if (!menu)
                this.scale = 1.0f / 4.0f;
            this.color = Color.Red;
            this.ticksBetweenFiring = 60;
            this.weapon = BarrageEngine.Weapons.Weapon.WeaponType.douubleway;
            this.name = "DoubleWay Character";
            this.radius = 32;
            this.BaseBombs = 0;
            this.BaseDamage = 1;
            this.scoremultiplier = 0.5f;
            this.useFlameLocation = true;
            this.flameLocation = new Vector2(0, -18);
        }
    }
}
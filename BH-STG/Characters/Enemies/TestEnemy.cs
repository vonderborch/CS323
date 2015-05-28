/*
 * Component: Enemies - Test Enemy
 * Version: 1.0.4
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Enemies
{
    class TestEnemy : Enemy
    {
        public TestEnemy(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor, int diff)
        {
            this.spritepath = this.basepath + "Pirate.png";
            this.speed = nSpeed;
            this.coords.X = baseRenderPos.X;
            this.coords.Y = baseRenderPos.Y;
            this.scale = rscale;
            this.color = baseColor;
            this.ticksBetweenFiring = 30;
            this.weapon = BarrageEngine.Weapons.Weapon.WeaponType.test;
            this.difficulty = diff;
            this.radius = 16;
            this.health = 1;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
        }
    }
}

/*
 * Component: Players - David Character
 * Version: 1.0.2
 * Created: April 14th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Players
{
    class DavidCharacter : Player
    {
        public DavidCharacter(Vector2 baseRenderPos, float rscale, Color baseColor, bool menu = false)
        {
            this.spritepath = this.basepath + "12614.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 0.5f;
            if (!menu)
                this.scale = 1.0f / 4.0f;
            this.color = baseColor;
            this.ticksBetweenFiring = 60;
            this.weapon = BarrageEngine.Weapons.Weapon.WeaponType.basic;
            this.name = "David Character";
            this.radius = 32;
            this.BaseBombs = 0;
            this.BaseDamage = 1;
            this.scoremultiplier = 1.0f;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
            this.canUseFlame = false;
        }
    }
}

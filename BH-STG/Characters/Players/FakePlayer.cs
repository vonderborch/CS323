/*
 * Component: Players - Fake Player
 * Version: 1.0.1
 * Created: April 29th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Players
{
    class FakePlayer : Player
    {
        public FakePlayer(Vector2 baseRenderPos, float rscale, Color baseColor, bool menu = false)
        {
            this.spritepath = this.basepath + "raptor.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 0.5f;
            if (!menu)
                this.scale = 1.0f / 4.0f;
            this.color = baseColor;
            this.ticksBetweenFiring = -1;
            this.weapon = BarrageEngine.Weapons.Weapon.WeaponType.fake;
            this.name = "Fake Player";
            this.radius = 32;
            this.BaseBombs = 100000;
            this.BaseDamage = 1;
            this.scoremultiplier = 1.00f;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
            this.canUseFlame = false;
        }
    }
}

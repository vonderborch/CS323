/*
 * Component: Bosses - Level 7 Boss
 * Version: 1.1.2
 * Created: April 14th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Bosses
{
    class Level7Boss : Boss
    {
        public Level7Boss(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor, int diff)
        {
            this.spritepath = this.basepath + "badguyintense.png";
            this.speed = nSpeed;
            this.coords.X = baseRenderPos.X;
            this.coords.Y = baseRenderPos.Y;
            this.scale = 1.0f / 4.0f;
            this.color = Color.Red;
            this.numBullets.Add(1);                                           
            this.ticksBetweenFiring.Add(30);                                   
            this.weapons.Add(BarrageEngine.Weapons.Weapon.WeaponType.singleway);    
			this.numBullets.Add(2);                                           
            this.ticksBetweenFiring.Add(25);  
			this.numBullets.Add(3);                                            
            this.ticksBetweenFiring.Add(20);  
			this.numBullets.Add(4);                                            
            this.difficulty = diff;
            this.radius = 32;
            this.health = 50;
            this.canFlip = false;
            this.maxFlipTicks = 60;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
        }
    }
}

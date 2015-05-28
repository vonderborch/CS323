/*
 * Component: Bosses - Badger Boss
 * Version: 1.1.2
 * Created: April 23rd, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;

using Microsoft.Xna.Framework;

namespace BH_STG.Characters.Bosses
{
    class BadgerBoss : Boss
    {
        public BadgerBoss(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor, int diff)
        {
            this.spritepath = this.basepath + "honeybadgerofdoom1.png";
            this.speed = nSpeed;
            this.coords.X = baseRenderPos.X;
            this.coords.Y = baseRenderPos.Y;
            this.scale = 1.0f / 4.0f;
            this.color = Color.White;
            this.numBullets.Add(1);                                             // add number of bullets to fire at once like this line.
            this.ticksBetweenFiring.Add(30);                                    // Add firing cooldowns like this line.
            this.weapons.Add(BarrageEngine.Weapons.Weapon.WeaponType.badger);    // Add weapons like this line.
            // Weapon Types: basic, laser, singleway, douubleway, randomdirection, randomspeed, randomdirectionspeed, secretenemy
            // ticksBetweenFiring and weapons do not need to match, but if they do, then the same order matters if you want a specific cooldown for a specific weapon
            // Same thing with numBullets and ticksBetweenFiring and weapons
            this.difficulty = diff;
            this.radius = 32;
            this.health = 10;
            this.canFlip = false;
            this.maxFlipTicks = 60;
            this.useFlameLocation = false;
            this.flameLocation = new Vector2();
            this.canUseFlame = false;
        }
    }
}

/*
 * Component: Weapon System - Weapon Database
 * Version: 1.0.3
 * Created: April 18th, 2014
 * Created By: Christian
 * Last Updated: April 29th, 2014
 * Last Updated By: Christian
*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BH_STG.BarrageEngine.Weapons;

namespace BH_STG.Weapons
{
    class WeaponDatabase
    {
        List<Weapon> bullets = new List<Weapon>();

        public WeaponDatabase(Main GameMain)
        {
            Random rand = new Random();
            bullets.Add(new BasicBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new SingleWayBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new DoubleWayBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new LaserBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new RandomDirectionBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new RandomDirectionSpeedBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new RandomSpeedBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new SecretEnemyBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new SecretPlayerBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new TestBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new FakeBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new BadgerBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
            bullets.Add(new MineBullet(GameMain, Color.White, new Vector2(0, 0), rand, false));
        }

        public string getWeaponName(Weapon.WeaponType type)
        {
            foreach (Weapon bullet in bullets)
            {
                if (bullet.returnType() == type)
                    return bullet.returnName();
            }
            return "N/A";
        }

        public int getWeaponMaxLife(Weapon.WeaponType type)
        {
            foreach (Weapon bullet in bullets)
            {
                if (bullet.returnType() == type)
                    return bullet.returnLife();
            }
            return -1;
        }

        public List<string> getWeaponDescription(Weapon.WeaponType type)
        {
            List<string> desc = new List<string>();
            string tempdesc = "", temp = "";

            foreach (Weapon bullet in bullets)
            {
                if (bullet.returnType() == type)
                {
                    tempdesc = bullet.returnDesc();
                    break;
                }
            }
            
            int strlength = tempdesc.Length;
            for (int i = 0; i < strlength; i += 30)
            {
                temp = tempdesc.Substring(i, Math.Min(30, strlength - i));

                desc.Add(temp);
            }

            return desc;
        }
    }
}

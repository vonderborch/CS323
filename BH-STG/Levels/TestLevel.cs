/*
 * Component: Levels - Test Level
 * Version: 1.0.8
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Level;
using BH_STG.Characters.Bosses;
using BH_STG.Characters.Enemies;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace BH_STG.Levels
{
    class TestLevel : Level
    {
        public TestLevel(Random rand, int diff, float enemySpeed, float bossSpeed)
        {
            this.name = "Test Level"; // name of level
            this.description = "The test level."; // description of level
            this.file = "stars.jpg"; // level background
            this.backgroundRenderPos = new Vector2(32, 0);
            this.backgroundColor = Color.White;
            this.badgerStartTick = 180; // -1 = never (60 = 1 sec)
            this.badgerChancePerTick = 10000; // out of 100000

            // enemySpeed = 3.75 pixels per tick (60 ticks per second): 225 pixels a second
            //                              Enemy Type                     Start x         Start Y                                     End Destination
            // this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.White, diff), new Vector2(64, 1000)));
            // Enemy Type:  BasicEnemy (1), RandomSpeedEnemy (2), LaserEnemy (3), RandomDirectionEnemy (4), RandomDirectionSpeedEnemy (5), DoubleWayEnemy (6), SingleWayEnemy (7), SecretEnemy (8)
            // Boss Type:  Level1Boss (1), Level2Boss (2), Level3Boss (3), Level4Boss (4), Level5Boss (5), Level6Boss (6), Level7Boss (7), SecretBoss (8)
            // Start X: Starting x position. More negative = more left (from left *edge* of the screen)
            // Start Y: Starting y position. More negative = more up (from upper *edge* of the screen)
            // End Destination: 3 Options
            //                  - , true): follows the player (glitchy though, so recommend don't use it).
            //                  - , new Vector2(x, y)): Go to the (x,y) coordinate on the screen.
            //                  - List<Vector2> coords = new List<Vector2>();
            //                                  coords.Add(new Vector2(x,y)        // can repeat as many times as you want.
            //                    , coords): Go to the coordinates in the coords list in order (can use for zigzagging).
            //                    , coords, true): Go to the coordinates in the coords list in order (can use for zigzagging), and repeat forever.
            //                  - List<List<Vector2>> coords = new List<List<Vector2>>(); // FOR BOSSES ONLY!!!!!!!!!!!!!!!!!!!!!!!!!
            //                                  List<Vector2> temp = new List<Vector2>();
            //                                  temp.Add(new Vector2(x,y))        // can repeat as many times as you want.
            //                                  coords.Add(temp)       // can repeat as many times as you want.
            //                    , coords): Go to the coordinates in the coords list in order (can use for zigzagging).
            //                    , coords, true): Go to the coordinates in the coords list in order (can use for zigzagging), and repeat forever.
            // this.Enemies = a new enemy (must use new Mob)
            // this.Bosses = a new boss (must use new MobBoss)


            List<Vector2> coords = new List<Vector2>();
            coords.Add(new Vector2(200, 20));
            coords.Add(new Vector2(100, 10));
            coords.Add(new Vector2(400, 40));
            coords.Add(new Vector2(600, 200));
            this.Enemies.Add(new Mob(new BasicEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            
            this.Enemies.Add(new Mob(new LaserEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new MineEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new RandomDirectionEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new RandomDirectionSpeedEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new RandomSpeedEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));

            this.Bosses.Add(new MobBoss(new Level1Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level2Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level3Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level4Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level5Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level6Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new Level7Boss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true));
            this.Bosses.Add(new MobBoss(new TestBoss(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Red, diff), coords, true)); 
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), 0), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -500), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -1000), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -1500), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -2500), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            //this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -3500), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            /*
            this.Enemies.Add(new Mob(new BasicEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Red, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new DoubleWayEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Yellow, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new LaserEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new RandomDirectionEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new RandomDirectionSpeedEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Green, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new RandomSpeedEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Black, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new SingleWayEnemy(new Vector2(32 + rand.Next(600), -2000), enemySpeed, 0.25f, Color.Pink, diff), enemySpeed, new Vector2(64, 1000)));

            this.Bosses.Add(new MobBoss(new BasicBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Red, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new DoubleWayBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Yellow, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new LaserBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Blue, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new RandomDirectionBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new RandomDirectionSpeedBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Green, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new RandomSpeedBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Black, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new SingleWayBoss(new Vector2(32 + rand.Next(600), -3000), enemySpeed, 0.25f, Color.Pink, diff), enemySpeed, new Vector2(64, 1000)));

            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), -1000), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), -1000), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), -1000), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));

            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Enemies.Add(new Mob(new TestEnemy(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), enemySpeed, 0.25f, Color.White, diff), enemySpeed, new Vector2(64, 1000)));
            this.Bosses.Add(new MobBoss(new TestBoss(new Vector2(32 + rand.Next(600), 32 + rand.Next(600)), bossSpeed, 0.25f, Color.White, diff), bossSpeed, new Vector2(64, 1000)));
             */
        }
    }
}

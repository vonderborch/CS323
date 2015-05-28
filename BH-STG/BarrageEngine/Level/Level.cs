/*
 * Component: Level System - Level Base
 * Version: 2.0.6
 * Created: April 28th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;
using BH_STG.Characters.Bosses;
using BH_STG.Particles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace BH_STG.BarrageEngine.Level
{
    class Level
    {
        #region structs
        #region point
        public struct Point
        {
            public bool visited;
            public Vector2 coords;

            public Point(Vector2 nCoords, bool nVisited = false)
            {
                visited = nVisited;
                coords = nCoords;
            }
        }

        #endregion

        #region Mob
        public struct Mob
        {
            public Enemy type;
            public List<Point> points;
            public bool alwaysTowardsPlayer, cycle;

            public Mob(Enemy nType, Vector2 nEndPos)
            {
                type = nType;
                alwaysTowardsPlayer = false;
                cycle = false;
                points = new List<Point>();
                points.Add(new Point(nEndPos));
            }

            public Mob(Enemy nType, List<Vector2> nPoints, bool nCycle = false)
            {
                type = nType;
                alwaysTowardsPlayer = false;
                cycle = nCycle;
                points = new List<Point>();
                foreach (Vector2 nPoint in nPoints)
                    points.Add(new Point(nPoint));
            }

            public Mob(Enemy nType, bool towardsPlayer)
            {
                type = nType;
                cycle = false;
                alwaysTowardsPlayer = towardsPlayer;
                points = new List<Point>();
            }
        }

        #endregion

        #region MobBoss
        public struct MobBoss
        {
            public Boss type;
            public List<List<Point>> points;
            public bool alwaysTowardsPlayer, cycle;

            public MobBoss(Boss nType, Vector2 nEndPos)
            {
                type = nType;
                alwaysTowardsPlayer = false;
                cycle = false;
                points = new List<List<Point>>();
                List<Point> temp = new List<Point>();
                temp.Add(new Point(nEndPos));
                points.Add(temp);
            }

            public MobBoss(Boss nType, List<Vector2> nPoints, bool nCycle = false)
            {
                type = nType;
                alwaysTowardsPlayer = false;
                cycle = nCycle;
                points = new List<List<Point>>();

                List<Point> temp = new List<Point>();
                foreach (Vector2 nCoord in nPoints)
                    temp.Add(new Point(nCoord));

                points.Add(temp);
            }

            public MobBoss(Boss nType, List<List<Vector2>> nPoints, bool nCycle = false)
            {
                type = nType;
                alwaysTowardsPlayer = false;
                cycle = nCycle;
                points = new List<List<Point>>();

                foreach (List<Vector2> vecA in nPoints)
                {
                    List<Point> temp = new List<Point>();
                    foreach (Vector2 nCoord in vecA)
                        temp.Add(new Point(nCoord));

                    points.Add(temp);
                }
            }

            public MobBoss(Boss nType, bool towardsPlayer)
            {
                type = nType;
                alwaysTowardsPlayer = towardsPlayer;
                cycle = false;
                points = new List<List<Point>>();
            }
        }

        #endregion

        #region bulletUpdates
        public struct bulletUpdates
        {
            public Vector2 coords;
            public Weapons.Weapon.WeaponType type;

            public bulletUpdates(Vector2 nCoords, Weapons.Weapon.WeaponType nType)
            {
                coords = nCoords;
                type = nType;
            }
        }

        #endregion

        #endregion

        #region variables
        protected string file, basepath = "Graphics\\GameBackgrounds\\", name, description = "N/A";
        protected Texture2D background;
        protected Vector2 backgroundRenderPos;
        protected Color backgroundColor;
        protected List<Mob> Enemies = new List<Mob>();
        protected List<MobBoss> Bosses = new List<MobBoss>();
        protected int badgerStartTick, badgerChancePerTick;
        bool hasKilled = false;

        #endregion

        public Main GameMain { get; set; }

        public Level() { }

        public void loading()
        {
            #region loading
            background = GameMain.Content.Load<Texture2D>(basepath + file);

            // load the enemies
            for (int i = 0; i < Enemies.Count; i++)
                Enemies[i].type.loading(GameMain);

            // load the bosses
            for (int i = 0; i < Bosses.Count; i++)
                Bosses[i].type.loading(GameMain);

            #endregion
        }

        public void update(int bgIncrease, Vector2 playerCoords)
        {
            #region update
            List<int> EnemyDeletions = new List<int>(),
                      BossDeletions = new List<int>();

            #region update background
            Vector2 newPos = returnPosition();
            newPos.Y += bgIncrease;

            //// update background image
            if (newPos.Y > 0)
                newPos.Y = -background.Height + 720;

            backgroundRenderPos = newPos;

            #endregion

            #region update enemies
            #region update
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                this.Enemies[i].type.setLast();

                // move towards player?
                #region move towards player?
                if (this.Enemies[i].alwaysTowardsPlayer)
                {
                    if (this.Enemies[i].points.Count == 1)
                        this.Enemies[i].points[0] = new Point(playerCoords);
                    else
                        this.Enemies[i].points.Add(new Point(playerCoords));
                }

                #endregion

                // determine where to go and go there!
                #region goal cycle
                for (int k = 0; k < this.Enemies[i].points.Count; k++)
                {
                    // visit a point
                    if (!this.Enemies[i].points[k].visited)
                    {
                        // have we reached the point?
                        if (this.Enemies[i].type.withinRange(this.Enemies[i].points[k].coords))
                        {
                            this.Enemies[i].points[k] = new Point(this.Enemies[i].points[k].coords, true);
                            continue;
                        }

                        // if we having, go towards it and then remove the enemy if we need to
                        if (this.Enemies[i].type.update(this.Enemies[i].points[k].coords))
                            EnemyDeletions.Add(i);

                        break;
                    }
                    // reset the points
                    else if (this.Enemies[i].cycle && k == (this.Enemies[i].points.Count - 1))
                    {
                        for (int j = 0; j < this.Enemies[i].points.Count; j++)
                            this.Enemies[i].points[j] = new Point(this.Enemies[i].points[j].coords);
                    }
                }

                #endregion

                // base update and removal
                #region base stuffs
                if (this.Enemies[i].type.hasNotMoved())
                    this.Enemies[i].type.baseUpdate(bgIncrease);

                if (this.Enemies[i].type.shouldRemove())
                    EnemyDeletions.Add(i);

                #endregion
            }
            #endregion

            #region deletion
            for (int i = (EnemyDeletions.Count - 1); i >= 0; i--)
                this.Enemies.RemoveAt(i);

            EnemyDeletions.Clear();

            #endregion

            #endregion

            #region update bosses
            #region update
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                this.Bosses[i].type.setLast();

                // move towards player?
                #region move towards player?
                if (this.Bosses[i].alwaysTowardsPlayer)
                {
                    if (this.Bosses[i].points.Count == 1)
                        this.Bosses[i].points[0][0] = new Point(playerCoords);
                    else
                    {
                        this.Bosses[i].points.Add(new List<Point>());
                        this.Bosses[i].points[0].Add(new Point(playerCoords));
                    }
                }

                #endregion

                // determine where to go and go there!
                #region goal cycle
                int ListToUse = this.Bosses[i].type.returnHealth() % this.Bosses[i].points.Count;
                for (int k = 0; k < this.Bosses[i].points[ListToUse].Count; k++)
                {
                    // visit a point
                    if (!this.Bosses[i].points[ListToUse][k].visited)
                    {
                        // have we reached the point?
                        if (this.Bosses[i].type.withinRange(this.Bosses[i].points[ListToUse][k].coords))
                        {
                            this.Bosses[i].points[ListToUse][k] = new Point(this.Bosses[i].points[ListToUse][k].coords, true);
                            continue;
                        }

                        // if we having, go towards it and then remove the enemy if we need to
                        if (this.Bosses[i].type.update(this.Bosses[i].points[ListToUse][k].coords))
                            BossDeletions.Add(i);

                        break;
                    }
                    // reset the points
                    else if (this.Bosses[i].cycle && k == (this.Bosses[i].points.Count - 1))
                    {
                        for (int j = 0; j < this.Bosses[i].points.Count; j++)
                            this.Bosses[i].points[ListToUse][j] = new Point(this.Bosses[i].points[ListToUse][j].coords);
                    }
                }

                #endregion

                // base update and removal
                #region base stuffs
                if (this.Bosses[i].type.hasNotMoved())
                    this.Bosses[i].type.baseUpdate(bgIncrease);

                if (this.Bosses[i].type.shouldRemove())
                    BossDeletions.Add(i);

                #endregion
            }
            #endregion

            #region deletion
            for (int i = (BossDeletions.Count - 1); i >= 0; i--)
                this.Bosses.RemoveAt(i);

            BossDeletions.Clear();

            #endregion

            #endregion

            #endregion
        }

        public void addBadger()
        {
            this.Bosses.Add(new MobBoss(new BadgerBoss(new Vector2(344, -200), 3.0f, 0.25f, Color.White, 4), true));
            this.Bosses[this.Bosses.Count - 1].type.loading(GameMain);
        }

        public string getName()
        {
            return this.name;
        }

        public void strengthenBadger(int healthincrease)
        {
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                MobBoss temp = this.Bosses[i];

                if (temp.type.returnWeaponType() == Weapons.Weapon.WeaponType.badger)
                {
                    temp.type.updateHealth(-healthincrease);
                    this.Bosses[i] = temp;
                    break;
                }
            }
        }

        public int getBadgerStart()
        {
            return badgerStartTick;
        }

        public int getBadgerChance()
        {
            return badgerChancePerTick;
        }

        public List<bulletUpdates> getNewBullets()
        {
            #region getNewBullets
            List<bulletUpdates> newBullets = new List<bulletUpdates>();
            
            #region enemies
            foreach (Mob enemy in this.Enemies)
            {
                if (enemy.type.shouldFireWeapon())
                {
                    int numberToFire = enemy.type.getNumberBullets();
                    if (numberToFire == 1)
                        newBullets.Add(new bulletUpdates(enemy.type.GetMidPoint(), enemy.type.returnWeaponType()));
                    else
                    {
                        Vector2 coords = enemy.type.GetMidPoint(),
                                size = enemy.type.GetSizeV();
                        coords.Y -= 8;
                        coords.X = enemy.type.getX() - 8;
                        float dx = size.X / (numberToFire - 1);

                        for (int i = 0; i < numberToFire; i++)
                        {
                            newBullets.Add(new bulletUpdates(coords, enemy.type.returnWeaponType()));
                            coords.X += dx;
                        }
                    }
                }
            }

            #endregion

            #region bosses
            foreach (MobBoss boss in this.Bosses)
            {
                if (boss.type.shouldFireWeapon())
                {
                    int numberToFire = boss.type.getNumberBullets();
                    if (numberToFire == 1)
                        newBullets.Add(new bulletUpdates(boss.type.GetMidPoint(), boss.type.returnWeaponType()));
                    else
                    {
                        Vector2 coords = boss.type.GetMidPoint(),
                                size = boss.type.GetSizeV();
                        coords.Y -= 8;
                        coords.X = boss.type.getX() - 8;
                        float dx = size.X / (numberToFire - 1);

                        for (int i = 0; i < numberToFire; i++)
                        {
                            newBullets.Add(new bulletUpdates(boss.type.GetMidPoint(), boss.type.returnWeaponType()));
                            coords.X += dx;
                        }
                    }
                }
            }

            #endregion

            return newBullets;

            #endregion
        }

        public void bombCollision(CollisionDetection.Detection collision,
                                          Explosion explosion, Random rand, Rectangle constraints)
        {
            #region bombCollision
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                if (collision.RectangleCircle(constraints,
                                     this.Enemies[i].type.GetMidPoint(),
                                     this.Enemies[i].type.getRadius()))
                {
                    explosion.AddParticles(this.Enemies[i].type.GetMidPoint(), rand);
                    Enemies.Remove(Enemies[i]);
                    i--;
                }
            }
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                if (collision.RectangleCircle(constraints,
                                     this.Bosses[i].type.GetMidPoint(),
                                     this.Bosses[i].type.getRadius()))
                {
                    explosion.AddParticles(this.Bosses[i].type.GetMidPoint(), rand);
                    if (this.Bosses[i].type.updateHealth(5))
                    {
                        Bosses.Remove(Bosses[i]);
                        i--;
                    }
                }
            }

            #endregion
        }

        public bool mobCollisionDetection(CollisionDetection.Detection collision,
                                          Explosion explosion, Random rand,
                                          Vector2 bulletCoords, int bulletRadius,
                                          int Damage, int DamageMod)
        {
            #region mobCollisionDetection
            hasKilled = false;
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                if (this.Enemies[i].type.getY() >= 0)
                {
                    if (collision.Circle(bulletCoords, bulletRadius,
                                         this.Enemies[i].type.GetMidPoint(),
                                         this.Enemies[i].type.getRadius()))
                    {
                        explosion.AddParticles(this.Enemies[i].type.GetMidPoint(), rand);
                        if (this.Enemies[i].type.updateHealth(Damage * DamageMod))
                        {
                            this.Enemies.Remove(this.Enemies[i]);
                            hasKilled = true;
                        }
                        return true;
                    }
                }
            }
            return false;

            #endregion
        }

        public bool mobBossCollisionDetection(CollisionDetection.Detection collision,
                                              Explosion explosion, Random rand,
                                              Vector2 bulletCoords, int bulletRadius,
                                              int Damage, int DamageMod)
        {
            #region mobBossCollisionDetection
            hasKilled = false;
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                if (this.Bosses[i].type.getY() >= 0)
                {
                    if (collision.Circle(bulletCoords, bulletRadius,
                                         this.Bosses[i].type.GetMidPoint(),
                                         this.Bosses[i].type.getRadius()))
                    {
                        explosion.AddParticles(this.Bosses[i].type.GetMidPoint(), rand);
                        if (this.Bosses[i].type.updateHealth(Damage * DamageMod))
                        {
                            this.Bosses.Remove(this.Bosses[i]);
                            hasKilled = true;
                        }
                        return true;
                    }
                }
            }
            return false;

            #endregion
        }

        public bool mobLaserCD(CollisionDetection.Detection collision,
                                          Explosion explosion, Random rand,
                                          Rectangle bulletCoords,
                                          int Damage, int DamageMod)
        {
            #region mobLaserCD
            hasKilled = false;
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                if (this.Enemies[i].type.getY() >= 0)
                {
                    if (collision.RectangleCircle(bulletCoords,
                                         this.Enemies[i].type.GetMidPoint(),
                                         this.Enemies[i].type.getRadius()))
                    {
                        explosion.AddParticles(this.Enemies[i].type.GetMidPoint(), rand);
                        if (this.Enemies[i].type.updateHealth(Damage * DamageMod))
                        {
                            this.Enemies.Remove(this.Enemies[i]);
                            hasKilled = true;
                        }
                        return true;
                    }
                }
            }
            return false;

            #endregion
        }

        public bool mobBossLaserCD(CollisionDetection.Detection collision,
                                          Explosion explosion, Random rand,
                                          Rectangle bulletCoords,
                                          int Damage, int DamageMod)
        {
            #region mobBossLaserCD
            hasKilled = false;
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                if (this.Enemies[i].type.getY() >= 0)
                {
                    if (collision.RectangleCircle(bulletCoords,
                                         this.Bosses[i].type.GetMidPoint(),
                                         this.Bosses[i].type.getRadius()))
                    {
                        explosion.AddParticles(this.Bosses[i].type.GetMidPoint(), rand);
                        if (this.Bosses[i].type.updateHealth(Damage * DamageMod))
                        {
                            this.Bosses.Remove(this.Bosses[i]);
                            hasKilled = true;
                        }
                        return true;
                    }
                }
            }
            return false;

            #endregion
        }

        public void flames(Flame flameeffect, Random rand)
        {
            foreach (Mob ent in Enemies)
            {
                if (ent.type.GetCanUseFlame())
                {
                    Vector2 coords = ent.type.GetFlameLocation();
                    flameeffect.AddParticles(coords, rand);
                }
            }
            foreach (MobBoss ent in Bosses)
            {
                if (ent.type.GetCanUseFlame())
                {
                    Vector2 coords = ent.type.GetFlameLocation();
                    flameeffect.AddParticles(coords, rand);
                }
            }
        }

        public bool HasKilled()
        {
            return hasKilled;
        }

        public Vector2 getNearestCharacter(Vector2 basecoords)
        {
            #region getNearestCharacter
            Vector2 coords = new Vector2(basecoords.X, -32);
            double dist = 100000000.0;

            for (int i = 0; i < this.Enemies.Count; i++)
            {
                double temp = getDistance(basecoords, this.Enemies[i].type.GetMidPoint());
                if (dist > temp)
                {
                    dist = temp;
                    coords = this.Enemies[i].type.GetMidPoint();
                }
            }
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                double temp = getDistance(basecoords, this.Bosses[i].type.GetMidPoint());
                if (dist > temp)
                {
                    dist = temp;
                    coords = this.Bosses[i].type.GetMidPoint();
                }
            }

            return coords;
            #endregion
        }

        public double getNearestDistance(Vector2 basecoords)
        {
            #region getNearestDistance
            Vector2 coords = new Vector2(basecoords.X, -32);
            double dist = 100000000.0;

            for (int i = 0; i < this.Enemies.Count; i++)
            {
                double temp = getDistance(basecoords, this.Enemies[i].type.GetMidPoint());
                if (dist > temp)
                {
                    dist = temp;
                    coords = this.Enemies[i].type.GetMidPoint();
                }
            }
            for (int i = 0; i < this.Bosses.Count; i++)
            {
                double temp = getDistance(basecoords, this.Bosses[i].type.GetMidPoint());
                if (dist > temp)
                {
                    dist = temp;
                    coords = this.Bosses[i].type.GetMidPoint();
                }
            }

            return dist;
            #endregion
        }

        private double getDistance(Vector2 coords1, Vector2 coords2)
        {
            #region function
            double dx = (double)(coords1.X - coords2.X),
                   dy = (double)(coords1.Y - coords2.Y);
            return Math.Sqrt(dx * dx + dy * dy);

            #endregion
        }

        public bool shouldFlip(bool current)
        {
            #region function
            for (int i = 0; i < Bosses.Count; i++)
            {
                current = Bosses[i].type.shouldFlipScreen(current);
                if (current)
                    break;
            }
            return current;

            #endregion
        }

        public void draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            #region draw
            //// draw background
            spriteBatch.Draw(background, new Vector2(backgroundRenderPos.X, backgroundRenderPos.Y), backgroundColor);

            #endregion
        }

        public void drawEnemies(SpriteBatch spriteBatch, bool isFlipped)
        {
            #region draw
            //// draw enemies
            for (int i = 0; i < Enemies.Count; i++)
                Enemies[i].type.draw(spriteBatch, isFlipped);

            //// draw bosses
            for (int i = 0; i < Bosses.Count; i++)
                Bosses[i].type.draw(spriteBatch, isFlipped);

            #endregion
        }

        public string returnName()
        {
            return name;
        }

        public Vector2 returnPosition()
        {
            return backgroundRenderPos;
        }

        public bool hasWon()
        {
            #region function
            if (Enemies.Count == 0 && Bosses.Count == 0)
                return true;
            return false;

            #endregion
        }

        public List<string> getLevelDescription()
        {
            #region function
            List<string> desc = new List<string>();
            string temp = "";

            int strlength = description.Length;
            for (int i = 0; i < strlength; i += 50)
            {
                temp = description.Substring(i, Math.Min(50, strlength - i));

                desc.Add(temp);
            }

            return desc;

            #endregion
        }

        public int getBGWidth()
        {
            return background.Width;
        }
    }
}

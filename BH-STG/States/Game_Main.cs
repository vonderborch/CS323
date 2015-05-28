/*
 * Component: States - Game
 * Version: 1.3.0
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Audio;
using BH_STG.BarrageEngine.Characters;
using BH_STG.BarrageEngine.CollisionDetection;
using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Items;
using BH_STG.BarrageEngine.Level;
using BH_STG.BarrageEngine.Replay;
using BH_STG.BarrageEngine.Screen;
using BH_STG.BarrageEngine.Weapons;
using BH_STG.Items;
using BH_STG.Levels;
using BH_STG.Particles;
using BH_STG.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace BH_STG.States
{
    class Game_Main : Screen
    {
        #region structs
        public struct ExtraText
        {
            public string text;
            public int life;

            public ExtraText(string nText, int nLife)
            {
                text = nText;
                life = nLife;
            }
        }

        #endregion

        #region available levels
        public enum Levels
        {
            test_level,
            secret_bob_level,
            level1,
            level2,
            level3,
            level4,
            level5,
            level6,
            level7
        }
        Levels currentLevel = Levels.test_level;
        Level level;

        #endregion

        #region available player characters
        public enum Players
        {
            test_player,
            secret_bob_level_player,
            basic,
            doubleway,
            laser,
            singleway,
            christian,
            david,
            jacob,
            fake_player
        }
        Players currentPlayer = Players.test_player;
        #endregion

        #region variables
        Player player;
        Rectangle constraints = new Rectangle(32, 0, 688, 720);
        Vector2 startingPos = new Vector2(688 / 2 - 16, 688 - 64);
        float PlayerSpeed = 3.0f, EnemySpeed = 1.75f, BossSpeed = 1.75f, 
              ItemSpeed = 2.0f, AdjustedDifficulty = 0.0f, Multiplier = 0.0f;
        Random randomNumber = new Random();

        const int FinalTicks = 60, ExtraTextLife = 180, BadgerHealthIncrease = 5,
                  bgIncrease = 2, 
                  NearFar = 30, NearMed = 20, NearClose = 10,
                  NearFarBonus = 2, NearMedBonus = 3, NearCloseBonus = 4,
                  EnemyPoints = 10, BossPoints = 50, LifeLossPoints = 50, HealthLossPoints = 10, 
                  TopHalfPoints = 2, TopQuarterPoints = 4, NearPoints = 2,
                  ItemBase = 10, ItemChance = 1000, ItemChoiceChance = 100;
        Vector2 HealthBase = new Vector2(0, 20), LifeBase = new Vector2(21, 30), BombBase = new Vector2(31, 40), 
                BulletsBase = new Vector2(41, 60), ReloadBase = new Vector2(61, 80), DamageBase = new Vector2(81, 100);

        ulong TickCount;
        string Difficulty;
        int CurrentScore = 0, HighScore = 0,
            Lives = 0, Health = 0, BaseHealth = 0, difficulty = 0, Bombs = 0, DamageMod = 1, ReloadMod = 1, BulletMod = 1,
            seed, ticksPerUpdate = 1, finalt = 0;
        bool usingReplay, replayOver, isFinal, playingMusic = false, IsFlipped = false, badgerIsHere = false;

        Random rand;
        Texture2D GameBackground;
        SpriteFont font;
        Music bgMusic = new Music();
        SoundFX explosion = new SoundFX(),
                getitem = new SoundFX(),
                bomb = new SoundFX();
        string music_file, explosion_file, getitem_file, bomb_file;

        Particles.Explosion explosioneffect;
        Particles.Flame flameeffect;

        List<Weapon> PlayerBullets = new List<Weapon>();
        List<Weapon> EnemyBullets = new List<Weapon>();
        List<Item> items = new List<Item>();
        List<ExtraText> extradrawtext = new List<ExtraText>();
        List<int> extratextremove = new List<int>();

        #endregion

        Color PlayerBulletTint = Color.White,
              EnemyBulletTint = Color.White;
        Replay replay = new Replay();
        Detection collisiondetection = new Detection();

        public Main GameMain { get; set; }
        public Defeat def { get; set; }
        public Victory vic { get; set; }
        public bool loading(Levels levelSelection, Players playerSelection, int diff, Explosion explosionssss, Flame flamer,
                            string musicstr, string explosionstr, string getitemstr, string bombstr,
                            bool useReplay, string filename = "")
        {
            //////// RESET VARIABLES ////////
            PlayerBullets = new List<Weapon>();
            EnemyBullets = new List<Weapon>();
            items = new List<Item>();
            explosioneffect = explosionssss;
            flameeffect = flamer;
            finalt = 0;
            TickCount = 0;
            isFinal = false;
            difficulty = diff;
            AdjustedDifficulty = diff;
            #region set difficulty strings
            switch (difficulty)
            {
                case 1:
                    Difficulty = "Easy";
                    break;
                case 2:
                    Difficulty = "Normal";
                    break;
                case 3:
                    Difficulty = "Hard";
                    break;
                case 4:
                    Difficulty = "Insanity";
                    break;
                case 5:
                    Difficulty = "Test Mode (Invulnerability)";
                    AdjustedDifficulty = 1;
                    break;
            }

            #endregion
            CurrentScore = 0;
            Multiplier = 0;
            #region set lives and health
            if (difficulty == 1) // easy
            {
                Lives = 5;
                BaseHealth = 10;
            }
            else if (difficulty == 2) // normal
            {
                Lives = 3;
                BaseHealth = 10;
            }
            else if (difficulty == 3) // hard
            {
                Lives = 1;
                BaseHealth = 5;
            }
            else if (difficulty == 4) // insanity
            {
                Lives = 0;
                BaseHealth = 1;
            }
            else if (difficulty == 5) // test mode
            {
                Lives = 1000000;
                BaseHealth = 1000000;
            }
            #endregion
            Health = BaseHealth;
            ticksPerUpdate = 1;
            DamageMod = 1;
            ReloadMod = 1;
            BulletMod = 1;
            IsFlipped = false;
            playingMusic = false;
            badgerIsHere = false;
            level = new Level();
            player = new Player();
            extradrawtext = new List<ExtraText>();
            extratextremove = new List<int>();

            // setup the replay system
            replayOver = false;
            usingReplay = useReplay;
            seed = (int)DateTime.Now.Ticks;
            if (usingReplay)
            {
                replay.loadReplay(filename);

                #region set level
                if (replay.getLevel() == "Test Level")
                    levelSelection = Levels.test_level;
                else if (replay.getLevel() == "Secret Bob Level")
                    levelSelection = Levels.secret_bob_level;
                else if (replay.getLevel() == "Level 1")
                    levelSelection = Levels.level1;
                else if (replay.getLevel() == "Level 2")
                    levelSelection = Levels.level2;
                else if (replay.getLevel() == "Level 3")
                    levelSelection = Levels.level3;
                else if (replay.getLevel() == "Level 4")
                    levelSelection = Levels.level4;
                else if (replay.getLevel() == "Level 5")
                    levelSelection = Levels.level5;
                else if (replay.getLevel() == "Level 6")
                    levelSelection = Levels.level6;
                else if (replay.getLevel() == "Level 7")
                    levelSelection = Levels.level7;

                #endregion

                #region set player
                if (replay.getCharacter() == "Test Player")
                {
                    playerSelection = Players.test_player;
                }
                else if (replay.getCharacter() == "Fake Player")
                {
                    playerSelection = Players.fake_player;
                }
                else if (replay.getCharacter() == "Secret Bob Character")
                {
                    playerSelection = Players.secret_bob_level_player;
                }
                else if (replay.getCharacter() == "Basic Character")
                {
                    playerSelection = Players.basic;
                }
                else if (replay.getCharacter() == "DoubleWay Character")
                {
                    playerSelection = Players.doubleway;
                }
                else if (replay.getCharacter() == "Laser Character")
                {
                    playerSelection = Players.laser;
                }
                else if (replay.getCharacter() == "SingleWay Character")
                {
                    playerSelection = Players.singleway;
                }
                else if (replay.getCharacter() == "von der borch")
                {
                    playerSelection = Players.christian;
                }
                else if (replay.getCharacter() == "David Character")
                {
                    playerSelection = Players.david;
                }
                else if (replay.getCharacter() == "Jacob Character")
                {
                    playerSelection = Players.jacob;
                }

                #endregion

                difficulty = replay.getDifficulty();
            }

            //////// Setup Basic Variables ////////
            currentLevel = levelSelection;
            currentPlayer = playerSelection;
            if (usingReplay)
                seed = replay.getSeed();
            rand = new Random(seed);

            //////// Load Fade Texture ////////
            this.FadeTexture = GameMain.Content.Load<Texture2D>("Graphics\\Other\\FadeBase.png");

            //////// Load Game Background Texture ////////
            GameBackground = GameMain.Content.Load<Texture2D>("Graphics\\Other\\FadeBase.png");

            //////// Load Fonts ////////
            font = GameMain.Content.Load<SpriteFont>("Fonts\\General");

            //////// Load Audio ////////
            #region load audio
            bgMusic.GameMain = this.GameMain;
            music_file = musicstr;
            bgMusic.loadContent(music_file);

            explosion.GameMain = this.GameMain;
            explosion_file = explosionstr;
            explosion.loadContent(explosion_file);
            getitem.GameMain = this.GameMain;
            getitem_file = getitemstr;
            getitem.loadContent(getitem_file);
            bomb.GameMain = this.GameMain;
            bomb_file = bombstr;
            bomb.loadContent(bomb_file);

            #endregion

            //////// Load the Level ////////
            #region load level
            #region Test Level
                // Load the Level
                level = new TestLevel(rand, difficulty, EnemySpeed, BossSpeed);
            #endregion
                
            #endregion

            level.GameMain = GameMain;
            level.loading();
            BarrageEngine.HighScores.OfflineScores tempScores = new BarrageEngine.HighScores.OfflineScores();
            tempScores.loadScores();
            HighScore = tempScores.GetTopScore(level.getName());

            // Load the Selected Player
            #region load character
            #region Test Player Character
            if (currentPlayer == Players.test_player)
            {
                player = new Characters.Players.TestPlayer(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region Fake Player Character
            if (currentPlayer == Players.fake_player)
            {
                player = new Characters.Players.FakePlayer(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region Secret Bob Character
            else if (currentPlayer == Players.secret_bob_level_player)
            {
                player.loading(GameMain);
            }
            #endregion

            #region Basic Character
            else if (currentPlayer == Players.basic)
            {
                player = new Characters.Players.BasicCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region Christian Character
            else if (currentPlayer == Players.christian)
            {
                player = new Characters.Players.ChristianCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region David Character
            else if (currentPlayer == Players.david)
            {
                player = new Characters.Players.DavidCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region DoubleWay Character
            else if (currentPlayer == Players.doubleway)
            {
                player = new Characters.Players.DoubleWayCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region Jacob Character
            else if (currentPlayer == Players.jacob)
            {
                player = new Characters.Players.JacobCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region Laser Character
            else if (currentPlayer == Players.laser)
            {
                player = new Characters.Players.LaserCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #region SingleWay Character
            else if (currentPlayer == Players.singleway)
            {
                player = new Characters.Players.SingleWayCharacter(startingPos, 0.15f, Color.White);
                player.loading(GameMain);
            }
            #endregion

            #endregion

            Bombs = player.returnBaseBombs();
            AdjustedDifficulty += player.returnScoreMultiplier();

            if (!usingReplay)
                replay.newReplay(seed, level.returnName(), player.returnName(), difficulty);

            return true;
        }

        public bool update(out Main.GameStates state, InputCommon input)
        {
            state = Main.GameStates.game;

            //// check if user wants to speed up/slow down the replay
            #region check input
            if (usingReplay)
            {
                if (input.Left.pressed && input.Left.pressedTicks == 0)
                {
                    ticksPerUpdate--;
                    if (ticksPerUpdate < 1)
                        ticksPerUpdate = 1;
                }
                else if (input.Right.pressed && input.Right.pressedTicks == 0)
                {
                    ticksPerUpdate++;
                    if (ticksPerUpdate > 32)
                        ticksPerUpdate = 32;
                }
            }

            #endregion

            #region tick
            for (int updateCycle = 0; updateCycle < ticksPerUpdate; updateCycle++)
            {
                #region handle replay system
                if (usingReplay)
                {
                    if (input.Cancel.pressed && input.Cancel.pressedTicks == 0)
                        state = Main.GameStates.pause_menu;
                    //// set input to the saved input
                    #region set input
                    if (replay.isDone())
                        replayOver = true;
                    else
                        input = replay.getNextTick();

                    #endregion
                }
                else
                    replay.addTick(input);

                #endregion

                //// pause the game if asked to
                if (input.Cancel.pressed && input.Cancel.pressedTicks == 0)
                    state = Main.GameStates.pause_menu;
                else if (!replayOver && !isFinal)
                {
                    #region handle control inversion
                    if (IsFlipped)
                        input.CommandVector.X = -input.CommandVector.X;

                    #endregion

                    #region base tick maintenance
                    TickCount++;
                    int enemieskilled = 0, bosseskilled = 0, healthlost = 0, liveslost = 0, tickdifficulty = difficulty;
                    //// update player position
                    player.update(constraints, input.CommandVector, PlayerSpeed);

                    //// update level backgound position
                    level.update(bgIncrease, player.GetMidPoint());

                    //// Check difficulty
                    if (tickdifficulty == 5)
                        tickdifficulty = 1;

                    #endregion

                    #region Item System
                    #region new item
                    // regular items
                    int iRand = rand.Next(ItemChance);
                    if (iRand < (ItemBase * tickdifficulty))
                    {
                        iRand = rand.Next(ItemChoiceChance);

                        // health
                        if (iRand >= HealthBase.X && iRand <= HealthBase.Y)
                            items.Add(new Health(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        // life
                        else if (iRand >= LifeBase.X && iRand <= LifeBase.Y)
                            items.Add(new Life(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        // bomb
                        else if (iRand >= BombBase.X && iRand <= BombBase.Y)
                            items.Add(new Bomb(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        // bullet count 
                        else if (iRand >= BulletsBase.X && iRand <= BulletsBase.Y)
                            items.Add(new Bullets(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        // player reload speed
                        else if (iRand >= ReloadBase.X && iRand <= ReloadBase.Y)
                            items.Add(new Reload(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        // player damage
                        else if (iRand >= DamageBase.X && iRand <= DamageBase.Y)
                            items.Add(new Damage(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                        items[items.Count - 1].loading(GameMain);
                    }
                    #endregion

                    #region the badger
                    // badger item
                    if (level.getBadgerStart() != -1 && level.getBadgerStart() <= (int)TickCount)
                    {
                        iRand = rand.Next(100000);
                        if (iRand < level.getBadgerChance())
                        {
                            items.Add(new Badger(new Vector2(32 + rand.Next(688), -64), ItemSpeed, .05f, Color.White));
                            items[items.Count - 1].loading(GameMain);
                        }
                    }
                    #endregion

                    #region update items
                    for (int i = 0; i < items.Count; i++)
                    {
                        bool remove = items[i].baseUpdate(bgIncrease);
                        if (remove)
                        {
                            items.Remove(items[i]);
                            continue;
                        }

                        //// check for collision against player
                        if (player.checkCollision(collisiondetection,
                                                    explosioneffect, randomNumber,
                                                    items[i].GetMidPoint(),
                                                    items[i].getRadius(), false))
                        {
                            bool manualadd = false;
                            if (items[i].getType() == Item.ItemType.life)
                                Lives++;
                            else if (items[i].getType() == Item.ItemType.health)
                                Health++;
                            else if (items[i].getType() == Item.ItemType.damage)
                                DamageMod++;
                            else if (items[i].getType() == Item.ItemType.reloadspeed)
                                ReloadMod++;
                            else if (items[i].getType() == Item.ItemType.bullets)
                                BulletMod++;
                            else if (items[i].getType() == Item.ItemType.badger && badgerIsHere == false)
                            {
                                level.addBadger();
                                badgerIsHere = true;
                            }
                            else if (items[i].getType() == Item.ItemType.badger && badgerIsHere == true)
                            {
                                manualadd = true;
                                level.strengthenBadger(BadgerHealthIncrease);
                                extradrawtext.Add(new ExtraText("The badger is getting stronger...", ExtraTextLife + 1));
                            }
                            else
                                Bombs++;

                            // add pickup text if not already asked
                            if (!manualadd)
                                extradrawtext.Add(new ExtraText(items[i].getPickupText(), ExtraTextLife + 1));
                            items.Remove(items[i]);
                            i--;
                            getitem.playSound(getitem_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                        }
                    }

                    #endregion

                    #region use items
                    if (input.UseItem.pressed && input.UseItem.pressedTicks == 0)
                    {
                        // use bomb
                        if (Bombs > 0)
                        {
                            level.bombCollision(collisiondetection, explosioneffect, rand, constraints);
                            Bombs--;
                            bomb.playSound(bomb_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                            input.controllerVibration(GameMain.gamesettings.getInputMethod(), 1.0f, 1.0f);
                        }
                    }

                    #endregion

                    #endregion

                    #region player weapons and updates
                    //// update player bullets
                    #region update player bullets
                    for (int i = 0; i < PlayerBullets.Count; i++)
                    {
                        //// update position
                        #region position update
                        if (PlayerBullets[i].returnType() == Weapon.WeaponType.douubleway ||
                            PlayerBullets[i].returnType() == Weapon.WeaponType.singleway ||
                            PlayerBullets[i].returnType() == Weapon.WeaponType.secretplayer)
                        {
                            Vector2 bcoords = player.GetMidPoint();
                            bcoords.Y += bgIncrease;
                            PlayerBullets[i].setPlayerPos(level.getNearestCharacter(bcoords));
                        }
                        PlayerBullets[i].updatePosition();

                        #endregion

                        //// update life
                        #region bullet life update
                        if (PlayerBullets[i].shouldDie())
                        {
                            PlayerBullets.Remove(PlayerBullets[i]);
                            i--;
                            continue;
                        }

                        #endregion

                        //// update collision detection
                        #region non-Laser Collision Detection
                        if (PlayerBullets[i].returnType() != Weapon.WeaponType.laser)
                        {
                            //// update collision detection vs. enemies
                            if (level.mobCollisionDetection(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        PlayerBullets[i].GetMidPoint(),
                                                        PlayerBullets[i].getRadius(),
                                                        player.returnBaseDamage(), DamageMod))
                            {
                                PlayerBullets.Remove(PlayerBullets[i]);
                                i--;
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                if (level.HasKilled())
                                    enemieskilled++;
                                continue;
                            }

                            //// update collision detection vs. bosses
                            if (level.mobBossCollisionDetection(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        PlayerBullets[i].GetMidPoint(),
                                                        PlayerBullets[i].getRadius(),
                                                        player.returnBaseDamage(), DamageMod))
                            {
                                PlayerBullets.Remove(PlayerBullets[i]);
                                i--;
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                if (level.HasKilled())
                                    bosseskilled++;
                                continue;
                            }
                        }
                        #endregion

                        #region laser collision detection
                        else
                        {
                            //// update collision detection vs. enemies
                            if (level.mobLaserCD(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        PlayerBullets[i].getBox(),
                                                        player.returnBaseDamage(), DamageMod))
                            {
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                if (level.HasKilled())
                                    enemieskilled++;
                                continue;
                            }

                            //// update collision detection vs. bosses
                            if (level.mobBossLaserCD(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        PlayerBullets[i].getBox(),
                                                        player.returnBaseDamage(), DamageMod))
                            {
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                if (level.HasKilled())
                                    bosseskilled++;
                                continue;
                            }
                        }
                        #endregion
                    }
                    #endregion

                    //// should the player auto-fire?
                    #region should fire?
                    if (player.shouldFireWeapon(ReloadMod))
                    {
                        if (BulletMod == 1)
                        {
                            Vector2 spawnCoords = player.GetMidPoint();
                            spawnCoords.X -= 8;
                            spawnCoords.Y -= 8;

                            if (player.returnWeaponType() == Weapon.WeaponType.test)
                                PlayerBullets.Add(new TestBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.basic)
                                PlayerBullets.Add(new BasicBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.douubleway)
                                PlayerBullets.Add(new DoubleWayBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.laser)
                                PlayerBullets.Add(new LaserBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.randomdirection)
                                PlayerBullets.Add(new RandomDirectionBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.randomdirectionspeed)
                                PlayerBullets.Add(new RandomDirectionSpeedBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.randomspeed)
                                PlayerBullets.Add(new RandomSpeedBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.singleway)
                                PlayerBullets.Add(new SingleWayBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                            else if (player.returnWeaponType() == Weapon.WeaponType.secretplayer)
                                PlayerBullets.Add(new SecretPlayerBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                        }
                        else
                        {
                            Vector2 spawnCoords = player.GetMidPoint();
                            Rectangle rec = player.GetSize();
                            spawnCoords.X = player.getX() - 8;
                            spawnCoords.Y -= 8;
                            float dx = rec.Width / (BulletMod - 1);

                            for (int i = 0; i < BulletMod; i++)
                            {
                                if (player.returnWeaponType() == Weapon.WeaponType.test)
                                    PlayerBullets.Add(new TestBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.basic)
                                    PlayerBullets.Add(new BasicBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.douubleway)
                                    PlayerBullets.Add(new DoubleWayBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.laser)
                                    PlayerBullets.Add(new LaserBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.randomdirection)
                                    PlayerBullets.Add(new RandomDirectionBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.randomdirectionspeed)
                                    PlayerBullets.Add(new RandomDirectionSpeedBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.randomspeed)
                                    PlayerBullets.Add(new RandomSpeedBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.singleway)
                                    PlayerBullets.Add(new SingleWayBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));
                                else if (player.returnWeaponType() == Weapon.WeaponType.secretplayer)
                                    PlayerBullets.Add(new SecretPlayerBullet(GameMain, PlayerBulletTint, spawnCoords, rand, IsFlipped, true));

                                spawnCoords.X += dx;
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region enemy weapons and updates
                    //// update enemy bullets
                    #region update enemy bullets
                    for (int i = 0; i < EnemyBullets.Count; i++)
                    {
                        //// update position
                        #region position update
                        if (EnemyBullets[i].returnType() == Weapon.WeaponType.douubleway ||
                            EnemyBullets[i].returnType() == Weapon.WeaponType.singleway ||
                            EnemyBullets[i].returnType() == Weapon.WeaponType.secretenemy)
                            EnemyBullets[i].setPlayerPos(player.GetMidPoint());
                        EnemyBullets[i].updatePosition();

                        #endregion

                        //// update life
                        #region bullet life update
                        if (EnemyBullets[i].shouldDie())
                        {
                            EnemyBullets.Remove(EnemyBullets[i]);
                            i--;
                            continue;
                        }

                        #endregion

                        //// update collision detection vs. player
                        #region non-Laser Collision Detection
                        if (EnemyBullets[i].returnType() != Weapon.WeaponType.laser)
                        {
                            if (player.checkCollision(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        EnemyBullets[i].GetMidPoint(),
                                                        EnemyBullets[i].getRadius()))
                            {
                                EnemyBullets.Remove(EnemyBullets[i]);
                                i--;
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                Health--;
                                healthlost++;
                                CurrentScore -= 10;
                                if (Health == 0)
                                {
                                    Health = BaseHealth;
                                    Lives--;
                                    liveslost++;
                                    ReloadMod--;
                                    if (ReloadMod < 1)
                                        ReloadMod = 1;
                                    DamageMod--;
                                    if (DamageMod < 1)
                                        DamageMod = 1;
                                    BulletMod--;
                                    if (BulletMod < 1)
                                        BulletMod = 1;
                                }
                                input.controllerVibration(GameMain.gamesettings.getInputMethod(), 1.0f, 1.0f);
                                continue;
                            }
                        }
                        #endregion

                        #region laser Collision Detection
                        else
                        {
                            if (player.checkLaserCD(collisiondetection,
                                                        explosioneffect, randomNumber,
                                                        EnemyBullets[i].getBox()))
                            {
                                i--;
                                explosion.playSound(explosion_file, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                                Health--;
                                healthlost++;
                                CurrentScore -= 10;
                                if (Health == 0)
                                {
                                    Health = BaseHealth;
                                    Lives--;
                                    liveslost++;
                                    ReloadMod--;
                                    if (ReloadMod < 1)
                                        ReloadMod = 1;
                                    DamageMod--;
                                    if (DamageMod < 1)
                                        DamageMod = 1;
                                    BulletMod--;
                                    if (BulletMod < 1)
                                        BulletMod = 1;
                                }
                                input.controllerVibration(GameMain.gamesettings.getInputMethod(), 1.0f, 1.0f);
                                continue;
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region create new bullets
                    List<Level.bulletUpdates> newBullets = level.getNewBullets();
                    foreach (Level.bulletUpdates nbullet in newBullets)
                    {
                        Vector2 spawnCoords = nbullet.coords;
                        spawnCoords.X -= 8;
                        spawnCoords.Y -= 8;
                        if (nbullet.type == Weapon.WeaponType.test)
                            EnemyBullets.Add(new TestBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.basic)
                            EnemyBullets.Add(new BasicBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.douubleway)
                            EnemyBullets.Add(new DoubleWayBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.laser)
                            EnemyBullets.Add(new LaserBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.randomdirection)
                            EnemyBullets.Add(new RandomDirectionBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.randomdirectionspeed)
                            EnemyBullets.Add(new RandomDirectionSpeedBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.randomspeed)
                            EnemyBullets.Add(new RandomSpeedBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.singleway)
                            EnemyBullets.Add(new SingleWayBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.secretenemy)
                            EnemyBullets.Add(new SecretEnemyBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.badger)
                            EnemyBullets.Add(new BadgerBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                        else if (nbullet.type == Weapon.WeaponType.mine)
                            EnemyBullets.Add(new MineBullet(GameMain, EnemyBulletTint, spawnCoords, rand, IsFlipped));
                    }

                    #endregion

                    #region flip the screen?
                    bool lastFlipped = IsFlipped;
                    IsFlipped = level.shouldFlip(IsFlipped);
                    if (lastFlipped != IsFlipped)
                    {
                        if (IsFlipped)
                            extradrawtext.Add(new ExtraText("Something weird just happened...", ExtraTextLife + 1));
                        else
                            extradrawtext.Add(new ExtraText("Looks like everything is normal now...", ExtraTextLife + 1));
                    }

                    #endregion

                    #endregion

                    #region scoring
                    //// update score
                    int scoreToAdd = 1;
                    scoreToAdd -= (int)(HealthLossPoints * healthlost);
                    scoreToAdd -= (int)(LifeLossPoints * liveslost);
                    scoreToAdd += (int)(EnemyPoints * enemieskilled);
                    scoreToAdd += (int)(BossPoints * bosseskilled);

                    Vector2 playerCoords = player.GetMidPoint();
                    Multiplier = AdjustedDifficulty;
                    if (playerCoords.Y < (constraints.Height / 4))
                        Multiplier *= TopQuarterPoints;
                    else if (playerCoords.Y < (constraints.Height / 2))
                        Multiplier *= TopHalfPoints;

                    double dist = level.getNearestDistance(playerCoords);
                    if (dist < NearClose)
                        Multiplier *= NearCloseBonus;
                    if (dist < NearMed)
                        Multiplier *= NearMedBonus;
                    if (dist < NearFar)
                        Multiplier *= NearFarBonus;

                    scoreToAdd *= (int)Multiplier;
                    CurrentScore += scoreToAdd;
                    #endregion

                    #region handle victory/defeat system
                    if (!usingReplay)
                    {
                        if (level.hasWon())
                        {
                            GameMain.gamesettings.setSecret(true);
                            GameMain.gamesettings.saveSettings();
                            isFinal = true;
                        }
                        else if (Health < 1 && Lives < 0)
                            isFinal = true;
                        else if (Lives < 0)
                            isFinal = true;
                    }

                    #endregion

                    #region update extra text
                    #region life update
                    for (int i = 0; i < extradrawtext.Count; i++)
                    {
                        ExtraText temp = extradrawtext[i];
                        temp.life--;
                        if (temp.life <= 0)
                            extratextremove.Add(i);
                        extradrawtext[i] = temp;
                    }

                    #endregion

                    #region cleanup
                    for (int i = (extratextremove.Count - 1); i >= 0; i--)
                        extradrawtext.RemoveAt(i);

                    extratextremove.Clear();

                    #endregion

                    #endregion

                    #region update flames
                    // add flame particles to player's position if desired
                    if (player.GetCanUseFlame())
                    {
                        Vector2 coords = player.GetFlameLocation();
                        flameeffect.AddParticles(coords, rand);
                    }
                    // update enemy flames
                    level.flames(flameeffect, rand);

                    #endregion
                }
                else if (isFinal)
                {
                    #region final ticks
                    finalt++;
                    if (finalt >= FinalTicks)
                    {
                        if (level.hasWon())
                        {
                            state = Main.GameStates.victory;
                            vic.setMatchInfo(GameMain.gamesettings.user, CurrentScore.ToString(), Difficulty, player.returnName(), level.returnName(), difficulty, CurrentScore);
                        }
                        else
                        {
                            if (Lives < 0)
                            {
                                state = Main.GameStates.defeat;
                                def.setMatchInfo(GameMain.gamesettings.user, CurrentScore.ToString(), Difficulty, player.returnName(), level.returnName(), difficulty, CurrentScore);
                            }
                        }
                        extradrawtext.Add(new ExtraText("The Game is Over!", ExtraTextLife + 1));
                    }
                    #endregion
                }
            }
            #endregion

            return true;
        }

        public void saveReplay()
        {
            replay.saveReplay(level.returnName(), player.returnName());
        }

        public bool isUsingReplay()
        {
            return usingReplay;
        }

        public bool isPlayingMusic()
        {
            return playingMusic;
        }

        public void playMusic(int volume)
        {
            if (!playingMusic)
            {
                bgMusic.playSong(music_file, true, volume, 0.0f);
                playingMusic = true;
            }
        }

        public void stopMusic()
        {
            playingMusic = false;
            bgMusic.stopMusic();
        }

        public void updateMusicVolume(int volume)
        {
            if (playingMusic)
                bgMusic.changeVolume(volume, 0.0f);
        }

        public bool GetIsFlipped()
        {
            return IsFlipped;
        }

        public bool draw(SpriteBatch spriteBatch)
        {
            //// draw level background
            level.draw(spriteBatch, IsFlipped);

            //// draw enemies
            level.drawEnemies(spriteBatch, IsFlipped);

            //// draw player
            player.draw(spriteBatch, IsFlipped);

            //// draw items
            for (int i = 0; i < items.Count; i++)
                items[i].draw(spriteBatch, IsFlipped);

            //// draw bullets
            for (int i = 0; i < PlayerBullets.Count; i++)
                PlayerBullets[i].draw(spriteBatch, IsFlipped);
            for (int i = 0; i < EnemyBullets.Count; i++)
                EnemyBullets[i].draw(spriteBatch, IsFlipped);

            //// Draw game background
            spriteBatch.Draw(GameBackground, new Rectangle(0, 0, 32, GameMain.videosettings.height), Color.White);
            spriteBatch.Draw(GameBackground, new Rectangle(32 + level.getBGWidth(), 0, GameMain.videosettings.width, GameMain.videosettings.height), Color.White);

            //// Draw text
            spriteBatch.DrawString(font, "Level: " + level.returnName(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(32)), Color.White);
            spriteBatch.DrawString(font, "Difficulty: " + Difficulty, new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(52)), Color.White);
            spriteBatch.DrawString(font, "Current Score: " + CurrentScore.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(72)), Color.White);
            spriteBatch.DrawString(font, "Multiplier: " + Multiplier.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(765),
                                   GameMain.videosettings.returnModifiedY(92)), Color.White);
            spriteBatch.DrawString(font, "High Score: " + HighScore.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(112)), Color.White);
            spriteBatch.DrawString(font, "Lives: " + Lives.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(132)), Color.White);
            spriteBatch.DrawString(font, "Health: " + Health.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(152)), Color.White);
            spriteBatch.DrawString(font, "Bombs: " + Bombs.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(172)), Color.White);
            spriteBatch.DrawString(font, "Damage Modifier: " + (DamageMod).ToString() + "x", new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(192)), Color.White);
            spriteBatch.DrawString(font, "Reload Speed Modifier: 1/" + ReloadMod.ToString() + "x", new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(212)), Color.White);
            spriteBatch.DrawString(font, "Number of Bullets Fired Per Reload: " + BulletMod.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                   GameMain.videosettings.returnModifiedY(232)), Color.White);

            int y = 320;
            foreach (ExtraText temp in extradrawtext)
            {
                spriteBatch.DrawString(font, temp.text, new Vector2(GameMain.videosettings.returnModifiedX(750),
                                       GameMain.videosettings.returnModifiedY(y)), Color.White);
                if (temp.life <= (ExtraTextLife / 3))
                {
                    float ratio = (float)temp.life / ((float)ExtraTextLife / 3.0f);
                    Color alphaColor = new Color(Color.Black, 1 - ratio);

                    Vector2 endCoords = font.MeasureString(temp.text);

                    spriteBatch.Draw(FadeTexture, new Rectangle(750, y, (int)endCoords.X + 5, (int)endCoords.Y + 5), alphaColor);
                }
                y += 20;
            }

            //// Draw Level Over Text
            if (isFinal)
                spriteBatch.DrawString(font, "Level Over", new Vector2(GameMain.videosettings.returnModifiedX(750),
                                       GameMain.videosettings.returnModifiedY(250)), Color.White);

            //// Draw Replay Over Screen Stuff
            if (usingReplay)
                spriteBatch.DrawString(font, "Replay Speed: " + ticksPerUpdate.ToString(), new Vector2(GameMain.videosettings.returnModifiedX(750),
                                       GameMain.videosettings.returnModifiedY(270)), Color.White);
            if (replayOver)
                spriteBatch.DrawString(font, "REPLAY OVER, PRESS ESC TO EXIT", new Vector2(GameMain.videosettings.returnModifiedX(750),
                                       GameMain.videosettings.returnModifiedY(290)), Color.White);

            return true;
        }
    }
}

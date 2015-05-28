/*
 * Component: Main
 * Version: 1.2.10
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.HighScores;
using BH_STG.BarrageEngine.Input;
//using BH_STG.BarrageEngine.Screen;
using BH_STG.Particles;
using BH_STG.States;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
//using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BH_STG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        #region variable definitions
        #region VideoSettings
        public struct VideoSettings
        {
            int basewidth, baseheight;
            float wRatio, hRatio;
            public int width, height, baseFPS;
            public float fps;
            public bool fullscreen, vsync, multisample, shouldChange;
            string filename, settingsversion;

            public VideoSettings(int bWidth, int bHeight, int FPS, bool fScreen, bool VSYNC, bool MULTISAMPLE, string SETTINGSVER)
            {
                basewidth = bWidth;
                baseheight = bHeight;
                width = bWidth;
                height = bHeight;
                baseFPS = FPS;
                fps = 1.0f / baseFPS;
                fullscreen = fScreen;
                vsync = VSYNC;
                multisample = MULTISAMPLE;
                wRatio = 1.0f;
                hRatio = 1.0f;
                filename = "";
                shouldChange = false;
                settingsversion = SETTINGSVER;
            }

            public void applyChanges(GraphicsDeviceManager graphics)
            {
                graphics.SynchronizeWithVerticalRetrace = vsync;
                graphics.PreferredBackBufferWidth = width;
                graphics.PreferredBackBufferHeight = height;
                graphics.IsFullScreen = fullscreen;
                graphics.PreferMultiSampling = multisample;
                graphics.ApplyChanges();
                shouldChange = false;
            }

            public void changeResolution(int newWidth, int newHeight)
            {
                width = newWidth;
                height = newHeight;
                wRatio = (float)width / (float)basewidth;
                hRatio = (float)height / (float)baseheight;
                shouldChange = true;
            }

            public void setFullscreen(bool full)
            {
                fullscreen = full;
                shouldChange = true;
            }

            public void setFPS(int FPS)
            {
                baseFPS = FPS;
                fps = 1.0f / baseFPS;
                shouldChange = true;
            }

            public void setVSYNC (bool VSYNC)
            {
                vsync = VSYNC;
                shouldChange = true;
            }

            public void setMULTISAMPLE(bool MULTISAMPLE)
            {
                multisample = MULTISAMPLE;
                shouldChange = true;
            }

            public int returnModifiedX (int x)
            {
                return (int)(x * wRatio);
            }

            public int returnModifiedY(int y)
            {
                return (int)(y * hRatio);
            }

            public void setFilename(string file)
            {
                filename = file;
            }

            public void saveSettings()
            {
                XmlDocument file = new XmlDocument();
                file.PreserveWhitespace = false;

                string data = "<settings><settingsversion>" + settingsversion +
                              "</settingsversion><width>" + width +
                              "</width><height>" + height +
                              "</height><fps>" + baseFPS +
                              "</fps><fullscreen>" + fullscreen +
                              "</fullscreen><vsync>" + vsync +
                              "</vsync><multisample>" + multisample +
                              "</multisample></settings>";

                // delete the old settings file
                if (File.Exists(filename))
                    File.Delete(filename);
                file.LoadXml(data);
                file.Save(filename);
                shouldChange = true;
            }
        }

        #endregion

        #region GameSettings
        public struct GameSettings
        {
            public bool unlockedSecret, testmode;
            public string user, inputMethod;
            public int musicvolume, soundeffectsvolume;
            string filename, settingsversion;

            public GameSettings(string nUser, string nInputMethod, int nMusicVolume, int nSoundEffectsVolume, string SETTINGSVER)
            {
                unlockedSecret = false;
                testmode = false;
                user = nUser;
                inputMethod = nInputMethod;
                musicvolume = nMusicVolume;
                soundeffectsvolume = nSoundEffectsVolume;
                filename = "";
                settingsversion = SETTINGSVER;
            }

            public InputCommon.InputMethod getInputMethod()
            {
                if (inputMethod == "keyboard")
                    return InputCommon.InputMethod.keyboard;
                else if (inputMethod == "gamepad")
                    return InputCommon.InputMethod.gamepad;
                else
                    return InputCommon.InputMethod.mouse;
            }

            public void setMusicVolume(int nVolume)
            {
                musicvolume = nVolume;
                if (musicvolume < 0)
                    musicvolume = 100;
                else if (musicvolume > 100)
                    musicvolume = 0;
            }

            public void setSoundEffectsVolume(int nVolume)
            {
                soundeffectsvolume = nVolume;
                if (soundeffectsvolume < 0)
                    soundeffectsvolume = 100;
                else if (soundeffectsvolume > 100)
                    soundeffectsvolume = 0;
            }

            public void setInputMethod(string nMethod)
            {
                inputMethod = nMethod;
            }

            public void setUser(string nUser)
            {
                user = nUser;
            }

            public void setSecret(bool nSecret)
            {
                unlockedSecret = nSecret;
            }

            public void setFilename(string file)
            {
                filename = file;
            }

            public void setTestMode(bool mode)
            {
                testmode = mode;
            }

            public void saveSettings()
            {
                XmlDocument file = new XmlDocument();
                file.PreserveWhitespace = false;

                string data = "<settings><settingsversion>" + settingsversion +
                              "</settingsversion><user>" + user +
                              "</user><inputMethod>" + inputMethod +
                              "</inputMethod><musicvolume>" + musicvolume +
                              "</musicvolume><soundeffectsvolume>" + soundeffectsvolume +
                              "</soundeffectsvolume><testmode>" + testmode +
                              "</testmode><unlockedsecret>" + unlockedSecret +
                              "</unlockedsecret></settings>";

                // delete the old settings file
                if (File.Exists(filename))
                    File.Delete(filename);
                file.LoadXml(data);
                file.Save(filename);
            }
        }

        #endregion

        #region TransitionState
        public enum TransitionState
        {
            normal,
            transition_in,
            transition_out
        }
        #endregion

        #region GameState
        public enum GameStates
        {
            initial_loading,
            main_menu,
            help,
            about,
            cheats,
            options_menu,
            highscore_menu,
            replay_menu,
            level_selection,
            difficulty_selection,
            character_selection,
            game_loading,
            game,
            pause_menu,
            victory,
            defeat,
            exit
        }
        #endregion

        #endregion

        #region variables
        #region graphics variables
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Explosion explosioneffect;
        Flame flameeffect;
        #endregion

        #region gamestate variables
        public GameStates state = GameStates.initial_loading;
        GameStates nextstate = GameStates.initial_loading;
        TransitionState transitionstate = TransitionState.normal;

        Initial_Loading initialloading = new Initial_Loading();
        Game_Loading gameloading = new Game_Loading();
        Main_Menu mainmenu = new Main_Menu();
        Game_Main game = new Game_Main();
        HighScore_Menu highscoremenu = new HighScore_Menu();
        Options_Menu optionsmenu = new Options_Menu();
        Replay_Menu replaymenu = new Replay_Menu();
        Difficulty_Selection difficultyselection = new Difficulty_Selection();
        Level_Selection levelselection = new Level_Selection();
        Character_Selection characterselection = new Character_Selection();
        Pause_Menu pausemenu = new Pause_Menu();
        Victory victory = new Victory();
        Defeat defeat = new Defeat();
        About about = new About();
        Help help = new Help();
        Cheats cheats = new Cheats();
        #endregion

        #region new gamestates - WIP
        /* List<Screen> ProgStates = new List<Screen>(); */

        #endregion

        #region input variables
        InputCommon input = new InputCommon(300, 300, 30);

        #endregion

        #region general program variables
        //Color baseBackgroundColor = new Color(109, 33, 20);
        Color baseBackgroundColor = Color.Black;
        public static string filesettingsversion = "1.0.3";
        public VideoSettings videosettings = new VideoSettings(1280, 720, 60, false, true, true, filesettingsversion);
        public GameSettings gamesettings = new GameSettings("default", "keyboard", 50, 50, filesettingsversion);
        public OfflineScores Offlinescores = new OfflineScores();
        public bool reloadMenus = true;

        #endregion

        #region gameplay variables
        bool useReplay = false;
        string filename = "";
        States.Game_Main.Levels level = States.Game_Main.Levels.test_level;
        States.Game_Main.Players playerChar = States.Game_Main.Players.test_player;
        int difficulty = 0;
        const int MaxExplosionParticles = 200,
                  MaxFlameParticles = 1000;

        #endregion

        #endregion

        public bool debugmode = true;
        public string versionInfo = "1.0.0 - 05/01/2014";

        public Main()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            explosioneffect = new Explosion(this, MaxExplosionParticles);
            Components.Add(explosioneffect);
            flameeffect = new Flame(this, MaxFlameParticles);
            Components.Add(flameeffect);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.Window.Position = new Point(0, 0);

            videosettings.applyChanges(graphics);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            /*ProgStates.Add(new Initial_Loading());
            ProgStates.Add(new Main_Menu());
            ProgStates.Add(new HighScore_Menu());
            ProgStates.Add(new Options_Menu());
            ProgStates.Add(new Replay_Menu());
            ProgStates.Add(new Game_Loading());
            ProgStates.Add(new Difficulty_Selection());
            ProgStates.Add(new Character_Selection());
            ProgStates.Add(new Level_Selection());
            ProgStates.Add(new Pause_Menu());
            ProgStates.Add(new Victory());
            ProgStates.Add(new Defeat());
            ProgStates.Add(new About());
            ProgStates.Add(new Help());
            ProgStates.Add(new Cheats());*/

            // set state mains to GameMain
            initialloading.GameMain = this;
            mainmenu.setGameMain(this);
            highscoremenu.setGameMain(this);
            optionsmenu.setGameMain(this);
            replaymenu.GameMain = this;
            gameloading.GameMain = this;
            difficultyselection.setGameMain(this);
            characterselection.setGameMain(this);
            levelselection.setGameMain(this);
            pausemenu.setGameMain(this);
            victory.setGameMain(this);
            defeat.setGameMain(this);
            about.setGameMain(this);
            help.setGameMain(this);
            input.GameMain = this;
            cheats.setGameMain(this);

            // load the program!
            initialloading.loading(input);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            bool stillTransitioning = false;
            #region update video setting
            TargetElapsedTime = TimeSpan.FromSeconds(videosettings.fps);
            if (videosettings.shouldChange)
                videosettings.applyChanges(graphics);

            #endregion

            #region update input
            gamesettings.getInputMethod();
            input.update(gamesettings.getInputMethod());

            if (state != GameStates.game)
                input.resetVibration();

            #endregion

            #region update music volumes
            mainmenu.updateMusicVolume(gamesettings.musicvolume);
            victory.updateMusicVolume(gamesettings.musicvolume);
            defeat.updateMusicVolume(gamesettings.musicvolume);

            #endregion

            #region Tick Logic

            if (transitionstate == TransitionState.normal)
            {
                #region game state logic
                switch (state)
                {
                    #region initial loading
                    case GameStates.initial_loading:
                        initialloading.update(out nextstate, graphics, mainmenu, replaymenu, highscoremenu, optionsmenu, 
                                              gameloading, pausemenu, levelselection, difficultyselection, victory, 
                                              defeat, characterselection, about, help, cheats);
                        break;

                    #endregion

                    #region main menu
                    case GameStates.main_menu:
                        useReplay = false;
                        if (victory.isPlayingMusic())
                            victory.stopMusic();
                        if (defeat.isPlayingMusic())
                            defeat.stopMusic();
                        mainmenu.update(out nextstate, input);
                        break;

                    #endregion

                    #region options menu
                    case GameStates.options_menu:
                        optionsmenu.update(out nextstate, input);
                        break;

                    #endregion

                    #region about
                    case GameStates.about:
                        about.update(out nextstate, input);
                        break;

                    #endregion

                    #region help
                    case GameStates.help:
                        help.update(out nextstate, input);
                        break;

                    #endregion

                    #region high scores
                    case GameStates.highscore_menu:
                        highscoremenu.update(out nextstate, input);
                        break;

                    #endregion

                    #region replay menu
                    case GameStates.replay_menu:
                        filename = replaymenu.update(out nextstate, input);
                        if (filename != "")
                            useReplay = true;
                        else
                            useReplay = false;
                        break;

                    #endregion

                    #region level selection
                    case GameStates.level_selection:
                        level = levelselection.update(out nextstate, input);
                        break;

                    #endregion

                    #region difficulty selection
                    case GameStates.difficulty_selection:
                        difficulty = difficultyselection.update(out nextstate, input);
                        break;

                    #endregion

                    #region character selection
                    case GameStates.character_selection:
                        playerChar = characterselection.update(out nextstate, input);
                        break;

                    #endregion

                    #region game loading
                    case GameStates.game_loading:
                        gameloading.update(out nextstate, game, defeat, victory, level, playerChar, explosioneffect, flameeffect,
                                           "GoCart", "BEEP1A", "BEEP1A", "BEEP1A", 
                                           difficulty, useReplay, filename);
                        break;

                    #endregion

                    #region game
                    case GameStates.game:
                        game.update(out nextstate, input);
                        break;

                    #endregion

                    #region pause menu
                    case GameStates.pause_menu:
                        pausemenu.game = game;
                        pausemenu.update(out nextstate, input);
                        break;

                    #endregion

                    #region victory
                    case GameStates.victory:
                        victory.game = game;
                        victory.update(out nextstate, input);
                        break;

                    #endregion

                    #region defeat
                    case GameStates.defeat:
                        defeat.game = game;
                        defeat.update(out nextstate, input);
                        break;

                    #endregion

                    #region exit
                    case GameStates.exit:
                        Exit();
                        break;

                    #endregion

                    #region cheats
                    case GameStates.cheats:
                        cheats.update(out nextstate, input);
                        break;
                    #endregion
                }
                #endregion
            }
            else if (transitionstate == TransitionState.transition_in)
            {
                #region transition in logic
                switch (nextstate)
                {
                    #region initial loading
                    case GameStates.initial_loading:
                        stillTransitioning = initialloading.UpdateTransitionIn();
                        break;

                    #endregion

                    #region main menu
                    case GameStates.main_menu:
                        if (reloadMenus || optionsmenu.reload() || cheats.reload() || victory.reload())
                        {
                            levelselection.createMenu();
                            characterselection.createMenu();
                            reloadMenus = false;
                        }
                        mainmenu.playMusic(gamesettings.musicvolume);
                        stillTransitioning = mainmenu.UpdateTransitionIn();
                        break;

                    #endregion

                    #region about
                    case GameStates.about:
                        stillTransitioning = about.UpdateTransitionIn();
                        break;

                    #endregion

                    #region help
                    case GameStates.help:
                        stillTransitioning = help.UpdateTransitionIn();
                        break;

                    #endregion

                    #region options menu
                    case GameStates.options_menu:
                        if (reloadMenus = optionsmenu.reload())
                            optionsmenu.resetReload();
                        optionsmenu.updateSettings();
                        stillTransitioning = optionsmenu.UpdateTransitionIn();
                        break;

                    #endregion

                    #region high scores
                    case GameStates.highscore_menu:
                        Offlinescores.loadScores();
                        stillTransitioning = highscoremenu.UpdateTransitionIn();
                        break;

                    #endregion

                    #region replay menu
                    case GameStates.replay_menu:
                        stillTransitioning = replaymenu.UpdateTransitionIn();
                        break;

                    #endregion

                    #region level selection
                    case GameStates.level_selection:
                        stillTransitioning = levelselection.UpdateTransitionIn();
                        break;

                    #endregion

                    #region difficulty selection
                    case GameStates.difficulty_selection:
                        stillTransitioning = difficultyselection.UpdateTransitionIn();
                        break;

                    #endregion

                    #region character selection
                    case GameStates.character_selection:
                        stillTransitioning = characterselection.UpdateTransitionIn();
                        break;

                    #endregion

                    #region game loading
                    case GameStates.game_loading:
                        stillTransitioning = gameloading.UpdateTransitionIn();
                        break;

                    #endregion

                    #region game
                    case GameStates.game:
                        if (mainmenu.isPlayingMusic())
                            mainmenu.stopMusic();
                        game.playMusic(gamesettings.musicvolume);
                        stillTransitioning = game.UpdateTransitionIn();
                        break;

                    #endregion

                    #region pause menu
                    case GameStates.pause_menu:
                        pausemenu.updateSettings();
                        stillTransitioning = pausemenu.UpdateTransitionIn();
                        break;

                    #endregion

                    #region victory
                    case GameStates.victory:
                        if (game.isPlayingMusic())
                            game.stopMusic();
                        victory.playMusic(gamesettings.musicvolume);
                        stillTransitioning = victory.UpdateTransitionIn();
                        break;

                    #endregion

                    #region defeat
                    case GameStates.defeat:
                        if (game.isPlayingMusic())
                            game.stopMusic();
                        defeat.playMusic(gamesettings.musicvolume);
                        stillTransitioning = defeat.UpdateTransitionIn();
                        break;

                    #endregion

                    #region cheats
                    case GameStates.cheats:
                        stillTransitioning = cheats.UpdateTransitionIn();
                        break;

                    #endregion
                }
                #endregion
            }
            else
            {
                #region transition out logic
                switch (nextstate)
                {
                    #region initial loading
                    case GameStates.initial_loading:
                        stillTransitioning = initialloading.UpdateTransitionOut();
                        break;

                    #endregion

                    #region main menu
                    case GameStates.main_menu:
                        stillTransitioning = mainmenu.UpdateTransitionOut();
                        break;

                    #endregion

                    #region about
                    case GameStates.about:
                        stillTransitioning = about.UpdateTransitionOut();
                        break;

                    #endregion

                    #region help
                    case GameStates.help:
                        stillTransitioning = help.UpdateTransitionOut();
                        break;

                    #endregion

                    #region options menu
                    case GameStates.options_menu:
                        reloadMenus = optionsmenu.reload();
                        stillTransitioning = optionsmenu.UpdateTransitionOut();
                        break;

                    #endregion

                    #region high scores
                    case GameStates.highscore_menu:
                        stillTransitioning = highscoremenu.UpdateTransitionOut();
                        break;

                    #endregion

                    #region replay menu
                    case GameStates.replay_menu:
                        stillTransitioning = replaymenu.UpdateTransitionOut();
                        break;

                    #endregion

                    #region level selection
                    case GameStates.level_selection:
                        stillTransitioning = levelselection.UpdateTransitionOut();
                        break;

                    #endregion

                    #region difficulty selection
                    case GameStates.difficulty_selection:
                        stillTransitioning = difficultyselection.UpdateTransitionOut();
                        break;

                    #endregion

                    #region character selection
                    case GameStates.character_selection:
                        stillTransitioning = characterselection.UpdateTransitionOut();
                        break;

                    #endregion

                    #region game loading
                    case GameStates.game_loading:
                        stillTransitioning = gameloading.UpdateTransitionOut();
                        break;

                    #endregion

                    #region game
                    case GameStates.game:
                        stillTransitioning = game.UpdateTransitionOut();
                        break;

                    #endregion

                    #region pause menu
                    case GameStates.pause_menu:
                        stillTransitioning = pausemenu.UpdateTransitionOut();
                        break;

                    #endregion

                    #region victory
                    case GameStates.victory:
                        stillTransitioning = victory.UpdateTransitionOut();
                        break;

                    #endregion

                    #region defeat
                    case GameStates.defeat:
                        stillTransitioning = defeat.UpdateTransitionOut();
                        break;
                    #endregion

                    #region cheats
                    case GameStates.cheats:
                        reloadMenus = cheats.reload();
                        stillTransitioning = cheats.UpdateTransitionOut();
                        break;
                    #endregion
                }
                #endregion
            }

            #endregion

            #region update states
            if (state != nextstate)
            {
                if (transitionstate == TransitionState.normal)
                    transitionstate = TransitionState.transition_out;
            }
            if (!stillTransitioning && transitionstate != TransitionState.normal)
            {
                if (state != nextstate)
                {
                    state = nextstate;
                    transitionstate = TransitionState.transition_in;
                }
                else
                    transitionstate = TransitionState.normal;
            }

            #endregion

            #region update particle flippingnessness
            explosioneffect.setFlipped(game.GetIsFlipped());
            flameeffect.setFlipped(game.GetIsFlipped());

            #endregion

            base.Update(gameTime);
        }

        public string returnSettingsVersion()
        {
            return filesettingsversion;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(baseBackgroundColor);

            spriteBatch.Begin();

            #region state drawing code
            switch (state)
            {
                #region draw initial loading state
                case GameStates.initial_loading:
                    initialloading.draw(spriteBatch);
                    break;
                #endregion

                #region draw main menu state
                case GameStates.main_menu:
                    mainmenu.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw about state
                case GameStates.about:
                    about.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw help state
                case GameStates.help:
                    help.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw level selection
                case GameStates.level_selection:
                    levelselection.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw difficulty selection
                case GameStates.difficulty_selection:
                    difficultyselection.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw character selection
                case GameStates.character_selection:
                    characterselection.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw high scores state
                case GameStates.highscore_menu:
                    highscoremenu.draw(spriteBatch, this);
                    break;

                #endregion

                #region draw options menu state
                case GameStates.options_menu:
                    optionsmenu.draw(spriteBatch, this);
                    break;

                #endregion

                #region draw replay menu state
                case GameStates.replay_menu:
                    replaymenu.draw(spriteBatch);
                    break;

                #endregion

                #region draw load game state
                case GameStates.game_loading:
                    gameloading.draw(spriteBatch);
                    break;
                #endregion

                #region draw game state
                case GameStates.game:
                    game.draw(spriteBatch);
                    break;
                #endregion

                #region draw pause menu
                case GameStates.pause_menu:
                    pausemenu.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw victory
                case GameStates.victory:
                    victory.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw defeat
                case GameStates.defeat:
                    defeat.draw(spriteBatch, this);
                    break;
                #endregion

                #region draw cheats
                case GameStates.cheats:
                    cheats.draw(spriteBatch, this);
                    break;
                #endregion
            }

            #endregion

            #region transition in drawing code
            if (transitionstate == TransitionState.transition_in)
            {
                switch (state)
                {
                    #region draw initial loading state
                    case GameStates.initial_loading:
                        initialloading.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw main menu state
                    case GameStates.main_menu:
                        mainmenu.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw about state
                    case GameStates.about:
                        about.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw help state
                    case GameStates.help:
                        help.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw level selection
                    case GameStates.level_selection:
                        levelselection.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw difficulty selection
                    case GameStates.difficulty_selection:
                        difficultyselection.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw character selection
                    case GameStates.character_selection:
                        characterselection.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw high scores state
                    case GameStates.highscore_menu:
                        highscoremenu.DrawTransitionIn(spriteBatch);
                        break;

                    #endregion

                    #region draw options menu state
                    case GameStates.options_menu:
                        optionsmenu.DrawTransitionIn(spriteBatch);
                        break;

                    #endregion

                    #region draw replay menu state
                    case GameStates.replay_menu:
                        replaymenu.DrawTransitionIn(spriteBatch);
                        break;

                    #endregion

                    #region load game state
                    case GameStates.game_loading:
                        gameloading.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw game state
                    case GameStates.game:
                        game.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw pause menu
                    case GameStates.pause_menu:
                        pausemenu.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw victory
                    case GameStates.victory:
                        victory.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw defeat
                    case GameStates.defeat:
                        defeat.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion

                    #region draw cheats
                    case GameStates.cheats:
                        cheats.DrawTransitionIn(spriteBatch);
                        break;
                    #endregion
                }
            }
            #endregion

            #region transition out drawing code
            else if (transitionstate == TransitionState.transition_out)
            {
                switch (state)
                {
                    #region draw initial loading state
                    case GameStates.initial_loading:
                        initialloading.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw main menu state
                    case GameStates.main_menu:
                        mainmenu.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw about state
                    case GameStates.about:
                        about.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw help state
                    case GameStates.help:
                        help.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw level selection
                    case GameStates.level_selection:
                        levelselection.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw difficulty selection
                    case GameStates.difficulty_selection:
                        difficultyselection.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw character selection
                    case GameStates.character_selection:
                        characterselection.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw high scores state
                    case GameStates.highscore_menu:
                        highscoremenu.DrawTransitionOut(spriteBatch);
                        break;

                    #endregion

                    #region draw options menu state
                    case GameStates.options_menu:
                        optionsmenu.DrawTransitionOut(spriteBatch);
                        break;

                    #endregion

                    #region draw replay menu state
                    case GameStates.replay_menu:
                        replaymenu.DrawTransitionOut(spriteBatch);
                        break;

                    #endregion

                    #region load game state
                    case GameStates.game_loading:
                        gameloading.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw game state
                    case GameStates.game:
                        game.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw pause menu
                    case GameStates.pause_menu:
                        pausemenu.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw victory
                    case GameStates.victory:
                        victory.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw defeat
                    case GameStates.defeat:
                        defeat.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion

                    #region draw cheats
                    case GameStates.cheats:
                        cheats.DrawTransitionOut(spriteBatch);
                        break;
                    #endregion
                }
            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

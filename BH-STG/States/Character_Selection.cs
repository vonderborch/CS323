/*
 * Component: States - Character Selection
 * Version: 1.1.10
 * Created: March 27th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Characters;
using BH_STG.BarrageEngine.Input;
using BH_STG.BarrageEngine.Screen;
using BH_STG.Characters.Players;
using BH_STG.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace BH_STG.States
{
    class Character_Selection : Menu
    {
        BasicCharacter basiccharacter;
        ChristianCharacter christiancharacter;
        DavidCharacter davidcharacter;
        DoubleWayCharacter doublewaycharacter;
        JacobCharacter jacobcharacter;
        LaserCharacter lasercharacter;
        SingleWayCharacter singlewaycharacter;
        TestPlayer testcharacter;
        FakePlayer fakecharacter;
        WeaponDatabase database;
        const string Christian = "von der borch", David = "antidavid", Jacob = "Alazandar";

        public override void loadMenu()
        {
            this.thisState = Main.GameStates.character_selection;

            // set classes
            Vector2 vec = new Vector2(950, 300);
            float scale = 0.25f;
            basiccharacter = new BasicCharacter(vec, scale, Color.White, true);
            christiancharacter = new ChristianCharacter(vec, scale, Color.White, true);
            davidcharacter = new DavidCharacter(vec, scale, Color.White, true);
            doublewaycharacter = new DoubleWayCharacter(vec, scale, Color.White, true);
            jacobcharacter = new JacobCharacter(vec, scale, Color.White, true);
            lasercharacter = new LaserCharacter(vec, scale, Color.White, true);
            singlewaycharacter = new SingleWayCharacter(vec, scale, Color.White, true);
            testcharacter = new TestPlayer(vec, scale, Color.White, true);
            fakecharacter = new FakePlayer(vec, scale, Color.White, true);
            database = new WeaponDatabase(GameMain);

            // add menu options
            selectedOption = 0;
        }

        public void createMenu()
        {
            options = new List<Option>();
            options.Add(new Option("(back)", true));
            options.Add(new Option(basiccharacter.returnName()));
            options.Add(new Option(lasercharacter.returnName()));
            options.Add(new Option(singlewaycharacter.returnName()));
            options.Add(new Option(doublewaycharacter.returnName()));
            
            if (GameMain.gamesettings.user == Christian)
                options.Add(new Option(christiancharacter.returnName()));
            else if (GameMain.gamesettings.user == David)
                options.Add(new Option(davidcharacter.returnName()));
            else if (GameMain.gamesettings.user == Jacob)
                options.Add(new Option(jacobcharacter.returnName()));
            else
                options.Add(new Option("Easter Egg Character - Enter the correct username to unlock!"));

            if (GameMain.gamesettings.testmode || GameMain.debugmode)
            {
                options.Add(new Option(testcharacter.returnName()));
                options.Add(new Option(fakecharacter.returnName()));
            }
        }


        public Game_Main.Players update(out Main.GameStates state, InputCommon input)
        {
            Game_Main.Players player = Game_Main.Players.secret_bob_level_player;

            updateSelection(input);
            state = updateEnter(input, out player);

            return player;
        }

        public Main.GameStates updateEnter(InputCommon input, out Game_Main.Players player)
        {
            Main.GameStates state = this.thisState;
            player = Game_Main.Players.secret_bob_level_player;

            #region Enter Key
            if (input.Accept.pressed && input.Accept.pressedTicks == 0)
            {
                beep.playSound(beep_name, false, GameMain.gamesettings.soundeffectsvolume, 0.0f);
                if (selectedOption == 0)
                    state = Main.GameStates.difficulty_selection;
                else if (selectedOption == 7 || (selectedOption == 7 && GameMain.debugmode))
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.test_player;
                }
                else if (selectedOption == 8 || (selectedOption == 8 && GameMain.debugmode))
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.fake_player;
                }
                else if (selectedOption == 1)
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.basic;
                }
                else if (selectedOption == 2)
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.laser;
                }
                else if (selectedOption == 3)
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.singleway;
                }
                else if (selectedOption == 4)
                {
                    state = Main.GameStates.game_loading;
                    player = Game_Main.Players.doubleway;
                }
                else if (selectedOption == 5)
                {
                    if (GameMain.gamesettings.unlockedSecret || (!GameMain.gamesettings.unlockedSecret && GameMain.debugmode))
                    {
                        state = Main.GameStates.game_loading;
                        player = Game_Main.Players.secret_bob_level_player;
                    }
                }
                else if (selectedOption == 6)
                {
                    if (GameMain.gamesettings.user == Christian)
                    {
                        state = Main.GameStates.game_loading;
                        player = Game_Main.Players.christian;
                    }
                    else if (GameMain.gamesettings.user == David)
                    {
                        state = Main.GameStates.game_loading;
                        player = Game_Main.Players.david;
                    }
                    else if (GameMain.gamesettings.user == Jacob)
                    {
                        state = Main.GameStates.game_loading;
                        player = Game_Main.Players.jacob;
                    }
                }
            }
            #endregion

            return state;
        }

        public bool draw(SpriteBatch spriteBatch, Main main)
        {
            // draw bg
            spriteBatch.Draw(this.BGIMAGE, new Rectangle(0, 0, 1280, 720), Color.White);

            // Draw program title
            spriteBatch.DrawString(this.titleFont, "BH-STG: Character Selection", new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(10)), this.fontColor);

            // Draw menu options
            Vector2 pos = new Vector2(10, 40);
            foreach (Option option in this.options)
            {
                option.draw(this.itemFont, spriteBatch, new Vector2(main.videosettings.returnModifiedX((int)pos.X),
                                                                    main.videosettings.returnModifiedY((int)pos.Y)));
                pos.Y += 20;
            }

            int itemx = 800;
            Player tempplayer = null;
            if (selectedOption == 1) // basiccharacter
                tempplayer = basiccharacter;
            else if (selectedOption == 2) // lasercharacter
                tempplayer = lasercharacter;
            else if (selectedOption == 3) // singlewaycharacter
                tempplayer = singlewaycharacter;
            else if (selectedOption == 4) // doublewaycharacter
                tempplayer = doublewaycharacter;
            else if (selectedOption == 5 && GameMain.gamesettings.user == Christian) // christiancharacter
                tempplayer = christiancharacter;
            else if (selectedOption == 5 && GameMain.gamesettings.user == David) // davidcharacter
                tempplayer = davidcharacter;
            else if (selectedOption == 5 && GameMain.gamesettings.user == Jacob) // jacobcharacter
                tempplayer = jacobcharacter;
            else if (selectedOption == 6 && (GameMain.gamesettings.testmode || GameMain.debugmode)) // testcharacter
                tempplayer = testcharacter;
            else if (selectedOption == 7 && (GameMain.gamesettings.testmode || GameMain.debugmode)) // fakecharacter
                tempplayer = fakecharacter;

            if (tempplayer != null)
            {
                spriteBatch.DrawString(this.itemFont, tempplayer.returnName(), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(40)), this.fontColor);
                spriteBatch.DrawString(this.itemFont, " - Weapon Type: " + database.getWeaponName(tempplayer.returnWeaponType()), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(60)), this.fontColor);

                List<string> weapondescription = database.getWeaponDescription(tempplayer.returnWeaponType());
                spriteBatch.DrawString(this.itemFont, " - Weapon Description: ", new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(80)), this.fontColor);
                int ypos = 80;
                itemx = 1000;
                foreach (string str in weapondescription)
                {
                    spriteBatch.DrawString(this.itemFont, str, new Vector2(main.videosettings.returnModifiedX(itemx),
                                           main.videosettings.returnModifiedY(ypos)), this.fontColor);
                    ypos += 20;
                }

                itemx = 800;
                spriteBatch.DrawString(this.itemFont, " - Weapon Bullet Lifespan: " + database.getWeaponMaxLife(tempplayer.returnWeaponType()), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(ypos)), this.fontColor);
                ypos += 20;
                spriteBatch.DrawString(this.itemFont, " - Weapon Base Cooldown: " + tempplayer.returnCooldown(), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(ypos)), this.fontColor);
                ypos += 20;
                spriteBatch.DrawString(this.itemFont, " - Weapon Base Damage: " + tempplayer.returnBaseDamage(), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(ypos)), this.fontColor);
                ypos += 20;
                spriteBatch.DrawString(this.itemFont, " - Base Bomb Count: " + tempplayer.returnBaseBombs(), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(ypos)), this.fontColor);
                ypos += 20;
                spriteBatch.DrawString(this.itemFont, " - Score Multiplier: " + tempplayer.returnScoreMultiplier(), new Vector2(main.videosettings.returnModifiedX(itemx),
                                       main.videosettings.returnModifiedY(ypos)), this.fontColor);
                tempplayer.loading(GameMain);
                tempplayer.drawSpecial(spriteBatch);
            }

            spriteBatch.DrawString(this.itemFont, GameMain.versionInfo, new Vector2(main.videosettings.returnModifiedX(10),
                                   main.videosettings.returnModifiedY(688)), this.fontColor);
            return true;
        }
    }
}

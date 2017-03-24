using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
/* Name: David Nolan
 * Student Number: c00204958
 * Start Date: 18/01/2016 9am
 * Finish Date: 18/1/2016 11am
 * Start Date: 19/01/2016 1pm
 * Finish Date: 19/1/2016 3pm 
 * Start  Date: 20/01/2016 9 - 11
 * Start  Date: 22/01/2016 3 - 7
 * Start  Date: 25/01/2016 9 -11 
 * Start Date:  26/1/16  1 - 7
 * Start Date:  27/1/16  8:30 - 10:00
 * Start Date:  28/1/16  5 - 7
 * Start Date:  27/1/16  10 - 12:15
 * Start Date   29/1/16 2:30 - 4
 * plus 1 hour of bits and bobs
 * Total Time: 27hrs 15mins
 * Bugs: no known bugs 
 * Program: My joint project
 */
namespace JointProject_DavidNolan_c00204958
{
    //enum to give the different options for my menu
    public enum GameState { MainMenu, StartGame, ControlsAndObjectives, Restart, QuitGame }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static MouseState currentMouseState, previousMouseState;
        static GameState currentState = GameState.MainMenu; //variables used tomake the mouse clicks seperate and also to give my game state changer an initial state
        //variables my different objects for each entity and sounds
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Shooter shooter1;
        Shooter shooter2;
        Chaser chaser1;
        Chaser chaser2;
        
        Player mPlayer;
        Bullet bullet;

        SoundEffect backgroundMusic;
        Bullet enemyBullet1;
        Bullet enemyBullet2;
        SoundEffect buttonClick;
        SoundEffect laserImpact;
       
        SoundEffect mPlayerShot;
        SoundEffect shooterCollisionSound;

        public SoundEffect shooterShot;
         
        
        //variables width and height of the screen 
        int width = 0;
        int height = 0;
        SpriteFont font;
        Texture2D backgroundImage;

         
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {



            // TODO: Add your initialization logic here
            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;//width and height of screen

            // setting up my objects passing them the values required for their code also setting up some initial textures of the way the enemies should look
            mPlayer = new Player(width, height);
            bullet = new Bullet(mPlayer);
             
             
            chaser1 = new Chaser(width);
            chaser2 = new Chaser(width, height);
            shooter1 = new Shooter(height);
            shooter2 = new Shooter(width, height);
            enemyBullet1 = new Bullet(mPlayer);
            enemyBullet2 = new Bullet(mPlayer);
             
 
            if (currentState == GameState.MainMenu)//mouse was annoying me
            {
                IsMouseVisible = true;//used for player to click on menu options properly
            }
            
            
            
            

            
             
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            backgroundImage = Content.Load<Texture2D>("Background");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mPlayer.LoadContent(this.Content, "PlayerUp", "PlayerDown", "PlayerLeft", "PlayerRight");
            bullet.LoadContent(this.Content, "BulletUp", "BulletDown", "BulletLeft", "BulletRight");
             
            chaser1.LoadContent(this.Content, "ChaserUp", "ChaserDown", "ChaserLeft", "ChaserRight");
            chaser2.LoadContent(this.Content, "ChaserUp", "ChaserDown", "ChaserLeft", "ChaserRight");
            font = Content.Load<SpriteFont>("SpriteFont1");
            shooter1.LoadContent(this.Content, "ShooterLeft", "ShooterRight");
            shooter2.LoadContent(this.Content, "ShooterLeft", "ShooterRight");
            backgroundMusic = Content.Load<SoundEffect>("BackgroundMusic");
            laserImpact = Content.Load<SoundEffect>("LaserImpact");
            buttonClick = Content.Load<SoundEffect>("ButtonClick");
            mPlayerShot = Content.Load<SoundEffect>("mPlayerShot");
            shooterCollisionSound = Content.Load<SoundEffect>("ShooterCollisionSound");
            shooterShot = Content.Load<SoundEffect>("ShooterShot");
            enemyBullet1.LoadContent(this.Content, "EnemyBullet");
            enemyBullet2.LoadContent(this.Content, "EnemyBullet");
            //loading in all of my sounds and textures
           
            SoundEffectInstance instance = backgroundMusic.CreateInstance();
            instance.IsLooped = true;
            backgroundMusic.Play(0.2F, 0, 0);//cue the awesome music
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
            
        protected override void Update(GameTime gameTime)
        {
            
            if (currentState != GameState.StartGame)//mouse was annoying me
            {
                IsMouseVisible = true;//used for player to click on menu options properly
            }
            else
            {
                IsMouseVisible = false;
            }
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();//used to make sure the individual clicks work right
           
            switch (currentState)//switch statement between my different options for the menu but we start at the main menu state
            {
                case GameState.MainMenu:
                    UpdateMenu(gameTime);
                    
                    break;
                case GameState.StartGame:
                    UpdateGame(gameTime);
                    break;
                case GameState.ControlsAndObjectives:
                    UpdateControlsAndObjectives(gameTime);
                    break;
                case GameState.Restart:
                    UpdateRestart();
                    break;
                case GameState.QuitGame:
                    UpdateQuitGame();
                    break;
                default:
                    break;
            }
             
            // Allows the game to exit
            
            
 
         
            
            base.Update(gameTime);
        }
        //method for when we seleect quit game 
        //because i have a click on yes or no system the draw method makes it be in the middle so we click on either side of the screen for the option we want
        private void UpdateQuitGame()
        {

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                //used to make sure we are pressing the button again not repeated from before 
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (currentMouseState.X < width / 2  )
                    {
                        
                        Exit();
                    }
                    else if (currentMouseState.X > width/2 )
                    {
                        buttonClick.Play(0.1F, 0, 0);
                        currentState = GameState.MainMenu;
                    }//if we press the button again and its in the left half of the screen then exit if not bring us back to the menu 
                }
            }
        }

        private void UpdateControlsAndObjectives(GameTime gameTime)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                buttonClick.Play(0.1F, 0, 0);
                currentState = GameState.MainMenu;
            }//when you click again it brings us back to the main menu
        }

         
        private void UpdateRestart()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (currentMouseState.X < width / 2)
                    {
                        
                        //Initialize();     //i had this in before but it didnt work it made the songs overlap so i done the needed ones manually
                        mPlayer.score = 0;
                        mPlayer.position = new Vector2(width / 2 - 25, height / 2 - 25);
                        mPlayer.playerTexture = mPlayer.playerTextureUp;
                        mPlayer.health = 5;
                        bullet.alive = false; bullet.bulletUnavailable = false;
                        chaser1.Top(width);
                        chaser2.Bottom(width, height);
                        enemyBullet1.alive = false; enemyBullet1.bulletUnavailable = false;
                        enemyBullet2.alive = false; enemyBullet2.bulletUnavailable = false;
                        
                        currentState = GameState.StartGame;
                    }
                    else if (currentMouseState.X > width / 2)
                    {
                        mPlayer.health = 5;
                        mPlayer.score = 0;
                        buttonClick.Play(0.1F, 0, 0);
                        currentState = GameState.MainMenu;
                    }//same statement as the quit except this instead initialises if the left half of the screen is pressed 
                }
            }
        }

        

        private void UpdateMenu(GameTime gameTime)
        {
            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.X < width / 2 && currentMouseState.Y < 150)
            {
                
                currentState = GameState.StartGame;
            }
            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.X < width/2 && currentMouseState.Y < 300 && currentMouseState.Y >150)
            {
                buttonClick.Play(0.1F, 0, 0);
                currentState = GameState.ControlsAndObjectives;
            }
            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.X < width / 2 && currentMouseState.Y < 400 && currentMouseState.Y > 250)
            {
                buttonClick.Play(0.1F, 0, 0);
                currentState = GameState.Restart;
            }
            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.X < width / 2 && currentMouseState.Y < 500 && currentMouseState.Y > 350)
            {
                buttonClick.Play(0.1F, 0, 0);
                currentState = GameState.QuitGame;
            }//this is the main menu it lets us click on any of the options and depending on what one we click it will change the gamestate to that one 
        }

        private void UpdateGame(GameTime gameTime)
        {
           
            //checking if the bullet is on the screen or not




            //calls the update to move the player and the bullet on the screen 
            mPlayer.Update();
            bullet.Update();
            CheckCollisions();
            mPlayer.BoundryChecking(width, height);
            enemyBullet1.Update();
            enemyBullet2.Update();
            chaser1.Update(mPlayer);
            chaser2.Update(mPlayer);
            shooter1.Update(mPlayer);
            shooter2.Update(mPlayer);
             
            CheckKeyboard();// series of methods called constantly that does the varius updates for the entities it also checks collisions and input from keyboard 
            if (mPlayer.health < 0)
            {
                mPlayer.health =0 ;
            }
            if (mPlayer.health == 0)
            {
                //Initialize(); //i had this in before but it didnt work it made the songs overlap so i done the needed ones manually
                
                mPlayer.position = new Vector2(width / 2 - 25, height / 2 - 25);
                 
                mPlayer.playerTexture = mPlayer.playerTextureUp;
                bullet.alive = false; bullet.bulletUnavailable = false;
                chaser1.Top(width);
                chaser2.Bottom(width, height);
                enemyBullet1.alive = false; enemyBullet1.bulletUnavailable = false;
                enemyBullet2.alive = false; enemyBullet2.bulletUnavailable = false;
                currentState = GameState.Restart;
                
                
            }

          
        }
        public void CheckKeyboard()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();



            if (aCurrentKeyboardState.IsKeyDown(Keys.A))
            {
                mPlayer.MoveLeft();


            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.D) == true)
            {
                mPlayer.MoveRight();


            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.W) == true)
            {
                mPlayer.MoveUp();


            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.S) == true)
            {
                mPlayer.MoveDown();


            }


            if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                bullet.ShootLeft(mPlayer);
                if ((bullet.bulletPosition.X == mPlayer.position.X) && (bullet.bulletPosition.Y == mPlayer.position.Y + 12.5F))
                {
                    mPlayerShot.Play(0.1F, 0, 0);
                }


            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                bullet.ShootRight(mPlayer);
                bullet.ShootLeft(mPlayer);
                if ((bullet.bulletPosition.X == mPlayer.position.X + 25) && (bullet.bulletPosition.Y == mPlayer.position.Y + 12.5F))
                {
                    mPlayerShot.Play(0.1F, 0, 0);
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Escape))
            {
                currentState = GameState.MainMenu;
            }//escape to main menu 

            if (aCurrentKeyboardState.IsKeyDown(Keys.Up))
            {
                bullet.ShootUp(mPlayer);
                bullet.ShootLeft(mPlayer);
                if ((bullet.bulletPosition.X == mPlayer.position.X + 12.5) && (bullet.bulletPosition.Y == mPlayer.position.Y))
                {
                    mPlayerShot.Play(0.1F, 0, 0);
                }

            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Down))
            {
                bullet.ShootDown(mPlayer);
                bullet.ShootLeft(mPlayer);
                if ((bullet.bulletPosition.X == mPlayer.position.X + 12.5) && (bullet.bulletPosition.Y == mPlayer.position.Y + 25))
                {
                    mPlayerShot.Play(0.1F, 0, 0);
                }

            }//various keyboard inpus w, a, s, d move arrow keys shoot escape brings up the main menu also makes sure that the sound is only 
            //played if the bullet is at the players position i.e. when he actually shoots

        }
        public void CheckCollisions()
        {
            bullet.CheckIfOffScreenDown(height);
            bullet.CheckIfOffScreenUp();
            bullet.CheckIfOffScreenLeft();
            bullet.CheckIfOffScreenRight(width);

            enemyBullet1.CheckIfOffScreenDown(height);
            enemyBullet1.CheckIfOffScreenUp();
            enemyBullet1.CheckIfOffScreenLeft();
            enemyBullet1.CheckIfOffScreenRight(width);

            enemyBullet2.CheckIfOffScreenDown(height);
            enemyBullet2.CheckIfOffScreenUp();
            enemyBullet2.CheckIfOffScreenLeft();
            enemyBullet2.CheckIfOffScreenRight(width);
            // calls methods that check if bullets are on the screen





            // TODO: Add your update logic here
            Rectangle playerRec = new Rectangle((int)mPlayer.position.X, (int)mPlayer.position.Y, 50, 50);
            Rectangle bulletRec = new Rectangle((int)bullet.bulletPosition.X, (int)bullet.bulletPosition.Y, 25, 25);
            Rectangle chaser1Rec = new Rectangle((int)chaser1.chaserPosition.X, (int)chaser1.chaserPosition.Y, 50, 50);
            Rectangle chaser2Rec = new Rectangle((int)chaser2.chaserPosition.X, (int)chaser2.chaserPosition.Y, 50, 50);
            Rectangle shooter1Rec = new Rectangle((int)shooter1.shooterPosition.X, (int)shooter1.shooterPosition.Y, 50, 50);
            Rectangle shooter2Rec = new Rectangle((int)shooter2.shooterPosition.X, (int)shooter2.shooterPosition.Y, 50, 50);
            Rectangle shooterBullet1Rec = new Rectangle((int)enemyBullet1.bulletPosition.X, (int)enemyBullet1.bulletPosition.Y, 50, 50);
            Rectangle shooterBullet2Rec = new Rectangle((int)enemyBullet2.bulletPosition.X, (int)enemyBullet2.bulletPosition.Y, 50, 50);
            //rectangles that i use for my collisions






            //if (mPlayer.health < 0)
            //{
            //    mPlayer.health = 0;
            //}i used this for testing  
            if (bulletRec.Intersects(chaser1Rec) && bullet.alive == true && chaser1.chaserPosition.Y >= 0 - 25)
            {
                chaser1.Top(width);
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                mPlayer.score++;
                laserImpact.Play(0.1F, 0, 0);
            }//if we shoot a chaser then he respawns we gain score we can shoot again because the bullet is reset and we play the impact sound
            if (playerRec.Intersects(chaser1Rec) && chaser1.chaserPosition.Y > 0)
            {
                chaser1.Top(width);
                mPlayer.health--;//if the chaser hits us the he respawns and we lose health
            }

            if (bulletRec.Intersects(chaser2Rec) && bullet.alive == true && chaser2.chaserPosition.Y <= height - 25)
            {
                chaser2.Bottom(width, height);
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                mPlayer.score++;
                laserImpact.Play(0.1F, 0, 0);
            }
            if (playerRec.Intersects(chaser2Rec) && chaser2.chaserPosition.Y <= height - 50)
            {
                chaser2.Bottom(width, height);
                mPlayer.health--;
            }
            if (playerRec.Intersects(shooter1Rec))
            {

                mPlayer.health--;
            }//i didnt want to make the shooters respawn because it was too easy then so the player cannot harm the shooter and will get hit if he tries
            if (playerRec.Intersects(shooter2Rec))
            {

                mPlayer.health--;
            }


            if (playerRec.Intersects(shooterBullet1Rec) && enemyBullet1.alive == true)
            {
                mPlayer.health--;

                enemyBullet1.alive = false;
                enemyBullet1.bulletUnavailable = false;
                shooterCollisionSound.Play(0.1F, 0, 0);//if the shooter hits us then his sound is played his bullet is reset and we take damage

            }

            if (playerRec.Intersects(shooterBullet2Rec) && enemyBullet2.alive == true)
            {
                mPlayer.health--;

                enemyBullet2.alive = false;
                enemyBullet2.bulletUnavailable = false;
                shooterCollisionSound.Play(0.1F, 0, 0);

            }

            if (bulletRec.Intersects(shooter2Rec) && bullet.alive == true)
            {
                mPlayer.HealthDown();
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                laserImpact.Play(0.1F, 0, 0);//again we try harm chaser we get hit
            }

            if (bulletRec.Intersects(shooter1Rec) && bullet.alive == true)
            {
                mPlayer.HealthDown();
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                laserImpact.Play(0.1F, 0, 0);
            }

            if (bulletRec.Intersects(shooterBullet1Rec) && bullet.alive == true && enemyBullet1.alive == true && enemyBullet1.bulletPosition.X >= 50)
            {
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                enemyBullet1.alive = false;
                enemyBullet1.bulletUnavailable = false;
                if (mPlayer.position.X - shooter1.shooterPosition.X >= 100 && mPlayer.score > 20)
                {
                    mPlayer.healthUpCounter++;
                }
                laserImpact.Play(0.1F, 0, 0);
                shooterCollisionSound.Play(0.1F, 0, 0);//i wanted to make us gain something for shooting the shooters bullets 
                //so after a certain score every ten we shoot gives us health also we both get our bullets back
            }

            if (bulletRec.Intersects(shooterBullet2Rec) && bullet.alive == true && enemyBullet2.alive == true && enemyBullet2.bulletPosition.X <= width - 50)
            {
                bullet.alive = false;
                bullet.bulletUnavailable = false;
                enemyBullet2.alive = false;
                enemyBullet2.bulletUnavailable = false;
                if (shooter2.shooterPosition.X - mPlayer.position.X >= 100 && mPlayer.score > 20)
                {
                    mPlayer.healthUpCounter++;
                }
                laserImpact.Play(0.1F, 0, 0);
                shooterCollisionSound.Play(0.1F, 0, 0);
            }


            shooter2.shooterTexture = shooter2.shooterTextureLeft;
            shooter1.shooterTexture = shooter1.shooterTextureRight;



            if (enemyBullet1.bulletUnavailable == false)
            {
                enemyBullet1.alive = true;
                enemyBullet1.EnemyShootRight(mPlayer, shooter1, shooterShot);
            }
            if (enemyBullet2.bulletUnavailable == false)
            {
                enemyBullet2.alive = true;
                enemyBullet2.EnemyShootLeft(mPlayer, shooter2, shooterShot);
            }//if you have a bullet shoot

            if (mPlayer.healthUpCounter == 10)
            {
                mPlayer.health++;
                mPlayer.healthUpCounter = 0;
            }//health up like mentioned before

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            switch (currentState)//what will draw when the different game states are selected
            {
                case GameState.MainMenu:
                    DrawMenu(spriteBatch, gameTime);
                    break;
                case GameState.StartGame:
                    DrawGame(spriteBatch, gameTime);
                    break;
                case GameState.ControlsAndObjectives:
                    DrawControlsAndObjectives(spriteBatch, gameTime);
                    break;
                case GameState.Restart:
                    DrawRestart(spriteBatch, gameTime);
                    break;
                case GameState.QuitGame:
                    DrawQuitGame(spriteBatch, gameTime);
                    break;
                default:
                    break;
            }
            
            spriteBatch.End();
             
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawQuitGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle backgroundRecForQuit = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(backgroundImage, backgroundRecForQuit, Color.SlateGray);
            spriteBatch.DrawString(font, "Are you sure?", new Vector2(width / 2 - 80, height / 2 - 100), Color.White);
            spriteBatch.DrawString(font, "Yes | No", new Vector2(width / 2 - 55, height / 2), Color.White);
             
            
        }//draws screen for quit asking if theyre sure 
        private void DrawRestart(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            Rectangle backgroundRecForRestart = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(backgroundImage, backgroundRecForRestart, Color.SlateGray);
            spriteBatch.DrawString(font, "Do you want to restart", new Vector2(width / 2 - 135, height / 2 - 100), Color.White);
            spriteBatch.DrawString(font, "Yes | No", new Vector2(width / 2 - 55, height / 2), Color.White);
            if (mPlayer.health == 0)
            {
                spriteBatch.DrawString(font, "You have died, your score is " + mPlayer.score, new Vector2(width / 2 -200, height / 2 +100), Color.White);
            }//draw screen for restart 
        }

        private void DrawControlsAndObjectives(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle backgroundRecForControlsAndObjectives = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(backgroundImage, backgroundRecForControlsAndObjectives, Color.SlateGray);
            spriteBatch.DrawString(font, "To move your ship use W, A, S and D", new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(font, "To shoot use the arrow keys", new Vector2(50, 100), Color.White);
            spriteBatch.DrawString(font, "Escape will bring you back to the main menu", new Vector2(50, 150), Color.White);
            spriteBatch.DrawString(font, "The objective is to get the highest score you can.\nIf you shoot the chasers you will gain score,\nIf you shoot the shooters you will lose health.\nWhen you have a score above 20 you can gain health by\nShooting enough enemy bullets, try to avoid enemies\n\nEnjoy! ", new Vector2(50, 200), Color.White);
        }//draw screen for controls and objectives 

        private void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle backgroundRec = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(backgroundImage, backgroundRec, Color.SlateGray);
            mPlayer.Draw(this.spriteBatch);
            bullet.Draw(this.spriteBatch);
            shooter1.Draw(this.spriteBatch);
            shooter2.Draw(this.spriteBatch);
            enemyBullet1.DrawEnemyBullet(this.spriteBatch);
            enemyBullet2.DrawEnemyBullet(this.spriteBatch); 


            chaser1.Draw(this.spriteBatch);
            chaser2.Draw(this.spriteBatch); // calls the draw method for all of the objects and sets the background screen 

            spriteBatch.DrawString(font, "Score: " + mPlayer.score, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Health: " + mPlayer.health, new Vector2(width - 150, 0), Color.White);// draws the score and health in top left and top right of screen 
        }

        private void DrawMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            Rectangle backgroundRecForMenu = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(backgroundImage, backgroundRecForMenu, Color.SlateGray);
            spriteBatch.DrawString(font, "Start Game", new Vector2(100, 50), Color.White);
            spriteBatch.DrawString(font, "Controls / Objective", new Vector2(100, 150), Color.White);
            spriteBatch.DrawString(font, "Restart", new Vector2(100, 250), Color.White);
            spriteBatch.DrawString(font, "Quit Game", new Vector2(100, 350), Color.White);
        }//draws the options for the menu
        
        
       
        
         
    
    
    }

}

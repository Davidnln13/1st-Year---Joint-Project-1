using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JointProject_DavidNolan_c00204958
{
    class Bullet
    {
        public bool alive;//whether to draw bullet or not
        public Vector2 bulletPosition = new Vector2();
        Texture2D enemyBulletTexture;
         
        Texture2D bulletTexture;
        Texture2D bulletTextureUp;
        Texture2D bulletTextureDown;
        Texture2D bulletTextureLeft;
        Texture2D bulletTextureRight;//textures to give bullet 
        int speedX = 0;
        int speedY = 0;// x and y speed of the bullet after its shot this changes depending on direction
        public bool bulletUnavailable = false;
        // checks if the bullet is fired 
         
      
        public Bullet(Player mPlayer)
        {
            bulletPosition.X = mPlayer.position.X;
            bulletPosition.Y = mPlayer.position.Y;
             
            alive = false;

            //constructor for the player bullet starting it at our position
        }
        public Bullet(Shooter shooter1)
        {
            bulletPosition = new Vector2(shooter1.shooterPosition.X + 50, shooter1.shooterPosition.Y + 25);
            alive = false;
            // constructor for shooter 1 
        }
        public Bullet(Shooter shooter2, Player mPlayer)
        {
            bulletPosition = new Vector2(shooter2.shooterPosition.X, shooter2.shooterPosition.Y + 25);
            alive = false;
            // constructor for shooter2 
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (alive == true)
            {
                theSpriteBatch.Draw(bulletTexture, bulletPosition, Color.White);
            }// draw player bullets at our position 
        }
            
        public void DrawEnemyBullet(SpriteBatch theSpriteBatch)
        {
            if (alive == true)
            {
                theSpriteBatch.Draw(enemyBulletTexture, bulletPosition, Color.White);
            }//draw enemy bullets at their position

        
        }
        public void LoadContent(ContentManager theContentManager, string theAssetNameBulletUp, string theAssetNameBulletDown, string theAssetNameBulletLeft, string theAssetNameBulletRight)
        {
            bulletTextureUp = theContentManager.Load<Texture2D>(theAssetNameBulletUp);
            bulletTextureDown = theContentManager.Load<Texture2D>(theAssetNameBulletDown);
            bulletTextureLeft = theContentManager.Load<Texture2D>(theAssetNameBulletLeft);
            bulletTextureRight = theContentManager.Load<Texture2D>(theAssetNameBulletRight);
            bulletTexture = bulletTextureUp;
           //loads in textures for the player bullet
        }
        public void LoadContent(ContentManager theContentManager, string enemyBullet)
        {
             enemyBulletTexture = theContentManager.Load<Texture2D>(enemyBullet);
              
            //loads in enemy bullet textures 
        }
        public void EnemyShootLeft(Player mPlayer, Shooter shooter2, SoundEffect shooterShot)
        {
            
            
            if (bulletUnavailable == false)
            {
                alive = true;
                shooterShot.Play();
                bulletPosition = new Vector2(shooter2.shooterPosition.X-20, shooter2.shooterPosition.Y + 15);
                if (mPlayer.score >= 0 && mPlayer.score < 5)//if the bullet is available we shoot the bullet make the sounds
                {
                    speedX = -2;
                    speedY = 0;
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                    speedX = -3;
                    speedY = 0;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                    speedX = -4;
                    speedY = 0;
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                    speedX = -5;
                    speedY = 0;
                }
                if (mPlayer.score >= 40)
                {
                    speedX = -6;
                    speedY = 0;
                }//changes speeds for progression based on players score 
                
                bulletUnavailable = true;
            }
        }
        public void EnemyShootRight(Player mPlayer, Shooter shooter1, SoundEffect shooterShot)
        {
            
            if (bulletUnavailable == false)
            {
                alive = true;
                shooterShot.Play();
                bulletPosition = new Vector2(shooter1.shooterPosition.X + 48, shooter1.shooterPosition.Y + 13);
                if (mPlayer.score >= 0 && mPlayer.score < 5)
                {
                    speedX = 2;
                    speedY = 0;
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                    speedX = 3;
                    speedY = 0;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                    speedX = 4;
                    speedY = 0;
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                    speedX = 5;
                    speedY = 0;
                }
                if (mPlayer.score >= 40)
                {
                    speedX = 6;
                    speedY = 0;
                }
                 
                bulletUnavailable = true;
            }
        }
       
        public void ShootLeft(Player mPlayer)
        {
            if (bulletUnavailable == false)
            {
                alive = true;
                bulletTexture = bulletTextureLeft;

                bulletPosition.X = mPlayer.position.X;
                bulletPosition.Y = mPlayer.position.Y + 12.5F;
                speedX = -15;
                speedY = 0;
                mPlayer.playerTexture = mPlayer.playerTextureLeft;
                bulletUnavailable = true;
            }//if my bullet is available and i shoot shoot the bullet from the front of my ship and change my texture to face bullet and make the bullet unavailable 
            //until its off the screen 
            
        }
        public void ShootRight(Player mPlayer)
        {
                if (bulletUnavailable == false)
                {
                    alive = true;
                    bulletTexture = bulletTextureRight;

                    bulletPosition.X = mPlayer.position.X + 25;
                    bulletPosition.Y = mPlayer.position.Y + 12.5F;
                    speedX = 15;
                    speedY = 0;
                    mPlayer.playerTexture = mPlayer.playerTextureRight;
                    bulletUnavailable = true;
                }//if my bullet is available and i shoot shoot the bullet from the front of my ship and change my texture to face bullet and make the bullet unavailable 
            //until its off the screen 
            
             
           
        }
        public void ShootUp(Player mPlayer)
        {
            if (bulletUnavailable == false)
            {
                alive = true;
                bulletTexture = bulletTextureUp;

                bulletPosition.X = mPlayer.position.X + 12.5F;
                bulletPosition.Y = mPlayer.position.Y;
                speedX = 0;
                speedY = -15;
                mPlayer.playerTexture = mPlayer.playerTextureUp;
                bulletUnavailable = true;
            }
        }//if my bullet is available and i shoot shoot the bullet from the front of my ship and change my texture to face bullet and make the bullet unavailable 
        //until its off the screen 
           
        
        public void ShootDown(Player mPlayer)
        {
            if(bulletUnavailable == false)
            {
                alive = true; 
                bulletTexture = bulletTextureDown;

                bulletPosition.X = mPlayer.position.X+12.5F;
                bulletPosition.Y = mPlayer.position.Y+25;
                speedX = 0;
                speedY = 15;
                mPlayer.playerTexture = mPlayer.playerTextureDown;
                bulletUnavailable = true;
            }//if my bullet is available and i shoot shoot the bullet from the front of my ship and change my texture to face bullet and make the bullet unavailable 
            //until its off the screen 
        }

        public void CheckIfOffScreenLeft()
        {
            if (bulletPosition.X <= 0 - 25)
            {
                alive = false;

                bulletUnavailable = false;
            }
        }
        public void CheckIfOffScreenRight(int width)
        {
            if (bulletPosition.X >= width)
            {
                alive = false;

                bulletUnavailable = false;

            }
        }
        public void CheckIfOffScreenUp()
        {
            if (bulletPosition.Y <= 0 - 25)
            {
                alive = false;

                bulletUnavailable = false;
            }
 
        }
        public void CheckIfOffScreenDown(int height)
        {
            if (bulletPosition.Y >= height)
            {
                alive = false;

                bulletUnavailable = false;

            }//if the bullet is off the screen then make it available again and stop updating it
        }
        public void Update( )
        {
            if (alive == true)
            {
                bulletPosition.X += speedX;
                bulletPosition.Y += speedY;
                //constantly updates alive bullets with a speed
            
            }
        }

    }
}

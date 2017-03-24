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
    class Player
    {
        bool alive = true;
        public Vector2 position;
        public Texture2D playerTexture;
        public Texture2D playerTextureUp;
        public Texture2D playerTextureDown;
        public Texture2D playerTextureLeft;
        public Texture2D playerTextureRight;
         //texture and vecotr variables for the player
        public int health;
        public int score = 0;
        public int healthUpCounter = 0;
        //other attributes
        public int moveNumber = 4;//amount to move in any direction
       
        
         
        
        public Player(int width, int height)
        {
            position.X = width / 2 - 25;
            position.Y = height / 2 - 25;
            health = 5;
            //constructor for player we start in the middle with 5 health
             
            
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(playerTexture, position, Color.White);
        }//draws the player in the middle of the screen

        public void LoadContent(ContentManager theContentManager, string theAssetNamePlayerUp, string theAssetNamePlayerDown, string theAssetNamePlayerLeft, string theAssetNamePlayerRight)
        {
            playerTextureUp = theContentManager.Load<Texture2D>(theAssetNamePlayerUp);
            playerTextureDown = theContentManager.Load<Texture2D>(theAssetNamePlayerDown);
            playerTextureLeft = theContentManager.Load<Texture2D>(theAssetNamePlayerLeft);
            playerTextureRight = theContentManager.Load<Texture2D>(theAssetNamePlayerRight);
            playerTexture = playerTextureUp;
        }//load the player textures 
        

               

        

        
        public void MoveUp()
        {
            position.Y = position.Y - moveNumber;
            playerTexture = playerTextureUp;
        }
        public void MoveDown()
        {
            position.Y = position.Y + moveNumber;
            playerTexture = playerTextureDown;
        }
        public void MoveLeft()
        {
            position.X = position.X - moveNumber;
            playerTexture = playerTextureLeft;
        }
        public void MoveRight()
        {
            position.X = position.X + moveNumber;
            playerTexture = playerTextureRight;
        }
         //moves the player depending on button pressed with a certain value that increases at certain points also changes texture to mimic direction 
        public void HealthDown()
        {
            health--;
        }//decrement player health
         
        
        public void BoundryChecking(int width, int height)
        {
            if (position.X <=0)
            {
                position.X = 1;
            }
            if (position.X >= width-50)
            {
                position.X = width - 50;
            }
            if (position.Y <= 0)
            {
                position.Y = 1;
            }
            if (position.Y >= height - 50)
            {
                position.Y = height - 50;
            }//if the player is off the screen but him on the screen
        }
        public void Update()
        {
            if (score >= 0 &&score < 5)
            {
                moveNumber = 4;
            }
            if (score >= 5 && score < 10)
            {
                moveNumber = 5;
            }
            if (score >= 10 && score < 20)
            {
                moveNumber = 6;
            }
            if (score >= 20 &&score < 40)
            {
                moveNumber = 7;
            }
            if (score >= 40)
            {
                moveNumber = 8;
            }//increases player speed at certain intervals of score
       
        }
    }
}
   
      







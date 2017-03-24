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
    class Chaser
    {
        public bool alive = true;
        public Vector2 chaserPosition = new Vector2();
        public Texture2D chaserTexture;
        public Texture2D chaserTextureUp;
        public Texture2D chaserTextureDown;
        public Texture2D chaserTextureLeft;
        public Texture2D chaserTextureRight;
         //textures for the chaser
         
        
        
        public Chaser(int width)
        {
            chaserPosition.X = width / 2 - 25;
            chaserPosition.Y = 0;//start point for the top chaser
        }
        public Chaser(int width, int height)
        {//start point for bottom chaser 
            chaserPosition.X = width / 2 - 25;
            chaserPosition.Y = height - 25;
        }
        public void Update(Player mPlayer)
        {
             
            if (mPlayer.position.X > chaserPosition.X)
            {
                chaserTexture = chaserTextureRight;
                if (mPlayer.score >= 0 && mPlayer.score < 5)
                {
                      
                     
                     chaserPosition.X += .2F;
                   
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                   chaserPosition.X += .5F;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                  chaserPosition.X += 1; 
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                   chaserPosition.X += 2; 
                }
                if (mPlayer.score >= 40)
                {
                  chaserPosition.X += 3;
                }
                 //if the player is in a certain place move towards him and change texture accordingly
                
            }
            if (mPlayer.position.X < chaserPosition.X)
            {
                chaserTexture = chaserTextureLeft;
                if (mPlayer.score >= 0 && mPlayer.score < 5)
                {
                    
                    chaserPosition.X -= .2F;
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                    chaserPosition.X -= .5F;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                    chaserPosition.X -= 1;
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                    chaserPosition.X -= 2;
                }
                if (mPlayer.score >= 40)
                {
                    chaserPosition.X -= 3;
                }
                //if the player is in a certain place move towards him and change texture accordingly
            }
            if (mPlayer.position.Y > chaserPosition.Y)
            {
                chaserTexture = chaserTextureDown;
                if (mPlayer.score >= 0 && mPlayer.score < 5)
                {
                    chaserPosition.Y += .2F;
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                    chaserPosition.Y += .5F;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                    chaserPosition.Y += 1;
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                    chaserPosition.Y += 2;
                }
                if (mPlayer.score >= 40)
                {
                    chaserPosition.Y += 3;
                }
                //if the player is in a certain place move towards him and change texture accordingly
            }
            if (mPlayer.position.Y < chaserPosition.Y)
            {
                chaserTexture = chaserTextureUp;
                if (mPlayer.score >= 0 && mPlayer.score < 5)
                {
                    chaserPosition.Y -= .2F;
                }
                if (mPlayer.score >= 5 && mPlayer.score < 10)
                {
                    chaserPosition.Y -= .5F;
                }
                if (mPlayer.score >= 10 && mPlayer.score < 20)
                {
                    chaserPosition.Y -= 1;
                }
                if (mPlayer.score >= 20 && mPlayer.score < 40)
                {
                    chaserPosition.Y -= 2;
                }
                if (mPlayer.score >= 40)
                {
                    chaserPosition.Y -= 3;
                }//if the player is in a certain place move towards him and change texture accordingly
                
            }
        }
        
        public void Draw(SpriteBatch theSpriteBatch)
        {
            
            theSpriteBatch.Draw(chaserTexture, chaserPosition, Color.White);
        }//draws the texture at the chasers position 


        public void LoadContent(ContentManager theContentManager, string chaserUp, string chaserDown, string chaserLeft, string chaserRight)
        {
            chaserTextureUp = theContentManager.Load<Texture2D>(chaserUp);
            chaserTextureDown = theContentManager.Load<Texture2D>(chaserDown);
            chaserTextureLeft = theContentManager.Load<Texture2D>(chaserLeft);
            chaserTextureRight = theContentManager.Load<Texture2D>(chaserRight);
            chaserTexture = chaserTextureUp;//loads textures for chaser
        }

        public void Top(int width)
        {
            chaserPosition.X = width / 2 - 25;
            chaserPosition.Y = -50;
        }
        //respawn for top chaser

        public void Bottom(int width, int height)
        {
            chaserPosition.X = width / 2 - 25;
            chaserPosition.Y = height;
        }//respawn for bottom chaser


        

        public void BoundryChecking(int width, int height)
        {
            if (chaserPosition.X <= 0)
            {
                chaserPosition.X = 1;
            }
            if (chaserPosition.X >= width - 50)
            {
                chaserPosition.X = width - 50;
            }
            if (chaserPosition.Y <= 0)
            {
                chaserPosition.Y = 1;
            }
            if (chaserPosition.Y >= height - 50)
            {
                chaserPosition.Y = height - 50;
            }//if the chaser is off the screen make him be on the screen 
        }

    }
}

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

   
    class Shooter
    {
        public bool alive = true;
       
        public Vector2 shooterPosition = new Vector2();
        public Texture2D shooterTexture;

        public Texture2D shooterTextureLeft;
        public Texture2D shooterTextureRight;
        public int health = 1;//texture variables for the shooter



        public Shooter(int height)
        {
            shooterPosition.X = 0;
            shooterPosition.Y = height / 2 -25;
            shooterTexture = shooterTextureRight;//shooter left constructor
        }
        public Shooter(int width, int height)
        {
            shooterPosition.X = width - 50;
            shooterPosition.Y = height/2 - 25;
            //shooter right constructor
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {

            theSpriteBatch.Draw(shooterTexture, shooterPosition, Color.White);//draws the shooters
        }
        public void Update(Player mPlayer)
        {
            if (mPlayer.position.Y > shooterPosition.Y)
            {
                shooterPosition.Y++;
            }
            if (mPlayer.position.Y < shooterPosition.Y)
            {
                shooterPosition.Y--;
            }
            //moves the shooter up or down based on the players y position 
        }
        public void LoadContent(ContentManager theContentManager, string shooterLeft, string shooterRight)
        {

            shooterTextureLeft = theContentManager.Load<Texture2D>(shooterLeft);
            shooterTextureRight = theContentManager.Load<Texture2D>(shooterRight);
            shooterTexture = shooterTextureLeft;//loads textures for shooter 
        }
        
        
        
    }
}

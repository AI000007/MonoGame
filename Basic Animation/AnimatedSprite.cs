using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Basic_Animation
{
    public class AnimatedSprite
    {
        public Texture2D Texture {  get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        private int _currentFrame;
        private int _totalFrames;

        public AnimatedSprite(Texture2D texture, int rows, int cols) 
        { 
            Texture = texture;
            Rows = rows;
            Cols = cols;
            _currentFrame = 0;
            _totalFrames = Rows * Cols;
        }

        public void Update()
        {
            _currentFrame++;
            if (_currentFrame == _totalFrames) _currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Cols;
            int height = Texture.Height / Rows;
            int row = _currentFrame / Cols;
            int col = _currentFrame % Cols;

            Rectangle sourceRectangle = new Rectangle(width * col, width*row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}

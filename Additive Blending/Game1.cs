using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Additive_Blending
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D blue;
        private Texture2D red;
        private Texture2D green;

        private float blueAngle = 0;
        private float greenAngle = 0;
        private float redAngle = 0;

        private float blueSpeed = 0.025f;
        private float redSpeed = 0.017f;
        private float greenSpeed = 0.022f;

        private float distance = 100;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            blue = Content.Load<Texture2D>("blue");
            red = Content.Load<Texture2D>("red");
            green = Content.Load<Texture2D>("green");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            blueAngle += blueSpeed;
            redAngle += redSpeed;
            greenAngle += greenSpeed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here


            Vector2 bluePosition = new Vector2(
                            (float)Math.Cos(blueAngle) * distance,
                            (float)Math.Sin(blueAngle) * distance);
            Vector2 greenPosition = new Vector2(
                            (float)Math.Cos(greenAngle) * distance,
                            (float)Math.Sin(greenAngle) * distance);
            Vector2 redPosition = new Vector2(
                            (float)Math.Cos(redAngle) * distance,
                            (float)Math.Sin(redAngle) * distance);

            Vector2 center = new Vector2(300, 140);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            _spriteBatch.Draw(blue, center + bluePosition, Color.White);
            _spriteBatch.Draw(green, center + greenPosition, Color.White);
            _spriteBatch.Draw(red, center + redPosition, Color.White);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}

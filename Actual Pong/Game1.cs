using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Actual_Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Random random;

        private Texture2D middle;

        private Texture2D leftRect;
        private Vector2 leftPos;
        private Rectangle leftBound;

        private Texture2D rightRect;
        private Vector2 rightPos;
        private Rectangle rightBound;

        private Texture2D ball;
        private Vector2 ballPos;
        private Vector2 ballSpeed;
        private Rectangle ballBound;

        private int rectSpeed;

        private int rightScore;
        private int leftScore;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            random = new Random();

            leftRect = new Texture2D(GraphicsDevice, 1, 1);
            leftRect.SetData(new[] { Color.White });
            leftPos = new Vector2(20, 50);
            leftBound = new Rectangle((int)leftPos.X, (int)leftPos.Y, 10, 70);
            

            rightRect = new Texture2D(GraphicsDevice, 1, 1);
            rightRect.SetData(new[] { Color.White });
            rightPos = new Vector2(770, 50);
            rightBound = new Rectangle((int)rightPos.X, (int)rightPos.Y, 10, 70);

            ball = new Texture2D(GraphicsDevice, 1, 1);
            ball.SetData(new[] { Color.White });
            ballPos = new Vector2(400, 300);
            ballSpeed = randomDirection();
            ballBound = new Rectangle((int)ballPos.X, (int)ballPos.Y, 10, 10);

            rectSpeed = 150;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _font = Content.Load<SpriteFont>("Score");
            //middle = Content.Load<Texture2D>("WhiteDots");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ballPos.X += ballSpeed.X* (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPos.Y += ballSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down)) rightPos.Y += rectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.Up)) rightPos.Y -= rectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.S)) leftPos.Y += rectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.W)) leftPos.Y -= rectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            rightBound.X = (int)rightPos.X;
            rightBound.Y = (int)rightPos.Y;

            leftBound.X = (int)leftPos.X;
            leftBound.Y = (int)leftPos.Y;

            ballBound.X = (int)ballPos.X;
            ballBound.Y = (int)ballPos.Y;

            if (rightBound.Intersects(ballBound) || leftBound.Intersects(ballBound)) ballSpeed.X = -ballSpeed.X;
            if (ballBound.Y <= 0 || ballBound.Y >= _graphics.PreferredBackBufferHeight-10) ballSpeed.Y = -ballSpeed.Y;

            if (ballBound.X <= 0)
            {
                rightScore += 1;
                ballPos.X = 400;
                ballPos.Y = random.Next(_graphics.PreferredBackBufferHeight);
                ballSpeed = randomDirection();
            }

            if (ballBound.X >= _graphics.PreferredBackBufferWidth - 10)
            {
                leftScore += 1;
                ballPos.X = 400;
                ballPos.Y = random.Next(_graphics.PreferredBackBufferHeight);
                ballSpeed = randomDirection();
            }
            base.Update(gameTime);

            if (leftPos.Y <= 0) leftPos.Y = 0;
            if (leftPos.Y >= _graphics.PreferredBackBufferHeight-70) leftPos.Y = _graphics.PreferredBackBufferHeight-70;

            if (rightPos.Y <= 0) rightPos.Y = 0;
            if (rightPos.Y >= _graphics.PreferredBackBufferHeight-70) rightPos.Y = _graphics.PreferredBackBufferHeight-70;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(leftRect, leftBound, Color.White);
            _spriteBatch.Draw(rightRect, rightBound, Color.White);
            _spriteBatch.Draw(ball, ballBound, Color.White);
            _spriteBatch.DrawString(_font, leftScore.ToString(), new Vector2(200, 10), Color.White);
            _spriteBatch.DrawString(_font, rightScore.ToString(), new Vector2(600, 10), Color.White);
            //_spriteBatch.Draw(middle, new Rectangle(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight / 2, 10, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

        private Vector2 randomDirection()
        {
            int xSpeed = random.Next(150, 250);
            Vector2 speed = new Vector2(xSpeed, (int)Math.Sqrt(80000 - xSpeed * xSpeed));
            if (random.Next(3) ==2) speed.Y = -speed.Y;
            if (random.Next(3) == 2) speed.X = -speed.X;
            return speed;
        }
    }
}

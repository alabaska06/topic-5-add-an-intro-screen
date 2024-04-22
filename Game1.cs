using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Threading;

namespace topic_5_add_an_intro_screen
{
    public class Game1 : Game
    {

        Rectangle greyTribbleRect;//for the size and start position
        Vector2 tribbleGreySpeed;//for the speed 

        Rectangle creamTribbleRect;
        Vector2 tribbleCreamSpeed;

        Rectangle orangeTribbleRect;
        Vector2 tribbleOrangeSpeed;

        Rectangle brownTribbleRect;
        Vector2 tribbleBrownSpeed;

        Color bgColor;

        List<Rectangle> tribbleRects = new List<Rectangle>();

        List<Vector2> tribbleSpeed = new List<Vector2>();

        List<Texture2D> tribbleTexture = new List<Texture2D>();

        Texture2D tribbleGreyTexture;
        Texture2D tribbleCreamTexture;
        Texture2D tribbleOrangeTexture;
        Texture2D tribbleBrownTexture;

        Random generator = new Random();

        private SpriteFont introfont;

       //intro
       Texture2D introscreenTexture;
        Texture2D tribbleGraveTexture;
        MouseState mouseState;
        //intro
        enum Screen
        {
            Intro,
            TribbleYard,
            TribbleGrave
        }
        Screen screen;

 

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Add an Intro Screen";

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            bgColor = Color.Goldenrod;

            //intro
            screen = Screen.Intro;

            base.Initialize();

            tribbleRects.Add(new Rectangle(150, 10, 100, 100));//size and start position
            tribbleSpeed.Add(new Vector2(5, 0));//first is speed horizontally, second is speed vertically
            tribbleTexture.Add(tribbleGreyTexture);

            tribbleRects.Add(new Rectangle(300, 10, 100, 100));
            tribbleSpeed.Add(new Vector2(0, 5));
            tribbleTexture.Add(tribbleCreamTexture);

            tribbleRects.Add(new Rectangle(450, 10, 100, 100));
            tribbleSpeed.Add(new Vector2(7, 6));
            tribbleTexture.Add(tribbleOrangeTexture);

            tribbleRects.Add(new Rectangle(600, 10, 100, 100));
            tribbleSpeed.Add(new Vector2(5, 5));
            tribbleTexture.Add(tribbleBrownTexture);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            introfont = Content.Load<SpriteFont>("File");

            //intro
            introscreenTexture = Content.Load<Texture2D>("introscreen");
            tribbleGraveTexture = Content.Load<Texture2D>("tribbleGrave");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //intro
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

            }
            else if (screen == Screen.TribbleYard)
            {
                for (int i = 0; i < tribbleRects.Count; i++)
                {
                    Rectangle temp = tribbleRects[i];
                    temp.X += (int)tribbleSpeed[i].X;
                    temp.Y += (int)tribbleSpeed[i].Y;
                    tribbleRects[i] = temp;


                    if (tribbleRects[i].Right > _graphics.PreferredBackBufferWidth || tribbleRects[i].Left < 0)
                    {
                        Vector2 tempSpeed = tribbleSpeed[i];
                        tempSpeed.X *= -1;
                        tribbleSpeed[i] = tempSpeed;
                        if (i == tribbleRects.Count - 1)
                            bgColor = GetRandomColor();
                    }
                    if (tribbleRects[i].Bottom > _graphics.PreferredBackBufferHeight || tribbleRects[i].Top < 0)
                    {
                        Vector2 tempSpeed = tribbleSpeed[i];
                        tempSpeed.Y *= -1;
                        tribbleSpeed[i] = tempSpeed;
                        if (i == tribbleRects.Count - 1)
                            bgColor = GetRandomColor();
                    }
                }

                    if (mouseState.RightButton == ButtonState.Pressed)
                        screen = Screen.TribbleGrave;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private Color GetRandomColor()
        {
            return new Color(generator.Next(256), generator.Next(256), generator.Next(256));
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //intro
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introscreenTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(introfont, ("Left click to see tribbles.\nOnce done, right click."), new Vector2(500, 100), Color.White);
            }
            else if (screen == Screen.TribbleYard)
            {
                for (int i = 0; i < tribbleTexture.Count; i++)
                {
                    _spriteBatch.Draw(tribbleTexture[i], tribbleRects[i], Color.White);
                }
            }
            else if (screen == Screen.TribbleGrave)
            {
                _spriteBatch.Draw(tribbleGraveTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(introfont, ("You killed them."), new Vector2(270, 200), Color.White);
            }
            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
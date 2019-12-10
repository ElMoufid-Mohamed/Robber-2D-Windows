using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Development_Project
{
    class HardLevel : Level
    {

        public List<Tank> AllTanks { get; set; }
        protected byte[,] EnemiesArray { get; set; }

        private int LastTimeShooted = 0;
        public HardLevel(byte[,] obstaclesArray, byte[,] pickablesArray, byte[,] enemiesArray, List<Block> allObstacles, List<Block> allPickables, List<Tank> allTanks) : base(obstaclesArray, pickablesArray, allObstacles, allPickables)
        {
            AllTanks = allTanks;
            EnemiesArray = enemiesArray;
        }

        private void CreateTanks(ContentManager contentManager)
        {
            Texture2D tempTexture = contentManager.Load<Texture2D>("Enemy1");
            Vector2 tempVector = new Vector2();
            Rectangle tempCollisonRectangle = new Rectangle();
            Sprite tempSprite = new Sprite(tempTexture, 1, tempVector);

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < LevelHeight; y++)
                {

                    int BLOCK_ID = EnemiesArray[y, x];
                    string ID_STRING = Convert.ToString(BLOCK_ID);

                    if (ID_STRING != "0")
                    {
                        tempTexture = contentManager.Load<Texture2D>("Enemy" + ID_STRING);
                    }

                    const int marginBottom = 1; // Margin between pickable and platform                                     
                    int maginLeft = (150 - tempTexture.Width) / 2; // Calculate margin to center Tank on the platform
                    tempVector = new Vector2((x * 150) + maginLeft, (y * SpaceBetweenPlatforms) - tempTexture.Height - marginBottom);
                    tempCollisonRectangle = new Rectangle((int)tempVector.X, (int)tempVector.Y, tempTexture.Width, tempTexture.Height);
                    tempSprite = new Sprite(tempTexture, 1, tempVector);

                    switch (EnemiesArray[y, x])
                    {
                        case 1:
                            Tank tempTank = new Tank(tempSprite, tempCollisonRectangle);                          
                            AllTanks.Add(tempTank);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DrawTanks(SpriteBatch spriteBatch)
        {
            foreach (Tank tank in AllTanks)
            {
                tank.SpriteImage.Draw(spriteBatch);

                foreach (Bullet bullet in tank.ShootedBullets)
                {
                    bullet.SpriteImage.Draw(spriteBatch);
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTanks(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Create(ContentManager contentManager)
        {
            CreateTanks(contentManager);
            base.Create(contentManager);

        }

        public void ShootAllTanks(ContentManager contentManager)
        {
            foreach (Tank tank in AllTanks)
            {
                tank.Shoot(contentManager, tank.SpriteImage.Position);

            }
        }

        public override void Update(GameTime gameTime, ContentManager contentManager)
        {
            UpdateTanks(gameTime, contentManager);

            base.Update(gameTime,contentManager);
        }

        public void UpdateTanks(GameTime gameTime, ContentManager contentManager)
        {
            // Shooting

            LastTimeShooted += 100 * gameTime.ElapsedGameTime.Milliseconds / 500;

            if (LastTimeShooted >= 100)
            {
                LastTimeShooted = 0;
                ShootAllTanks(contentManager);
            }

            //Update  

            foreach (Tank tank in AllTanks)
            {              
                foreach (Bullet bullet in tank.ShootedBullets)
                {
                    bullet.Update(gameTime);
                }
            }
        }
    }
}
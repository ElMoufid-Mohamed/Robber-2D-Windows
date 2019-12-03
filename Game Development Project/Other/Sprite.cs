﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Development_Project
{
    class Sprite
    {
        public Texture2D Texture1 { get; set; }
        public Vector2 Position { get; set; }

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture1 = texture;
            Position = position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture1, Position, Color.White);
        }

    }
}

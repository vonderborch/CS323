/*
 * Component: Item System - Item Base
 * Version: 1.0.6
 * Created: April 2nd, 2014
 * Created By: Christian
 * Last Updated: April 29th, 2014
 * Last Updated By: Jacob
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.BarrageEngine.Items
{
    class Item
    {
        public enum ItemType
        {
            health,
            life,
            bomb,
            reloadspeed,
            damage,
            badger,
            bullets
        }

        protected ItemType type;
        protected string spritepath, basepath = "Graphics\\Sprites\\Items\\", pickuptext;
        protected Texture2D sprite;
        protected Rectangle renderPos;
        protected Color color;
        protected float scale, speed;
        protected int radius;

        public Item() { }

        public void loading(Main GameMain)
        {
            sprite = GameMain.Content.Load<Texture2D>(spritepath);

            renderPos.Width = (int)(sprite.Width * scale);
            renderPos.Height = (int)(sprite.Height * scale);
        }

        public bool baseUpdate(int addY)
        {
            renderPos.Y += addY + (int)speed;

            if (renderPos.Y > 720)
                return true;

            return false;
        }

        public void update(Rectangle newPos)
        {
            renderPos = newPos;
        }

        public void draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            if (renderPos.X >= 0 && renderPos.X <= 688 && renderPos.Y >= 0 && renderPos.Y <= 720)
            {
                if (isFlipped)
                {
                    Rectangle newRec = renderPos;
                    newRec.Y = -(int)(renderPos.Y) + 720;
                    spriteBatch.Draw(sprite, newRec, null, color, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                }
                else
                    spriteBatch.Draw(sprite, renderPos, color);
            }
        }

        public string getPickupText()
        {
            return pickuptext;
        }

        public Rectangle GetSize()
        {
            return renderPos;
        }

        public int getRadius()
        {
            return radius;
        }

        public float getScale()
        {
            return scale;
        }

        public Vector2 GetMidPoint()
        {
            return new Vector2(renderPos.X + renderPos.Width / 2,
                               renderPos.Y + renderPos.Height / 2);
        }

        public int getX()
        {
            return renderPos.X;
        }

        public int getY()
        {
            return renderPos.Y;
        }

        public ItemType getType()
        {
            return type;
        }
    }
}

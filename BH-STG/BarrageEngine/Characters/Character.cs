/*
 * Component: Character System - Character Base
 * Version: 1.1.8
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.BarrageEngine.Characters
{
    public class Character
    {
        protected string spritepath, basepath = "Graphics\\Sprites\\";
        protected Texture2D sprite;
        protected Rectangle renderPos, lastPos;
        protected Color color;
        protected float scale;
        protected double angle, mX = 0, mY = 0;
        protected int radius, nanticks = 0, numToFire = 1;
        protected Vector2 coords, lastcoords, size, flameLocation;
        protected bool useVectorCoords = false, isEnemy = true, useFlameLocation = false, canUseFlame = true;

        public Character() { }

        public void loading(Main GameMain)
        {
            sprite = GameMain.Content.Load<Texture2D>(spritepath);

            if (useVectorCoords)
            {
                size.X = sprite.Width * scale;
                size.Y = sprite.Height * scale;
                renderPos.Width = (int)(size.X);
                renderPos.Height = (int)(size.Y);
            }
            else
            {
                renderPos.Width = (int)(sprite.Width * scale);
                renderPos.Height = (int)(sprite.Height * scale);
            }

            extraloading(GameMain);
        }

        public virtual void extraloading(Main GameMain) { }

        public void baseUpdate(int addY)
        {
            if (useVectorCoords)
                coords.Y += addY;
            else
                renderPos.Y += addY;
        }

        public void update(Rectangle newPos)
        {
            renderPos = newPos;
        }

        public int getNumberBullets()
        {
            return numToFire;
        }

        public void drawSpecial(SpriteBatch spriteBatch, bool isFlipped = false)
        {
            spriteBatch.Draw(sprite, renderPos, color);
        }

        public void draw(SpriteBatch spriteBatch, bool isFlipped = false)
        {
            CollisionDetection.Detection detection = new CollisionDetection.Detection();
            Rectangle rec = new Rectangle(0, 0, 720, 720);
            if (useVectorCoords)
            {
                preDraw(spriteBatch, isFlipped);
                if (isFlipped)
                {
                    Vector2 newRec = coords;
                    newRec.Y = -(int)(coords.Y) + 720;
                    spriteBatch.Draw(sprite, new Rectangle((int)newRec.X, (int)newRec.Y, (int)size.X, (int)size.Y), null, color, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                }
                else
                    spriteBatch.Draw(sprite, new Rectangle((int)coords.X, (int)coords.Y, (int)size.X, (int)size.Y), color);
                postDraw(spriteBatch, isFlipped);
            }
            else
            {
                preDraw(spriteBatch, isFlipped);
                if (isFlipped)
                {
                    Rectangle newRec = renderPos;
                    newRec.Y = -(int)(renderPos.Y) + 720;
                    spriteBatch.Draw(sprite, newRec, null, color, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                }
                else
                    spriteBatch.Draw(sprite, renderPos, color);
                postDraw(spriteBatch, isFlipped);
            }
        }

        public virtual void preDraw(SpriteBatch spriteBatch, bool isFlipped = false) { }
        public virtual void postDraw(SpriteBatch spriteBatch, bool isFlipped = false) { }

        public Rectangle GetSize()
        {
            return renderPos;
        }

        public Vector2 GetSizeV()
        {
            return size;
        }

        public int getRadius()
        {
            return radius;
        }

        public float getAngle()
        {
            return (float)angle;
        }

        public float getScale()
        {
            return scale;
        }

        public Vector2 GetMidPoint()
        {
            if (useVectorCoords)
                return new Vector2(coords.X + size.X / 2,
                                   coords.Y + size.Y / 2);
            else
                return new Vector2(renderPos.X + renderPos.Width / 2,
                                   renderPos.Y + renderPos.Height / 2);
        }

        public bool GetCanUseFlame()
        {
            return canUseFlame;
        }

        public Vector2 GetFlameLocation()
        {
            if (useFlameLocation)
            {
                Vector2 tempcoords = GetBottomMidPoint();
                if (isEnemy)
                    tempcoords = GetTopMidPoint();

                tempcoords += flameLocation;
                return tempcoords;
            }

            if (isEnemy)
                return GetTopMidPoint();
            else
                return GetBottomMidPoint();
        }

        private Vector2 GetBottomMidPoint()
        {
            if (useVectorCoords)
                return new Vector2(coords.X + size.X / 2,
                                   coords.Y + size.Y);
            else
                return new Vector2(renderPos.X + renderPos.Width / 2,
                                   renderPos.Y + renderPos.Height);
        }

        private Vector2 GetTopMidPoint()
        {
            if (useVectorCoords)
                return new Vector2(coords.X + size.X / 2,
                                   coords.Y);
            else
                return new Vector2(renderPos.X + renderPos.Width / 2,
                                   renderPos.Y);
        }

        public int getX()
        {
            if (useVectorCoords)
                return (int)coords.X;
            else
                return renderPos.X;
        }

        public int getY()
        {
            if (useVectorCoords)
                return (int)coords.Y;
            else
                return renderPos.Y;
        }

        public bool hasNotMoved()
        {
            if (useVectorCoords)
            {
                if (coords.X == lastcoords.X && coords.Y == lastcoords.Y)
                    return true;
                else if ((int)coords.X == (int)lastcoords.X && (int)coords.Y == (int)lastcoords.Y)
                    return true;
            }
            else
                if (renderPos.X == lastPos.X && renderPos.Y == lastPos.Y)
                    return true;
            return false;
        }
    }
}

/*
 * Component: Weapon System - Weapon
 * Version: 1.0.7
 * Created: March 6th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.BarrageEngine.Weapons
{
    public class Weapon
    {
        public enum WeaponType
        {
            test,
            basic,
            laser,
            singleway,
            douubleway,
            randomdirection,
            randomspeed,
            randomdirectionspeed,
            secretenemy,
            secretplayer,
            fake,
            badger,
            mine
        }

        protected Texture2D sprite;
        protected Vector2 position, size, speed, playerCoords;
        protected Color color;
        protected float angle, scale;
        protected int lifeticks, maxlifeticks, radius;
        protected bool isPlayers;
        protected WeaponType type;
        protected string name, description = "No Description Yet!";

        public Weapon() { }

        public virtual void updatePosition() { }

        public bool shouldDie()
        {
            if (lifeticks == maxlifeticks)
                return true;
            return false;
        }

        public void setPlayerPos(Vector2 newCoords)
        {
            playerCoords = newCoords;
        }

        public WeaponType returnType()
        {
            return type;
        }

        public Vector2 GetMidPoint()
        {
            return new Vector2(position.X + size.X / 2,
                               position.Y + size.Y / 2);
        }

        public int getRadius()
        {
            return radius;
        }

        public Rectangle getBox()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                                 (int)size.X, (int)size.Y);
        }

        public Rectangle getCollisionBox()
        {
            return new Rectangle((int)position.X + (int)size.X / 2 - 16, (int)position.Y,
                                 (int)position.X + (int)size.X / 2 + 16, (int)size.Y);
        }

        public string returnName()
        {
            return name;
        }

        public string returnDesc()
        {
            return description;
        }

        public int returnLife()
        {
            return maxlifeticks;
        }

        public void draw(SpriteBatch spriteBatch, bool isFlipped = false)
        {
            if (isFlipped)
            {
                int newY = -(int)(position.Y) + 720;
                if (this.type == WeaponType.laser)
                    newY -= 720;
                if (!this.isPlayers)
                    spriteBatch.Draw(sprite, new Rectangle((int)position.X, newY, (int)size.X, (int)size.Y), null, color, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(sprite, new Rectangle((int)position.X, newY, (int)size.X, (int)size.Y), null, color, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
            }
            else
            {
                if (!this.isPlayers)
                    spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, color, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                else
                    spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, color, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
            }
        }
    }
}

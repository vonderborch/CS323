/*
 * Component: Items - Bullets
 * Version: 1.0.0
 * Created: May 1st, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Items;

using Microsoft.Xna.Framework;

namespace BH_STG.Items
{
    class Bullets : Item
    {
        public Bullets(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor)
        {
            this.spritepath = this.basepath + "itembullets.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 1.0f / 8.0f;
            this.color = Color.White;
            this.radius = 16;
            this.type = ItemType.bullets;
            this.speed = nSpeed;
            this.pickuptext = "Number of bullets fired increased!";
        }
    }
}

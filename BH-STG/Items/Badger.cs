/*
 * Component: Items - Badger
 * Version: 1.0.2
 * Created: April 23rd, 2014
 * Created By: Christian
 * Last Updated: April 30th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Items;

using Microsoft.Xna.Framework;

namespace BH_STG.Items
{
    class Badger : Item
    {
        public Badger (Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor)
        {
            this.spritepath = this.basepath + "itembadger.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 1.0f / 8.0f;
            this.color = baseColor;
            this.radius = 16;
            this.type = ItemType.badger;
            this.speed = nSpeed;
            this.pickuptext = "The badger is coming...";
        }
    }
}

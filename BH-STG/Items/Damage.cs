/*
 * Component: Items - Damage
 * Version: 1.0.3
 * Created: April 2nd, 2014
 * Created By: Christian
 * Last Updated: April 30th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Items;

using Microsoft.Xna.Framework;

namespace BH_STG.Items
{
    class Damage : Item
    {
        public Damage(Vector2 baseRenderPos, float nSpeed, float rscale, Color baseColor)
        {
            this.spritepath = this.basepath + "itemdamage.png";
            this.renderPos.X = (int)baseRenderPos.X;
            this.renderPos.Y = (int)baseRenderPos.Y;
            this.scale = 1.0f / 8.0f;
            this.color = baseColor;
            this.radius = 16;
            this.type = ItemType.damage;
            this.speed = nSpeed;
            this.pickuptext = "Damage increased!";
        }
    }
}

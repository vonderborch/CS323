/*
 * Component: Particles - Explosion Particle
 * Version: 1.0.3
 * Created: March 30th, 2014
 * Created By: Christian
 * Last Updated: April 29th, 2014
 * Last Updated By: Christian
 * Based on XNA Particle Sample
*/

using BH_STG.BarrageEngine.ParticleSystem;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace BH_STG.Particles
{
    public class Explosion : ParticleSystem
    {
        public Explosion(Main game, int howManyEffects)
            : base(game, howManyEffects) { }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "Graphics\\Sprites\\Particles\\explosionicon";

            // high initial speed with lots of variance.  make the values closer
            // together to have more consistently circular explosions.
            minInitialSpeed = 75;
            maxInitialSpeed = 450;

            // doesn't matter what these values are set to, acceleration is tweaked in
            // the override of InitializeParticle.
            minAcceleration = 0;
            maxAcceleration = 0;

            // explosions should be relatively short lived
            minLifetime = .25f;
            maxLifetime = .5f;

            minScale = .3f;
            maxScale = .5f;

            // we need to reduce the number of particles on Windows Phone in order to keep
            // a good framerate
            minNumParticles = 10;
            maxNumParticles = 15;

            minRotationSpeed = -MathHelper.PiOver4;
            maxRotationSpeed = MathHelper.PiOver4;

            // additive blending is very good at creating fiery effects.
			blendState = BlendState.Additive;

            DrawOrder = AdditiveDrawOrder;
        }

        protected override void InitializeParticle(Particle p, Vector2 where, Random rand)
        {
            base.InitializeParticle(p, where, rand);
            
            // The base works fine except for acceleration. Explosions move outwards,
            // then slow down and stop because of air resistance. Let's change
            // acceleration so that when the particle is at max lifetime, the velocity
            // will be zero.

            // We'll use the equation vt = v0 + (a0 * t). (If you're not familar with
            // this, it's one of the basic kinematics equations for constant
            // acceleration, and basically says:
            // velocity at time t = initial velocity + acceleration * t)
            // We'll solve the equation for a0, using t = p.Lifetime and vt = 0.
            p.Acceleration = -p.Velocity / p.Lifetime;
        }
    }
}

/*
 * Component: Audio System - SoundFX
 * Version: 1.0.1
 * Created: September 9th, 2013
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
*/
using Microsoft.Xna.Framework.Audio;

namespace BH_STG.BarrageEngine.Audio
{
    class SoundFX
    {
        public Main GameMain { get; set; }

        SoundEffect fx;
        SoundEffectInstance fxInstance;

        string oldName = "";

        public void loadContent(string audioName)
        {
            oldName = audioName;
            fx = GameMain.Content.Load<SoundEffect>(@"Audio\SoundFX\" + audioName);
        }

        public void playSound(string audioName, bool loop, float volume, float pitch)
        {
            volume = volume / 100;
            if (oldName != audioName)
            {
                loadContent(audioName);
            }
            fxInstance = fx.CreateInstance();
            if (loop == true)
            {
                fxInstance.IsLooped = true;
            }
            fxInstance.Volume = volume;
            fxInstance.Pitch = pitch;
            fxInstance.Play();
        }
    }
}

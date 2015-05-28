/*
 * Component: Audio System - Music
 * Version: 1.0.1
 * Created: September 9th, 2013
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
*/
using Microsoft.Xna.Framework.Audio;

namespace BH_STG.BarrageEngine.Audio
{
    class Music
    {
        public Main GameMain { get; set; }

        SoundEffect musics;
        SoundEffectInstance musicInstance;

        string oldName = "";

        public void loadContent(string musicName)
        {
            oldName = musicName;
            musics = GameMain.Content.Load<SoundEffect>(@"Audio\Music\" + musicName);
        }

        // volume: 0 to 100
        // pitch: -1 to 1
        public void playSong(string musicName, bool loop, float volume, float pitch)
        {
            volume = volume / 100;
            if (musicName != oldName)
            {
                loadContent(musicName);
            }
            musicInstance = musics.CreateInstance();
            if (loop == true)
            {
                musicInstance.IsLooped = true;
            }
            musicInstance.Volume = volume;
            musicInstance.Pitch = pitch;
            musicInstance.Play();
        }

        public void changeVolume(float volume, float pitch)
        {
            volume = volume / 100;
            musicInstance.Volume = volume;
            musicInstance.Pitch = pitch;
        }

        public void stopMusic()
        {
            musicInstance.Stop();
        }
    }
}

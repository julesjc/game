using SFML.Audio;
using System.Collections.Generic;

namespace Game
{
    public class AudioManager
    {
        static List<Sound> sounds = new List<Sound>();

        public static Sound LoadSound(string path)
        {
            SoundBuffer buffer = new SoundBuffer(path);
            return new Sound(buffer);
        }
        public static Music LoadMusic(string path)
        {
            return new Music(path);
        }
        public static void PlaySound(Sound? sound, float pitch = 1)
        {
            //limit to 128
            if (sounds.Count == 128)
            {
                for (int i = 0; i < 128; i++)
                    sounds[i].Dispose();
                sounds = new List<Sound>();
            }
            if (sound != null)
            {
                sounds.Add(sound);
                sound.Pitch = pitch;
                sound.Play();
            }
        }
    }
}

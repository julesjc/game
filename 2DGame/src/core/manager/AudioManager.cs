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
            Sound sound = new Sound(buffer);
            return sound;
        }
        public static Music LoadMusic(string path)
        {
            Music music = new Music(path);
            return music;
        }
        public static void PlaySound(Sound sound, float pitch = 1)
        {
            if (sounds.Count == 128)
            {
                for (int i = 0; i < 128; i++)
                    sounds[i].Dispose();
                sounds = new List<Sound>();
            }
            sounds.Add(sound);
            sound.Pitch = pitch;
            sound.Play();
        }
    }
}

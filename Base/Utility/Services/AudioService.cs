using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;


namespace Base.Utility.Services
{
   public static class AudioService
   {
      static ContentManager Content;
      static string currentSong;
      static Dictionary<string, Song> songs;
      static Dictionary<string, SoundEffect> soundEffects;

      public static void Initialize(ContentManager content)
      {
         MediaPlayer.Volume = 1f;
         MediaPlayer.IsRepeating = true;
         SoundEffect.MasterVolume = 1f;
         songs = new Dictionary<string, Song>();
         soundEffects = new Dictionary<string, SoundEffect>();
         currentSong = "";
         Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
      }

      public static void AddSong(string songName)
      {
         songs[songName] = Content.Load<Song>(songName);
      }

      public static void RemoveSong(string songName)
      {
         Song s = songs[songName];
         s.Dispose();
         songs.Remove(songName);
      }

      public static void UnloadContent()
      {
         Content.Unload();
      }

      public static void PlaySong(string song)
      {
         currentSong = song;
         MediaPlayer.Stop();
         if (song != "" && songs.ContainsKey(song))
            MediaPlayer.Play(songs[currentSong]);
      }

      public static void PauseSong()
      {
         MediaPlayer.Pause();
      }

      public static void ResumeSong()
      {
         MediaPlayer.Resume();
      }

      public static void StopSong()
      {
         MediaPlayer.Stop();
      }

      public static void ToggleSong()
      {
         if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Pause();
         else
            MediaPlayer.Resume();
      }

      public static float GetBackgroundVolume()
      {
         return MediaPlayer.Volume;
      }

      public static float GetSFXVolume()
      {
         return SoundEffect.MasterVolume;
      }

      public static void SetBackgroundVolume(float volume)
      {
         MediaPlayer.Volume = volume;
      }

      public static void IncreaseBackgroundVolume()
      {
         MediaPlayer.Volume += .1f;
      }

      public static void DecreaseBackgroundVolume()
      {
         MediaPlayer.Volume -= .1f;
      }


      // Finish implimenting later
      public static void AddSoundEffect(string sfxName)
      {
         soundEffects[sfxName] = Content.Load<SoundEffect>(sfxName);
      }

      public static void PlaySoundEffect(string sfxName)
      {
         soundEffects[sfxName].CreateInstance().Play();
      }

      public static void SetSoundEffectVolume(float volume)
      {
         SoundEffect.MasterVolume = volume;
      }

      public static void IncreaseSoundEffectVolume()
      {
         SoundEffect.MasterVolume += .1f;
      }

      public static void DecreaseSoundEffectVolume()
      {
         SoundEffect.MasterVolume -= .1f;
      }
   }
}


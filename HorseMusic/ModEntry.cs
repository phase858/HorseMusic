using System;
using System.Media;
using System.IO;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;


namespace HorseMusic
{
    public class ModEntry : Mod
    {
        bool playing;
        string path = string.Empty;
        SoundPlayer SongPlayer = new SoundPlayer();

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.GameEvents_UpdateTick;
            path = helper.DirectoryPath;
        }

        public string Chooser()
        {
            int choice;
            string[] choices = Directory.GetFiles(Path.Combine(path,"songs"), "*.wav");
            int count = choices.Length - 1;
            choice = new Random().Next(0, count);
            return Path.Combine(path, choices[choice]);
        }

        public void Player(bool input)
        {       
            if (playing == false && input == true)
            {
                SongPlayer.SoundLocation = Chooser();
                SongPlayer.Load();
                SongPlayer.PlayLooping();
            }
            else if (playing == true && input == false)
            {
                SongPlayer.Stop();
            }
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            if (Game1.player.isRidingHorse() == true && playing == false)
            {
                Player(true);
                playing = true;
            }
            else if (Game1.player.isRidingHorse() == false && playing == true)
            {
                Player(false);
                playing = false;
            }
        }
    }
}
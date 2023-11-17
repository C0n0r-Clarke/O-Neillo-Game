using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game2
{
    public class SaveGame
    {
        public string SaveFileName = Directory.GetCurrentDirectory() + @"\assets\game_data.json";
        List<GameStateObject> gamestateobjects = new List<GameStateObject>();
        

        internal void savegame(string p1, string p2, int[,] digitarray, int prevplayer, int filesavelocation)
        {
            int nexttoplay;
            if (prevplayer == 0) { nexttoplay = 1; }
            else { nexttoplay = 0; }

            var Save = new GameStateObject();
            Save.Player1 = p1;
            Save.Player2 = p2;
            Save.DigitArray = digitarray;
            Save.NextPlayer = nexttoplay;

            gamestateobjects.RemoveAt(filesavelocation - 1);
            gamestateobjects.Insert(filesavelocation-1,Save);

            var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(gamestateobjects);

            File.WriteAllText(SaveFileName, serializedsaves);
        }
        internal void LoadGame()
        {
            
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<GameStateObject[]>(jsontext);

            MessageBox.Show($"{loadobject[0].DigitArray}");
            MessageBox.Show($"{loadobject[0].Player1}");
            MessageBox.Show($"{loadobject[0].Player2}");
            MessageBox.Show($"{loadobject[0].NextPlayer}");
        }
        internal void PreLoad()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<GameStateObject[]>(jsontext);



        }

        public class GameStateObject
        {
            public int[,] DigitArray;
            public string Player1;
            public string Player2;
            public int NextPlayer;

        }
    }
}

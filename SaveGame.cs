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
        SaveDataForm savedataform = new SaveDataForm();

        public bool savesfull;


        internal void savegame(string p1, string p2, int[,] digitarray, int prevplayer)
        {
            int nexttoplay;
            if (prevplayer == 0) { nexttoplay = 1; }
            else { nexttoplay = 0; }

            var Save = new GameStateObject();
            Save.Player1 = p1;
            Save.Player2 = p2;
            Save.DigitArray = digitarray;
            Save.NextPlayer = nexttoplay;


            savedataform.ShowForm(savesfull);

            if (savedataform.userexit == true) { }
            else
            {
                Save.Title = savedataform.SaveTitle;

                if (savesfull == true)
                {
                    gamestateobjects.RemoveAt(savedataform.SaveFileSelect - 1);
                }

                gamestateobjects.Insert(savedataform.SaveFileSelect - 1, Save);

                if (gamestateobjects.Count == 5)
                {
                    savesfull = true;
                    for (int i = 0; i < gamestateobjects.Count; i++)
                    {
                        savedataform.SaveNames(gamestateobjects[i].Title); ;
                    }

                    var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(gamestateobjects);

                    File.WriteAllText(SaveFileName, serializedsaves);

                }
            }
        }

        internal void LoadGame()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<GameStateObject[]>(jsontext);

        }
        internal void PreLoad()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<GameStateObject[]>(jsontext);
            if (loadobject != null)
            {
                if (loadobject.Length > 4)
                {
                    savesfull = true;
                }
                for (int i = 0; i < loadobject.Length; i++)
                {
                    gamestateobjects.Add(loadobject[i]);

                    savedataform.SaveNames(loadobject[i].Title); ;
                }
            }
        }

        public class GameStateObject
        {
            public int[,] DigitArray;
            public string Player1;
            public string Player2;
            public int NextPlayer;
            public string Title;

        }
    }
}

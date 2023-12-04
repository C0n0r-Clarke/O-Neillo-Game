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
        AppSettings appsettings = new AppSettings();
        SaveDataForm savedataform = new SaveDataForm();

        public bool speechstart;
        public bool infopanlestart;
        public bool savesfull;
        internal void savegame(string p1, string p2, int[,] digitarray, int prevplayer)
        {
            CheckFileExists();

            int nexttoplay;
            if (prevplayer == 0) { nexttoplay = 1; }
            else { nexttoplay = 0; }

            var Save = new GameStateObject();
            Save.Player1 = p1;
            Save.Player2 = p2;
            Save.DigitArray = digitarray;
            Save.NextPlayer = nexttoplay;

            savedataform.ShowForm(savesfull);

            if (savedataform.SaveTitle == null) { }
            else
            {
                Save.Title = savedataform.SaveTitle;

                if (savesfull == true)
                {
                    appsettings.GameStates.RemoveAt(savedataform.SaveFileSelect - 1);
                }

                appsettings.GameStates.Insert(savedataform.SaveFileSelect - 1, Save);

                if (appsettings.GameStates.Count == 5)
                {
                    savesfull = true;
                    for (int i = 0; i < appsettings.GameStates.Count; i++)
                    {
                        savedataform.SaveNames(appsettings.GameStates[i].Title); ;
                    }
                    SerialiseSave();
                }
            }
        }
        internal void SerialiseSave()
        {
            var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(appsettings);
            File.WriteAllText(SaveFileName, serializedsaves);
        }
        internal void LoadGame()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<AppSettings>(jsontext);

            savedataform.LoadItems(appsettings.GameStates);
        }
        internal void PreLoad()
        {
            CheckFileExists();

            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<AppSettings>(jsontext);

            speechstart = loadobject.Speech;
            infopanlestart = loadobject.InfoPanel;

            if (loadobject.GameStates.Count > 4)
            {
                savesfull = true;
            }
            for (int i = 0; i < loadobject.GameStates.Count; i++)
            {
                appsettings.GameStates.Add(loadobject.GameStates[i]);

                savedataform.SaveNames(loadobject.GameStates[i].Title); ;
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
        public class AppSettings
        {
            public bool InfoPanel;
            public bool Speech;
            public List<GameStateObject> GameStates = new List<GameStateObject>();
        }
        internal void CheckFileExists()
        {
            if (!File.Exists(SaveFileName))
            {
                var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(appsettings);
                File.WriteAllText(SaveFileName, serializedsaves);
            }
        }
        internal void SavePreset(bool speech, bool infopanel)
        {
            appsettings.Speech = speech;
            appsettings.InfoPanel = infopanel;

            SerialiseSave();
        }
    }
}

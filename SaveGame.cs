using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis.TtsEngine;
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

        public int[,] LoadedTiles;
        public string Player1;
        public string Player2;
        public int PrevPlayer;

        public bool speechstart;
        public bool infopanlestart;
        public bool savesfull;
        public bool restoreshow = true;
        public bool closed;
        /// <summary>
        /// Saves the current gameboard to GameStates object list
        /// </summary>
        /// <param name="p1">Player 1s name</param>
        /// <param name="p2">Player 2s name</param>
        /// <param name="digitarray">The Tile 2D array currently used</param>
        /// <param name="prevplayer">Who the previous player was</param>
        internal void savegame(string p1, string p2, int[,] digitarray, int prevplayer)
        {
            CheckFileExists();
          
            GameStateObject Save = new GameStateObject();
            Save.Player1 = p1;
            Save.Player2 = p2;
            Save.DigitArray = digitarray;
            Save.PrevPlayer = prevplayer;

            savedataform.ShowForm(savesfull, 0);

            if (savedataform.userexit == true) { }
            else
            {
                Save.Title = savedataform.SaveTitle;

                if (savesfull == true)
                {
                    appsettings.GameStates.RemoveAt(savedataform.SaveFileSelect);
                    appsettings.GameStates.Insert(savedataform.SaveFileSelect, Save);
                }
                else
                {
                    appsettings.GameStates.Add(Save);
                }

                if (appsettings.GameStates.Count == 5)
                {
                    savesfull = true;
                }
                SerialiseSave();
                Retrieve();
                restoreshow = true;
            }
        }
        /// <summary>
        /// Serialises the Appsettings object and adds it to the json file
        /// </summary>
        internal void SerialiseSave()
        {
            var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(appsettings);
            File.WriteAllText(SaveFileName, serializedsaves);
        }
        /// <summary>
        /// Deserialises the data in the json file, calls the ShowForm method
        /// </summary>
        internal void LoadGame()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<AppSettings>(jsontext);

            savedataform.ShowForm(savesfull, 1);

            if (savedataform.userexit == true) { closed = true; }
            else
            {
                closed = false;
                LoadedTiles = loadobject.GameStates[savedataform.LoadFile].DigitArray;
                Player1 = loadobject.GameStates[savedataform.LoadFile].Player1;
                Player2 = loadobject.GameStates[savedataform.LoadFile].Player2;
                PrevPlayer = loadobject.GameStates[savedataform.LoadFile].PrevPlayer;
            }
        }
        /// <summary>
        /// deserializes the data in the json file and uploads it to the appsettings object
        /// </summary>
        internal void Retrieve()
        {
            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<AppSettings>(jsontext);

            appsettings.GameStates.Clear();

            for (int i = 0; i < loadobject.GameStates.Count; i++)
            {
                appsettings.GameStates.Add(loadobject.GameStates[i]);
            }
        }
        /// <summary>
        /// Deserializes the data in the json file and uploads it to the appsettings object at application startup
        /// </summary>
        internal void PreLoad()
        {
            CheckFileExists();

            string jsontext = File.ReadAllText(SaveFileName);
            var loadobject = JsonConvert.DeserializeObject<AppSettings>(jsontext);

            speechstart = loadobject.Speech;
            infopanlestart = loadobject.InfoPanel;
            
            if (loadobject.GameStates.Count == 0)
            {
                restoreshow = false;
            }
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
        /// <summary>
        /// Object which contains gameboard saved data
        /// </summary>
        public class GameStateObject
        {
            public int[,] DigitArray;
            public string Player1;
            public string Player2;
            public int PrevPlayer;
            public string Title;
        }
        /// <summary>
        /// Object which contains gameboard list and current settings
        /// </summary>
        public class AppSettings
        {
            public bool InfoPanel = true;
            public bool Speech;
            public List<GameStateObject> GameStates = new List<GameStateObject>();
        }
        /// <summary>
        /// Checks if json file exists, if not creates it
        /// </summary>
        internal void CheckFileExists()
        {
            if (!File.Exists(SaveFileName))
            {
                var serializedsaves = Newtonsoft.Json.JsonConvert.SerializeObject(appsettings);
                File.WriteAllText(SaveFileName, serializedsaves);
            }
        }
        /// <summary>
        /// Sets the current game settings in appsettings, calls SerializeSave
        /// </summary>
        /// <param name="speech"></param>
        /// <param name="infopanel"></param>
        internal void SavePreset(bool speech, bool infopanel)
        {
            appsettings.Speech = speech;
            appsettings.InfoPanel = infopanel;

            SerialiseSave();
        }
    }
}

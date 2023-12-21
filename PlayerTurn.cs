using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    public class PlayerTurn
    {
        public int currentplayer;
        /// <summary>
        /// Determines who the current player is, based off who the previous player was
        /// </summary>
        /// <param name="switchplayer"></param>
        public void playerturn(int switchplayer) //take previous player and return next player
        {
            switch (switchplayer)
            {
                case 1:
                    {
                        currentplayer = 0;
                        return;
                    }
                case 0:
                    {
                        currentplayer = 1;
                        return;
                    }
            }
        }
    }
}
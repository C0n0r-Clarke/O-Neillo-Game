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
    public class ValidPlacement
    {
        public int[] xsurrounding = { -1, -1, -1, 0, 0, 1, 1, 1};
        public int[] ysurrounding = { -1, 0, 1, -1, 1, 1, 0, -1};

    }
}
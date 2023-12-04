using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    public class GameEngine
    {
        public int[,] gameboardTiles;
        public PictureBox[,] gameboardPictures;

        public bool endgame = false;
        public int P1Tile = 0;
        public int P2Tile = 0;

        public int nextplayer;

        public bool validmovepossible;
        public bool validmove; //valid move default is false

        public GameEngine(int[,] Tileboard, PictureBox[,] Pictureboard) 
        {
            gameboardTiles = Tileboard; 
            gameboardPictures = Pictureboard;
        }
        internal void CheckSurroundings(int placedX, int placedY, int oppplayer)
        {
            ValidPlacement vp = new ValidPlacement(); // new valid placement instance
            int curplayer = 10; //default current player
           

            if (oppplayer == 0) { curplayer = 1; } //current player is the opposite to the previous player
            else if (oppplayer == 1) { curplayer = 0; }

            int xcheck; //set default value 
            int ycheck; // set default value 

            int furtherx;
            int furthery;

            for (int i = 0; i < vp.ysurrounding.Length; ++i) //run through each possible movement around the placed tile
            {
                int lookincrease = 2; //starting increase when looking for a friendly tile

                xcheck = placedX + vp.xsurrounding[i]; //check the top left tile first and check clockwise after
                ycheck = placedY + vp.ysurrounding[i];

                if (xcheck >= 8 || xcheck < 0) { } //cancel the check if next to a corner or side
                else if (ycheck >= 8 || ycheck < 0) { }

                else if (gameboardTiles[xcheck, ycheck] == oppplayer) //check that the tile next to the placed tile is an opposition player
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        furtherx = placedX + (vp.xsurrounding[i] * lookincrease); //check a tile further for a friendly tile
                        furthery = placedY + (vp.ysurrounding[i] * lookincrease);

                        if (furtherx >= 8 || furtherx < 0) { }
                        else if (furthery >= 8 || furthery < 0) { }
                        else if (gameboardTiles[furtherx, furthery] == 10)// if the further tile is an empty game board piece then break the for loop
                        {
                            break;
                        }
                        else if (gameboardTiles[furtherx, furthery] == curplayer) //if a friendly tile is found along the same line as the opposition tile
                        {
                            validmove = true;
                            changetiles(placedX, placedY, furtherx, furthery, curplayer);
                            break;
                        }
                        else
                        {
                            lookincrease++; //if no friendly tile found, increase distance to look
                        }
                    }
                }

            }

            if (validmove != true)
            {
                MessageBox.Show("This is not a valid move.");
            }
        }
        internal void ValidMovePossible(int prevplayer)
        {
            if (prevplayer == 0)
            {
                nextplayer = 1;
            }
            else
            {
                nextplayer = 0;
            }
            ValidPlacement vp = new ValidPlacement(); // new valid placement instance

            int xcheck; //set default value 
            int ycheck; // set default value 

            int furtherx;
            int furthery;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gameboardTiles[x, y] == 10) //if a blank gameboard peice
                    {
                        gameboardPictures[x, y].Image = Image.FromFile("10.png"); // return to normal gamepiece look
                    }

                    for (int i = 0; i < vp.ysurrounding.Length; ++i)
                    {
                        int lookincrease = 2; //starting increase when looking for a friendly tile

                        xcheck = x + vp.xsurrounding[i]; //check the top left tile first and check clockwise after
                        ycheck = y + vp.ysurrounding[i];

                        if (xcheck >= 8 || xcheck < 0) { } //cancel the check if next to a corner or side
                        else if (ycheck >= 8 || ycheck < 0) { }

                        else if (gameboardTiles[xcheck, ycheck] == prevplayer) //check that the tile next to the placed tile is an opposition player
                        {
                            for (int j = 0; j < 8; ++j)
                            {
                                furtherx = x + (vp.xsurrounding[i] * lookincrease); //check a tile further for a friendly tile
                                furthery = y + (vp.ysurrounding[i] * lookincrease);

                                if (furtherx >= 8 || furtherx < 0) { }
                                else if (furthery >= 8 || furthery < 0) { }
                                else if (gameboardTiles[furtherx, furthery] == 10)// if the further tile is an empty game board piece then break the for loop
                                {
                                    break;
                                }
                                else if (gameboardTiles[x, y] == nextplayer || gameboardTiles[x, y] == prevplayer)
                                {
                                    break;
                                }
                                else if (gameboardTiles[furtherx, furthery] == nextplayer) //if a friendly tile is found along the same line as the opposition tile
                                {
                                    validmovepossible = true;
                                    gameboardPictures[x, y].Image = Image.FromFile("11.png"); //show that this is a possiible move
                                    break;
                                }
                                else
                                {
                                    lookincrease++; //if no friendly tile found, increase distance to look
                                }

                            }
                        }
                    }
                }
            }
        }
        internal void changetiles(int startx, int starty, int endx, int endy, int curplayer)
        {
            int diffx = endx - startx;
            int diffy = endy - starty;
            int xrep;
            int yrep;

            if (diffx < 0)
            {
                xrep = diffx * -1;
            }
            else
            {
                xrep = diffx;
            }

            if (diffy < 0)
            {
                yrep = diffy * -1;
            }
            else
            {
                yrep = diffy;
            }

            int rep = Math.Max(xrep, yrep);

            int xplace;
            int yplace;

            for (int i = 1; i < rep; i++)
            {
                if (diffx == 0)
                {
                    xplace = startx;
                }
                else
                {
                    if (diffx < 0)
                    {
                        xplace = endx + i;
                    }
                    else
                    {
                        xplace = endx - i;
                    }
                }

                if (diffy == 0)
                {
                    yplace = starty;
                }
                else
                {
                    if (diffy < 0)
                    {
                        yplace = endy + i;
                    }
                    else
                    {
                        yplace = endy - i;
                    }
                }
                gameboardTiles[xplace, yplace] = curplayer;
                gameboardPictures[xplace, yplace].Image = Image.FromFile(curplayer + ".png");
            }
        }
        internal void TileCount()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameboardTiles[i, j] == 0)
                    {
                        P1Tile++;
                    }
                    if (gameboardTiles[i, j] == 1)
                    {
                        P2Tile++;
                    }
                }
            }

            WinCheck();
        }
        internal void WinCheck()
        {
            int freespaces = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameboardTiles[i, j] == 10)
                    {
                        freespaces++;
                    }
                }
            }
            if (freespaces > 0) { }
            else
            {
                endgame = true;
            }
        }
        public void ResetBoard()
        {
            for(int i = 0;i < 8; i++)
            {
                for(int j = 0; j < 8;j++)
                {
                    gameboardTiles[i, j] = 10;
                    gameboardPictures[i, j].Image = Image.FromFile("10.png");

                }
            }
            gameboardTiles[3, 4] = 0;
            gameboardPictures[3, 4].Image = Image.FromFile("0.png");
            gameboardTiles[4, 3] = 0;
            gameboardPictures[4,3].Image = Image.FromFile("0.png");
            gameboardTiles[3, 3] = 1;
            gameboardPictures[3, 3].Image = Image.FromFile("1.png");
            gameboardTiles[4, 4] = 1;
            gameboardPictures[4, 4].Image = Image.FromFile("1.png");
        }
    }
}

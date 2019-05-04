using System;
using System.Collections.Generic;
using System.Linq;
using CoreGame.Board.Interfaces;
using UnityEngine;
using Utility;

namespace CoreGame.Board
{
    /// <summary>
    /// Class that contains the logic for modifying the GameState with moves.
    /// </summary>
    public class LOGameBoard : GameBoard<LOGameState, int>
    {  
        public override bool IsWon(LOGameState gameState)
        {
            return gameState.isWon;
        }

        public override LOGameState MakeMove(LOGameState gameState, int move)
        {
            if (!IsValidMove(gameState, move)) return null;
            bool[] boardState = gameState.boardState;
            List<int> adjacentSpaces = GetAdjacentSpaces(gameState, move);
            adjacentSpaces.Add(move); // add the move itself to the adjacent spaces
            foreach (int space in adjacentSpaces) // flip the state of any affected spaces
            {
                boardState[space] = !boardState[space];
            }

            return new LOGameState(gameState.dimensions, boardState, CheckBoardstateWin(boardState), adjacentSpaces);
        }

        public override List<int> GetValidMoves(LOGameState gameState, int move)
        {
            return Enumerable.Range(0, gameState.boardState.Length).ToList();
        }

        public override bool IsValidMove(LOGameState gameState, int move)
        {
            return move >= 0 && move < gameState.boardState.Length;
        }

        private List<int> GetAdjacentSpaces(LOGameState gameState, int spaceIndex)
        {
            List<int> adjacentSpaces = new List<int>();
            if (gameState.dimensions.Length < 1) return adjacentSpaces;

            int[] checkedDimensions = GetMultiplesArrayHelper(gameState.dimensions);

            for (int i = 0; i < checkedDimensions.Length-1; ++i)
            {
                int spaceIndexRegion = spaceIndex/checkedDimensions[i+1]; //Important! Integer division!
                int prevSpace = spaceIndex - checkedDimensions[i];
                int nextSpace = spaceIndex + checkedDimensions[i];
                if (prevSpace / checkedDimensions[i + 1] == spaceIndexRegion)
                {
                    adjacentSpaces.Add(prevSpace);
                }

                if (nextSpace / checkedDimensions[i + 1] == spaceIndexRegion)
                {
                    adjacentSpaces.Add(nextSpace);
                }
            }

            return adjacentSpaces;
        }

        private int[] GetMultiplesArrayHelper(int[] dimensions)
        {
            int[] ret = new int[dimensions.Length+1];
            ret[0] = 1; // first dimension set to 0
            for (int i = 1; i < ret.Length; ++i)
            {
                ret[i] = dimensions.ElementWiseMultiply(1, i);
            }

            return ret;
        }

        private bool CheckBoardstateWin(bool[] boardState)
        {
            return boardState.All(space => space == false); // more explicit notation as opposed to !space
        }
    }
}
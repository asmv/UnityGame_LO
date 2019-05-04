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
            List<int> adjacentSpaces = GetAffectedSpaces(gameState, move); // return value includes the source move
            foreach (int space in adjacentSpaces) // flip the state of any affected spaces
            {
                boardState[space] = !boardState[space];
            }

            return new LOGameState(gameState.dimensions, boardState, CheckBoardstateWin(boardState), adjacentSpaces);
        }

        public override List<int> GetValidMoves(LOGameState gameState)
        {
            return Enumerable.Range(0, gameState.boardState.Length).ToList();
        }

        public override bool IsValidMove(LOGameState gameState, int move)
        {
            return move >= 0 && move < gameState.boardState.Length;
        }

        /// <summary>
        /// Returns a list of all spaces affected by the move. For this game, these are all spaces adjacent to <paramref name="spaceIndex"/>.
        /// </summary>
        /// <param name="gameState">The state used to determine which spaces are adjacent to <paramref name="spaceIndex"/>.</param>
        /// <param name="spaceIndex">The array index representing the move made.</param>
        /// <returns>A list of integers, including <paramref name="spaceIndex"/> that a move of the value <paramref name="spaceIndex"/> has affected.</returns>
        private List<int> GetAffectedSpaces(LOGameState gameState, int spaceIndex) // possible to adjust this function to return spaces 1-over or the entire grid of spaces if desired for a different implementation
        {
            List<int> adjacentSpaces = new List<int>{spaceIndex}; // add the source move to the list
            if (gameState.dimensions.Length < 1) return adjacentSpaces;

            int[] checkedDimensions = GetMultiplesArrayHelper(gameState.dimensions);

            for (int i = 0; i < checkedDimensions.Length-1; ++i)
            {
                int spaceIndexRegion = spaceIndex/checkedDimensions[i+1]; // important! integer division!
                int prevSpace = spaceIndex - checkedDimensions[i];
                int nextSpace = spaceIndex + checkedDimensions[i];
                if (prevSpace >= 0 && prevSpace / checkedDimensions[i + 1] == spaceIndexRegion) // The additional check is required here since integer division does not floor divide negative values
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

        /// <summary>
        /// A helper function that returns element-wise multiples of dimensions (with 1 prepended) for use in array indexing.
        /// </summary>
        /// <param name="dimensions">A <see cref="Array"/> of <see cref="int"/> representing the dimensions of the board.</param>
        /// <returns>A <see cref="List{T}"/> of size <paramref name="dimensions"/>+1 with each element being the element-wise multiplication of all previous elements and itself.</returns>
        private int[] GetMultiplesArrayHelper(int[] dimensions)
        {
            int[] ret = new int[dimensions.Length+1];
            ret[0] = 1; // first dimension set to 0
            for (int i = 1; i < ret.Length; ++i)
            {
                ret[i] = dimensions.ElementWiseMultiply(0, i);
            }

            return ret;
        }

        /// <summary>
        /// Internal function to check wins. The interface implementation takes a shortcut and stores this value in state.
        /// </summary>
        /// <param name="boardState">A boolean array representing the state of the board.</param>
        /// <returns><c>true</c> if all values in <paramref name="boardState"/> are false, <c>false</c> otherwise.</returns>
        private bool CheckBoardstateWin(bool[] boardState)
        {
            return boardState.All(space => space == false); // more explicit notation as opposed to !space
        }
    }
}
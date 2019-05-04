using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoreGame.Board.Interfaces;
using UnityEngine;
using Utility;

namespace CoreGame.Board
{
    public class LOGameState : IGameState
    {

        public LOGameState(int[] dimensions, bool[] boardState = null, bool isWon = false, List<int> lastSpacesToggled=null)
        {
            this.dimensions = dimensions;
            if (boardState == null)
            {
                this.boardState = new bool[dimensions.ElementWiseMultiply()];
                this.boardState.SetAll(false);
            }
            else
            {
                this.boardState = boardState.Copy();
            }

            this.lastSpacesToggled = lastSpacesToggled ?? new List<int>();
            this.isWon = isWon;
        }

        LOGameState(LOGameState gameState)
        {
            isWon = gameState.isWon;
            dimensions = gameState.dimensions.Copy();
            boardState = gameState.boardState.Copy();
        }

        public LOGameState Copy()
        {
            return new LOGameState(this);
        }

        public readonly bool isWon;
        
        public readonly int[] dimensions;

        public readonly bool[] boardState;

        public readonly List<int> lastSpacesToggled; // Efficiency Optimization, the interface does not need to check every tile for an update.
    }
}
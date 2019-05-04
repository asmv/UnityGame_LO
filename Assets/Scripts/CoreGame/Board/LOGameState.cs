using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoreGame.Board.Interfaces;
using UnityEngine;
using Utility;

namespace CoreGame.Board
{
    /// <summary>
    /// Game State class, which serves as a data wrapper around several data types used to express the state of the game.
    /// </summary>
    public class LOGameState : IGameState
    {

        public LOGameState(int[] dimensions, bool[] boardState = null, bool isWon = false)
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
    }
}
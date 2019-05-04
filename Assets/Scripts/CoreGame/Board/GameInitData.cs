using System;
using UnityEngine;

namespace CoreGame.Board
{
    /// <summary>
    /// ScriptableObject containing initialization parameters for any given game.
    /// </summary>
    [CreateAssetMenu]
    public class GameInitData : ScriptableObject
    {
        public int[] dimensions;
        public int numPlayers;
        public int randomShuffleFactor;
    }
}
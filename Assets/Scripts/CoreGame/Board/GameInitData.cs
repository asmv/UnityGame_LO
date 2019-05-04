using System;
using UnityEngine;

namespace CoreGame.Board
{
    [CreateAssetMenu]
    public class GameInitData : ScriptableObject
    {
        public int[] dimensions;
        public int numPlayers;
        public int randomShuffleFactor;
    }
}
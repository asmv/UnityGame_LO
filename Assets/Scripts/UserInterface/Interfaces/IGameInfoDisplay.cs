using UnityEngine;

namespace UserInterface.Interfaces
{
    public interface IGameInfoDisplay
    {
        void DisplayMoves(int moves);
        
        void ResetHUD();
    }
}
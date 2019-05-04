using UnityEngine;
using UnityEngine.UI;
using UserInterface.Interfaces;

namespace UserInterface.Elements
{
    public class GameHUD : IGameInfoDisplay
    {
        [SerializeField] private Text movesText;
        [SerializeField] private Text timerText;

        public void DisplayMoves(int moves)
        {
            throw new System.NotImplementedException();
        }

        public void ResetHUD()
        {
            throw new System.NotImplementedException();
        }
        
        private int m_moves;
    }
}

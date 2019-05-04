using System;
using CoreGame.Board;
using CoreGame.Board.Interfaces;
using UnityEngine;
using UserInterface.Interfaces;

namespace CoreGame.Management
{
    public class GameManager : MonoBehaviour
    {
        public GameManager Instance => m_instance;

        public event Action<GameManagerState> OnGameManagerStateChanged;
        public event Action<IGameState> OnGameStateChanged;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
        }

        private void HandleUserInteraction()
        {
            throw new NotImplementedException();
        }
        
        public void ChangeGameState(GameManagerState state)
        {
            if (state == GameManagerState.ActivePlay)
            {
                m_loGameState = m_loGameBuilder.CreateSolvableGameState(m_loGameBoard, new[] {5, 5});
            }
            OnGameManagerStateChanged?.Invoke(state);
        }

        [SerializeField] private IGameStateDisplay m_gameStateDisplay;
        private LOGameState m_loGameState;
        private static LOGameBuilder m_loGameBuilder = new LOGameBuilder();
        private static LOGameBoard m_loGameBoard = new LOGameBoard();

        private GameManager m_instance;
    }
}

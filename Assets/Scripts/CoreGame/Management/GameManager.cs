using System;
using CoreGame.Board;
using CoreGame.Board.Interfaces;
using UnityEngine;
using UserInterface.Elements;
using UserInterface.Interfaces;
using Utility.ScriptableObjects;

namespace CoreGame.Management
{
    public class GameManager : MonoBehaviour
    {        
        public static GameManager Instance => m_instance;

        public event Action<GameManagerState> OnGameManagerStateChanged;
        public event Action<LOGameState> OnGameStateChanged;
        
        [SerializeField] private IntReference m_movesMade;
        [SerializeField] private GameInitData m_gameInitData;
        [SerializeField] private LightsOutDefinition m_lightsOutDefinition;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
        }

        public void ResetGame()
        {
            ChangeGameState(GameManagerState.ActivePlay);
        }

        public void HandleUserInteraction(int selected)
        {
            m_loGameState = m_lightsOutDefinition.gameBoard.MakeMove(m_loGameState, selected);
            m_movesMade.value += 1;
            OnGameStateChanged?.Invoke(m_loGameState);
            if (m_loGameState.isWon)
            {
                ChangeGameState(GameManagerState.ResultsDisplay);
            }
        }
        
        public void ChangeGameState(GameManagerState state)
        {
            if (state == GameManagerState.ActivePlay)
            {
                m_loGameState = m_lightsOutDefinition.gameBuilder.Create(m_lightsOutDefinition.gameBoard, m_gameInitData);
            }
            OnGameManagerStateChanged?.Invoke(state);
            OnGameStateChanged?.Invoke(m_loGameState);
        }
        
        private void Start()
        {
            ResetGame();
        }

        private LOGameState m_loGameState;

        private static GameManager m_instance;
    }
}

﻿using System;
using CoreGame.Board;
using CoreGame.Board.Interfaces;
using UnityEngine;
using UserInterface.Elements;
using UserInterface.Interfaces;

namespace CoreGame.Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => m_instance;

        public event Action<GameManagerState> OnGameManagerStateChanged;
        public event Action<IGameState> OnGameStateChanged;
        [SerializeField] private LightGrid m_gameStateDisplay;

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
            m_loGameState = m_loGameBoard.MakeMove(m_loGameState, selected);
            if (m_loGameState.isWon)
            {
                ChangeGameState(GameManagerState.ResultsDisplay);
            }
            else
            {
                m_gameStateDisplay.Refresh(m_loGameState);
            }
        }
        
        public void ChangeGameState(GameManagerState state)
        {
            if (state == GameManagerState.ActivePlay)
            {
                m_loGameState = m_loGameBuilder.CreateSolvableGameState(m_loGameBoard, new[] {5, 5});
            }
            m_gameStateDisplay.Display(m_loGameState);
            OnGameManagerStateChanged?.Invoke(state);
        }
        
        private void Start()
        {
            ResetGame();
        }

        private LOGameState m_loGameState;
        private static LOGameBuilder m_loGameBuilder = new LOGameBuilder();
        private static LOGameBoard m_loGameBoard = new LOGameBoard();

        private static GameManager m_instance;
    }
}

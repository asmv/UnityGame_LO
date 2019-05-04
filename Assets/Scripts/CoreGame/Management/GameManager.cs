using UnityEngine;

namespace CoreGame.Management
{
    public class GameManager : MonoBehaviour
    {
        public GameManager Instance => m_instance;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
        }

        private GameManager m_instance;
    }
}

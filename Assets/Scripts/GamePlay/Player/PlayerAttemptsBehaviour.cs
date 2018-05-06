using UnityEngine;
using UnityEngine.Events;

namespace GamePlay.Player
{
    public class PlayerAttemptsBehaviour : MonoBehaviour
    {
        private int _initialAttempts;
        private int _currentAttempts;

        public int attempts;
        public UnityEvent attemptsExpired;
        public UnityEvent attemptsChanged;

        public int currentAttempts
        {
            get
            {
                return _currentAttempts;
            }

            set
            {
                _currentAttempts = value;
                if (_currentAttempts < 0)
                {
                    attemptsExpired.Invoke();
                }
                attemptsChanged.Invoke();
            }
        }

        private void Awake()
        {
            _initialAttempts = attempts;
            _currentAttempts = attempts;
            CreateForceOnInput.released += OnReleasedInput;
        }

        public void ResetAttempts()
        {
            _currentAttempts = _initialAttempts;
        }

        void OnReleasedInput()
        {
            currentAttempts--;
        }
    }
}
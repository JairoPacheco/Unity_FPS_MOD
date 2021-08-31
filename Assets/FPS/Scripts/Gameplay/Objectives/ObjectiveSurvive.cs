using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveSurvive : Objective
    {

        int m_KillTotal;

        public int KillsToCompleteObjective;

        public int RoundNumber;

        bool roundCompleted = false;

        protected override void Start()
        {
            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);

            Title = "Round " + RoundNumber;
        }

        public void initialize(int RoundNumber, int KillsToCompleteObjective){
            this.RoundNumber = RoundNumber;
            this.KillsToCompleteObjective = KillsToCompleteObjective;
            Title = "Round " + RoundNumber;
            base.Start();
            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), string.Empty);
        }

        void OnEnemyKilled(EnemyKillEvent evt)
        {
            if (IsCompleted)
                return;

            m_KillTotal++;

            int targetRemaining = evt.RemainingEnemyCount;

            // update the objective text according to how many enemies remain to kill
            if (targetRemaining == 0)
            {
                roundCompleted = true;
            }
            else if (targetRemaining == 1)
            {
                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), "One enemy left");
            }
            else
            {
                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), string.Empty);
            }
        }

        string GetUpdatedCounterAmount()
        {
            return m_KillTotal + " / " + KillsToCompleteObjective;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
        }

        public void FinishRound(){
            CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Round Finished");
        }
    }
}
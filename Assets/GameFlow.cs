using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class GameFlow : MonoBehaviour
    {
        public PlayerController player;
        public Health health;
        public UIManager uiManager;

        private void OnEnable()
        {
            player.DeathEvent += Defeat;
            health.Defeat += Victory;
        }

        private void OnDisable()
        {
            player.DeathEvent -= Defeat;
            health.Defeat -= Victory;
        }

        private void Defeat()
        {
            uiManager.Show(typeof(DefeatView));
        }

        private void Victory()
        {
            uiManager.Show(typeof(WinView));
        }

        public void EnterBossBattle(GameObject boss)
        {
            //Change Music
            //Change Camera
            //Play Cutscene
            health = boss.GetComponentInChildren<Health>();
        }
    }
}

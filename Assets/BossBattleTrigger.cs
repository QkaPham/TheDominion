using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class BossBattleTrigger : MyMonoBehaviour
    {
        [SerializeField] private Collider trigger;
        [SerializeField] private GameObject Boss;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private CameraZoom cameraZoom;

        private TargetDetector detector;
        private PlayerController player;
        private Health health;

        private void Awake()
        {
            detector = Boss.GetComponent<TargetDetector>();
            health = Boss.GetComponentInChildren<Health>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StartBossBattle(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OpenBossRoom(false);
        }

        private void OnEnable()
        {
            health.Defeat += Victory;
        }

        private void OnDisable()
        {
            health.Defeat -= Victory;
        }

        private void StartBossBattle(Collider collider)
        {
            collider.GetComponent<PlayerStateMachine>().EnterBossRoom();
            player = collider.GetComponent<PlayerController>();
            player.DeathEvent += Defeat;
            detector.Target = player.transform;
            cameraZoom.ZoomTo(60f);
        }

        private void EndBossBattle()
        {
            player.DeathEvent -= Defeat;
            OpenBossRoom(true);
        }

        private void OpenBossRoom(bool open)
        {
            trigger.isTrigger = open;
        }

        private void Defeat()
        {
            EndBossBattle();
        }

        private void Victory()
        {
            EndBossBattle();
            uiManager.Show(typeof(WinView));
        }
    }
}

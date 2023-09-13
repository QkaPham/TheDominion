using System;
using System.Collections;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateDefeat : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Defeat";
        [field: SerializeField] protected override float TransitionDuration { get; set;  } = 0f;


        public override void Enter()
        {
            base.Enter();

            player.StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            while (!IsAnimationFinished)
            {
                yield return null;
            }
            player.Death();
        }
    }
}
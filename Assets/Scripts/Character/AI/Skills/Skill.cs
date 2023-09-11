using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public abstract class Skill
    {
        public abstract string Name { get; protected set; }
        public abstract float Range { get; set; }

        public int Hash { get; protected set; }

        public virtual bool CanUse { get; protected set; } = true;

        protected TargetDetector targetDetector;
        protected AILook aiLook;

        public void Init(TargetDetector targetDetector, AILook ailook)
        {
            Hash = Animator.StringToHash(Name);
            this.targetDetector = targetDetector;
            this.aiLook = ailook;
        }

        public virtual void Activate(NavMeshAgent agent) { }
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public abstract class Skill
    {
        public abstract string Name { get; protected set; }
        public abstract float SkillRange { get; set; }

        public int Hash { get; protected set; }

        public virtual bool CanUse { get; protected set; } = true;

        protected TargetDetector targetDetector;

        public void Init(TargetDetector targetDetector)
        {
            Hash = Animator.StringToHash(Name);
            this.targetDetector = targetDetector;
        }

        public virtual void Activate(NavMeshAgent agent) { }
    }
}

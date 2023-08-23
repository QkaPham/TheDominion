using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AISkills : MyMonoBehaviour
    {
        [SerializeField] private TargetDetector targetDetector;
        public List<Skill> skillList = new();

        public bool HasSkillAvailable => nextSkill.CanUse;

        public Skill nextSkill;

        public float SkillRange => nextSkill.skillRange;

        public SkillBite bite;
        public SkillPounch pounch;


        public override void LoadComponent()
        {
            base.LoadComponent();
            
            targetDetector = GetComponent<TargetDetector>();
        }

        private void Awake()
        {
            skillList = new List<Skill> { bite, pounch };
            bite.Init(targetDetector);
            pounch.Init(targetDetector);
            nextSkill = skillList[0];
        }

        private void Update()
        {
            ChooseSkill();
        }

        public void Activate(NavMeshAgent agent)
        {
            nextSkill.Activate(agent);
        }

        public void ChooseSkill()
        {
            if (HasSkillAvailable) return;

            foreach (var skill in skillList)
            {
                if (skill.CanUse)
                {
                    nextSkill = skill;
                }
            }
        }
    }

    [Serializable]
    public class SkillBite : Skill
    {
        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.stoppingDistance = 2f;
            agent.enabled = false;
            Debug.Log(agent.enabled);
        }
    }

    [Serializable]
    public class SkillPounch : Skill
    {
        public float minDistance = 2f;
        public override bool CanUse => base.CanUse && targetDetector.DistanceToTarget < minDistance;

        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.stoppingDistance = 2f;
            agent.SetDestination(targetDetector.Target.position - targetDetector.TargetDirection.normalized * 2f);
        }
    }

    [Serializable]
    public  class Skill 
    {
        public string name;
        public int hash;
        public float cooldown;
        public float lastUseTime;
        public virtual bool CanUse => Time.time - lastUseTime >= cooldown;
        public float skillRange;
        public TargetDetector targetDetector;

        public void Init(TargetDetector targetDetector)
        {
            hash = Animator.StringToHash(name);
            this.targetDetector = targetDetector;
        }

        public virtual void Activate(NavMeshAgent agent)
        {
            lastUseTime = Time.time;
        }
    }
}

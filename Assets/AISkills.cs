using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AISkills : MyMonoBehaviour
    {
        public List<Skill> skillList = new();

        public bool HasSkillAvailable => nextSkill.CanUse;

        public Skill nextSkill;

        public float SkillRange => nextSkill.skillRange;

        public Skill bite;
        public Skill pounch;

        private void Awake()
        {
            skillList = new List<Skill> { bite, pounch };
            bite.Init();
            pounch.Init();
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

            agent.enabled = false;
        }
    }

    public class SkillPounch : Skill
    {
        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.speed = 10;
        }
    }

    [Serializable]
    public class Skill
    {
        public string name;
        public int hash;
        public float cooldown;
        public float lastUseTime;
        public bool CanUse => Time.time - lastUseTime >= cooldown;
        public float skillRange;

        public void Init()
        {
            hash = Animator.StringToHash(name);
        }

        public virtual void Activate(NavMeshAgent agent)
        {
            lastUseTime = Time.time;
        }
    }
}

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AISkills : MyMonoBehaviour
    {
        [SerializeField] protected TargetDetector targetDetector;

        protected List<Skill> skillList;
        [SerializeField] protected float coolDownTime = 5f;
        [SerializeField] protected SkillPound pound;
        [SerializeField] protected SkillBite bite;
        protected WaitForSeconds cooldown;
        public bool canAttack;
        public bool CanAttack
        {
            get => canAttack;
            set
            {
                canAttack = value;
                if (canAttack) ChooseSkill();
            }
        }

        public Skill NextSkill { get; protected set; }

        public float SkillRange => NextSkill.SkillRange;

        public override void LoadComponent()
        {
            base.LoadComponent();

            targetDetector = GetComponent<TargetDetector>();
        }

        private void Awake() => Initialize();

        private void Initialize()
        {
            skillList = new List<Skill>
            {
                pound,
                bite,
            };

            foreach (Skill skill in skillList)
            {
                skill.Init(targetDetector);
            }
            NextSkill = skillList[0];
            CanAttack = true;
            cooldown = new WaitForSeconds(coolDownTime);
        }

        public void Activate(NavMeshAgent agent)
        {
            NextSkill.Activate(agent);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            CanAttack = false;
            yield return cooldown;
            CanAttack = true;
        }

        public void ChooseSkill()
        {
            var canUseSkills = skillList.Where(skill => skill.CanUse).ToList();
            NextSkill = canUseSkills[Random.Range(0, canUseSkills.Count())];
        }
    }
}

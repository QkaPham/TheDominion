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
        [SerializeField] protected AILook aiLook;

        protected List<Skill> skillList;
        [SerializeField] protected float coolDownTime = 5f;
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

        public Skill ReadySkill { get; protected set; }

        public float SkillRange => ReadySkill.Range;

        public override void LoadComponent()
        {
            base.LoadComponent();

            targetDetector = GetComponent<TargetDetector>();
            aiLook = GetComponent<AILook>();
        }

        protected void Awake() => Initialize();

        protected virtual void Initialize()
        {
            skillList.ForEach(skill => skill.Init(targetDetector, aiLook));
            CanAttack = true;
            cooldown = new WaitForSeconds(coolDownTime);
            ChooseSkill();
        }

        public void Activate(NavMeshAgent agent)
        {
            ReadySkill.Activate(agent);
            CanAttack = false;
        }

        public void CoolDown()
        {
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            yield return cooldown;
            CanAttack = true;
        }

        protected virtual void ChooseSkill()
        {
            var canUseSkills = skillList.Where(skill => skill.CanUse).ToList();
            ReadySkill = canUseSkills[Random.Range(0, canUseSkills.Count())];
        }
    }
}

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class AISkills : MyMonoBehaviour
    {
        [SerializeField] protected TargetDetector targetDetector;
        [SerializeField] protected AILook aiLook;

        protected List<Skill> skillList;
        [SerializeField] protected float coolDownTime = 5f;
        protected WaitForSeconds cooldown;
        public bool isCoolDownFinish;
        public bool IsCoolDownFinish
        {
            get => isCoolDownFinish;
            set
            {
                isCoolDownFinish = value;
                if (isCoolDownFinish) StartCoroutine(ChooseSkill());
            }
        }

        public Skill ReadySkill { get; protected set; }

        public bool HasCanUseSkill() => ReadySkill != null;

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
            cooldown = new WaitForSeconds(coolDownTime);
            StartCoroutine(TargetCheck());
        }

        public void Activate(AIStateMachine ai)
        {
            ReadySkill.Activate(ai);
            IsCoolDownFinish = false;
        }

        public void CoolDown()
        {
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            ReadySkill = null;
            yield return cooldown;
            IsCoolDownFinish = true;
        }

        protected virtual IEnumerator ChooseSkill()
        {
            List<Skill> canUseSkills = new List<Skill>();

            while (canUseSkills.Count <= 0)
            {
                canUseSkills = skillList.Where(skill => skill.CanUse).ToList();
                yield return null;
            }
            ReadySkill = canUseSkills[Random.Range(0, canUseSkills.Count())];
        }

        private IEnumerator TargetCheck()
        {
            while (!targetDetector.HasTarget())
            {
                yield return null;
            }
            IsCoolDownFinish = true;
        }
    }
}

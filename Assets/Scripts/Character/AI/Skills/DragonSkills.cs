using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DragonSkills : AISkills
    {
        [SerializeField] private SkillBite bite;
        [SerializeField] private SkillBite tail;
        [SerializeField] private SkillStump stump;
        [SerializeField] private SkillFireBreath fireBreath;
        [SerializeField] private SkillSpecial special;

        protected override void Initialize()
        {
            skillList = new List<Skill>
            {
                //bite,
                //tail,
                stump,
                //fireBreath,
                //special,
            };

            base.Initialize();
        }
    }
}

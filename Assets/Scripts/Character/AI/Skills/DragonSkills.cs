using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DragonSkills : AISkills
    {
        [SerializeField] private SkillBite bite;
        [SerializeField] private SkillBite tail;
        [SerializeField] private SkillFireBreath fireBreath;
        [SerializeField] private SkillSpecial special;

        protected override void Initialize()
        {
            skillList = new List<Skill>
            {
                bite,
                tail,
                fireBreath,
                special,
            };

            base.Initialize();
        }
    }
}

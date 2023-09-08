using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DragonSkills : AISkills
    {
        [SerializeField] private SkillFireBreath fireBreath;
        [SerializeField] private SkillSpecial special;

        protected override void Initialize()
        {
            skillList = new List<Skill>
            {
                fireBreath,
                special,
            };

            base.Initialize();
        }
    }
}

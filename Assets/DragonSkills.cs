using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DragonSkills : AISkills
    {
        [SerializeField] private SkillFireBreath fireBreath;

        protected override void Initialize()
        {
            skillList = new List<Skill>
            {
                fireBreath
            };
            base.Initialize();
        }
    }
}

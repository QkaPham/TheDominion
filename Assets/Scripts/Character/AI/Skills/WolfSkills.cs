using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class WolfSkills : AISkills
    {
        [SerializeField] protected SkillPound pound;
        [SerializeField] protected SkillBite bite;

        protected override void Initialize()
        {
            skillList = new List<Skill>
            {
                pound,
                bite,
            };

            base.Initialize();
        }
    }
}

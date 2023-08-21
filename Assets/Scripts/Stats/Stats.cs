using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class Stats : MyMonoBehaviour
    {
        [SerializeField] private Stat[] stats;

        public Stat GetStat(StatID statID)
        {
            return stats.FirstOrDefault(stat => stat.id == statID);
        }

        public bool Upgrade(StatID statID)
        {
            var stat = GetStat(statID);
            if (stat.IsMax()) return false;

            stat.Upgrade();
            return true;
        }
    }
}
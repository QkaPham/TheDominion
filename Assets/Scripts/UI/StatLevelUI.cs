using TMPro;
using UnityEngine;

namespace Project3D
{
    public class StatLevelUI : MyMonoBehaviour
    {
        [SerializeField] private Stat stat;
        [SerializeField] private TMP_Text LevelText;

        public override void LoadComponent()
        {
            base.LoadComponent();
            LevelText = GetComponent<TMP_Text>();
        }

        private void Start() => stat.OnLevelChange += UpdateUI;

        private void OnDestroy() => stat.OnLevelChange -= UpdateUI;

        private void UpdateUI(int level)
        {
            LevelText.text = $"Lv." + stat.Level;
        }
    }
}

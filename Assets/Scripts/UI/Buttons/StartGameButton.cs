using UnityEngine;

namespace Project3D
{
    public class StartGameButton : ButtonBinder
    {
        [SerializeField] private SceneLoader loader;

        protected override void OnClick()
        {
            loader.LoadScene("Game");
        }
    }
}

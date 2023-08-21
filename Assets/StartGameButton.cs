using UnityEngine;

namespace Project3D
{
    public class StartGameButton : ButtonBinder
    {
        protected override void OnClick()
        {
            Debug.Log("Start Game");
            Debug.Log("Load Game");
        }
    }
}

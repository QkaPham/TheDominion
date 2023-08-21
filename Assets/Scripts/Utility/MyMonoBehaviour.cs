using UnityEngine;

namespace Project3D
{
    public abstract class MyMonoBehaviour : MonoBehaviour, IHaveDependencies
    {
        public virtual void LoadComponent() { }

        protected void Reset()
        {
            LoadComponent();
        }
    }

    public interface IHaveDependencies
    {
        void LoadComponent() { }
    }
}
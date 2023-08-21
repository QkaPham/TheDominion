using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Project3D
{
    public static class MyTools
    {
        [MenuItem("My Tools/Load Components")]
        private static void HelloWorld()
        {
            var monoBehaviors = Object.FindObjectsOfType<MonoBehaviour>().OfType<IHaveDependencies>();

            foreach (var monoBehavior in monoBehaviors)
            {
                monoBehavior.LoadComponent();
            }
        }
    }
}
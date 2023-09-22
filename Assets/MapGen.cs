using UnityEngine;

namespace Project3D
{
    public class MapGen : MonoBehaviour
    {
        [SerializeField] private Transform forestTile;
        [SerializeField] private Vector2 mapSize;

        private void OnValidate()
        {
            foreach (Transform t in transform)
            {
                if (t != null && t != transform)
                {
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        DestroyImmediate(t.gameObject);
                    };
                }
            }

            if (forestTile != null)
            {
                Generate();
            }
        }

        private void Generate()
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    var position = new Vector3(2 * x, 0, 2 * y);
                    Instantiate(forestTile, position, Quaternion.identity, transform);
                }
            }
        }
    }
}

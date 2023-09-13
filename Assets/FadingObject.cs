using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class FadingObject : MyMonoBehaviour, IEquatable<FadingObject>
    {
        public List<Renderer> Renderers { get; private set; } = new List<Renderer>();
        public Vector3 Position;
        public List<Material> Materials { get; private set; } = new List<Material>();
        public float InitialAlpha;

        private void Awake()
        {
            Position = transform.position;
            if (Renderers.Count == 0)
            {
                Renderers.AddRange(GetComponentsInChildren<Renderer>());
            }
            foreach (Renderer renderer in Renderers)
            {
                Materials.AddRange(renderer.materials);
            }

            InitialAlpha = Materials[0].color.a;
        }

        public bool Equals(FadingObject other)
        {
            return Position.Equals(other.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}

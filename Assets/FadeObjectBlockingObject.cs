using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class FadeObjectBlockingObject : MonoBehaviour
    {
        [SerializeField] private LayerMask LayerMask;
        [SerializeField] private Transform Target;
        [SerializeField] private Camera Camera;
        [SerializeField, Range(0, 1f)] private float FadeAlpha = 0.33f;
        [SerializeField] private bool retainShadow = true;
        [SerializeField] private Vector3 TargetPositionOffset = Vector3.up;
        [SerializeField] private float FadeSpeed = 1f;

        private List<FadingObject> ObjectsBlockingView = new List<FadingObject>();
        private Dictionary<FadingObject, Coroutine> RunningCoroutines = new Dictionary<FadingObject, Coroutine>();
        private RaycastHit[] Hits = new RaycastHit[10];
        private void Start()
        {
            StartCoroutine(CheckForObjects());
        }

        private IEnumerator CheckForObjects()
        {
            while (true)
            {
                int hits = Physics.RaycastNonAlloc(
                    Camera.transform.position,
                    (Target.transform.position + TargetPositionOffset - Camera.transform.position).normalized,
                    Hits,
                    Vector3.Distance(Camera.transform.position, Target.transform.position + TargetPositionOffset),
                    LayerMask
                    );
                if (hits > 0)
                {
                    for (int i = 0; i < hits; i++)
                    {
                        FadingObject fadingObject = GetFadingObjectFromHit(Hits[i]);

                        if (fadingObject != null && !ObjectsBlockingView.Contains(fadingObject))
                        {
                            if (RunningCoroutines.ContainsKey(fadingObject))
                            {
                                if (RunningCoroutines[fadingObject] != null)
                                {
                                    StopCoroutine(RunningCoroutines[fadingObject]);
                                }

                                RunningCoroutines.Remove(fadingObject);
                            }

                            RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                            ObjectsBlockingView.Add(fadingObject);
                        }
                    }
                }
                FadeObjectsNoLongerBeingHit();
                ClearHits();

                yield return null;
            }
        }

        private void FadeObjectsNoLongerBeingHit()
        {
            List<FadingObject> objectsToRemove = new List<FadingObject>(ObjectsBlockingView.Count);

            foreach (FadingObject fadingObject in ObjectsBlockingView)
            {
                bool objectIsBeingHit = false;
                for (int i = 0; i < Hits.Length; i++)
                {
                    FadingObject hitFadingObject = GetFadingObjectFromHit(Hits[i]);
                    if (hitFadingObject != null && fadingObject == hitFadingObject)
                    {
                        objectIsBeingHit = true;
                    }
                }

                if (!objectIsBeingHit)
                {
                    if (RunningCoroutines.ContainsKey(fadingObject))
                    {
                        if (RunningCoroutines[fadingObject] != null)
                        {
                            StopCoroutine(RunningCoroutines[fadingObject]);
                        }
                        RunningCoroutines.Remove(fadingObject);
                    }

                    RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));
                    objectsToRemove.Add(fadingObject);
                }
            }

            foreach (FadingObject removeObject in objectsToRemove)
            {
                ObjectsBlockingView.Remove(removeObject);
            }

        }


        private IEnumerator FadeObjectOut(FadingObject fadingObject)
        {
            foreach (Material material in fadingObject.Materials)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.SetInt("_Surface", 1);

                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                material.SetShaderPassEnabled("DepthOnly", false);
                material.SetShaderPassEnabled("SHADOWCASTER", retainShadow);

                material.SetOverrideTag("RenderType", "Transparent");

                material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            float time = 0f;

            while (fadingObject.Materials[0].color.a > FadeAlpha)
            {
                foreach (Material material in fadingObject.Materials)
                {
                    if (material.HasProperty("_BaseColor"))
                    {
                        material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(fadingObject.InitialAlpha, FadeAlpha, time * FadeSpeed));
                    }
                }

                time += Time.deltaTime;
                yield return null;
            }

            if (RunningCoroutines.ContainsKey(fadingObject))
            {
                StopCoroutine(RunningCoroutines[fadingObject]);
                RunningCoroutines.Remove(fadingObject);
            }
        }

        private IEnumerator FadeObjectIn(FadingObject fadingObject)
        {
            float time = 0f;

            while (fadingObject.Materials[0].color.a < fadingObject.InitialAlpha)
            {
                foreach (Material material in fadingObject.Materials)
                {
                    if (material.HasProperty("_BaseColor"))
                    {
                        material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(FadeAlpha, fadingObject.InitialAlpha, time * FadeSpeed));
                    }
                }

                time += Time.deltaTime;
                yield return null;
            }

            foreach (Material material in fadingObject.Materials)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.SetInt("_Surface", 0);

                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                material.SetShaderPassEnabled("DepthOnly", true);
                material.SetShaderPassEnabled("SHADOWCASTER", true);

                material.SetOverrideTag("RenderType", "Opaque");

                material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            if (RunningCoroutines.ContainsKey(fadingObject))
            {
                StopCoroutine(RunningCoroutines[fadingObject]);
                RunningCoroutines.Remove(fadingObject);
            }
        }

        private void ClearHits()
        {
            Array.Clear(Hits, 0, Hits.Length);
        }

        private FadingObject GetFadingObjectFromHit(RaycastHit hit)
        {
            return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
        }
    }
}

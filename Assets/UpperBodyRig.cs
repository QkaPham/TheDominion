using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    public class UpperBodyRig : MyMonoBehaviour
    {
        [SerializeField] private MultiAimConstraint multiaimContrain;

        public override void LoadComponent()
        {
            base.LoadComponent();
            multiaimContrain = GetComponentInChildren<MultiAimConstraint>();
        }

        public void SetWeightSourceObject(int index, float newWeight, float smoothTime, Action onComplete = null)
        {
            StartCoroutine(SetWeightSourceObjectCoroutine(index, newWeight, smoothTime, onComplete));
        }
        private IEnumerator SetWeightSourceObjectCoroutine(int index, float newWeight, float smoothTime, Action onComplete)
        {
            var sourceObjects = multiaimContrain.data.sourceObjects;
            while (sourceObjects.GetWeight(index) != newWeight)
            {
                sourceObjects.SetWeight(index, Mathf.MoveTowards(sourceObjects.GetWeight(index), newWeight, Time.deltaTime / smoothTime));
                multiaimContrain.data.sourceObjects = sourceObjects;
                yield return null;
            }
            onComplete?.Invoke();
        }
    }
}

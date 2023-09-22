using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    public class UpperBodyRig : MyMonoBehaviour
    {
        [SerializeField] private MultiAimConstraint[] multiaimContrains;
        [SerializeField] private MultiAimConstraint multiaimContrain;

        public override void LoadComponent()
        {
            base.LoadComponent();
            multiaimContrains = GetComponentsInChildren<MultiAimConstraint>();
            multiaimContrain = GetComponentInChildren<MultiAimConstraint>();
        }

        public void SetWeightSourceObject(int index, float newWeight, float smoothTime, Action onComplete = null)
        {
            for (int i = 0; i < multiaimContrains.Length; i++)
            {
                var sourceObjects = multiaimContrains[i].data.sourceObjects;
                StartCoroutine(SetWeightSourceObjectCoroutine(index, newWeight, smoothTime, sourceObjects, onComplete));
            }
        }
        private IEnumerator SetWeightSourceObjectCoroutine(int index, float newWeight, float smoothTime, WeightedTransformArray sourceObjects, Action onComplete)
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightScaleManager : MonoBehaviour
{
    public static WeightScaleManager Instance;

    [HideInInspector] public List<WeightScale> scales = new List<WeightScale>();

    public void UpdateWeights()
    {
        foreach (WeightScale scale in scales)
        {
            scale.shouldCalculateWeight = true;
        }
    }

    void Awake()
    {
        Instance = this;
    }
}

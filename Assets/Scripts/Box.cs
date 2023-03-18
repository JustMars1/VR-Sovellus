using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IWeight
{
    [SerializeField] int weight = 1;

    HashSet<IWeight> weights = new HashSet<IWeight>();

    public bool InsideScaleArea => scaleAreas > 0;

    int scaleAreas = 0;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            weights.Add(other.gameObject.GetComponent<IWeight>());
            WeightScaleManager.Instance.UpdateWeights();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            weights.Remove(other.gameObject.GetComponent<IWeight>());
            WeightScaleManager.Instance.UpdateWeights();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScaleArea"))
        {
            scaleAreas++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ScaleArea"))
        {
            scaleAreas--;
        }
    }

    public int TotalWeight(HashSet<IWeight> weighted)
    {
        int totalWeight = weight;
        foreach (IWeight weight in weights)
        {
            if (!weighted.Contains(weight))
            {
                weighted.Add(weight);

                if (scaleAreas > 0)
                {
                    totalWeight += weight.TotalWeight(weighted);
                }
            }
        }
        return totalWeight;
    }
}

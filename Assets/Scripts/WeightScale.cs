using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class WeightScale : MonoBehaviour
{
    [SerializeField] int targetWeight = 10;
    [SerializeField] TMP_Text counterText;

    public UnityEvent onTargetReached;

    int _currentWeight;

    HashSet<IWeight> weights = new HashSet<IWeight>();

    bool targetReached = false;

    int CurrentWeight
    {
        get => _currentWeight;
        set
        {
            _currentWeight = value;
            counterText.text = _currentWeight + "/" + targetWeight;

            float maxDepth = rend.bounds.size.y - 0.01f;

            if (_currentWeight >= targetWeight)
            {
                if (!targetReached)
                {
                    onTargetReached.Invoke();
                }

                rend.material.color = Color.green;
                targetY = startY - maxDepth;
                targetReached = true;
            }
            else
            {
                targetReached = false;
                rend.material.color = Color.red;
                targetY = startY - (maxDepth / targetWeight) * _currentWeight;
            }
        }
    }

    Renderer rend;
    Rigidbody rb;

    float startY;
    float targetY;

    [HideInInspector] public bool shouldCalculateWeight;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        startY = transform.position.y;
        CurrentWeight = 0;
    }

    void Start()
    {
        WeightScaleManager.Instance.scales.Add(this);
    }

    void FixedUpdate()
    {
        if (shouldCalculateWeight)
        {
            shouldCalculateWeight = false;
            CalculateWeight();
        }

        Vector3 pos = rb.position;
        pos.y = Mathf.MoveTowards(pos.y, targetY, Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }

    public void CalculateWeight()
    {
        HashSet<IWeight> weighted = new HashSet<IWeight>();
        int totalWeight = 0;
        foreach (IWeight weight in weights)
        {
            if (!weighted.Contains(weight))
            {
                weighted.Add(weight);

                if (weight.InsideScaleArea)
                    totalWeight += weight.TotalWeight(weighted);
            }
        }

        CurrentWeight = totalWeight;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            weights.Add(other.gameObject.GetComponent<IWeight>());
            CalculateWeight();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            weights.Remove(other.gameObject.GetComponent<IWeight>());
            CalculateWeight();
        }
    }
}

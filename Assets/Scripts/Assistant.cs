using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    private Animator animator;

    private const string IS_SERVING_STRING = "IsServing";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (QueueManager.Instance.isServing)
            animator.SetBool(IS_SERVING_STRING, true);
        else
            animator.SetBool(IS_SERVING_STRING, false);
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    private Camera mainCamera;
    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Target target))
        {
            return;
        }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Target target))
        {
            return;
        }
        RemoveTarget(target);
    }
    public bool SelectTartget()
    {
        if (targets.Count == 0)
            return false;
        Target closestTarget = null;
        float closestTargetDistace = Mathf.Infinity;
        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);//near to camera center vector2
            if (viewPos.x<0||viewPos.x>1||viewPos.y<0||viewPos.y>1)
            {
                continue;//if target is not in screen, continuel
            }
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude<closestTargetDistace)
            {
                closestTarget = target;
                closestTargetDistace=toCenter.sqrMagnitude;//새로운 가장 가까운거, 다시 갱신
            }
        }
        if (closestTarget == null) return false;
        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }
    public void Cancel()
    {
        if (CurrentTarget == null) return;
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }
    private void RemoveTarget(Target target)
    {
        if(CurrentTarget==target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}

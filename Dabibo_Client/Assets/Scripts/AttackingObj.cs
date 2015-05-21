using UnityEngine;
using System.Collections;

public abstract class AttackingObj : MonoBehaviour
{
	public LayerMask blockingLayer;

	// Use this for initialization
	protected virtual void Start ()
	{

	}

	protected bool Attack()
	{
		return false;
	}	

	
	protected virtual void AttemptAttack<T>(Transform target)
		where T : Component
	{
		bool canAttack = Attack ();
				
		T hitComponent = target.transform.GetComponent<T>();
		
		if(!canAttack && hitComponent != null)
			OnCantAttack(hitComponent);
	}
	
	protected abstract void OnCantAttack<T>(T compoment)
		where T : Component;
}

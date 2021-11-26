using System;
using System.Collections;
using System.Collections.Generic;
using CommanTickManager;
using UnityEngine;

namespace CollidedObjectSelector
{
    public class CollisionSelectionManager : MonoBehaviour
    {
        public static CollisionSelectionManager Instance { get; private set; }
        public List<BaseCollisionSelectable> baseCollisionSelectables;
        private BaseCollisionSelectable updatableCollsionSelectable;

        private void Awake()
        {
            Instance = this;
        }

        public void AddCollisionSelectable(BaseCollisionSelectable baseCollisionSelectable)
        {
            baseCollisionSelectables.Add(baseCollisionSelectable);
        }
        
        public void RemoveCollisionSelectable(BaseCollisionSelectable baseCollisionSelectable)
        {
            for (int indexOfSelectable = 0; indexOfSelectable < baseCollisionSelectables.Count; indexOfSelectable++)
            {
                if (baseCollisionSelectables[indexOfSelectable] == baseCollisionSelectable)
                {
                   baseCollisionSelectables.RemoveAt(indexOfSelectable);
                }
            }
        }
        
        public void OnObjectCollisionEnter(Collider collider,SelectableTag selectableTag ,Vector3 collidedPosition, params Action[] onObjectCollided)
        {
            for (int indexOfCollider = 0; indexOfCollider < baseCollisionSelectables.Count; indexOfCollider++)
            {
                if (baseCollisionSelectables[indexOfCollider].collider == collider && baseCollisionSelectables[indexOfCollider].selectableTag == selectableTag)
                {
                    baseCollisionSelectables[indexOfCollider].OnEnterCollision(collider,collidedPosition);
                    updatableCollsionSelectable = baseCollisionSelectables[indexOfCollider];
                    for (int indexOfAction = 0; indexOfAction < onObjectCollided.Length; indexOfAction++)
                    {
                        onObjectCollided[indexOfAction]?.Invoke();
                    }
                }
            }
        }
        
        public void OnObjectCollisionExit(Collider collider,SelectableTag selectableTag, Vector3 collidedPosition,params Action[] onObjectCollided)
        {
            for (int indexOfCollider = 0; indexOfCollider < baseCollisionSelectables.Count; indexOfCollider++)
            {
                if (baseCollisionSelectables[indexOfCollider].collider == collider && baseCollisionSelectables[indexOfCollider].selectableTag == selectableTag)
                {
                    baseCollisionSelectables[indexOfCollider].OnExitCollision(collider,collidedPosition);
                    updatableCollsionSelectable = null;
                    for (int indexOfAction = 0; indexOfAction < onObjectCollided.Length; indexOfAction++)
                    {
                        onObjectCollided[indexOfAction]?.Invoke();
                    }
                }
            }
        }

        public void OnObjectCollisionUpdate(Collider collider,Vector3 collidedPosition)
        {
            if (updatableCollsionSelectable != null)
            {
                updatableCollsionSelectable.OnStayInCollision(collider,collidedPosition);
            }
        }
    }

    public enum SelectableTag
    {
        leafTag,
        CoconutTag,
        RougeLeafTag
    }
}
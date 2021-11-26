using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollidedObjectSelector
{
    public class BaseCollisionSelectable : MonoBehaviour
    {
        public Collider collider;
        public bool isCollisionObject;
        public SelectableTag selectableTag;
        public virtual void OnEnterCollision(Collider collider,Vector3 collidedPosition)
        {
            isCollisionObject = true;
        }

        public virtual void OnStayInCollision(Collider collider,Vector3 collidedPosition)
        {
            
        }
        
        public virtual void OnExitCollision(Collider collider,Vector3 collidedPosition)
        {
            isCollisionObject = false;
        }
    }
}

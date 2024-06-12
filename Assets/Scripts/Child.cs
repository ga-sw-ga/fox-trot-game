using System;
using UnityEngine;

public class Child : MonoBehaviour
{
    private const string DEFAULT_PARENT_TAG = "Player";
    
    [SerializeField] private Transform parent;
    [SerializeField] private bool enabledFromStart = true;
    [SerializeField] private bool keepInitialDistance;
    [SerializeField] private bool freezeXPosition, freezeYPosition, freezeZPosition, freezeRotation;
    private Vector3 pos, fw, up;
    private bool firstInitialized, isEnabled;

    public Transform Parent
    {
        get => parent;

        set
        {
            parent = value;
            firstInitialized = false;
        }
    }
    public bool IsInitialized => parent != null;

    private void Awake()
    {
        isEnabled = enabledFromStart;
    }

    void LateUpdate()
    {
        if (IsInitialized && isEnabled)
        {
            if (firstInitialized)
            {
                var newpos = parent.transform.TransformPoint(pos);
                var newfw = parent.transform.TransformDirection(fw);
                var newup = parent.transform.TransformDirection(up);
                Quaternion newrot = Quaternion.identity;
                if (newfw != Vector3.zero && newup != Vector3.zero)
                {
                    newrot = Quaternion.LookRotation(newfw, newup);
                }

                Transform t = transform;
                Vector3 position = t.position;
                Vector3 eulerAngles = t.eulerAngles;
            
                // calculating position
                position += new Vector3(freezeXPosition ? 0f : newpos.x - position.x,
                    freezeYPosition ? 0f : newpos.y - position.y,
                    freezeZPosition ? 0f : newpos.z - position.z);
                t.position = position;
            
                // calculating rotation
                if (!freezeRotation)
                {
                    eulerAngles += new Vector3(newrot.eulerAngles.x - eulerAngles.x,
                        newrot.eulerAngles.y - eulerAngles.y,
                        newrot.eulerAngles.z - eulerAngles.z);
                }
                t.eulerAngles = eulerAngles;
            }
            // else if (!firstInitialized && GameManager.instance.State != GameManager.FrameworkState.Loading)
            // {
            //     if (parent != null)
            //     {
            //         if (!keepInitialDistance)
            //         {
            //             pos = parent.transform.InverseTransformPoint(transform.position);
            //             fw = parent.transform.InverseTransformDirection(transform.forward);
            //             up = parent.transform.InverseTransformDirection(transform.up);
            //         }
            //     
            //         firstInitialized = true;
            //     }
            // }
        }
        else if (!IsInitialized && isEnabled)
        {
            GameObject tempParent = GameObject.FindWithTag(DEFAULT_PARENT_TAG);
            if (tempParent != null)
            {
                Parent = tempParent.transform;
            }
        }
    }

    public void Disable()
    {
        isEnabled = false;
        if (!keepInitialDistance)
        {
            firstInitialized = false;
        }
    }
    
    public void Enable()
    {
        isEnabled = true;
    }
}

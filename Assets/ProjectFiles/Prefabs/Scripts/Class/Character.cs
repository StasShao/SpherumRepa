using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMechanicSystem;
public abstract class Character : MonoBehaviour, IControllable
{
    #region IControllable
    public float ForwardDirection { get; private set; }
    public float SideDirection { get; private set; }
    public virtual void SetDirection(float forwardDir, float sideDir)
    {
        ForwardDirection = forwardDir;
        SideDirection = sideDir;
    }
    #endregion
    protected abstract Rigidbody CharacterRigidbody();
    protected abstract Controller m_CahracterController();
    protected abstract Inputer cs_CharacterInputer();
}

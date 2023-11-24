using GameMechanicSystem;
using UnityEngine;

public class GreenCubeCharacter : Character
{
    public Rigidbody CharacterRb;
    [SerializeField][Range(0.0f,2000.0f)] private float MoveForce;
    protected override Rigidbody CharacterRigidbody()
    {
        return CharacterRb;
    }
    protected override Controller m_CahracterController()
    {
       
        return new Controller(CharacterRigidbody());
    }
    protected override Inputer cs_CharacterInputer()
    {
        return new Inputer(this);
    }
    private void OnCharacterMove()
    {
        m_CahracterController().RigidbodyMove(ForwardDirection,SideDirection,MoveForce);
    }
    public virtual void OnInput()
    {
        cs_CharacterInputer().SetButtonInputArrowDirection();
    }
    public void CharacterBehavior()
    {
        OnCharacterMove();
        OnInput();
    }
    public override void SetDirection(float forwardDir, float sideDir)
    {
        base.SetDirection(forwardDir, sideDir);
    }

}

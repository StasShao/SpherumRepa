using UnityEngine;
using GameMechanicSystem;
using UnityEngine.UI;
public class GameManager : MonoBehaviour,IData
{
    public int NextScene;
    public GreenCubeCharacter greenCharacter;
    public RedCubeCharacter redCharacter;
    public SphereSpawner SphereSpawner;
    public Text RandeDebuger;
    [SerializeField] [Range(0, 1)] private int SceneSelectMinRange;
    [SerializeField] [Range(0, 2)] private int MinDistance;
    public bool IsMinDistance;

    #region IData
    public float Data { get; private set; }
    public void SetData(float data)
    {
        Data = data;
    }
    #endregion

    private void Awake()
    {
        SphereSpawner.CreatePool();
    }
    private void Start()
    {
        SphereSpawner.CallMultipleInstance();

    }
    void Update()
    {
        TextRederer.SetDataParametres(this, Interactor.DistanceInteract<GameManager>(this, "DesableSpheres", "EnableSpheres", greenCharacter.CharacterRb.transform, redCharacter.CharacterRb.transform, MinDistance, IsMinDistance));
        TextRederer.ShowTextResult(this,RandeDebuger);
        SceneSelector.SelectCondition(NextScene,SceneSelectMinRange,this,true);
    }
    private void FixedUpdate()
    {
        greenCharacter.CharacterBehavior();
        redCharacter.CharacterBehavior();
    }
    public void EnableSpheres()
    {
        SphereSpawner.CallMultipleDesableElements();
    }
    public void DesableSpheres()
    {
        SphereSpawner.CallMultipleInstance();
    }

    
}

using GameMechanicSystem.PoolSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SphereSpawner : ObjectPooler<SphereProjector>
{
    [SerializeField] private SphereProjector Prefab;
    [SerializeField] private List<Transform> ListPositions = new List<Transform>();
    [SerializeField] private List<Material> ListMaterials = new List<Material>();
    [SerializeField] private int PoolCount;
    [SerializeField] private Transform Container;
    [SerializeField] private bool IsAutoExpand;
    private PoolMono<SphereProjector> PoolMono;
    protected override SphereProjector m_var()
    {
        return Prefab;
    }
    protected override PoolMono<SphereProjector> Pooler(SphereProjector prefab, int poolCount, Transform container, bool isAutoExpand)
    {
        return new PoolMono<SphereProjector>(m_var(),PoolCount,Container,IsAutoExpand);
    }
    public virtual SphereProjector GetPoolElement()
    {
        return PoolMono.GetFreeElement(Container);
    }
    public virtual void SetListPositions()
    {
        PoolMono.SetElementsPositionsToList(ListPositions);
    }
    public virtual void CreatePool()
    {
       PoolMono = Pooler(Prefab, PoolCount, Container, IsAutoExpand);
    }
    public virtual void SetListMaterials()
    {
        PoolMono.SetElementsMaterialsToList(ListMaterials);
    }
    public virtual void CallMultipleInstance()
    {
        PoolMono.CallMultiplePoolElements(Container,ListPositions,ListMaterials);
    }
    public virtual void CallMultipleDesableElements()
    {
        PoolMono.CallMultipleDesableElements();
    }
}

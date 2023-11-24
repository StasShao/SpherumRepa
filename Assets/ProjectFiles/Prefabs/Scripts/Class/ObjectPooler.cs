using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMechanicSystem.PoolSystems;
public abstract class ObjectPooler<T>:MonoBehaviour where T: MonoBehaviour
{
    protected abstract T m_var();
  protected abstract PoolMono<T> Pooler(T prefab,int poolCount,Transform container,bool isAutoExpand);
}

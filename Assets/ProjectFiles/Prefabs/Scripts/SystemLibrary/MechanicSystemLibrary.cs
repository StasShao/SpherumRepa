using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameMechanicSystem
{
    using UnityEngine.UI;
    public class Controller
    {
        private Rigidbody _rb;
        public Controller(Rigidbody rb)
        {
            _rb = rb;
        }
        public void RigidbodyMove(float forwardDir,float sideDir,float moveForce)
        {
            _rb.AddForce(_rb.transform.forward * forwardDir * moveForce,ForceMode.Force);
            _rb.AddForce(_rb.transform.right * sideDir * moveForce, ForceMode.Force);
        }
    }
    public class Interactor
    {
        public static float DistanceInteract<T>(Object objectCallMethod,string firstMethod, string secondMethod,Transform targetA,Transform targetB,float distance,bool IsMaxDistane)where T:Object
        {
            var distanceBetween = Vector3.Distance(targetA.position,targetB.position);
            if(IsMaxDistane&distanceBetween < distance)
            {
                MethodCall<T>(objectCallMethod, firstMethod);
            }
            if(IsMaxDistane&distanceBetween >distance)
            {
                MethodCall<T>(objectCallMethod, secondMethod);
            }

            return distanceBetween;
        }
        public static void MethodCall<T>(Object objectCallMethod, string methodName) where T:Object
        {
            var t = (T)objectCallMethod;
            var methodInfo = t.GetType().GetMethod(methodName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(objectCallMethod, null);
            }
        }
    }
    public class Inputer
    {
        private float _v;
        private float _h;
        private IControllable _icontrollable;
        public Inputer(IControllable icontrollable)
        {
            _icontrollable = icontrollable;
        }
        public  void SetButtonInputArrowDirection()
        {
            
            if(Input.GetKey(KeyCode.UpArrow))
            {
                _v = 1;
            }
            if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                _v = 0;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _v = -1;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                _v = 0;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _h = 1;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _h = 0;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _h = -1;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _h = 0;
            }
            _icontrollable.SetDirection(_v, _h);
           
        }
        public  void SetWASDDirection()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _v = 1;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                _v = 0;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _v = -1;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                _v = 0;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _h = 1;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _h = 0;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _h = -1;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                _h = 0;
            }
            _icontrollable.SetDirection(_v, _h);
        }
    }
    public class TextRederer
    {
        public static void SetDataParametres(IData idata,float data)
        {
            idata.SetData(data);
        }
        public static void ShowTextResult(IData idata,Text text)
        {
            text.text = idata.Data.ToString();
        }
    }
    public class SceneSelector
    {
        public static void SelectCondition(int loadingScene,float floatCondition,IData idata,bool isLess)
        {
            if(isLess&idata.Data <floatCondition)
            {
                SceneManager.LoadScene(loadingScene);
            }
        }
        public static void SelectCondition(int loadingScene, bool boolCondition)
        {
            if (boolCondition)
            {
                SceneManager.LoadScene(loadingScene);
            }
        }

    }
    
    namespace PoolSystems
    {
        using System;

        /// <summary>
        /// Клас generic реализует пул объектов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class PoolMono<T> where T : MonoBehaviour
        {
            #region Variables
            public T prefab { get; }
            public bool IsAutoExpand { get; set; }
            public Transform container { get; }
            public List<T> pool { get; private set; }
            public List<MeshRenderer> renderList { get; private set; }
            #endregion

            #region CTOR
            public PoolMono(T prefab, int count, Transform container, bool isAutoExpand)
            {
                this.prefab = prefab;
                this.container = container;
                this.IsAutoExpand = isAutoExpand;
                this.CreatePool(count);
            }
            #endregion

            #region Voides
            private void CreatePool(int count)
            {
                this.pool = new List<T>();
                this.renderList = new List<MeshRenderer>();
                for (int i = 0; i < count; i++)
                {
                    this.CreateObject();
                }
                for (int i = 0; i < pool.Count; i++)
                {
                    var meshR = pool[i].GetComponentInChildren<MeshRenderer>();
                    if(meshR!=null)
                    {
                        renderList.Add(meshR);
                    }
                }
            }
            private T CreateObject(bool isActiveByDefoult = false)
            {
                var createdObject = PoolCreator<T>.Instance(prefab, container);
                createdObject.gameObject.SetActive(isActiveByDefoult);
                this.pool.Add(createdObject);
                return createdObject;

            }
            public bool HasFreeElement(out T element)
            {
                foreach (var mono in pool)
                {
                    if (!mono.gameObject.activeInHierarchy)
                    {
                        element = mono;
                        mono.gameObject.SetActive(true);
                        return true;
                    }
                }
                element = null;
                return false;
            }
            public T GetFreeElement(Transform pos)
            {
                if (this.HasFreeElement(out var element))
                {
                    element.transform.position = pos.position;
                    element.transform.rotation = pos.rotation;
                    return element;
                }
                if (this.IsAutoExpand)
                {
                    return this.CreateObject(true);
                }
                return null;


            }
            public void SetElementsPositionsToList(List<Transform> listPosition)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    for (int j = 0; j < listPosition.Count; j++)
                    {
                        pool[i].transform.position = listPosition[i].position;
                        pool[i].transform.rotation = listPosition[i].rotation;
                    }
                }
               
            }
            public void SetElementsMaterialsToList(List<Material> listMaterials)
            {
                for (int i = 0; i < renderList.Count; i++)
                {
                    var element = renderList[i];
                    for (int j = 0; j < listMaterials.Count; j++)
                    {
                        var rendererElement = listMaterials[i];
                        element.material = rendererElement;

                    }
                }
            }
            public void CallMultiplePoolElements(Transform pos, List<Transform> listPosition, List<Material> listMaterials)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    GetFreeElement(pos);
                    SetElementsMaterialsToList(listMaterials);
                    SetElementsPositionsToList(listPosition);
                }
            }
            public void CallMultipleDesableElements()
            {
                foreach (var item in pool)
                {
                    item.gameObject.SetActive(false);
                }
            }
            #endregion
        }
        public class PoolCreator<T> : MonoBehaviour where T : MonoBehaviour
        {
            public static T Instance(T prefab, Transform container)
            {
                var createdObject = Instantiate(prefab, container.position, container.rotation, container);
                return createdObject;
            }
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Reuse.Patterns;
using UnityEngine;

namespace Reuse.Pooling
{
    public class Spawner : Singleton<Spawner>
    {
        [SerializeField] private List<GameObject> initialPool;
        private readonly Dictionary<string, Queue<GameObject>> _pool = new();
            
        [Serializable]
        public class PooledObjects
        {
            public int amount;
            public GameObject gameObject;
        }
    
        protected override void Awake()
        {
            base.Awake();
            SendToPool(initialPool);
        }
    
        public static void SendToPool(List<PooledObjects> toPool)
        { 
            foreach (var pooled in toPool)
            {
                var pool = GetPool(pooled.gameObject);
                for (int i = 0; i < pooled.amount; i++)
                {
                    var go = Instantiate(pooled.gameObject, Instance.transform);
                    go.SetActive(false);
                        
                    pool.Enqueue(go);
                }
            }
        }
    
        public static void SendToPool(List<GameObject> alreadyInstantiated)
        {
            foreach (var go in alreadyInstantiated)
            {
                go.SetActive(false);
                GetPool(go.gameObject).Enqueue(go);
            }
        }
    
        public static GameObject Spawn(GameObject gameObject)
        {
            var pooled = GetPool(gameObject);
            GameObject res;
                
            if (pooled.Count == 0)
            {
                res = Instantiate(gameObject);
            }
            else
            {
                res = pooled.Dequeue();
                res.SetActive(true);
            }
                
            res.name = gameObject.name;
            return res;
        }
    
        public static GameObject Spawn(GameObject gameObject, Transform parent)
        {
            var go = Spawn(gameObject);
            go.transform.SetParent(parent);
    
            return go;
        }
    
        public static GameObject Spawn(GameObject gameObject, Vector3 position)
        {
            var go = Spawn(gameObject);
            go.transform.position = position;
    
            return go;
        }
            
        public static GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            var go = Spawn(gameObject, position);
            go.transform.rotation = rotation;
    
            return go;
        }
    
        public static GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
        {
            var go = Spawn(gameObject, position, rotation);
            go.transform.SetParent(parent);
    
            return go;
        }
    
        public static void DeSpawn(GameObject gameObject)
        {
            var pooled = GetPool(gameObject);
                
            gameObject.SetActive(false);
            gameObject.transform.SetParent(Instance.transform);
                
            pooled.Enqueue(gameObject);
        }
            
        public static void DeSpawn(GameObject gameObject, float duration)
        { 
            Instance.StartCoroutine(WaitAndDeSpawn(gameObject, duration));
        }
    
        private static IEnumerator WaitAndDeSpawn(GameObject gameObject, float duration)
        {
            yield return new WaitForSeconds(duration);
            DeSpawn(gameObject);
        }
    
        private static Queue<GameObject> GetPool(GameObject gameObject)
        {
            var key = gameObject.name;
    
            if (!Instance._pool.ContainsKey(key)) Instance._pool.Add(key, new Queue<GameObject>());
    
            return Instance._pool[key];
        }
    }
}
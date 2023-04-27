using System;
using System.Collections;
using Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace ActorMono
{
    public class EnemyCreator : MonoBehaviour
    {
        [SerializeField] private float _interval = 1f;
        [SerializeField] private EnemyPoolSO _pool = default;

        private void OnEnable()
        {
            StartCoroutine(CreateObj(_interval));
        }

        public IEnumerator CreateObj(float interval)
        {
            var previousTime = Time.time;
            var waitTime = new WaitForSeconds(0.02f);
            while(true )
            {
                if(Time.time > previousTime + interval)
                {
                    var enemy = _pool.Request();
                    enemy.transform.position = transform.position;
                    enemy.transform.rotation = transform.rotation;
                    previousTime = Time.time;
                }
                else
                {
                    yield return waitTime;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdateAgent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateAgent()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            Vector3 targetPostion = new Vector3(target.position.x, 0, target.position.z);
            agent.SetDestination(targetPostion);
            yield return new WaitForSeconds(refreshRate);
        }
        StartCoroutine(UpdateAgent());
    }
}

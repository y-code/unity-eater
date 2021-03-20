using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Agent AgentPrefab;

    void Start()
    {
        if (AgentPrefab != null)
		{
            var agent = Instantiate(AgentPrefab, transform.position, transform.rotation);
            agent.Goal = transform;
        }
    }
}

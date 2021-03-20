using UnityEngine;
using ycode.Eater;

public class Lawn : MonoBehaviour
{
    private Food _food;
    void Start()
    {
        _food = GetComponent<Food>();
    }

    void Update()
    {
        if (_food != null)
        {
            if (_food.IsEmpty)
                Destroy(gameObject);
        }
    }
}

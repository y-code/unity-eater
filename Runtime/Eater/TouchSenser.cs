using UnityEngine;

namespace ycode.Eater
{
    public class TouchSenser : MonoBehaviour
    {
        private Eater _eater;

        private void Start()
        {
            _eater = GetComponentInParent<Eater>();
        }

        private void _onTriggerEnter(Food food)
        {
            if (food != null)
                _eater?.Starteating(food);
        }

        private void OnTriggerEnter(Collider other)
        {
            var food = other.gameObject.GetComponent<Food>();
            _onTriggerEnter(food);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var food = other.gameObject.GetComponent<Food>();
            _onTriggerEnter(food);
        }

        private void _onTriggerExit(Food food)
        {
            if (food != null)
                _eater?.ReleaseFood(food);
        }

        private void OnTriggerExit(Collider other)
        {
            var food = other.gameObject.GetComponent<Food>();
            _onTriggerExit(food);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var food = other.gameObject.GetComponent<Food>();
            _onTriggerExit(food);
        }
    }
}

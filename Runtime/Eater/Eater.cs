using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ycode.Eater
{
    public class Eater : MonoBehaviour
    {
        public bool logging = false;
        public int StomachFull = 20;
        public int CachingDurationInFrameCounts = 100;

        public AbstractITextDisplay StomachDisplay;

        public EaterState State { get; private set; }

        private bool _active;

        public bool IsFull => State.IsFull;
        public bool HasEnergy => State.HasEnergy;

        private readonly List<Food> _targetingFoods = new List<Food>();
        private int _targetingFoodCacheUntil = 0;
        private Food _targetingFood;
        public Food TargetingFood
        {
            get
            {
                if (Time.frameCount > _targetingFoodCacheUntil)
                {
                    _targetingFood = _targetingFoods.OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault();
                    _targetingFoodCacheUntil = Time.frameCount + CachingDurationInFrameCounts;
                }
                return _targetingFood;
            }
        }

        private readonly List<Food> _eatingFoods = new List<Food>();
        private int _eatingFoodCacheUntil = 0;
        private Food _eatingFood;
        public Food eatingFood
        {
            get
            {
                if (Time.frameCount > _eatingFoodCacheUntil)
                {
                    _eatingFood = _eatingFoods.OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault();
                    _eatingFoodCacheUntil = Time.frameCount + CachingDurationInFrameCounts;
                }
                return _eatingFood;
            }
        }

        void Start()
        {
            State = new EaterState(StomachFull);
            _active = true;
            StartCoroutine(Eat());
        }

        private void OnDestroy()
        {
            _active = false;
            _targetingFoods.Clear();
            _eatingFoods.Clear();
        }

        void Update()
        {
            if (StomachDisplay != null)
                StomachDisplay.Text = State.Stomach.ToString();
        }

        public void TargetFood(Food food)
        {
            if (food == null)
                return;

            if (!_targetingFoods.Contains(food))
                _targetingFoods.Add(food);

            if (logging) Debug.Log($"[{gameObject.name}] Next destination is food");

            food.OnFoodEmpty += Food_OnFoodEmpty;
        }

        public void ForgetFood(Food food)
        {
            if (_targetingFoods.Contains(food))
                _targetingFoods.Remove(food);
        }

        public void Starteating(Food food)
        {
            if (food == null)
                return;

            _eatingFoods.Add(food);
        }

        public void ReleaseFood(Food food)
        {
            if (_eatingFoods.Contains(food))
                _eatingFoods.Remove(food);
        }

        private void Food_OnFoodEmpty(Food food)
        {
            food.OnFoodEmpty -= Food_OnFoodEmpty;
            ReleaseFood(food);
            ForgetFood(food);
        }

        private IEnumerator Eat()
        {
            yield return new WaitForFixedUpdate(); // wait for all the game objects to finish `Start()` method call

            while (_active)
            {
                Food food;
                if (!IsFull && (food = eatingFood) != null && food.State.TryConsuming(out var calorie))
                {
                    State.Eat(calorie);

                    if (logging) Debug.Log($"[{gameObject.name}] An Agent ate {calorie} calories of a food.");
                }

                yield return new WaitForSeconds(1f * Random.value);
            }

            yield return null;
        }
    }
}

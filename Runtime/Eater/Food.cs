using UnityEngine;

namespace ycode.Eater
{
    public class Food : MonoBehaviour
    {
        public int CalorieByServing;
        public int Servings;

        public AbstractITextDisplay CalorieDisplay;

        public FoodState State { get; private set; }

        public bool IsEmpty => State.IsEmpty;

        public delegate void OnFoodEmptyHandler(Food sender);
        public event OnFoodEmptyHandler OnFoodEmpty;

        void Start()
        {
            State = new FoodState(CalorieByServing, Servings);
        }

        void Update()
        {
            if (CalorieDisplay != null)
                CalorieDisplay.Text = State.Calorie.ToString();

            if (IsEmpty)
                OnFoodEmpty?.Invoke(this);
        }
    }
}

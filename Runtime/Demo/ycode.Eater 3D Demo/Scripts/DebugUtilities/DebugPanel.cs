using System;
using System.Linq;
using UnityEngine;

namespace ycode.DebugUtilities
{
	public class DebugPanel : MonoBehaviour
	{
		public DebugItem ItemPrefab;

		public void Add(Func<string> getLabel, Func<string> getValue)
		{
			var item = Instantiate(ItemPrefab, this.transform);
			item.GetLabel = getLabel;
			item.GetValue = getValue;
		}
	}
}

using System.Linq;
using UnityEngine;
using ycode.DebugUtilities;
using ycode.Eater;

public class SampleSceneManager : MonoBehaviour
{
	private void Start()
	{
		var debugPanel = FindObjectOfType<DebugPanel>();
		debugPanel.Add(() => "Total",
			() => FindObjectsOfType<Eater>().Select(x => x.State.Stomach).Sum().ToString());
	}
}

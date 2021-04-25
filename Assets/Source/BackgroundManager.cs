using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
	private GameObject fog;

	private Dictionary<int, List<GameObject>> backgrounds = new Dictionary<int, List<GameObject>>();

    // Start is called before the first frame update
    void Awake()
    {
		fog = transform.Find("Fog").gameObject;

		PopulateBackground(1);
		PopulateBackground(2);
	}

	public void ToggleFog(bool state)
	{
		Debug.Log("Toggle fog " + state);
		if (state)
		{
			fog.GetComponent<Parallax>().StartFog();
		}
		else
		{
			fog.GetComponent<Parallax>().StopFog();
		}
	}

	private void PopulateBackground(int i) {

		var backgrnds = new List<GameObject>();
		backgrnds.Add(transform.Find("Background_0" + i.ToString()).gameObject);
		backgrnds.Add(transform.Find("Midground_0" + i.ToString()).gameObject);
		backgrnds.Add(transform.Find("Foreground_0" + i.ToString()).gameObject);
		backgrounds.Add(i, backgrnds);

	}
}

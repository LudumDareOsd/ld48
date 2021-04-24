using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
	private GameObject fog;

    // Start is called before the first frame update
    void Awake()
    {
		fog = transform.Find("Fog").gameObject;
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
}

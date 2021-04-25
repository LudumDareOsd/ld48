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
		PopulateBackground(3);
		PopulateBackground(4);
		PopulateBackground(5);
		PopulateBackground(6);

	}

	public void ToggleFog(bool state)
	{
		if (state)
		{
			fog.GetComponent<Parallax>().StartFog();
		}
		else
		{
			fog.GetComponent<Parallax>().StopFog();
		}
	}

	public void LoadBackGround(int bgNr) {
		var bgs = new List<GameObject>();
		backgrounds.TryGetValue(bgNr, out bgs);

		for (var i = 0; i < bgs.Count; i++) {
			bgs[i].SetActive(true);
		}
	}

	public void UnLoadBackGround(int bgNr) {
		var bgs = new List<GameObject>();
		backgrounds.TryGetValue(bgNr, out bgs);

		for (var i = 0; i < bgs.Count; i++) {
			bgs[i].SetActive(false);
		}
	}

	private void PopulateBackground(int i) {
		var backgrnds = new List<GameObject>();

		var bg = transform.Find("Background_0" + i.ToString()).gameObject;
		var mg = transform.Find("Midground_0" + i.ToString()).gameObject;
		var fg = transform.Find("Foreground_0" + i.ToString()).gameObject;

		bg.SetActive(false);
		mg.SetActive(false);
		fg.SetActive(false);

		backgrnds.Add(bg);
		backgrnds.Add(mg);
		backgrnds.Add(fg);
		backgrounds.Add(i, backgrnds);
	}
}

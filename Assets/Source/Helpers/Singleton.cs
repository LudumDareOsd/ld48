using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T self = null;

	public static T Instance
	{
		get
		{
			if (self == null) self = FindObjectOfType<T>();
			return self;
		}
	}
}

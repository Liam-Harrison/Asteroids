using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
			}

			return instance;
		}

		set => instance = value;
	}

	protected virtual void Awake()
	{
		instance = this as T;
	}
}

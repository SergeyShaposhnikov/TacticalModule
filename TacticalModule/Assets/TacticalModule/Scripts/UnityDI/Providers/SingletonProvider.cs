using System;

namespace UnityDI.Providers
{
	public class SingletonProvider<T> : IObjectProvider<T> where T : class, new()
	{
		private T _instance;

		public T GetObject(Container container)
		{
			if (_instance == null)
			{
				_instance = Activator.CreateInstance<T>();
				container.BuildUp(_instance);
			}
			return _instance;
		}
	}
}

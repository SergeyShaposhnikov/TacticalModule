using System.Linq;
using UnityDI.Finders;
using UnityEngine;

namespace UnityDI.Providers
{
	public class ScenePathProvider<T> : IObjectProvider<T> where T : class
	{
		private readonly string _path;
		private T _cached;

		public ScenePathProvider(string path)
		{
			_path = path;
		}

		public T GetObject(Container container)
		{
			if (IsDestroyed(_cached))
			{
				_cached = FindObject();
			}
			return _cached;
		}

		private bool IsDestroyed(T obj)
		{
			if (obj == null)
				return true;

			if (typeof(T) == typeof(GameObject))
			{
				GameObject gameObj = (GameObject) (object)obj;
				return !gameObj;
			}

			if (typeof(Component).IsAssignableFrom(typeof(T)))
			{
				Component component = (Component) (object) obj;
				if (component)
					return !component.gameObject;

				return true;
			}

			return false;
		}

		private T FindObject()
		{
			var gameObject = new MaskFinder().Find(_path);
			if (gameObject == null)
				throw new ContainerException("Can't find game object \"" + _path + "\"");

			if (typeof (T) == typeof (GameObject))
				return (T) (object) gameObject;

			if (typeof (T) == typeof (Transform))
				return (T) (object) gameObject.transform;

			var components = gameObject.GetComponents<Component>();
			T component = components.OfType<T>().FirstOrDefault();
			if (component != null)
				return component;

			throw new ContainerException("Can't find component \"" + typeof (T).FullName + "\" of game object \"" + _path + "\"");
		}
	}
}

using UnityEngine;

namespace UnityDI.Providers
{
	public class ResourceProvider<TBase, TDerived> : IObjectProvider<TBase> where TBase : class
	                                                                        where TDerived : UnityEngine.Object, TBase
	{
		private bool _inited;
		private TDerived _asset;
		private TDerived _instance;
		private readonly string _path;

		public ResourceProvider(string path)
		{
			_path = path;
		}

		public TBase GetObject(Container container)
		{
			if (!_inited)
			{
				_asset = Resources.Load<TDerived>(_path);
				_inited = true;
				if (_asset != null)
				{
					_instance = (TDerived)Object.Instantiate(_asset);
					container.BuildUp(_instance);
				}
				else
					Debug.LogError("Can't load resource '" + _path + "'");
			}
			return _instance;
		}
	}
}

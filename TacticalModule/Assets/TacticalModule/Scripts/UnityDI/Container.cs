using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityDI.Providers;
using UnityEngine;

namespace UnityDI
{
	/// <summary>
	/// Класс DI-контейнера
	/// </summary>
	public class Container
	{
		private readonly Dictionary<ContainerKey, IProviderWrapper> _providers = new Dictionary<ContainerKey, IProviderWrapper>();

		public Container RegisterType<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new ActivatorObjectProvider<T>(), name);
		}

		public bool IsRegistered<T>(string name = null) where T : class, new()
		{
			return (ResolveProvider(typeof (T), name) != null);
		}

		public Container RegisterType<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new ActivatorObjectProvider<TDerived>(), name);
		}

		public Container RegisterSingleton<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new SingletonProvider<T>(), name);
		}

		public Container RegisterSingleton<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new SingletonProvider<TDerived>(), name);
		}

		public Container RegisterInstance<T>(T obj, string name = null) where T : class
		{
			return RegisterProvider(new InstanceProvider<T>(obj), name);
		}

		public Container RegisterInstance<TBase, TDerived>(TDerived obj, string name = null) where TDerived : class, TBase
		{
			return RegisterProvider<TBase, TDerived>(new InstanceProvider<TDerived>(obj), name);
		}

		public Container RegisterSceneObject<T>(string path, string name = null) where T : class
		{
			return RegisterProvider(new ScenePathProvider<T>(path), name);
		}

		/// <summary>
		/// Зарегестрировать объект находящийся в Resources
		/// </summary>
		public Container RegisterResource<T>(string path, string name = null) where T : UnityEngine.Object
		{
			return RegisterProvider(new ResourceProvider<T, T>(path), name);
		}

		public Container RegisterResource<TBase, TDerived>(string path, string name = null) where TBase : class where TDerived : UnityEngine.Object, TBase
		{
			return RegisterProvider(new ResourceProvider<TBase, TDerived>(path), name);
		}

		public Container RegisterProvider<T>(IObjectProvider<T> provider, string name = null) where T : class
		{
			var key = new ContainerKey(typeof (T), name);
			_providers[key] = new ProviderWrapper<T>(provider);
			return this;
		}

		public Container RegisterProvider<TBase, TDerived>(IObjectProvider<TDerived> provider, string name = null) where TDerived : class, TBase
		{
			var key = new ContainerKey(typeof (TBase), name);
			_providers[key] = new ProviderWrapper<TDerived>(provider);
			return this;
		}

		/// <summary>
		/// Получить объект нужного типа
		/// </summary>
		public T Resolve<T>(string name = null)
		{
			return (T) Resolve(typeof (T), name);
		}

		/// <summary>
		/// Получить объект нужного типа
		/// </summary>
		public object Resolve(Type type, string name = null)
		{
			IProviderWrapper provider = ResolveProvider(type, name);
			if (provider == null)
				throw new ContainerException("Can't resolve type " + type.FullName +
				                             (name == null ? "" : " registered with name \"" + name + "\""));

			return provider.GetObject(this);
		}

		/// <summary>
		/// Попытаться получить объект нужного типа. Если объекта нет, то возвращет null не кидая исключения.
		/// </summary>
		public T TryResolve<T>(string name = null)
		{
			return (T) TryResolve(typeof (T), name);
		}

		/// <summary>
		/// Попытаться получить объект нужного типа. Если объекта нет, то возвращет null не кидая исключения.
		/// </summary>
		public object TryResolve(Type type, string name = null)
		{
			IProviderWrapper provider = ResolveProvider(type, name);
			if (provider == null)
				return null;
			return provider.GetObject(this);
		}

		private IProviderWrapper ResolveProvider(Type type, string name)
		{
			IProviderWrapper provider = null;
			_providers.TryGetValue(new ContainerKey(type, name), out provider);
			return provider;
		}

		/// <summary>
		/// Заинжектить зависимости в уже существующий объект
		/// </summary>
		public void BuildUp(object obj)
		{
			BuildUpStack.PushObject(obj);
			try
			{
				Type type = obj.GetType();
#if NETFX_CORE
				IEnumerable<MemberInfo> members =
type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);
	#else
				MemberInfo[] members = type.FindMembers(MemberTypes.Property,
					BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null,
					null);
#endif

				foreach (MemberInfo member in members)
				{
					var attrs = member.GetCustomAttributes(typeof(DependencyAttribute), true);
					if (!attrs.Any())
						continue;
					var attr = attrs.FirstOrDefault();

					var attrib = (DependencyAttribute) attr;
					var propertyInfo = (PropertyInfo) member;
					object valueObj = null;

					BuildUpStack.SetPropertyName(propertyInfo.Name);

					try
					{
						valueObj = Resolve(propertyInfo.PropertyType, attrib.Name);
					}
					catch (ContainerException ex)
					{
						Debug.LogError(ex.Message + " for " + type.FullName + "." + propertyInfo.Name);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.Message + " while resolving " + type.FullName + "." + propertyInfo.Name + " " + ex.StackTrace);
					}

					propertyInfo.SetValue(obj, valueObj, null);
				}

				var dependent = obj as IDependent;
				if (dependent != null)
					dependent.OnInjected();
			}
			finally
			{
				BuildUpStack.Pop();
			}
		}
	}
}

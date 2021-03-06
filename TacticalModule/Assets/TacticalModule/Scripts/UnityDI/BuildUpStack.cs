﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityDI
{
	internal static class BuildUpStack
	{
		private static List<IInitializable> _initializables = new List<IInitializable>();
		private static List<Entry> _stack = new List<Entry>();

		internal class Entry
		{
			public object Object { get; set; }
			public string PropertyName { get; set; }
		}

		public static void PushObject(object obj)
		{
			_stack.Add(new Entry
			{
				Object = obj
			});
			CheckCircularDependency(obj);

			var initializable = obj as IInitializable;
			if (initializable != null)
				AddInitializable(initializable);
		}

		public static void SetPropertyName(string propertyName)
		{
			Peek().PropertyName = propertyName;
		}

		public static void Pop()
		{
			if (_stack.Count == 0)
				throw new ContainerException("Unexpected state: stack is empty");
			_stack.RemoveAt(_stack.Count - 1);

			if (_stack.Count == 0)
				CallInitialize();
		}

		private static void AddInitializable(IInitializable obj)
		{
			_initializables.Add(obj);
		}

		private static void CheckCircularDependency(object obj)
		{
			for (int i = _stack.Count - 2; i >= 0; --i)
			{
				if (ReferenceEquals(_stack[i].Object, obj))
				{
					DumpCircularDependency(i);
				}
			}
		}

		private static void DumpCircularDependency(int from)
		{
			var builder = new StringBuilder();
			builder.Append("Type: " + _stack[from].Object.GetType() + ". Property: " + _stack[from].PropertyName);
			for (int i = from; i < _stack.Count; ++i)
			{
				builder.AppendLine(" -> ");
				builder.Append("Type: " + _stack[i].Object.GetType() + ". Property: " + _stack[i].PropertyName);
			}

			Debug.LogWarning("Circular dependency found: ");
			Debug.LogWarning(builder.ToString());
		}

		private static Entry Peek()
		{
			if (_stack.Count == 0)
				throw new ContainerException("Unexpected state: stack is empty");
			return _stack[_stack.Count - 1];
		}

		private static void CallInitialize()
		{
			var initializables = _initializables;
			_initializables = new List<IInitializable>();
			foreach (var initializable in initializables)
			{
				initializable.Initialize();
			}
		}
	}
}
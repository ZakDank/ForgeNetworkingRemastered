﻿using System;
using System.Collections.Generic;

namespace BeardedManStudios.Forge
{
	public static class ForgeTypeFactory
	{
		private readonly static Dictionary<Type, Func<object>> _typeLookup = new Dictionary<Type, Func<object>>();

		public static void Register<T>(Func<object> factoryMethod)
		{
			Type t = typeof(T);
			if (_typeLookup.ContainsKey(t))
			{
				throw new Exception($"The type ({t}) is already registered");
			}
			_typeLookup.Add(t, factoryMethod);
		}

		public static void Unregister<T>()
		{
			_typeLookup.Remove(typeof(T));
		}

		public static T Get<T>()
		{
			Type t = typeof(T);
			if (!_typeLookup.TryGetValue(t, out var factoryMethod))
			{
				throw new Exception($"The type ({t}) is not registered");
			}
			return (T)factoryMethod();
		}
	}
}

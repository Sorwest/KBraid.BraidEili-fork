﻿using System.Diagnostics.CodeAnalysis;
using System;

namespace KBraid.BraidEili;
public partial interface IKokoroApi
{
    void RegisterTypeForExtensionData(Type type);
    T GetExtensionData<T>(object o, string key);
    bool TryGetExtensionData<T>(object o, string key, [MaybeNullWhen(false)] out T data);
    T ObtainExtensionData<T>(object o, string key, Func<T> factory);
    T ObtainExtensionData<T>(object o, string key) where T : new();
    bool ContainsExtensionData(object o, string key);
    void SetExtensionData<T>(object o, string key, T data);
    void RemoveExtensionData(object o, string key);
}
---
description: Утилиты для работы с доступом.
---

# AccessUtils

```csharp
/// <summary>
/// Чтение флагов.
/// </summary>
/// <typeparam name="TEnum">Тип перечисления.</typeparam>
/// <param name="mask">Маска доступа.</param>
/// <returns>Перечесление флагов доступа.</returns>
public static TEnum ReadFlags<TEnum>(int mask) where TEnum : Enum

/// <summary>
/// Записать флаги.
/// </summary>
/// <typeparam name="TEnum">Тип перечисления.</typeparam>
/// <param name="flags">Набор флагов.</param>
/// <returns>Маска доступа.</returns>
public static int WriteFlags<TEnum>(TEnum flags) where TEnum : Enum

/// <summary>
/// Проверяет является ли перечесление типом флагов.
/// </summary>
/// <typeparam name="TEnum">Тип перечисления.</typeparam>
/// <returns>True - перечисление типа флагов, false - не является перечислением флагов.</returns>
public static bool IsFlagsEnum<TEnum>() where TEnum : Enum


/// <summary>
/// Проверяет в маске доступа, есть нужный флаг.
/// </summary>
/// <typeparam name="TEnum">Тип перечисления.</typeparam>
/// <param name="mask">Маска доступа</param>
/// <param name="flag">Флаг который проверяем.</param>
/// <returns>True - есть флаг, False - нет флага.</returns>
public static bool HasFlag<TEnum>(int mask, TEnum flag) where TEnum : Enum

```

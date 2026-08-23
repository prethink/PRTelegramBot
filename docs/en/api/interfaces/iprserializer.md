---
description: Interface of the serializer wrapper.
---

# IPRSerializer

Interface of the serializer wrapper.

## Methods

| Method | Description |
| --- | --- |
| `T Deserialize<T>(string data)` | Deserializes the string representation of an object into an instance of type `T`. |
| `string Serialize<T>(T data)` | Serializes an object of type `T` into a string. |


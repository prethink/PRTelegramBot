---
description: Message builder with support for named tokens and positional arguments. Lets you compose strings in the style of `string.Format(string, object?[])`, but extended with tokens such as {QA}, {Dev} and so on.
---

# MessageBuilder

Message builder with support for named tokens and positional arguments. Lets you compose strings in the style of `string.Format(string, object?[])`, but extended with tokens such as {QA}, {Dev} and so on.

## Methods

| Method | Description |
| --- | --- |
| `MessageBuilder AddResolver(string key, Func<string> resolver)` | Adds a named token with a lazy resolver (Func<string>). |
| `MessageBuilder AddResolver(string key, string value)` | Adds a named token with a static value. |
| `MessageBuilder AddArgument(object arg)` | Adds a single positional argument to substitute into {0}, {1} and so on. |
| `MessageBuilder AddArguments(params object[] arguments)` | Adds several positional arguments at once. |
| `string Build()` | Builds the final string, substituting the positional arguments and the values of the named tokens. Tokens that are not found are left as {TokenName}. |

## Constructors

| Constructor | Description |
| --- | --- |
| `MessageBuilder(string template)` | Initializes a new message builder with the given template. |


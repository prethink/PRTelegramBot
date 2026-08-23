---
description: Telegram bot options for working over a webhook.
---

# WebHookOptions

Telegram bot options for working over a webhook.

## Properties

| Property | Description |
| --- | --- |
| `string Url { get; set; }` | The webhook URL. |
| `InputFileStream? Certificate { get; set; }` | Certificate for HTTPS connections. |
| `string? IpAddress { get; set; }` | The IP address to listen for incoming connections on. |
| `int? MaxConnections { get; set; }` | Maximum number of simultaneous connections. |
| `bool DropPendingUpdates { get; set; }` | Flag that drops pending updates at startup. |
| `string? SecretToken { get; set; }` | Secret token used to verify requests coming from Telegram. |


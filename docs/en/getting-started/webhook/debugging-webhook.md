---
description: Testing a webhook bot from your development machine.
---

# Debugging a webhook

A webhook needs a public HTTPS address, which your development machine does not have. **ngrok** solves that: it opens a public URL and forwards every request that arrives there to a local port.

Download it from [https://ngrok.com/download](https://ngrok.com/download).

## Steps

1. Start your application and note the port it listens on.
2. Run ngrok against that port:

```sh
ngrok http 8443
```

Replace `8443` with your own port.

3. ngrok prints a **Forwarding** line with a public HTTPS address, something like `https://a1b2-c3d4.ngrok-free.app`.
4. Pass that address to the builder, adding the route you registered:

```csharp
new PRBotBuilder("Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetUrlWebHook("https://a1b2-c3d4.ngrok-free.app/botendpoint")
    .Build();
```

5. Restart the application. Telegram now delivers updates to ngrok, which forwards them to your machine, and you can set breakpoints as usual.

{% hint style="info" %}
On the free plan ngrok gives you a new address every restart, so the URL has to be updated in the builder each time.
{% endhint %}

ngrok also serves a local inspector, by default at `http://127.0.0.1:4040`, where you can see every request Telegram sent and replay it — useful when you want to hit the same update again without asking a person to press the button once more.

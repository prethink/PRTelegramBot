---
description: Video walkthroughs, and the project that goes with them.
---

# Video tutorials

Several videos were recorded to make the framework easier to pick up: [the playlist on YouTube](https://www.youtube.com/watch?v=AOO3dTQP_vQ&list=PLq67v69P7SNDWnu0vuzp03HOMEIYaB03R).

They are in Russian, and they are not always current — the API has moved on since they were recorded. What they still show well is the shape of things: how a bot is put together and where each piece sits.

There is a companion project on [GitHub](https://github.com/prethink/PRTelegramYoutube), with one branch per lesson.

{% hint style="warning" %}
Where a video and this documentation disagree, the documentation is right. Version 1.0.0 renamed and removed a number of things — the [migration page](migrating-to-1.0.md) lists them. In particular, `Helpers.Message.Send`, which appears throughout the videos, is now `MessageSender.Send`.
{% endhint %}

For code that is guaranteed to compile against the current version, the [example projects](https://github.com/prethink/PRTelegramBot/tree/master/Examples) in the repository are the better starting point — they are built on every commit.

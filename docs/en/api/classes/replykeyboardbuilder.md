---
description: Builder for conveniently constructing a ReplyKeyboardMarkup. Lets you set keyboard options and add buttons and rows dynamically.
---

# ReplyKeyboardBuilder

Builder for conveniently constructing a ReplyKeyboardMarkup. Lets you set keyboard options and add buttons and rows dynamically.

Inherits `KeyboardBuilderBase<KeyboardButton`, `ReplyKeyboardMarkup`, `ReplyKeyboardBuilder>`.

## Values

| Value | Description |
| --- | --- |
| `Top` | Top. |
| `Bottom` | Bottom. |

## Methods

| Method | Description |
| --- | --- |
| `ReplyKeyboardBuilder SetPersistent(bool value = true)` | Sets the persistent keyboard flag. |
| `ReplyKeyboardBuilder SetResizeKeyboard(bool value = true)` | Sets the keyboard resize flag. |
| `ReplyKeyboardBuilder SetOneTimeKeyboard(bool value = true)` | Sets the one-time keyboard flag. |
| `ReplyKeyboardBuilder SetInputFieldPlaceholder(string placeholder)` | Sets the placeholder text in the input field. |
| `ReplyKeyboardBuilder SetSelective(bool value = true)` | Shows the keyboard only to specific users. |
| `ReplyKeyboardBuilder SetMainMenuButton(string buttonName, MainMenuButtonPosition mainMenuButtonPosition = MainMenuButtonPosition.Bottom)` | Sets the name of the main menu button and the position it is added at (the top or the bottom of the keyboard). If no name is given, the button is not added. |
| `ReplyKeyboardBuilder AddButton(string buttonName, bool newRow = false)` | Adds a regular button with the specified text. You can specify whether the button should be added on a new row. |
| `ReplyKeyboardBuilder AddButtonWebApp(string buttonName, string url, bool newRow = false)` | Adds a button that opens a WebApp by its link. |
| `ReplyKeyboardBuilder AddRequestContact(string buttonName, bool newRow = false)` | Adds a button that requests the user's contact. When pressed, Telegram sends the user's contact. |
| `ReplyKeyboardBuilder AddRequestLocation(string buttonName, bool newRow = false)` | Adds a button that requests the user's location. When pressed, Telegram sends the user's current location. |
| `ReplyKeyboardBuilder AddRequestChat(string buttonName, KeyboardButtonRequestChat requestChat, bool newRow = false)` | Adds a button that requests a chat selection. Lets the user pick a chat according to the request parameters. |
| `ReplyKeyboardBuilder AddRequestChat(string buttonName, int requestId, bool chatIsChannel, bool newRow = false)` | Adds a button that requests a chat selection, with the request parameters specified manually. |
| `ReplyKeyboardBuilder AddRequestUsers(string buttonName, KeyboardButtonRequestUsers requestUsers, bool newRow = false)` | Adds a button that requests a user selection. |
| `ReplyKeyboardBuilder AddRequestManagedBot(string buttonName, KeyboardButtonRequestManagedBot requestManagedBot, bool newRow = false)` | Adds a button that asks the user to create and share a bot managed by this one. |
| `ReplyKeyboardBuilder AddRequestUsers(string buttonName, int requestId, int? maxQuantity = null, bool newRow = false)` | Adds a button that requests a user selection, with the request parameters specified manually. |
| `ReplyKeyboardBuilder AddRequestPoll(string buttonName, KeyboardButtonPollType pollType, bool newRow = false)` | Adds a button that requests a poll to be created. When pressed, Telegram prompts the user to create a poll of the specified type. |
| `ReplyKeyboardBuilder AddEmptyButton(int count = 1, bool newRow = false)` | Adds the specified number of "empty" buttons — decorative elements used to align the layout or fill up free space. |
| `override ReplyKeyboardMarkup Build()` |  |


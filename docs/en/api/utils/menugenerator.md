---
description: Class that makes generating menus convenient.
---

# MenuGenerator

Class that makes generating menus convenient.

## Methods

| Method | Description |
| --- | --- |
| `static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)` | Generates a reply menu for the bot. |
| `static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<KeyboardButton> keyboardButtons, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)` | Generates a reply menu for the bot. |
| `static ReplyKeyboardMarkup ReplyKeyboard(List<List<KeyboardButton>> buttons, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)` | Generates a reply menu for the bot. |
| `static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<string> menu, string mainMenu = "")` | Generates reply buttons for the bot. |
| `static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<KeyboardButton> buttons, string mainMenu = "")` | Generates reply buttons for the bot. |
| `static List<List<KeyboardButton>> ReplyButtons(List<List<KeyboardButton>> buttonsOne, List<List<KeyboardButton>> buttonsTwo)` | Merges the bot's reply buttons. |
| `static InlineKeyboardMarkup InlineKeyboard(List<List<InlineKeyboardButton>> buttons)` | Creates an inline menu for the bot. |
| `static InlineKeyboardMarkup InlineKeyboard(int maxColumn, List<IInlineContent> menu)` | Creates an inline menu for the bot. |
| `static List<List<InlineKeyboardButton>> InlineButtons(int maxColumn, List<IInlineContent> menu)` | Creates a collection of inline buttons. |
| `static InlineKeyboardMarkup GetPageMenu(` | Generates a menu for paginated output. |
| `static InlineKeyboardMarkup GetPageMenu(` | Generates a menu for paginated output. |
| `static InlineKeyboardMarkup GetPageMenu(` | Generates a menu for paginated output. |
| `static InlineKeyboardMarkup GetPageMenu(` | Generates a menu for paginated output. |
| `static InlineKeyboardMarkup GetPageMenu(` | Generates a menu for paginated output. |


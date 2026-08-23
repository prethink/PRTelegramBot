---
description: Command that carries a date from the calendar.
---

# CalendarTCommand

Command that carries a date from the calendar.

Inherits `TCommandBase`.

## Properties

| Property | Description |
| --- | --- |
| `DateTime Date { get; set; }` | Date. |
| `string Culture { get; set; }` | Culture. |

## Constructors

| Constructor | Description |
| --- | --- |
| `CalendarTCommand(DateTime date)` | Constructor. |
| `CalendarTCommand(DateTime date, int headerCallbackCommand)` | Constructor. |
| `CalendarTCommand(DateTime date, CultureInfo culture, int headerCallbackCommand)` | Constructor. |
| `CalendarTCommand() { }` | Constructor. |


---
description: Base class for building keyboards.
---

# KeyboardBuilderBase

Base class for building keyboards.

## Fields

| Field | Description |
| --- | --- |
| `const string KEY_EMPTY_BUTTON_NAME = "%EMPTY_BUTTON%"` | Default name of an empty button. |

## Methods

| Method | Description |
| --- | --- |
| `TSelf SetEmptyButtonsName(string buttonName)` | Sets the text that will be used for "empty" buttons — decorative or filler elements. |
| `TSelf AddButton(TButton button, bool newRow = false)` | Adds a button. |
| `TSelf AddButton(params TButton[] buttons)` | Adds buttons. |
| `TSelf AddRow()` | Adds a new row. |
| `TSelf AddRowWithButton(TButton button)` | Adds a new row with a button. |
| `TSelf AddRowWithButtons(params TButton[] buttons)` | Adds a new row with buttons. |
| `void Clear()` | Clears the keyboard. |
| `TSelf GenerateButtons<T>(IEnumerable<T> items, Func<T, TButton> generator, Predicate<T>? filter = null, bool addNewRow = false)` | Generates buttons from a collection, applying a filter. |
| `TSelf GenerateRow<T>(IEnumerable<T> items, Func<T, TButton> generator)` | Generates a row of buttons from a collection. |
| `IEnumerable<IEnumerable<TButton>> GetButtonGrid()` | Gets the buttons as rows and columns (a table / grid). |
| `IEnumerable<TButton> GetAllButtons()` | Returns all buttons as a single flat sequence. |
| `long GetAllButtonsCount()` | Returns the total number of buttons. |
| `IEnumerable<TButton> GetRow(int rowIndex)` | Gets the collection of buttons in the row. |
| `long GetRowCount()` | Gets the current number of rows. |
| `IEnumerable<TButton> GetColumn(int columnIndex)` | Gets the collection of buttons in the column. |
| `long GetColumnCount()` | Gets the number of columns. |
| `abstract TKeyboard Build()` | Creates the keyboard. |


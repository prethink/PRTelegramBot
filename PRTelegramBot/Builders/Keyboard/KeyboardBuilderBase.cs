using PRTelegramBot.Extensions;

namespace PRTelegramBot.Builders.Keyboard
{
    /// <summary>
    /// Base class for building keyboards.
    /// </summary>
    /// <typeparam name="TButton">Button type.</typeparam>
    /// <typeparam name="TKeyboard">Keyboard type.</typeparam>
    /// <typeparam name="TSelf">Builder type.</typeparam>
    public abstract class KeyboardBuilderBase<TButton, TKeyboard, TSelf>
        where TSelf : KeyboardBuilderBase<TButton, TKeyboard, TSelf>
    {
        #region Constants

        /// <summary>
        /// Default name of an empty button.    
        /// </summary>
        public const string KEY_EMPTY_BUTTON_NAME = "%EMPTY_BUTTON%";

        #endregion

        #region Fields and properties

        /// <summary>
        /// Keyboard buttons.
        /// </summary>
        protected List<List<TButton>> buttons = new();

        /// <summary>
        /// The button name used for an empty cell. Use it when the slot should visibly look occupied,
        /// but the button performs no action.  
        /// The default is the plain "·" character.
        /// </summary>
        protected string emptyButtonName = " ";

        #endregion

        #region Methods

        /// <summary>
        /// Sets the text that will be used
        /// for "empty" buttons — decorative or filler elements.
        /// </summary>
        /// <param name="buttonName">Text for the empty button.</param>
        /// <returns>The current builder instance.</returns>
        public TSelf SetEmptyButtonsName(string buttonName)
        {
            this.emptyButtonName = buttonName;
            return (TSelf)this;
        }

        /// <summary>
        /// Adds a button.
        /// </summary>
        /// <param name="button">Button.</param>
        /// <param name="newRow">If true, every button is added on a new row.</param>
        public TSelf AddButton(TButton button, bool newRow = false)
        {
            if (buttons.Count == 0)
                buttons.Add(new List<TButton>());

            var lastRow = buttons[^1];

            if (newRow)
                this.AddRow();

            lastRow = buttons[^1];
            lastRow.Add(button);

            return (TSelf)this;
        }

        /// <summary>
        /// Adds buttons.
        /// </summary>
        /// <param name="buttons">Collection of buttons.</param>
        public TSelf AddButton(params TButton[] buttons)
        {
            foreach (var button in buttons)
                this.AddButton(button);

            return (TSelf)this;
        }

        /// <summary>
        /// Adds a new row.
        /// </summary>
        public TSelf AddRow()
        {
            var lastRow = buttons[^1];

            if (lastRow.Any())
                buttons.Add(new List<TButton>());

            return (TSelf)this;
        }

        /// <summary>
        /// Adds a new row with a button.
        /// </summary>
        /// <param name="button">Button.</param>
        public TSelf AddRowWithButton(TButton button)
        {
            this.AddRow();
            this.AddButton(button);
            return (TSelf)this;
        }

        /// <summary>
        /// Adds a new row with buttons.
        /// </summary>
        /// <param name="buttons">Buttons.</param>
        public TSelf AddRowWithButtons(params TButton[] buttons)
        {
            this.AddRow();
            this.AddButton(buttons);
            return (TSelf)this;
        }

        /// <summary>
        /// Clears the keyboard.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }

        /// <summary>
        /// Generates buttons from a collection, applying a filter.
        /// </summary>
        public TSelf GenerateButtons<T>(IEnumerable<T> items, Func<T, TButton> generator, Predicate<T>? filter = null, bool addNewRow = false)
        {
            foreach (var item in items)
            {
                if (filter == null || filter(item))
                    this.AddButton(generator(item), addNewRow);
            }
            return (TSelf)this;
        }

        /// <summary>
        /// Generates a row of buttons from a collection.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="items">Items.</param>
        /// <param name="generator">Generator.</param>
        /// <returns></returns>
        public TSelf GenerateRow<T>(IEnumerable<T> items, Func<T, TButton> generator)
        {
            this.AddRow();

            foreach (var item in items)
                this.AddButton(generator(item));

            return (TSelf)this;
        }

        /// <summary>
        /// Gets the buttons as rows and columns (a table / grid). 
        /// </summary>
        /// <returns>Collection of buttons.</returns>
        public IEnumerable<IEnumerable<TButton>> GetButtonGrid()
        {
            return buttons.ToList();
        }

        /// <summary>
        /// Returns all buttons as a single flat sequence.
        /// </summary>
        /// <returns>Collection of buttons.</returns>
        public IEnumerable<TButton> GetAllButtons()
        {
            return buttons.SelectMany(row => row ?? Enumerable.Empty<TButton>());
        }

        /// <summary>
        /// Returns the total number of buttons.
        /// </summary>
        public long GetAllButtonsCount()
        {
            return GetAllButtons().Count();
        }

        /// <summary>
        /// Gets the collection of buttons in the row.
        /// </summary>
        /// <param name="rowIndex">Row index.</param>
        /// <returns>The collection of buttons in the row.</returns>
        public IEnumerable<TButton> GetRow(int rowIndex)
        {
            return buttons.GetRow(rowIndex);
        }

        /// <summary>
        /// Gets the current number of rows.
        /// </summary>
        /// <returns>Number of rows.</returns>
        public long GetRowCount()
        {
            return buttons.GetRowCount();
        }

        /// <summary>
        /// Gets the collection of buttons in the column.
        /// </summary>
        /// <param name="columnIndex">Column index.</param>
        /// <returns>The collection of buttons in the column.</returns>
        public IEnumerable<TButton> GetColumn(int columnIndex)
        {
            return buttons.GetColumn(columnIndex);
        }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <returns>Number of columns.</returns>
        public long GetColumnCount()
        {
            return buttons.GetColumnCount();
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void ReplaceEmptyButtons();

        /// <summary>
        /// Creates the keyboard.
        /// </summary>
        /// <returns>Keyboard.</returns>
        public abstract TKeyboard Build();

        #endregion
    }
}

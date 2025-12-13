using PRTelegramBot.Extensions;

namespace PRTelegramBot.Builders.Keyboard
{
    /// <summary>
    /// Базовый класс для построения клавиатур.
    /// </summary>
    /// <typeparam name="TButton">Тип кнопки.</typeparam>
    /// <typeparam name="TKeyboard">Тип клавиатуры.</typeparam>
    /// <typeparam name="TSelf">Тип билдера.</typeparam>
    public abstract class KeyboardBuilderBase<TButton, TKeyboard, TSelf>
        where TSelf : KeyboardBuilderBase<TButton, TKeyboard, TSelf>
    {
        #region Константы

        /// <summary>
        /// Название пустой кнопки по умолчанию.    
        /// </summary>
        public const string KEY_EMPTY_BUTTON_NAME = "%EMPTY_BUTTON%";

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Кнопки клавиатуры.
        /// </summary>
        protected List<List<TButton>> buttons = new();

        /// <summary>
        /// Имя кнопки для пустой ячейки. Используется, если нужно визуально указать, что место занято,
        /// но кнопка не выполняет действий.  
        /// По умолчанию стоит простой символ "·".
        /// </summary>
        protected string emptyButtonName = " ";

        #endregion

        #region Методы

        /// <summary>
        /// Устанавливает текст, который будет использоваться
        /// для "пустых" кнопок — декоративных или заполняющих элементов.
        /// </summary>
        /// <param name="buttonName">Текст для пустой кнопки.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public TSelf SetEmptyButtonsName(string buttonName)
        {
            this.emptyButtonName = buttonName;
            return (TSelf)this;
        }

        /// <summary>
        /// Добавить кнопку.
        /// </summary>
        /// <param name="button">Кнопка.</param>
        /// <param name="newRow">Если true — каждая кнопка будет добавляться на новую строку.</param>
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
        /// Добавить кнопки.
        /// </summary>
        /// <param name="buttons">Коллекция кнопок.</param>
        public TSelf AddButton(params TButton[] buttons)
        {
            foreach (var button in buttons)
                this.AddButton(button);

            return (TSelf)this;
        }

        /// <summary>
        /// Добавить новую строку.
        /// </summary>
        public TSelf AddRow()
        {
            var lastRow = buttons[^1];

            if (lastRow.Any())
                buttons.Add(new List<TButton>());

            return (TSelf)this;
        }

        /// <summary>
        /// Добавить новую строку с кнопкой.
        /// </summary>
        /// <param name="button">Кнопка.</param>
        public TSelf AddRowWithButton(TButton button)
        {
            this.AddRow();
            this.AddButton(button);
            return (TSelf)this;
        }

        /// <summary>
        /// Добавить новую строку с кнопками.
        /// </summary>
        /// <param name="buttons">Кнопки.</param>
        public TSelf AddRowWithButtons(params TButton[] buttons)
        {
            this.AddRow();
            this.AddButton(buttons);
            return (TSelf)this;
        }

        /// <summary>
        /// Очистить клавиатуру.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }

        /// <summary>
        /// Генерирует кнопки из коллекции с фильтром.
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
        /// Генерирует строку кнопок из коллекции.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="items">Объекты.</param>
        /// <param name="generator">Генератор.</param>
        /// <returns></returns>
        public TSelf GenerateRow<T>(IEnumerable<T> items, Func<T, TButton> generator)
        {
            this.AddRow();

            foreach (var item in items)
                this.AddButton(generator(item));

            return (TSelf)this;
        }

        /// <summary>
        /// Получает коллекцию кнопок в формате строк и столбцов (таблицы/грида). 
        /// </summary>
        /// <returns>Коллекция кнопок.</returns>
        public IEnumerable<IEnumerable<TButton>> GetButtonGrid()
        {
            return buttons.ToList();
        }

        /// <summary>
        /// Возвращает все кнопки как одну плоскую последовательность.
        /// </summary>
        /// <returns>Коллекция кнопок.</returns>
        public IEnumerable<TButton> GetAllButtons()
        {
            return buttons.SelectMany(row => row ?? Enumerable.Empty<TButton>());
        }

        /// <summary>
        /// Возвращает общее количество кнопок.
        /// </summary>
        public long GetAllButtonsCount()
        {
            return GetAllButtons().Count();
        }

        /// <summary>
        /// Получить коллекцию кнопок из строки.
        /// </summary>
        /// <param name="rowIndex">Индекс строки.</param>
        /// <returns>Коллекция кнопок из строки.</returns>
        public IEnumerable<TButton> GetRow(int rowIndex)
        {
            return buttons.GetRow(rowIndex);
        }

        /// <summary>
        /// Получить текущее количество строк.
        /// </summary>
        /// <returns>Количество строк.</returns>
        public long GetRowCount()
        {
            return buttons.GetRowCount();
        }

        /// <summary>
        /// Получить коллекцию кнопок из столбца.
        /// </summary>
        /// <param name="columnIndex">Индекс столбца.</param>
        /// <returns>Коллекция кнопок из столбца.</returns>
        public IEnumerable<TButton> GetColumn(int columnIndex)
        {
            return buttons.GetColumn(columnIndex);
        }

        /// <summary>
        /// Получить количество столбцов.
        /// </summary>
        /// <returns>Количество столбцов.</returns>
        public long GetColumnCount()
        {
            return buttons.GetColumnCount();
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void ReplaceEmptyButtons();

        /// <summary>
        /// Создать клавиатуру.
        /// </summary>
        /// <returns>Клавиатура.</returns>
        public abstract TKeyboard Build();

        #endregion
    }
}

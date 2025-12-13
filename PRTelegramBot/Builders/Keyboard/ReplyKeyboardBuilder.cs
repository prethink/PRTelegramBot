using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Builders.Keyboard
{
    /// <summary>
    /// Билдер для удобного построения ReplyKeyboardMarkup.
    /// Позволяет задавать параметры клавиатуры и динамически добавлять кнопки и строки.
    /// </summary>
    public class ReplyKeyboardBuilder : KeyboardBuilderBase<KeyboardButton, ReplyKeyboardMarkup, ReplyKeyboardBuilder>
    {
        #region Поля и свойства 

        /// <summary>
        /// Запрашивает у клиентов Telegram всегда показывать
        /// пользовательскую клавиатуру, даже если обычная системная клавиатура скрыта.
        /// По умолчанию: false.  
        /// Если false — пользовательская клавиатура может быть скрыта и открыта иконкой клавиатуры.
        /// </summary>
        private bool isPersistent;

        /// <summary>
        /// Запрашивает у клиентов автоматически изменять высоту
        /// клавиатуры для оптимального отображения (например, уменьшить высоту, если всего
        /// две строки кнопок).  
        /// По умолчанию: false.  
        /// Если false — клавиатура всегда отображается той же высоты, что и стандартная.
        /// </summary>
        private bool resizeKeyboard;

        /// <summary>
        /// Запрашивает у клиентов скрывать клавиатуру сразу после того,
        /// как пользователь нажал на кнопку.  
        /// Клавиатура остаётся доступной, но Telegram автоматически переключится на обычную клавиатуру,
        /// и пользователь сможет повторно открыть кастомную клавиатуру кнопкой в поле ввода.
        /// По умолчанию: false.
        /// </summary>
        private bool oneTimeKeyboard;

        /// <summary>
        /// Текст-заполнитель, отображаемый в поле ввода, пока клавиатура
        /// активна. Может содержать от 1 до 64 символов.
        /// </summary>
        private string? inputFieldPlaceholder;

        /// <summary>
        /// Используется, если нужно показать клавиатуру только
        /// определённым пользователям.  
        /// Клавиатура будет видна:
        /// 1) Пользователям, упомянутым в тексте сообщения (@username).  
        /// 2) Если сообщение — ответ, то отправителю оригинального сообщения в этом же чате/треде.
        /// </summary>
        private bool selective;

        /// <summary>
        /// Название кнопки главного меню. Если не указано, кнопки нет.
        /// </summary>
        private string? mainMenuButton;

        /// <summary>
        /// Позиция кнопки главного меню, если кнопка есть.
        /// </summary>
        private MainMenuButtonPosition mainMenuButtonPosition;

        #endregion

        #region Методы

        /// <summary>
        /// Установить флаг постоянной клавиатуры.
        /// </summary>
        public ReplyKeyboardBuilder SetPersistent(bool value = true)
        {
            this.isPersistent = value;
            return this;
        }

        /// <summary>
        /// Установить флаг изменения размера клавиатуры.
        /// </summary>
        public ReplyKeyboardBuilder SetResizeKeyboard(bool value = true)
        {
            this.resizeKeyboard = value;
            return this;
        }

        /// <summary>
        /// Установить флаг временной клавиатуры.
        /// </summary>
        public ReplyKeyboardBuilder SetOneTimeKeyboard(bool value = true)
        {
            this.oneTimeKeyboard = value;
            return this;
        }

        /// <summary>
        /// Установить текст-подсказку в поле ввода.
        /// </summary>
        public ReplyKeyboardBuilder SetInputFieldPlaceholder(string placeholder)
        {
            this.inputFieldPlaceholder = placeholder;
            return this;
        }

        /// <summary>
        /// Показ клавиатуры только определённым пользователям.
        /// </summary>
        public ReplyKeyboardBuilder SetSelective(bool value = true)
        {
            this.selective = value;
            return this;
        }

        /// <summary>
        /// Устанавливает название кнопки главного меню и позицию,
        /// в которой она будет добавлена (сверху или снизу клавиатуры).
        /// Если название не указано — кнопка не будет добавлена.
        /// </summary>
        /// <param name="buttonName">Текст кнопки главного меню.</param>
        /// <param name="mainMenuButtonPosition">Позиция кнопки в клавиатуре (по умолчанию — Bottom).</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder SetMainMenuButton(string buttonName, MainMenuButtonPosition mainMenuButtonPosition = MainMenuButtonPosition.Bottom)
        {
            this.mainMenuButton = buttonName;
            this.mainMenuButtonPosition = mainMenuButtonPosition;
            return this;
        }

        /// <summary>
        /// Добавляет обычную кнопку с указанным текстом.
        /// Можно указать, должна ли кнопка быть добавлена в новую строку.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddButton(string buttonName, bool newRow = false)
        {
            this.AddButton(new KeyboardButton(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку, открывающую WebApp по ссылке.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="url">URL WebApp.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddButtonWebApp(string buttonName, string url, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithWebApp(buttonName, new WebAppInfo() { Url = url }), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса контакта пользователя.
        /// При нажатии Telegram отправит контакт пользователя.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestContact(string buttonName, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestContact(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса геолокации.
        /// При нажатии Telegram отправит текущее местоположение пользователя.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestLocation(string buttonName, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestLocation(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса выбора чата.
        /// Позволяет пользователю выбрать чат согласно параметрам запроса.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="requestChat">Объект параметров запроса чата.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestChat(string buttonName, KeyboardButtonRequestChat requestChat, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestChat(buttonName, requestChat), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса выбора чата, указывая параметры запроса вручную.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="requestId">ID запроса.</param>
        /// <param name="chatIsChannel">True — выбирать только каналы; false — только группы/чаты.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestChat(string buttonName, int requestId, bool chatIsChannel, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestChat(buttonName, requestId, chatIsChannel), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса выбора пользователей.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="requestUsers">Параметры запроса пользователей.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        /// <returns></returns>
        public ReplyKeyboardBuilder AddRequestUsers(string buttonName, KeyboardButtonRequestUsers requestUsers, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestUsers(buttonName, requestUsers), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса выбора пользователей,
        /// указывая параметры запроса вручную.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="requestId">ID запроса.</param>
        /// <param name="maxQuantity">Максимальное количество выбираемых пользователей.</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestUsers(string buttonName, int requestId, int? maxQuantity = null, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestUsers(buttonName, requestId, maxQuantity), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет кнопку запроса создания опроса.
        /// При нажатии Telegram предложит пользователю создать опрос указанного типа.
        /// </summary>
        /// <param name="buttonName">Текст кнопки.</param>
        /// <param name="pollType">Тип опроса (обычный или квиз).</param>
        /// <param name="newRow">Если true — кнопка добавляется в новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddRequestPoll(string buttonName, KeyboardButtonPollType pollType, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestPoll(buttonName, pollType), newRow);
            return this;
        }

        /// <summary>
        /// Добавляет указанное количество "пустых" кнопок — декоративных элементов,
        /// используемых для выравнивания или заполнения свободного места.
        /// </summary>
        /// <param name="count">Сколько пустых кнопок добавить.</param>
        /// <param name="newRow">Если true — каждая кнопка будет добавляться на новую строку.</param>
        /// <returns>Текущий экземпляр билдера.</returns>
        public ReplyKeyboardBuilder AddEmptyButton(int count = 1, bool newRow = false)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddButton(new KeyboardButton(KEY_EMPTY_BUTTON_NAME), newRow);
                newRow = false;
            }

            return this;
        }

        #endregion

        #region Базовый класс

        /// <inheritdoc/>
        protected override void ReplaceEmptyButtons()
        {
            foreach (var row in buttons)
            {
                foreach (var button in row)
                {
                    if(button.Text.Equals(KEY_EMPTY_BUTTON_NAME, StringComparison.OrdinalIgnoreCase))
                        button.Text = emptyButtonName;
                }
            }
        }

        /// <inheritdoc/>
        public override ReplyKeyboardMarkup Build()
        {
            this.ReplaceEmptyButtons();

            var resultButtons = buttons.ToList();
            buttons.Clear();

            if (!string.IsNullOrEmpty(mainMenuButton) && mainMenuButtonPosition == MainMenuButtonPosition.Top)
            {
                this.AddButton(mainMenuButton);
                this.AddRow();
            }

            buttons.AddRange(resultButtons);

            if (!string.IsNullOrEmpty(mainMenuButton) && mainMenuButtonPosition == MainMenuButtonPosition.Bottom)
            {
                this.AddRow();
                this.AddButton(mainMenuButton);
            }

            buttons.RemoveAll(x => x == null || x.Count == 0);

            ReplyKeyboardMarkup replyKeyboardMarkup = new(buttons);
            replyKeyboardMarkup.IsPersistent = isPersistent;
            replyKeyboardMarkup.ResizeKeyboard = resizeKeyboard;
            replyKeyboardMarkup.OneTimeKeyboard = oneTimeKeyboard;
            replyKeyboardMarkup.InputFieldPlaceholder = inputFieldPlaceholder;
            replyKeyboardMarkup.Selective = selective;

            return replyKeyboardMarkup;
        }

        #endregion
    }

    /// <summary>
    /// Перечисление позиции кнопки главного меню.
    /// </summary>
    public enum MainMenuButtonPosition
    {
        /// <summary>
        /// Сверху.
        /// </summary>
        Top,
        /// <summary>
        /// Снизу.
        /// </summary>
        Bottom
    }
}

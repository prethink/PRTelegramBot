using PRTelegramBot.Core;
using PRTelegramBot.Utils.Controls.CalendarControl.Common;
using System.Globalization;

namespace PRTelegramBot.Tests.ControlTests
{
    internal class CalendarTests
    {
        private CultureInfo cultureInfo { get; set; }

        private readonly DateTime TestData = DateTime.Parse("14.05.2024");

        private const string January = "01.01.2024";
        private const string February = "02.01.2024";
        private const string March = "03.01.2024";
        private const string April = "04.01.2024";
        private const string May = "05.01.2024";
        private const string June = "06.01.2024";
        private const string July = "07.01.2024";
        private const string August = "08.01.2024";
        private const string September = "09.01.2024";
        private const string October = "10.01.2024";
        private const string November = "11.01.2024";
        private const string December = "12.01.2024";

        [OneTimeSetUp]
        public void SetUp()
        {
            cultureInfo = CultureInfo.GetCultureInfo("ru-RU", false);
            var bot = new PRBotBuilder("55555:Token").Build();
            bot.ReloadHandlers();
        }

        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void CreateCalendar()
        {
            var calendarMarkup = Markup.Calendar(TestData, cultureInfo, 0);
        }

        [Test]
        [TestCase(0,"пн")]
        [TestCase(1,"вт")]
        [TestCase(2,"ср")]
        [TestCase(3,"чт")]
        [TestCase(4,"пт")]
        [TestCase(5,"сб")]
        [TestCase(6,"вс")]
        public void CreateCalendarWithPanelDays(int indexDay, string exceptedDay)
        {
            var calendarMarkup = Markup.Calendar(TestData, cultureInfo, 0);
            var day = calendarMarkup.InlineKeyboard.ElementAt(1).ElementAt(indexDay).Text;
            Assert.AreEqual(exceptedDay, day);
        }

        [Test]
        public void CreateCalendarWithControl()
        {
            var calendarMarkup = Markup.Calendar(TestData, cultureInfo, 0);
            var prevControl = calendarMarkup.InlineKeyboard.ElementAt(7).ElementAt(0).Text;
            var nextControl = calendarMarkup.InlineKeyboard.ElementAt(7).ElementAt(2).Text;
            Assert.AreEqual("<", prevControl);
            Assert.AreEqual(">", nextControl);
        }

        [Test]
        [TestCase(January, "Январь")]
        [TestCase(February, "Февраль")]
        [TestCase(March, "Март")]
        [TestCase(April, "Апрель")]
        [TestCase(May, "Май")]
        [TestCase(June, "Июнь")]
        [TestCase(July, "Июль")]
        [TestCase(August, "Август")]
        [TestCase(September, "Сентябрь")]
        [TestCase(October, "Октябрь")]
        [TestCase(November, "Ноябрь")]
        [TestCase(December, "Декабрь")]
        public void CreateCalendarWithTitleMonth(DateTime data, string month)
        {
            var calendarMarkup = Markup.Calendar(data, cultureInfo, 0);
            string title = calendarMarkup.InlineKeyboard.ElementAt(0).ElementAt(0).Text;
            Assert.IsTrue(title.Contains(month, StringComparison.OrdinalIgnoreCase));
        }
    }
}

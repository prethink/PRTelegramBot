using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.UtilsTests
{
    internal class MenuGeneratorTests
    {
        private const string MAIN_MENU = "Главное меню";
        private const string MENU_ITEM = "Menu item";

        [Test]
        [TestCase(1, "", 1, 1)]
        [TestCase(1, "", 2, 2)]
        [TestCase(1, "", 6, 6)]
        [TestCase(2, "", 1, 1)]
        [TestCase(2, "", 1, 1)]
        [TestCase(1, MAIN_MENU, 1, 2)]
        [TestCase(1, MAIN_MENU, 6, 7)]
        [TestCase(2, MAIN_MENU, 1, 2)]
        [TestCase(2, MAIN_MENU, 6, 4)]
        public void ReplyKeyboard(int maxColumn, string mainMenu, int elementInList, int exceptedRow)
        {
            var list = GenerateMenu(elementInList);
            var menu = MenuGenerator.ReplyKeyboard(maxColumn, list, true, mainMenu);
            var rowCount = menu.Keyboard.Count();

            Assert.AreEqual(exceptedRow, rowCount);
        }

        private List<string> GenerateMenu(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(MENU_ITEM);
            }
            return list;
        }

    }
}

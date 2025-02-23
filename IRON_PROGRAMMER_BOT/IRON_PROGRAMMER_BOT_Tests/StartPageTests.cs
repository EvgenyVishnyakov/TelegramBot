using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests
{
    public class StartPageTests
    {
        [Test]
        public void View_FirstEnter_CorrectTextAndKeyboard()
        {
            //Arrange
            var startPage = new StartPage();
            var pages = new Stack<IPage>([new NotStatedPage()]);
            var userState = new UserState(pages, new UserData());
            var expectedButtons = new InlineKeyboardButton[][]
            {
                [InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", "HelpByCoursePage")],
                 [InlineKeyboardButton.WithCallbackData("Узнать о курсах", "InfoByCoursePage"),
                  InlineKeyboardButton.WithCallbackData("Позвать менеджера", "ConnectWithManagerPage")]
            };
            //Act
            var result = startPage.View(null, userState);

            //Assert
            ClassicAssert.IsInstanceOf<PageResultBase>(result);

            Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(startPage));
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
            Assert.That(result.Text, Is.EqualTo(Resources.StartPageText));
            Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
            ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
            KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
        }

        [Test]
        public void Handle_StartPageCallback_HelpByCoursePage()
        {
            //Arrange
            var startPage = new StartPage();
            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "HelpByCoursePage" } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert            
            Assert.That(result.GetType(), Is.EqualTo(typeof(VideoPageResult)));
            ClassicAssert.IsInstanceOf<HelpByCoursePage>(result.UpdatedUserState.CurrenntPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_StartPageCallback_InfoByCoursePage()
        {
            //Arrange
            var startPage = new StartPage();
            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "InfoByCoursePage" } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert           
            Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
            ClassicAssert.IsInstanceOf<InfoByCoursePage>(result.UpdatedUserState.CurrenntPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_StartPageCallback_ConnectWithManagerPage()
        {
            //Arrange
            var startPage = new StartPage();
            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "ConnectWithManagerPage" } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert           
            Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
            ClassicAssert.IsInstanceOf<ConnectWithManagerPage>(result.UpdatedUserState.CurrenntPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_UnknownMessage_StartPageView()
        {
            //Arrange
            var startPage = new StartPage();
            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
            var expectedButtons = new InlineKeyboardButton[][]
            {
                [InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", "HelpByCoursePage")],
                 [InlineKeyboardButton.WithCallbackData("Узнать о курсах", "InfoByCoursePage"),
                  InlineKeyboardButton.WithCallbackData("Позвать менеджера", "ConnectWithManagerPage")]
            };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert            
            Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(startPage));
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));

            Assert.That(result.Text, Is.EqualTo(Resources.StartPageText));
            Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

            ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
            KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
        }
    }
}

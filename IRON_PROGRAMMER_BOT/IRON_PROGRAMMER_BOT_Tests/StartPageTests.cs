using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests
{
    public class StartPageTests
    {
        private IServiceProvider _services;

        [OneTimeSetUp]
        public void SetUp()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var serviceCollection = new ServiceCollection();

            ContainerConfigurator.Configure(configuration, serviceCollection);
            _services = serviceCollection.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (_services is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Test]
        public void View_FirstEnter_CorrectTextAndKeyboard()
        {
            //Arrange
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>()]);
            var userState = new UserState(pages, new UserData());
            var expectedButtons = new InlineKeyboardButton[][]
            {
                [InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", Resources.HelpByCoursePage)],
                 [InlineKeyboardButton.WithCallbackData("Узнать о курсах", Resources.InfoByCoursePage)],
                  [InlineKeyboardButton.WithCallbackData("Обратиться к кураторам курсов", Resources.ConnectWithTutorPage)],
                  [InlineKeyboardButton.WithCallbackData("Позвать менеджера", Resources.ConnectWithManagerPage)]
            };
            //Act
            var result = startPage.View(null, userState);

            //Assert
            ClassicAssert.IsInstanceOf<PageResultBase>(result);

            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(startPage));
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
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.HelpByCoursePage } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert            
            Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBas)));
            ClassicAssert.IsInstanceOf<HelpByCoursePage>(result.UpdatedUserState.CurrentPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_StartPageCallback_InfoByCoursePage()
        {
            //Arrange
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.InfoByCoursePage } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert           
            Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBas)));
            ClassicAssert.IsInstanceOf<InfoByCoursePage>(result.UpdatedUserState.CurrentPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_StartPageCallback_ConnectWithTutorPage()
        {
            //Arrange
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.ConnectWithTutorPage } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert           
            Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBas)));
            ClassicAssert.IsInstanceOf<ConnectWithTutorPage>(result.UpdatedUserState.CurrentPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_StartPageCallback_ConnectWithManagerPage()
        {
            //Arrange
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.ConnectWithManagerPage } };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert           
            Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBas)));
            ClassicAssert.IsInstanceOf<ConnectWithManagerPage>(result.UpdatedUserState.CurrentPage);

            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_UnknownMessage_StartPageView()
        {
            //Arrange
            var startPage = _services.GetRequiredService<StartPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), startPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
            var expectedButtons = new InlineKeyboardButton[][]
            {
                 [InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", Resources.HelpByCoursePage)],
                 [InlineKeyboardButton.WithCallbackData("Узнать о курсах", Resources.InfoByCoursePage)],
                  [InlineKeyboardButton.WithCallbackData("Обратиться к кураторам курсов", Resources.ConnectWithTutorPage)],
                  [InlineKeyboardButton.WithCallbackData("Позвать менеджера", Resources.ConnectWithManagerPage)]

            };
            //Act
            var result = startPage.Handle(update, userState);

            //Assert            
            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(startPage));
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));

            Assert.That(result.Text, Is.EqualTo(Resources.StartPageText));
            Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

            ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
            KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
        }
    }
}

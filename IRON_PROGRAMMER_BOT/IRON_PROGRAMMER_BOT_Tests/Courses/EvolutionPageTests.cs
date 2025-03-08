using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.CoursesPage;
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

namespace IRON_PROGRAMMER_BOT_Tests.Courses
{
    public class EvolutionPageTests
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
        public void View_Enter_CorrectTextAndKeyboard()
        {
            //Arrange
            var evolutionPage = _services.GetRequiredService<EvolutionPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<ConnectWithTutorPage>()]);
            var userState = new UserState(pages, new UserData());
            var expectedButtons = new InlineKeyboardButton[][]
            {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
            };

            //Act
            var result = evolutionPage.View(null, userState);

            //Assert
            ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(evolutionPage));
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));
            Assert.That(result.Text, Is.EqualTo(Resources.CommonTutorText));
            Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
            ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
            KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
        }

        [Test]
        public void Handle_ConnectWithTutorPageCallback_ConnectWithTutorPage()
        {
            //Arrange
            var evolutionPage = _services.GetRequiredService<EvolutionPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<ConnectWithTutorPage>(), evolutionPage]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.Back } };

            //Act
            var result = evolutionPage.Handle(update, userState);

            //Assert        
            Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
            ClassicAssert.IsInstanceOf<ConnectWithTutorPage>(result.UpdatedUserState.CurrentPage);
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        }

        [Test]
        public void Handle_UnknownMessage_EvolutionPage()
        {
            //Arrange
            var evolutionPage = _services.GetRequiredService<EvolutionPage>();
            var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<ConnectWithTutorPage>()]);
            var userState = new UserState(pages, new UserData());
            var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
            var expectedButtons = new InlineKeyboardButton[][]
            {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
            };
            //Act
            var result = evolutionPage.View(update, userState);

            //Assert       
            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(evolutionPage));
            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));

            Assert.That(result.Text, Is.EqualTo(Resources.CommonTutorText));
            Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

            ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
            KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
        }
    }
}

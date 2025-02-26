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

namespace IRON_PROGRAMMER_BOT_Tests;

public class ConnectWithManagerPageTests
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
        var сonnectWithManagerPage = _services.GetRequiredService<ConnectWithManagerPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Отправить вопрос", Resources.SendQuastion),
                InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };

        //Act
        var result = сonnectWithManagerPage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(сonnectWithManagerPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        Assert.That(result.Text, Is.EqualTo(Resources.ConnectWithManagerPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }


    [Test]
    public void Handle_ConnectWithManagerPageCallback_StartPage()
    {
        //Arrange
        var сonnectWithManagerPage = _services.GetRequiredService<ConnectWithManagerPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), сonnectWithManagerPage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.Back } };

        //Act
        var result = сonnectWithManagerPage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBase)));
        ClassicAssert.IsInstanceOf<StartPage>(result.UpdatedUserState.CurrentPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
    }

    [Test]
    public void Handle_UnknownMessage_ConnectWithManagerPage()
    {
        //Arrange
        var сonnectWithManagerPage = _services.GetRequiredService<ConnectWithManagerPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Отправить вопрос",Resources.SendQuastion),
                InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };
        //Act
        var result = сonnectWithManagerPage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(сonnectWithManagerPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));

        Assert.That(result.Text, Is.EqualTo(Resources.ConnectWithManagerPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}

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

public class ResolveTaskPageTests
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
        var resolveTaskPage = _services.GetRequiredService<ResolveTaskPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<HelpByCoursePage>()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };

        //Act
        var result = resolveTaskPage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(resolveTaskPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));
        Assert.That(result.Text, Is.EqualTo(Resources.ResolveTaskPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }

    [Test]
    public void Handle_ResolveTaskPageCallback_HelpByCoursePage()
    {
        //Arrange
        var resolveTaskPage = _services.GetRequiredService<ResolveTaskPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<HelpByCoursePage>(), resolveTaskPage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.Back } };

        //Act
        var result = resolveTaskPage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
        ClassicAssert.IsInstanceOf<HelpByCoursePage>(result.UpdatedUserState.CurrentPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
    }

    [Test]
    public void Handle_UnknownMessage_ResolveTaskPage()
    {
        //Arrange
        var resolveTaskPage = _services.GetRequiredService<ResolveTaskPage>();
        var pages = new Stack<IPage>([_services.GetRequiredService<NotStatedPage>(), _services.GetRequiredService<StartPage>(), _services.GetRequiredService<HelpByCoursePage>()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };
        //Act
        var result = resolveTaskPage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(resolveTaskPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));

        Assert.That(result.Text, Is.EqualTo(Resources.ResolveTaskPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}

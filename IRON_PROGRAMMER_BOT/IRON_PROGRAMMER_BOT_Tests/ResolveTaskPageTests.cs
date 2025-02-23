using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests;

public class ResolveTaskPageTests
{
    [Test]
    public void View_Enter_CorrectTextAndKeyboard()
    {
        //Arrange
        var resolveTaskPage = new ResolveTaskPage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), new HelpByCoursePage()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };

        //Act
        var result = resolveTaskPage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(resolveTaskPage));
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
        var resolveTaskPage = new ResolveTaskPage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), new HelpByCoursePage(), resolveTaskPage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.Back } };

        //Act
        var result = resolveTaskPage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(VideoPageResult)));
        ClassicAssert.IsInstanceOf<HelpByCoursePage>(result.UpdatedUserState.CurrenntPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
    }

    [Test]
    public void Handle_UnknownMessage_ResolveTaskPage()
    {
        //Arrange
        var resolveTaskPage = new ResolveTaskPage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), new HelpByCoursePage()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };
        //Act
        var result = resolveTaskPage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(resolveTaskPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));

        Assert.That(result.Text, Is.EqualTo(Resources.ResolveTaskPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}

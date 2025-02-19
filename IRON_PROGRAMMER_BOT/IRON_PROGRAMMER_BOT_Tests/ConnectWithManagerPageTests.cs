using IRON_PROGRAMMER_BOT_ConsoleApp;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests;

public class ConnectWithManagerPageTests
{
    [Test]
    public void View_Enter_CorrectTextAndKeyboard()
    {
        //Arrange
        var сonnectWithManagerPage = new ConnectWithManagerPage();
        var pages = new Stack<IPage>([new NotStatedPage()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Отправить вопрос", "sendQuastion"),
                InlineKeyboardButton.WithCallbackData("Назад", "Назад")]
        };

        //Act
        var result = сonnectWithManagerPage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(сonnectWithManagerPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
        Assert.That(result.Text, Is.EqualTo(Resources.ConnectWithManagerPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }


    [Test]
    public void Handle_InfoByCoursePageCallback_StartPage()
    {
        //Arrange
        var сonnectWithManagerPage = new ConnectWithManagerPage();
        var pages = new Stack<IPage>([new NotStatedPage(), сonnectWithManagerPage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "Назад" } };

        //Act
        var result = сonnectWithManagerPage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBase)));
        ClassicAssert.IsInstanceOf<StartPage>(result.UpdatedUserState.CurrenntPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
    }

    [Test]
    public void Handle_UnknownMessage_ConnectWithManagerPage()
    {
        //Arrange
        var сonnectWithManagerPage = new ConnectWithManagerPage();
        var pages = new Stack<IPage>([new NotStatedPage()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Отправить вопрос", "sendQuastion"),
                InlineKeyboardButton.WithCallbackData("Назад", "Назад")]
        };
        //Act
        var result = сonnectWithManagerPage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(сonnectWithManagerPage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));

        Assert.That(result.Text, Is.EqualTo(Resources.ConnectWithManagerPageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}

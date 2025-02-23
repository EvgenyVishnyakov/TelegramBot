using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests;

public class InfoByCoursePageTests
{
    [Test]
    public void View_Enter_CorrectTextAndKeyboard()
    {
        //Arrange
        var infoByCoursePage = new InfoByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithUrl("Переход в школу", "https://ironprogrammer.ru/#rec460811109")],
                 [InlineKeyboardButton.WithCallbackData("Назад", "Назад")]
        };

        //Act
        var result = infoByCoursePage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<PhotoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(infoByCoursePage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        Assert.That(result.Text, Is.EqualTo(Resources.InfoByCoursePageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }

    [Test]
    public void Handle_InfoByCoursePageCallback_StartPage()
    {
        //Arrange
        var infoByCoursePage = new InfoByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), infoByCoursePage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "Назад" } };

        //Act
        var result = infoByCoursePage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBase)));
        ClassicAssert.IsInstanceOf<StartPage>(result.UpdatedUserState.CurrenntPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
    }

    [Test]
    public void Handle_UnknownMessage_InfoByCoursePage()
    {
        //Arrange
        var infoByCoursePage = new InfoByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithUrl("Переход в школу", "https://ironprogrammer.ru/#rec460811109")],
                 [InlineKeyboardButton.WithCallbackData("Назад", "Назад")]
        };
        //Act
        var result = infoByCoursePage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(infoByCoursePage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));

        Assert.That(result.Text, Is.EqualTo(Resources.InfoByCoursePageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}


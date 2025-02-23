using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using NUnit.Framework.Legacy;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Tests;

public class HelpByCoursePageTests
{
    [Test]
    public void View_Enter_CorrectTextAndKeyboard()
    {
        //Arrange
        var helpByCoursePage = new HelpByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage()]);
        var userState = new UserState(pages, new UserData());
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", Resources.CommonQuestionsPage)],
                [InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче", Resources.ResolveTaskPage)],
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };

        //Act
        var result = helpByCoursePage.View(null, userState);

        //Assert
        ClassicAssert.IsInstanceOf<VideoPageResult>(result);

        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(helpByCoursePage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
        Assert.That(result.Text, Is.EqualTo(Resources.HelpByCoursePageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));
        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }

    [Test]
    public void Handle_HelpByCoursePageCallback_CommonQuestionsPage()
    {
        //Arrange
        var helpByCoursePage = new HelpByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), helpByCoursePage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.CommonQuestionsPage } };
        //Act
        var result = helpByCoursePage.Handle(update, userState);

        //Assert           
        Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
        ClassicAssert.IsInstanceOf<CommonQuestionsPage>(result.UpdatedUserState.CurrenntPage);

        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));
    }

    [Test]
    public void Handle_HelpByCoursePageCallback_ResolveTaskPage()
    {
        //Arrange
        var helpByCoursePage = new HelpByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), helpByCoursePage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.ResolveTaskPage } };
        //Act
        var result = helpByCoursePage.Handle(update, userState);

        //Assert           
        Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
        ClassicAssert.IsInstanceOf<ResolveTaskPage>(result.UpdatedUserState.CurrenntPage);

        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(4));
    }


    [Test]
    public void Handle_HelpByCoursePageCallback_StartPage()
    {
        //Arrange
        var helpByCoursePage = new HelpByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage(), helpByCoursePage]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { CallbackQuery = new CallbackQuery() { Data = Resources.Back } };

        //Act
        var result = helpByCoursePage.Handle(update, userState);

        //Assert        
        Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBase)));
        ClassicAssert.IsInstanceOf<StartPage>(result.UpdatedUserState.CurrenntPage);
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
    }

    [Test]
    public void Handle_UnknownMessage_HelpByCoursePage()
    {
        //Arrange
        var helpByCoursePage = new HelpByCoursePage();
        var pages = new Stack<IPage>([new NotStatedPage(), new StartPage()]);
        var userState = new UserState(pages, new UserData());
        var update = new Update() { Message = new Message() { Text = "Неверный текст" } };
        var expectedButtons = new InlineKeyboardButton[][]
        {
                [InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", Resources.CommonQuestionsPage)],
                [InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче", Resources.ResolveTaskPage)],
                 [InlineKeyboardButton.WithCallbackData(Resources.Back)]
        };
        //Act
        var result = helpByCoursePage.View(update, userState);

        //Assert       
        Assert.That(result.UpdatedUserState.CurrenntPage, Is.EqualTo(helpByCoursePage));
        Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));

        Assert.That(result.Text, Is.EqualTo(Resources.HelpByCoursePageText));
        Assert.That(result.ParseMode, Is.EqualTo(ParseMode.Html));

        ClassicAssert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
        KeyboardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
    }
}

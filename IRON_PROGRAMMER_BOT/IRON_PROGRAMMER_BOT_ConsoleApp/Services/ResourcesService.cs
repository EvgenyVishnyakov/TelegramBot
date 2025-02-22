using System;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResource(string path)
        {
            try
            {
                //var directory = Path.GetDirectoryName(@"C: \\Users\vis - e\\Source\\Repos\\TG_Bot_stream\\IRON_PROGRAMMER_BOT\\IRON_PROGRAMMER_BOT_ConsoleApp\\Resources\\Videos\\ИИ.mp4");
                //if (!Directory.Exists(directory))
                //{
                //    Directory.CreateDirectory(directory);
                //}

                //// Проверка наличия файла
                //if (!System.IO.File.Exists(path))
                //{
                //    throw new FileNotFoundException("Файл не найден.", path);
                //}
                Console.WriteLine("Текущая рабочая директория: " + Directory.GetCurrentDirectory());

                var fileStream = new FileStream(@"~Resources\\Videos\\ИИ.mp4", FileMode.Open, FileAccess.Read);
                var filename = Path.GetFileName(@"C: \\Users\vis - e\\Source\\Repos\\TG_Bot_stream\\IRON_PROGRAMMER_BOT\\IRON_PROGRAMMER_BOT_ConsoleApp\\Resources\\Videos\");
                return InputFileStream.FromStream(fileStream, filename);
                //var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //var filename = path.Split("//").Last();
                //return InputFile.FromStream(fileStream, filename);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}

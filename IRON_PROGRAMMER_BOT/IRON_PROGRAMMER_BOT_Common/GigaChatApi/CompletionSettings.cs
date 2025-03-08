namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class CompletionSettings
    {
        public string Model { get; set; } = Resources.Model!;

        //private float? _temperature;

        public float? Temperature { get; set; } = Convert.ToInt64(Resources.Temperature)!;

        //private float? _topP = Convert.ToInt64(Resources.TopP);

        public float? TopP { get; set; } = Convert.ToSingle(Resources.TopP);
        //{
        //    get
        //    {
        //        return Convert.ToSingle(Resources.TopP); // Используем Convert.ToSingle для преобразования в float
        //    }
        //    set
        //    {
        //        if (value.HasValue && value.Value == -1) // Проверяем, является ли значение null и равно ли оно -1
        //        {
        //            Resources.TopP = null; // Присваиваем null, если значение равно -1
        //        }
        //        else
        //        {
        //            Resources.TopP = value; // Присваиваем значение
        //        }
        //    }
        //}
        //private long? _count;

        public long? Count { get; set; } = Convert.ToInt64(Resources.Count)!;
        //private long? _maxTokens;

        public long? MaxTokens { get; set; } = Convert.ToInt64(Resources.MaxTokens)!;

        //public CompletionSettings(string modelName, float? temperature, float? topP, long? maxTokens)
        //{
        //    Model = modelName;
        //    Temperature = temperature;
        //    TopP = topP;
        //    MaxTokens = maxTokens;
        //}
    }
}
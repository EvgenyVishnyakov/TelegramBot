﻿using Newtonsoft.Json;

namespace IRON_PROGRAMMER_BOT_Common.StepikAPI
{
    public class Course
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
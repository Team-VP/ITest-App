using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Common.Constants
{
    public class ModelConstants
    {
        public const int MaxQuestionContentLength = 500;
        public const int MinQuestionContentLength = 1;

        public const int MaxAnswerContentLength = 500;
        public const int MinAnswerContentLength = 1;

        public const int MaxTestTitleLength = 50;
        public const int MinTestTitleLength = 2;

        public const int MaxTestCategoryLength = 50;
        public const int MinTestCategoryLength = 2;

        public const int MaxRequiredTestTime = 1000;
        public const int MinRequiredTestTime = 1;
        public const string RequiredTestTimeErrorMsg = "Time must be positive value, between 1 and 1000 minutes";
    }
}

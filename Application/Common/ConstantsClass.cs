using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class ConstantsClass
    {
        // for pagination
        public const int maxPageSize = 50;

        // for validation rules
        public const string alphanumRule = "[A-Za-z0-9]+";

        // for validation errors messages
        public const string alphanumMessage = "only alphabet spaces and numbers allowed";
        public const string usernameRequiredMessage = "UserName is required";
        public const string usernameMaxLengthMessage = "UserName maximum length 100 characters";
        public const string titleRequiredMessage = "Title is required";
        public const string titleMaxLengthMessage = "Title maximum length 100 characters";
        public const string descriptionMaxLengthMessage = "Description maximum length 1000 characters";
        public const string priorityLevelMessage = "Priority Level is not recognized it should be (None,Low,Medium,High)";
        public const string roleMessage = "role is not recognized it should be (Guest,Owner)";
        public const string dueDateInPastMessage = "Due Date should be at least 1 day ahead";
        public const string negativeIdMessage = "Identifier should be positive";
        public const string emailMessage = "the email is not valid";


    }
}

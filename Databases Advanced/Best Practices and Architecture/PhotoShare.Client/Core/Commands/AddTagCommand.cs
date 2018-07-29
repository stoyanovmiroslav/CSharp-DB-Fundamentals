using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using PhotoShare.Client.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand : ICommand
    {
        private readonly ITagService tagService;
        private readonly IUserSessionService userSessionService;

        public AddTagCommand(ITagService tagService, IUserSessionService userSessionService)
        {
            this.tagService = tagService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            string tagName = args[0];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            tagName = tagName.ValidateOrTransform();

            TagDto tagDto = new TagDto { Name = tagName };

            if (!IsValid(tagDto))
            {
                throw new ArgumentException("Invalid tag!");
            }

            bool isTagExist = tagService.Exists(tagName);

            if (isTagExist)
            {
                throw new ArgumentException($"Tag {tagName} exists!");
            }

            tagService.AddTag(tagName);

            return $"Tag {tagName} was added successfully!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
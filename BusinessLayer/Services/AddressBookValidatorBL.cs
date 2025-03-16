using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ModelLayer.Models;

namespace BusinessLayer.Services
{
    public class AddressBookValidatorBL : AbstractValidator<ResponseAddressBookModel>
    {
        public AddressBookValidatorBL() // constructor
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format");
        }
    }
}
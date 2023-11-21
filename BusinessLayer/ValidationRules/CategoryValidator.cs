using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x=>x.CategoryName).NotEmpty().WithMessage("Kategori ismi boş olamaz!!");
            RuleFor(x=>x.CategoryDescription).NotEmpty().WithMessage("Kategori açıklaması boş olamaz!!");
            RuleFor(x=>x.CategoryName).MinimumLength(3).WithMessage("Kategori ismi 3 harften çok olmalı!!");
            RuleFor(x=>x.CategoryName).MaximumLength(20).WithMessage("Kategori ismi 20 harften az olmalı!!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator() 
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar isim kısmı boş olamaz!!");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar soyisim kısmı boş olamaz!!");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Yazar hakkında kısmı boş olamaz!!");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Yazar ünvan kısmı boş olamaz!!");
            RuleFor(x => x.WriterSurname).MinimumLength(2).WithMessage("Yazar soyismi 2 harften az olmalı!!");
            RuleFor(x => x.WriterSurname).MaximumLength(50).WithMessage("Yazar soyismi 50 harften az olmalı!!");
            //RuleFor(x => x.WriterAbout).Must(x => x != null && x.ToUpper().Contains("A")).WithMessage("Hakkında kısmında en az bir a harfi içermelidir");
        }
    }
}

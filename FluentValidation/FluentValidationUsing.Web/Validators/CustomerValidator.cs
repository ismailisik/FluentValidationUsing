using FluentValidation;
using FluentValidationUsing.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationUsing.Web.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {

        //Aşağıdaki gibi propName eklediğimizde o direkt gidip hangi prop üzerinde rule yazıyorsak onun adını alıcaktır.
        public string NotEmptyMessage { get; } = "{PropertyName} alanı zorunludur.";
        public CustomerValidator()
        {
            //FluentValidator hem client side hemde server side validasyon uygular.Kullanıcı javascript yorumlayıcısını kapatsa dahai server side takılır ve kayıt gerçekleşmez...

            //Bir api controller için yazssaydık sadece server side validasyon uygulamış olacaktık...

            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.LastName).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email Formatına Uygun Giriş Yapınız.");

            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 100).WithMessage("Age 0 ile 100 arasında olmalıdır.");

            //Peki ben custom olarak nasıl kural belirleyebilirim. Aşağıya bakınız...

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage(NotEmptyMessage).Must(x => {

                return DateTime.Now.AddYears(-18) >= x;
                
            }).WithMessage("Yaşınız 18 yada ondan büyük olmalıdır.");

            //**Önemli Not**: Benim entitim eğer başka bir entiti ile relation ise ve ben ilgili diğer entity'İ de validete etmek ister isem.Aşağıya bakınız.


            //Git beim entitm içerisindeki address entitisini validete et diyorum.
            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());

        }
    }
}

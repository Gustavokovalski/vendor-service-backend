using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using VendorService.Domain.Enums;
using VendorService.Shared;

namespace VendorService.Application.Models
{
    public class BaseModel<T> where T : class
    {
        #region Constructors
        public BaseModel()
        {

        }

        public BaseModel(bool success, EMessages message)
        {
            Success = success;
            Message = new EnumModel[]
            {
                new EnumModel()
                {
                    Id = message.GetEnumValue(),
                    Name = message.GetEnumName(),
                    Description = message.GetEnumDescription()
                }
            };
        }



        public BaseModel(bool sucesso, IList<ValidationFailure> validationResult)
        {
            this.Success = sucesso;
            this.Message = validationResult.Select(x => new EnumModel
            {
                Id = 99,
                Name = x.PropertyName,
                Description = x.ErrorMessage
            }).ToArray();

        }

        public BaseModel(bool success, EMessages message, T result) : this(success, message) => this.Result = result;

        public BaseModel(bool success, EMessages message, T result, ValidationResult[] validationResult) : this(success, message, result) => this.ValidationResult = validationResult;
        #endregion

        #region Properties
        public T Result { get; set; }

        public EnumModel[] Message { get; set; }

        public ValidationResult[] ValidationResult { get; set; }

        public bool Success { get; set; }
        #endregion
    }

    public partial class BaseModel : BaseModel<dynamic>
    {
        #region Constructors
        public BaseModel() : base()
        {

        }

        public BaseModel(bool success, EMessages message) : base(success, message)
        {

        }

        public BaseModel(bool success, EMessages message, dynamic result) : base(success, message) => this.Result = result;

        public BaseModel(bool success, EMessages message, dynamic result, ValidationResult[] validationResult) : base(success, message)
        {
            this.Result = result;
            this.ValidationResult = validationResult;
        }
        #endregion
    }
}

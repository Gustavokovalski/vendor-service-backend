using System;
using VendorService.Domain.Extensions;

namespace VendorService.Application.Models
{
    public class EnumModel
    {
        #region Constructors
        public EnumModel()
        {

        }

        public EnumModel(Enum enumItem)
        {
            this.Id = enumItem.GetEnumValue();
            this.Name = enumItem.GetEnumName();
            this.Description = enumItem.GetEnumDescription();
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}

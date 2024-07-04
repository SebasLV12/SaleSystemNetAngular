using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AutoMapper;
using SaleSystem.DTO;
using SaleSystem.Model;

namespace SaleSystem.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region User
            CreateMap<User, UserDTO>()
                .ForMember(destination =>
                destination.RolDescription,
                opt => opt.MapFrom(source => source.IdRolNavigation.Name)
               )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
                );

            CreateMap<User, SessionDTO>()
               .ForMember(destination =>
               destination.RolDescription,
               opt => opt.MapFrom(source => source.IdRolNavigation.Name)
              );


            CreateMap<UserDTO, User>()
               .ForMember(destination =>
               destination.IdRolNavigation,
               opt => opt.Ignore()
              )
               .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(source => source.IsActive == 1 ? true : false)
                );
            #endregion User

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion Category

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(destination =>
                    destination.CategoryDescription,
                    opt => opt.MapFrom(source => source.IdCategoryNavigation.Name)
                 )
                 .ForMember(destination =>
                 destination.Price,
                 opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
                 ).ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
                ); ;

            CreateMap<ProductDTO, Product>()
               .ForMember(destination =>
                   destination.IdCategoryNavigation,
                   opt => opt.Ignore()
                )
                .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(source => Convert.ToDecimal(source.Price, new CultureInfo("es-PE")))
                ).ForMember(destination =>
               destination.IsActive,
               opt => opt.MapFrom(source => source.IsActive == 1 ? true : false)
               ); 
            #endregion Product

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(destination =>
                 destination.TotalString,
                 opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                 ).ForMember(destination =>
                 destination.CreatedOn,
                 opt => opt.MapFrom(source => source.CreatedOn.Value.ToString("dd/MM/yyyy"))
                 );
            CreateMap<SaleDTO, Sale>()
               .ForMember(destination =>
                destination.Total, 
                opt => opt.MapFrom(source => Convert.ToDecimal(source.TotalString, new CultureInfo("es-PE")))
                );
            #endregion Sale

            #region DetaiLSale
            CreateMap<DetailSale, DetailSaleDTO>()
                .ForMember(destination =>
                destination.ProductDescription,
                opt => opt.MapFrom(source => source.IdProductNavigation.Name)
               ).ForMember(destination =>
                 destination.PriceString,
                 opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
               )
               .ForMember(destination =>
                 destination.TotalString,
                 opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                 );

            CreateMap<DetailSaleDTO, DetailSale>()
              .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(source => Convert.ToDecimal(source.PriceString, new CultureInfo("es-PE")))
              )
              .ForMember(destination =>
                destination.Total,
                opt => opt.MapFrom(source => Convert.ToDecimal(source.TotalString, new CultureInfo("es-PE")))
                );
            #endregion DetaiLSale

            #region Report
            CreateMap<DetailSale, ReportDTO>()
                .ForMember(destination =>
                 destination.CreatedOn,
                 opt => opt.MapFrom(source => source.IdSaleNavigation.CreatedOn.Value.ToString("dd/MM/yyyy"))
                 )
                .ForMember(destination =>
                 destination.NumberDocument,
                 opt => opt.MapFrom(source => source.IdSaleNavigation.NumberDoc)
                 ).ForMember(destination =>
                 destination.PaymentType,
                 opt => opt.MapFrom(source => source.IdSaleNavigation.PaymentType)
                 )
                 .ForMember(destination =>
                 destination.TotalSale,
                 opt => opt.MapFrom(source => Convert.ToString(source.IdSaleNavigation.Total.Value, new CultureInfo("es-PE")))
                 ).ForMember(destination =>
                 destination.Product,
                 opt => opt.MapFrom(source => source.IdProductNavigation.Name)
                 ).ForMember(destination =>
                 destination.Price,
                 opt => opt.MapFrom(source => source.Price.Value)
                 ).ForMember(destination =>
                 destination.Total,
                 opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                 );
            #endregion Report
        }
    }
}

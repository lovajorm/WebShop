using System.Linq;
using AutoMapper;
using WebShop.Bo;
using WebShop.Web.ViewModels;

namespace WebShop.Web
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //var test = Mapper.Map<List<Product>>()
            //CreateMap<Product, ProductListViewModel>().ForMember(a => a.Products,
            //    opts => opts.MapFrom(src => new Product
            //    {
            //        Category = src.Category, CategoryId = src.CategoryId, Description = src.Description,
            //        ImageUrl = src.ImageUrl, Price = src.Price, ProductID = src.ProductID, Title = src.Title
            //    }()));
            //IList<ProductListViewModel> ilstan = Mapper.Map<Product[], IList<ProductListViewModel>>()
            // List<ProductListViewModel> test = Mapper.Map<List<Product>, List<ProductListViewModel>>();


            //CreateMap<ProductListViewModel, Product>()
            //   .ForMember(dest => dest.Category.Products,
            //       opts => opts.MapFrom(source => new Category(){Products = source.Products.ToList()}));

            CreateMap<Product, ProductListViewModel>(MemberList.Source)
                .ForMember(dest => dest.Products,
                    opts => opts.MapFrom(source => new Category(){ Products = source.Category.Products }));

        }
    }
}

using System;
using System.Linq;
using Framework.DataBaseContext;
using Microsoft.AspNetCore.Http;
using Framework.Models;
using Framework.Models.Database;
using System.Collections.Generic;

namespace Framework.Services
{
    public class ProductManagement : IProductManagement
    {
        private readonly ProductContext context;
        private readonly ISession session;

        private const String userId = nameof(userId);
        private const String userName = nameof(userName);

        public ProductManagement(ProductContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            session = httpContextAccessor.HttpContext.Session;
        }

        public void AddProduct(ProductDto dto)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                var userGuid = session.GetString(userId);

                if (!String.IsNullOrWhiteSpace(userGuid))
                    dto.UserId = Guid.Parse(userGuid);

                dto.UserId = Guid.NewGuid();

                var productId = Guid.NewGuid();

                Int32 productType = GetProductTypeValue(dto.Type);
                Int32 productState = GetProductStateValue(dto.State);

                context.Products.Add(new Product()
                {
                    ProductId = productId,
                    UserId = dto.UserId,
                    Name = dto.Name,
                    Manufacturer = dto.Manufacturer,
                    ProductType = productType,
                    ProductState = productState

                });

                var quantityType = GetQuantityTypeValue(dto.Macro.QuantityType);

                context.ProductsDetails.Add(new ProductDetails()
                {
                    ProductId = productId,
                    Protein = dto.Macro.Protein,
                    Fat = dto.Macro.Fat,
                    Carbohydrates = dto.Macro.Carbohydrates,
                    Fibre = dto.Macro.Fibre,
                    Calories = dto.Macro.Calories,
                    Quantity = dto.Macro.Quantity,
                    QuantityType = quantityType
                });

                context.SaveChanges();
                trans.Commit();
            }
        }

        public void RemoveProduct(Guid productId)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var p = context.Products.FirstOrDefault(x => x.ProductId.Equals(productId));
                var pd = context.ProductsDetails.FirstOrDefault(x => x.ProductId.Equals(productId));
                context.ProductsDetails.Remove(pd);
                context.Products.Remove(p);

                context.SaveChanges();
                trans.Commit();
            }
        }

        public void UpdateProduct(ProductDto dto)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var p = context.Products.FirstOrDefault(x => x.ProductId.Equals(dto.ProductId));
                var pd = context.ProductsDetails.FirstOrDefault(x => x.ProductId.Equals(dto.ProductId));

                p.Name = dto.Name;
                p.Manufacturer = dto.Manufacturer;
                p.ProductType = GetProductTypeValue(dto.Type);
                p.ProductState = GetProductStateValue(dto.State);

                pd.Calories = dto.Macro.Calories;
                pd.Fat = dto.Macro.Fat;
                pd.Fibre = dto.Macro.Fibre;
                pd.Protein = dto.Macro.Protein;
                pd.Quantity = dto.Macro.Quantity;
                pd.QuantityType = GetQuantityTypeValue(dto.Macro.QuantityType);

                context.SaveChanges();
                trans.Commit();
            }
        }

        public void SubscribeProduct(Guid productId)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var p = context.Products.FirstOrDefault(x => x.ProductId.Equals(productId));
                p.ProductState = 1;
                context.SaveChanges();
                trans.Commit();
            }
        }

        public void CancelSubscription(Guid productId)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var p = context.Products.FirstOrDefault(x => x.ProductId.Equals(productId));
                p.ProductState = 0;
                context.SaveChanges();
                trans.Commit();
            }
        }

        public ProductDto GetProduct(Guid productId)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var p = context.Products.FirstOrDefault(x => x.ProductId.Equals(productId));
                var pd = context.ProductsDetails.FirstOrDefault(x => x.ProductId.Equals(productId));

                return new ProductDto()
                {
                    Name = p.Name,
                    Manufacturer = p.Manufacturer,
                    ProductId = p.ProductId,
                    UserId = p.UserId,
                    State = GetProductStateEnum(p.ProductState),
                    Type = GetProductTypeEnum(p.ProductType),
                    Macro = new Macro()
                    {
                        Calories = pd.Calories,
                        Fat = pd.Fat,
                        Fibre = pd.Fibre,
                        Carbohydrates = pd.Carbohydrates,
                        Protein = pd.Protein,
                        Quantity = pd.Quantity,
                        QuantityType = GetQuantityTypeEnum(pd.QuantityType)
                    }
                };
            }
        }

        /// <summary>
        /// Pozyskuje listę wszystkich produktów w bazie danych.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetProducts()
        {
            var result = from p in context.Products
                         join pd in context.ProductsDetails
                         on p.ProductId equals pd.ProductId
                         select new
                         {
                             p,
                             pd
                         };

            var list = result.Select(x => new ProductDto()
            {
                ProductId = x.p.ProductId,
                UserId = x.p.UserId,
                Name = x.p.Name,
                Manufacturer = x.p.Manufacturer,
                Type = GetProductTypeEnum(x.p.ProductType),
                State = GetProductStateEnum(x.p.ProductState),
                Macro = new Macro()
                {
                    Protein = x.pd.Protein,
                    Fat = x.pd.Fat,
                    Fibre = x.pd.Fibre,
                    Carbohydrates = x.pd.Carbohydrates,
                    Calories = x.pd.Calories,
                    Quantity = x.pd.Quantity,
                    QuantityType = GetQuantityTypeEnum(x.pd.QuantityType)
                }
            });
            
            return list != null ? list.ToList() : new List<ProductDto>();
        }

        /// <summary>
        /// Pozyskuje listę wszystkich produktów dodanych przez użytkownika
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetUserProducts()
        {
            var userGuid = session.GetString(userId);

            if (!String.IsNullOrWhiteSpace(userGuid)) throw new KeyNotFoundException(nameof(userGuid));

            var result = from p in context.Products
                         join pd in context.ProductsDetails
                         on p.ProductId equals pd.ProductId
                         where p.UserId.Equals(userGuid)
                         select new
                         {
                             p,
                             pd
                         };

            var list = result.Select(x => new ProductDto()
            {
                ProductId = x.p.ProductId,
                UserId = x.p.UserId,
                Name = x.p.Name,
                Manufacturer = x.p.Manufacturer,
                Type = GetProductTypeEnum(x.p.ProductType),
                State = GetProductStateEnum(x.p.ProductState),
                Macro = new Macro()
                {
                    Protein = x.pd.Protein,
                    Fat = x.pd.Fat,
                    Fibre = x.pd.Fibre,
                    Carbohydrates = x.pd.Carbohydrates,
                    Calories = x.pd.Calories,
                    Quantity = x.pd.Quantity,
                    QuantityType = GetQuantityTypeEnum(x.pd.QuantityType)
                }
            });

            return list != null  ? list.ToList() : new List<ProductDto>();
        }

        /// <summary>
        /// Konwertuje typ produktu na wartość zapisaną do bazy danych.
        /// </summary>
        /// <param name="typeEnum"><see cref="ProductType"/></param>
        /// <returns></returns>
        private Int32 GetProductTypeValue(ProductType typeEnum)
        {
            Int32 value = 0;

            if (typeEnum == ProductType.DairyProducts)
                value = 0;
            else if (typeEnum == ProductType.FastFood)
                value = 1;
            else if (typeEnum == ProductType.Fruits)
                value = 2;
            else if (typeEnum == ProductType.Sweets)
                value = 3;
            else if (typeEnum == ProductType.Vegetables)
                value = 4;
            else
                throw new NotSupportedException(nameof(typeEnum));

            return value;
        }

        /// <summary>
        /// Konwertuje typ pojemnościowy produktu na wartość zapisaną do bazy danych.
        /// </summary>
        /// <param name="typeEnum"><see cref="QuantityType"/></param>
        /// <returns></returns>
        private Int32 GetQuantityTypeValue(QuantityType typeEnum)
        {
            Int32 value = 0;

            if (typeEnum == QuantityType.Grams)
                value = 0;
            else if (typeEnum == QuantityType.Milliliters)
                value = 1;
            else if (typeEnum == QuantityType.Piece)
                value = 2;
            else if (typeEnum == QuantityType.Package)
                value = 3;
            else
                throw new NotSupportedException(nameof(typeEnum));

            return value;
        }

        /// <summary>
        /// Konwertuje status produktu na wartość zapisaną do bazy danych.
        /// </summary>
        /// <param name="stateEnum"><see cref="ProductState"/></param>
        /// <returns></returns>
        private Int32 GetProductStateValue(ProductState stateEnum)
        {
            Int32 value = 0;

            if (stateEnum == ProductState.Private)
                value = 0;
            else if (stateEnum == ProductState.Pending)
                value = 1;
            else if (stateEnum == ProductState.Denied)
                value = 2;
            else if (stateEnum == ProductState.Accepted)
                value = 3;
            else
                throw new NotSupportedException(nameof(stateEnum));

            return value;
        }

        /// <summary>
        /// Konwertuje wartość typu produktu przechowywaną na typ enumeracyjny.
        /// </summary>
        /// <param name="typeValue"></param>
        /// <returns></returns>
        private ProductType GetProductTypeEnum(Int32 typeValue)
        {
            ProductType type;

            if (typeValue == 0)
                type = ProductType.DairyProducts;
            else if (typeValue == 1)
                type = ProductType.FastFood;
            else if (typeValue == 2)
                type = ProductType.Fruits;
            else if (typeValue == 3)
                type = ProductType.Sweets;
            else if (typeValue == 4)
                type = ProductType.Vegetables;
            else throw new NotSupportedException(nameof(typeValue));

            return type;
        }

        /// <summary>
        /// Konwertuje wartość typu pojemności produktu na typ enumeracyjny.
        /// </summary>
        /// <param name="typeValue"></param>
        /// <returns></returns>
        private QuantityType GetQuantityTypeEnum(Int32 typeValue)
        {
            QuantityType type;

            if (typeValue == 0)
                type = QuantityType.Grams;
            else if (typeValue == 1)
                type = QuantityType.Milliliters;
            else if (typeValue == 2)
                type = QuantityType.Piece;
            else if (typeValue == 3)
                type = QuantityType.Package;
            else throw new NotSupportedException(nameof(typeValue));

            return type;
        }

        /// <summary>
        /// Konwertuje wartości stanu produktu na typ enumeracyjny.
        /// </summary>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        private ProductState GetProductStateEnum(Int32 stateValue)
        {
            ProductState type;

            if (stateValue == 0)
                type = ProductState.Private;
            else if (stateValue == 1)
                type = ProductState.Pending;
            else if (stateValue == 2)
                type = ProductState.Denied;
            else if (stateValue == 3)
                type = ProductState.Accepted;
            else throw new NotSupportedException(nameof(stateValue));

            return type;
        }
    }
}

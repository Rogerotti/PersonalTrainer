﻿using Framework.Models;
using System;
using System.Collections.Generic;

namespace Framework.Services
{
    public interface IProductManagement
    {
        void AddDailyFood(DateTime date, DailyFoodProductDto dto);

        DailyFoodDto GetDailyFood(DateTime date);

        DailyFoodDto GetDailyFoodFromDailyFoodProductDto(DateTime date, IEnumerable<DailyFoodProductDto> dto);
     
        /// <summary>
        /// Dodaje produkt do bazy.
        /// </summary>
        /// <param name="dto">Dto produktu. <see cref="ProductDto"/></param>
        void AddProduct(ProductDto dto);

        /// <summary>
        /// Aktualizuje wybrany produkt w bazie danych.
        /// </summary>
        /// <param name="productId">Id produktu.</param>
        /// <param name="dto">Dto produktu. <see cref="ProductDto"/></param>
        void UpdateProduct(ProductDto dto);

        /// <summary>
        /// Usuwa produkt z bazy.
        /// </summary>
        /// <param name="productId">Id produktu.</param>
        void RemoveProduct(Guid productId);

        ProductDto GetProduct(Guid productId);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProducts();

        /// <summary>
        /// Pozyskuje listę wszystkich produktów dodanych przez użytkownika
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetUserProducts();

        void SubscribeProduct(Guid productId);

        void CancelSubscription(Guid productId);
    }
}
﻿using Framework.Models;
using System;
using System.Collections.Generic;

namespace Framework.Services
{
    public interface IProductManagement
    {
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
        void UpdateProduct(Guid productId, ProductDto dto);

        /// <summary>
        /// Usuwa produkt z bazy.
        /// </summary>
        /// <param name="productId">Id produktu.</param>
        void RemoveProduct(Guid productId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProducts();
    }
}

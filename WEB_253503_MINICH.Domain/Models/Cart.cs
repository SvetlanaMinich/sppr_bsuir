using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_253503_MINICH.Domain.Entities;

namespace WEB_253503_MINICH.Domain.Models
{
    public class Cart
    {
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = [];

        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="product">Добавляемый объект</param>
        public virtual void AddToCart(Cup product)
        {
            if (product == null) return;

            // Если товар уже в корзине, увеличиваем количество
            if (CartItems.TryGetValue(product.Id, out CartItem? value))
            {
                value.Count++;
            }
            else
            {
                CartItems[product.Id] = new CartItem { Cup = product, Count = 1 };
            }
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id"> id удаляемого объекта</param>
        public virtual void RemoveItem(int id)
        {
            CartItems.Remove(id);
        }

        ///<summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        ///<summary>
        /// Количество объектов в корзине
        ///</summary>
        public int Count
        {
            get => CartItems.Sum(item => item.Value.Count);
        }

        ///// <summary>
        ///// Общее количество калорий
        ///// </summary>
        /*public double TotalPrice
        {
            get => CartItems.Sum(item => item.Value.Cup.Price * item.Value.Count);
        }*/
    }
}

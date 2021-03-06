﻿namespace NPredicateBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class BaseOrder<T>
    {
        private readonly List<IThenByOrder<T>> _secondaryOrders;

        protected BaseOrder()
        {
            _secondaryOrders = new List<IThenByOrder<T>>();
        }

        public IOrder<T> FirstOrder { get; private set; }

        public IEnumerable<IThenByOrder<T>> SecondaryOrders => _secondaryOrders;

        /// <summary>
        /// States the first order by operation to be executed. If one is already present, this will overwrite the current operation.
        /// </summary>
        /// <typeparam name="TKey">The key that you want to order by.</typeparam>
        /// <param name="orderExpression">The expression that will be used to order an IQueryable of type T.</param>
        protected void OrderBy<TKey>(Expression<Func<T, TKey>> orderExpression)
        {
            FirstOrder = new OrderBy<T, TKey>(orderExpression);
        }

        /// <summary>
        /// States the first order by descending operation to be executed. If one is already present, this will overwrite the current operation.
        /// </summary>
        /// <typeparam name="TKey">The key that you want to order by descending by.</typeparam>
        /// <param name="orderExpression">The expression that will be used to order an IQueryable of type T.</param>
        protected void OrderByDescending<TKey>(Expression<Func<T, TKey>> orderExpression)
        {
            FirstOrder = new OrderByDescending<T, TKey>(orderExpression);
        }

        /// <summary>
        /// States a then by operation to be executed.
        /// </summary>
        /// <typeparam name="TKey">The key that you want to order by after an initial order by has completed.</typeparam>
        /// <param name="orderExpression">The expression that will be used to order an IOrderedQueryable of type T.</param>
        protected void ThenBy<TKey>(Expression<Func<T, TKey>> orderExpression)
        {
            _secondaryOrders.Add(new ThenBy<T, TKey>(orderExpression));
        }

        /// <summary>
        /// State a then by descending operation to be executed.
        /// </summary>
        /// <typeparam name="TKey">The key that you want to order by descending after an initial order by has completed.</typeparam>
        /// <param name="orderExpression">The expression that will be used to order an IOrderedQueryable of type T.</param>
        protected void ThenByDescending<TKey>(Expression<Func<T, TKey>> orderExpression)
        {
            _secondaryOrders.Add(new ThenByDescending<T, TKey>(orderExpression));
        }
    }
}

using AutoMapper;
using System;
using System.Linq.Expressions;

namespace FAS.WebUI.Infrastructure
{
    public static class MapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreProperty<TSource, TDestination, TProperty>
            (this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, TProperty>> selector)
        {
            return map.ForMember(selector, option => option.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> IgnoreProperties<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> map, params Expression<Func<TDestination, object>>[] selectors)
        {
            foreach (var selector in selectors)
            {
                map.IgnoreProperty(selector);
            }

            return map;
        }
    }
}
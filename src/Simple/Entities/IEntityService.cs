﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Services;
using FluentValidation.Results;
using Simple.Validation;

namespace Simple.Entities
{
    [ServiceContract]
    public interface IEntityService<T> : IService
    {
        T Load(object id);
        T Refresh(T entity);
        T Reload(T entity);
        T Merge(T entity);
        T Evict(T entity);

        int Count(EditableExpression<Func<T, bool>> filter);
        T Find(EditableExpression<Func<T, bool>> filter, OrderBy<T> orderBy);
        IPage<T> List(EditableExpression<Func<T, bool>> filter, OrderBy<T> order, int? skip, int? take);
        IPage<T> Linq(EditableExpression<Func<IQueryable<T>, IQueryable<T>>> mapExpression, EditableExpression<Func<IQueryable<T>, IQueryable<T>>> reduceExpression);

        void Delete(object id);
        void Delete(T entity);
        int Delete(EditableExpression<Func<T, bool>> filter);

        T SaveOrUpdate(T entity);
        T Save(T entity);
        T Update(T entity);
        T Persist(T entity);
        ValidationList Validate(T entity);
        ValidationList ValidateProperty(T entity, params string[] propName);

    }
}
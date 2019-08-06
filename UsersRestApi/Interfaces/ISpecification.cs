// **********************************************************************************
// Filename					- ISpecification.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

namespace UsersRestApi.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}

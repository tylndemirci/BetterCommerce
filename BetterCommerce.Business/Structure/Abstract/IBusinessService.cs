﻿using BetterCommerce.Business.Abstract;

namespace BetterCommerce.Business.Structure.Abstract
{
    public interface IBusinessService
    {
        public IAuthService Auth { get; }
        public IProductService Product { get; }
        public IRoleService Role { get; }
    }
}
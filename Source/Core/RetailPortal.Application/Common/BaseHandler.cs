﻿using RetailPortal.Core.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Common;

public class BaseHandler(IUnitOfWork uow)
{
    protected readonly IUnitOfWork Uow = uow;
}
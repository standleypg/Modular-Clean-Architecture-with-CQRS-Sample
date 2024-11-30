﻿

using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using ErrorOr;

namespace RetailPortal.Application.Commands.AddUser;

public record AddUserCommand(string FirstName, string LastName, string Email, string Password): IRequireTransaction, IRequest<ErrorOr<User>>;
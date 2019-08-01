using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUsersFinderService
    {
        Maybe<Email> FindUserEmailById(int id);
    }
}
